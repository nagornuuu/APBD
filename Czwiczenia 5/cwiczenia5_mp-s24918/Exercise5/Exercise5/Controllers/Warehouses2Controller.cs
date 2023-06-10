using Exercise5.Models.DTOs;
using Exercise5.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Exercise5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IWarehouse2Repository _warehouses2Repository;
        public Warehouses2Controller(IWarehouse2Repository warehouses2Repository)
        {
            _warehouses2Repository = warehouses2Repository;
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouse(NewProductRequest newProductRequest)
        {
            var productExists = await _warehouses2Repository.DoesProductExist(newProductRequest.IdProduct);
            if (!productExists)
            {
                return NotFound("Product does not exist.");
            }
            if (newProductRequest is null)
            {
                return BadRequest(nameof(newProductRequest));
            }
            if (newProductRequest.Amount <= 0)
            {
                return BadRequest("Amount must be greater than 0.");
            }
            if (!await _warehouses2Repository.DoesProductExist(newProductRequest.IdProduct))
            {
                return NotFound($"Product with ID {newProductRequest.IdProduct} not found");
            }
            if (!await _warehouses2Repository.DoesWarehouseExist(newProductRequest.IdWarehouse))
            {
                return NotFound($"Warehouse with ID {newProductRequest.IdWarehouse} not found");
            }
            if (!await _warehouses2Repository.IsOrderFulfilled(newProductRequest.IdProduct, newProductRequest.Amount))
            {
                return BadRequest("The order is not fulfilled yet");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                int productId = await _warehouses2Repository.RegisterNewProductByProcedure(newProductRequest);
                if (productId <= 0)
                {
                    return BadRequest($"Failed to register product with ID {newProductRequest.IdProduct} in warehouse with ID {newProductRequest.IdWarehouse}");
                }
                DateTime createdAt = DateTime.UtcNow;
                await _warehouses2Repository.InsertProductWarehouse(productId, newProductRequest.IdWarehouse, createdAt);
                scope.Complete();
            }

            int key = await _warehouses2Repository.InsertProductWarehouse(newProductRequest.IdProduct, newProductRequest.IdWarehouse, newProductRequest.CreatedAt);
            return Ok(key);
        }
    }
}
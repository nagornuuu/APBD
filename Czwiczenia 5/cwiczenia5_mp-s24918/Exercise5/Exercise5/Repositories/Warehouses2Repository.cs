using Exercise5.Models;
using Exercise5.Models.DTOs;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace Exercise5.Repositories
{
    public interface IWarehouse2Repository
    {
        Task<bool> DoesProductExist(int productId);
        Task<bool> DoesWarehouseExist(int warehouseId);
        Task<bool> IsOrderFulfilled(int productId, int amount);
        Task<int> Create();
        Task<int> RegisterNewProductByProcedure(NewProductRequest newProductRequest);
        Task Update(int productId);
        Task<int> InsertProductWarehouse(int productId, int warehouseId, DateTime createdAt);
    }
    public class Warehouses2Repository : IWarehouse2Repository
    {
        private readonly string _connectionString;
        public Warehouses2Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default")
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<int> Create()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Product VALUES ('t', 't', 20); SELECT SCOPE_IDENTITY();";
                await connection.OpenAsync();
                object? res = await command.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
        }

        public async Task<bool> DoesProductExist(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Product WHERE IdProduct = @ProductId";
                command.Parameters.AddWithValue("@ProductId", productId);
                await connection.OpenAsync();
                int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                return count > 0;
            }
        }

        public async Task<bool> DoesWarehouseExist(int warehouseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @WarehouseId";
                command.Parameters.AddWithValue("@WarehouseId", warehouseId);
                await connection.OpenAsync();
                int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                return count > 0;
            }
        }

        public async Task<bool> IsOrderFulfilled(int productId, int amount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM [Order] WHERE IdProduct = @ProductId AND Amount = @Amount AND FulfilledAt IS NULL";
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@Amount", amount);
                await connection.OpenAsync();
                int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                return count > 0;
            }
        }

        public async Task<int> RegisterNewProductByProcedure(NewProductRequest newProductRequest)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddProductToWarehouse";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProduct", newProductRequest.IdProduct);
                command.Parameters.AddWithValue("@IdWarehouse", newProductRequest.IdWarehouse);
                command.Parameters.AddWithValue("@Amount", newProductRequest.Amount);
                command.Parameters.AddWithValue("@CreatedAt", newProductRequest.CreatedAt);
                await connection.OpenAsync();
                object? res = await command.ExecuteScalarAsync();
                return Convert.ToInt32(res);
            }
        }

        public async Task Update(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Product SET UpdatedAt = @UpdatedAt WHERE IdProduct = @ProductId";
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                command.Parameters.AddWithValue("@ProductId", productId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<int> InsertProductWarehouse(int productId, int warehouseId, DateTime createdAt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                                      "SELECT @IdWarehouse, @IdProduct, o.IdOrder, o.Amount, o.Amount * p.Price, @CreatedAt FROM [Order] o" +
                                      "INNER JOIN Product p ON o.IdProduct = p.IdProduct WHERE o.IdProduct = @IdProduct AND o.FulfilledAt IS NOT NULL;" +
                                      "SELECT SCOPE_IDENTITY();";
                command.Parameters.AddWithValue("@IdProduct", productId);
                command.Parameters.AddWithValue("@IdWarehouse", warehouseId);
                command.Parameters.AddWithValue("@CreatedAt", createdAt);

                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    transaction.Commit();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}



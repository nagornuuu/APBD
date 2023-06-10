using System.Data.SqlClient;

namespace Exercise4.Models.Repositories
{
    public interface IAnimalsRepository
    {
        Task<ICollection<Animal>> GetAnimalsAsync(string orderBy);
        Task<Animal> GetAnimalByIdAsync(int id);
        Task AddAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(int id);
    }
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly string _connectionString;
        public AnimalsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default")
                ?? throw new Exception("Nie podano connectionStringa");
        }
        public async Task AddAnimalAsync(Animal animal)
        {
            var query = $"INSERT INTO Animal ([ID], [Name], [Description], [Category], [Area]) VALUES (@ID, @Name, @Description, @Category, @Area)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", animal.ID);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", animal.Description);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<ICollection<Animal>> GetAnimalsAsync(string orderBy)
        {
            var query = $"SELECT * FROM Animal ORDER BY {orderBy}";
            var animals = new List<Animal>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        animals.Add(new Animal
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            Area = reader.GetString(reader.GetOrdinal("Area")),
                        });
                    }
                }
            }
            return animals;
        }
        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            var query = "SELECT * FROM Animal WHERE [ID] = @ID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Animal
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            Area = reader.GetString(reader.GetOrdinal("Area")),
                        };
                    }
                }
            }
            return null;
        }
        public async Task UpdateAnimalAsync(Animal animal)
        {
            var query = "UPDATE Animal SET [Name] = @Name, [Description] = @Description, [Category] = @Category, [Area] = @Area WHERE [ID] = @ID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", animal.ID);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", animal.Description); 
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task DeleteAnimalAsync(int id)
        {
            var query = "DELETE FROM Animal WHERE [ID] = @ID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

using Domain.Models;

namespace Repository
{
    public static class DataSeeder
    {
        public static void SeedData(AppDbContext context)
        {
            if (!context.DataTypes.Any())
            {
                var dataTypes = new List<DataType>
                {
                    new DataType { Name = "int" },
                    new DataType { Name = "bigint" },
                    new DataType { Name = "boolean" },
                    new DataType { Name = "decimal" },
                    new DataType { Name = "double precision" },
                    new DataType { Name = "float" },
                    new DataType { Name = "text" },
                    new DataType { Name = "char" },
                    new DataType { Name = "date" },
                    new DataType { Name = "timestamp" },
                };

                context.DataTypes.AddRange(dataTypes);
                context.SaveChanges();
            }
        }
    }
}

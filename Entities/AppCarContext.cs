using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entities;

public class AppCarContext : DbContext
{
    private readonly ILogger<AppCarContext> _logger;
    internal DbSet<Car> Cars { get; set; }
    internal DbSet<Brand> Brands { get; set; }
    internal DbSet<BodyType> BodyTypes { get; set; }

    public AppCarContext(DbContextOptions<AppCarContext> options, ILogger<AppCarContext> logger):base(options)
    {
        _logger = logger;
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BodyType>().HasData(
            new List<BodyType>
            {
                new BodyType{BodyTypeId = 1, BodyTypeName = "Sedan"},
                new BodyType{BodyTypeId = 2, BodyTypeName = "Hatchback"},
                new BodyType{BodyTypeId = 3, BodyTypeName = "Wagon"},
                new BodyType{BodyTypeId = 4, BodyTypeName = "Minivan"},
                new BodyType{BodyTypeId = 5, BodyTypeName = "SUV"},
                new BodyType{BodyTypeId = 6, BodyTypeName = "Cooper"}
            });

        modelBuilder.Entity<Brand>().HasData(
            new List<Brand>
            {
                new Brand{BrandId = 1, BrandName = "Audi"},
                new Brand{BrandId = 2, BrandName = "Ford"},
                new Brand{BrandId = 3, BrandName = "Jeep"},
                new Brand{BrandId = 4, BrandName = "Nissan"},
                new Brand{BrandId = 5, BrandName = "Toyota"},
            });
            
        modelBuilder.Entity<Car>().HasData(
            new Car[] 
            {
                new Car{ Id = Guid.NewGuid(), BrandId = 1, ModelName = "Model1", Image = "/Files/Unknown1.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 1, SeatsCount = 1, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 2, ModelName = "Model2", Image = "/Files/Unknown2.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 2, SeatsCount = 11, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 3, ModelName = "Model3", Image = "/Files/Unknown.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 3, SeatsCount = 1, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 4, ModelName = "Model4", Image = "/Files/Unknown1.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 4, SeatsCount = 3, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 5, ModelName = "Model5", Image = "/Files/Unknown2.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 5, SeatsCount = 1, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 5, ModelName = "Model6", Image = "/Files/Unknown.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 6, SeatsCount = 4, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 4, ModelName = "Model7", Image = "/Files/Unknown2.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 4, SeatsCount = 1, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 3, ModelName = "Model8", Image = "/Files/Unknown1.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 3, SeatsCount = 1, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 2, ModelName = "Model9", Image = "/Files/Unknown.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 2, SeatsCount = 5, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 2, ModelName = "Model10", Image = "/Files/Unknown2.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 1, SeatsCount = 1, Url = "https://www.ilya.ru"},
                new Car{ Id = Guid.NewGuid(), BrandId = 1, ModelName = "Model11", Image = "/Files/Unknown1.jpeg", CreatedData = DateTime.UtcNow, BodyTypeId = 1, SeatsCount = 1, Url = "https://www.ilya.ru"},
            });
    }
}
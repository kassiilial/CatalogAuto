using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Car
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    
    [MaxLength(1000)]
    public string ModelName { get; set; }
    public string Image { get; set; }
    public DateTime CreatedData { get; set; }
    public int BodyTypeId { get; set; }
    public BodyType BodyType { get; set; }
    
    [Range(1, 12)]
    public int SeatsCount { get; set; }
   
    [MaxLength(1000)]
    public string? Url { get; set; }
}

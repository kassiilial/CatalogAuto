using System.ComponentModel.DataAnnotations;

namespace SiteRazor;

public class CarModel
{
    public Guid Id { get; set; }
    public int BrandId { get; set; }
    
    [Required(ErrorMessage = "Бренд является обязательным")]
    public BrandModel Brand { get; set; }
    
    [Required(ErrorMessage = "Имя модели является обязательным")]
    [StringLength(1000, ErrorMessage = "Длина строки должна быть до 1000 символов")]
    public string ModelName { get; set; }
    public string Image { get; set; }
    public DateTime? CreatedData { get; set; }
    public int BodyTypeId { get; set; }
    
    [Required(ErrorMessage = "Тип кузова является обязательным")]
    public BodyTypeModel BodyType { get; set; }
    
    [Required(ErrorMessage = "Количество мест является обязательным")]
    [Range(1,12, ErrorMessage = "Количество мест может быть между 1 и 12")]
    public int SeatsCount { get; set; }
    
    [Url]
    [StringLength(1000, ErrorMessage = "Длина строки должна быть до 1000 символов")]
    [RegularExpression(@".*\.ru", ErrorMessage = "Некорректный адрес, используйте российскую доменную область")]
    public string? Url { get; set; }
}
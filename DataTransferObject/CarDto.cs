namespace DataTransferObject;

public class CarDto
{
    public Guid Id { get; set; }
    public int BrandId { get; set; }
    public BrandDto Brand { get; set; }
    public string ModelName { get; set; }
    public string Image { get; set; }
    public DateTime? CreatedData { get; set; }
    public int BodyTypeId { get; set; }
    public BodyTypeDto BodyType { get; set; }
    public int SeatsCount { get; set; }
    public string? Url { get; set; }
}
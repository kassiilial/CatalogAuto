using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteRazor.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICarServices _carServices;
    private readonly IMapper _mapper;
    
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    public int Count { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    public IList<CarModel> Cars { get; set; }
    public string Message { get; set; } = String.Empty;

    public IndexModel(ILogger<IndexModel> logger, ICarServices carService, IMapper mapper)
    {
        _logger = logger;
        _carServices = carService;
        _mapper = mapper;
    }
    public void OnGet(string? text)
    {
        if (text!=null)
        {
            Message = text;
        }
        else
        {
            text = String.Empty;
        }
        Cars = GetPaginatedResult(CurrentPage, PageSize);
    }
    public List<CarModel> GetPaginatedResult(int currentPage, int pageSize = 10)
    {
        var data = _carServices.GetAll().Select(n=> _mapper.Map<CarModel>(n)).ToList();
        Count = data.Count;
        return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
    }
}
using System.Data;
using AutoMapper;
using BusinessLogic;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteRazor.Pages;

public class EditModel : PageModel
{
    private readonly ICarServices _carServices;
    private readonly IMapper _mapper;
    private readonly ILogger<EditModel> _logger;

    public EditModel(ICarServices carServices, IMapper mapper, ILogger<EditModel> logger)
    {
        _carServices = carServices;
        _mapper = mapper;
        _logger = logger;
    }

    [BindProperty]
    public CarModel CarFrom { get; set; }
    [BindProperty]
    public CarModel CarTo { get; set; }
    public SelectList CarBodyTypeNames { get; set; }
    public SelectList CarBrandNames { get; set; }

    public async Task<IActionResult> OnGet(Guid id)
    {
        CarFrom = _mapper.Map<CarModel>(_carServices.Get(id));
        CarTo = CarFrom;
        CarBodyTypeNames = new SelectList(_carServices.GetAllBodyType());
        CarBrandNames = new SelectList(_carServices.GetAllBrandName());
        if (CarFrom == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CarFrom = _mapper.Map<CarModel>(_carServices.Get(CarFrom.Id));
        try
        {
            await _carServices.UpdateAsync(_mapper.Map<CarDto>(CarFrom), _mapper.Map<CarDto>(CarTo));
        }
        catch (DataException e)
        {
            return RedirectToPage("./Index", new {text="This car already has existed"});
        }
        return RedirectToPage("./Index", new {text="This car has modified"});
    }
}
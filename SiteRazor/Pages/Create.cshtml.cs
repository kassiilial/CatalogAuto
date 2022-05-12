using System.Data;
using AutoMapper;
using BusinessLogic;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiteRazor.Pages;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;
    private readonly ICarServices _carServices;
    private readonly IMapper _mapper;
    IWebHostEnvironment _appEnvironment;
    [BindProperty] public CarModel Car { get; set; }
    public SelectList CarBodyTypeNames { get; set; }
    public SelectList CarBrandNames { get; set; }

    public CreateModel(ILogger<CreateModel> logger, ICarServices carServices, IMapper mapper,
        IWebHostEnvironment appEnvironment)
    {
        _logger = logger;
        _carServices = carServices;
        _mapper = mapper;
        _appEnvironment = appEnvironment;
    }
    
    public IActionResult OnGet()
    {
        CarBodyTypeNames = new SelectList(_carServices.GetAllBodyType());
        CarBrandNames = new SelectList(_carServices.GetAllBrandName());
        return Page();
    }
    public async Task<IActionResult> OnPostAsync(IFormFile uploadedFile)
    {
        var path = await AddFileAsync(uploadedFile);
        Car.Image = path;
        try
        {
            await _carServices.CreateAsync(_mapper.Map<CarDto>(Car));
        }
        catch (DataException e)
        {
            return RedirectToPage("./Index", new {text="This car already has exist"});
        }
        return RedirectToPage("./Index", new {text="This car has created"});
    }
    private async Task<string> AddFileAsync(IFormFile uploadedFile)
    {
        var extension = Path.GetExtension(uploadedFile.FileName);
        if (!(extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg")))
        {
            return "Not correct file type";
        }
        var path = "/Files/" + uploadedFile.FileName;
        await using var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
        await uploadedFile.CopyToAsync(fileStream);
        return path;
    }
}
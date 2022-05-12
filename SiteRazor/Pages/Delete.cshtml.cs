using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteRazor.Pages;

public class Delete : PageModel
{
    private readonly ICarServices _carServices;
    private readonly ILogger<Delete> _logger;

    public Delete(ICarServices carServices, ILogger<Delete> logger)
    {
        _carServices = carServices;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        await _carServices.DeleteAsync(id);
        return RedirectToPage("./Index", new {text="This car has deleted"});
    }
}
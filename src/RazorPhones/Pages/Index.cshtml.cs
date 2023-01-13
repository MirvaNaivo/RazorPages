using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPhones.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RazorPhones.Models;

namespace RazorPhones.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Data.PhonesContext _context;

    public IndexModel(ILogger<IndexModel> logger, Data.PhonesContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IList<Phone> Phones { get; set; }

    public async Task OnGetAsync()
    {
        Phones = await _context.Phones.OrderByDescending(p => p.Modified).Take(3).ToListAsync();
    }
}

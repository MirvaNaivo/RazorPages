using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPhones.Data;
using RazorPhones.Models;

namespace RazorPhones.Pages.Phones
{
    public class PhonesModel : PageModel
    {
        private readonly Data.PhonesContext _context;

    public PhonesModel(Data.PhonesContext context)
        {
            _context = context;
        }
        public IList<Phone> Phones { get; set; }

        public async Task OnGetAsync()
        {
            Phones = await _context.Phones.OrderBy(p => p.Make).ToListAsync();
        }
    }
}

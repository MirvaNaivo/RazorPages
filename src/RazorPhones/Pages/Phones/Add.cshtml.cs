using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPhones.Data;
using System.Xml.Linq;

namespace RazorPhones.Pages.Phones
{
    public class AddModel : PageModel
    {
        private readonly Data.PhonesContext _context;

        public AddModel(Data.PhonesContext context)
        {
            _context = context;

            Phone = new Phone();
        }

        [BindProperty]
        public Phone Phone { get; set; }

        public void OnGet(string make, string model, int ram, DateTime publishDate )
        {
            Phone.Make = make;
            Phone.Model = model;
            Phone.RAM = ram;
            Phone.PublishDate = publishDate;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (false == ModelState.IsValid)
            {
                return Page();
            }

            _context.Phones.Add(Phone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting;
using RazorPhones.Data;
using System.Threading.Channels;
using System.Xml.Linq;

namespace RazorPhones.Pages.Phones
{
    public class EditModel : PageModel
    {
        private readonly Data.PhonesContext _context;

        public EditModel(Data.PhonesContext context)
        {
            _context = context;

            Phone = new Phone();
        }

        [BindProperty]
        public Phone Phone { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == id);

            if(Phone == null)
            {
                return NotFound();
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Phone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(Phone.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PhoneExists(int id)
        {
            return (_context.Phones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}


//Edit page(/phones/edit/[id]) that allows editing of the phone's data. Value for the [id] part of the url selects the phone
//by it's Id property to be edited. Edit page renders input fields for all of the Phone class' properties and marks input fields
//for properties Id, Created and Modified to readonly so user gets the indication that these fields should not be modified by the user.
//The page must not contain any other fields for data input. Use Phone class as the bind property type and use Phone as
//the property's name.POSTing the form makes the changes to the selected phone object and persists them to the database.When the POST is
//successfull the app redirects to /phones page to list all the phones including the changed phone.
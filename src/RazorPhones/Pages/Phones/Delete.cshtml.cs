using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPhones.Data;

namespace RazorPhones.Pages.Phones
{
    public class DeleteModel : PageModel
    {
        private readonly Data.PhonesContext _context;

        public DeleteModel(Data.PhonesContext context)
        {
            _context = context;

            Phone = new Phone();
        }

        [BindProperty]
        public Phone Phone { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {


            if (id == null)
            {
                return NotFound();
            }

            Phone = await _context.Phones.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (Phone == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var phone = await _context.Phones.FindAsync(id);

            if (phone == null)
            {
                return NotFound();
            }

            try
            {
                _context.Phones.Remove(phone);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }

        private bool PhoneExists(int id)
        {
            return (_context.Phones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

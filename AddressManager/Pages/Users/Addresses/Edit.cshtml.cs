using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressManager.Data;
using AddressManager.Models;

namespace AddressManager.Pages.Users.Addresses
{
    public class EditModel : PageModel
    {
        private readonly AddressManager.Data.ManagerContext _context;

        public EditModel(AddressManager.Data.ManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Address Address { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int userId, int id)
        {
            if (id == null || userId == null || _context.Addresses == null )
            {
                return NotFound();
            }

            var address =  await _context.Addresses.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
  
            if (address == null)
            {
                return NotFound();
            }
            Address = address;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int userId, int id)
        {
           // if (!ModelState.IsValid)
          //  {
           //     return Page();
          //  }

            _context.Attach(Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(Address.UserId, Address.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect("/Users/Details/" + userId);
        }

        private bool AddressExists(int userId, int id)
        {
          return (_context.Addresses?.Any(e => e.Id == id && e.UserId == userId)).GetValueOrDefault();
        }
    }
}

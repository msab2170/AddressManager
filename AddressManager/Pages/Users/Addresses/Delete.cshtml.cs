using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AddressManager.Data;
using AddressManager.Models;

namespace AddressManager.Pages.Users.Addresses
{
    public class DeleteModel : PageModel
    {
        private readonly AddressManager.Data.ManagerContext _context;

        public DeleteModel(AddressManager.Data.ManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Address Address { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? userId, int? id)
        {
            if (id == null || userId == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (address == null)
            {
                return NotFound();
            }
            else 
            {
                Address = address;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? userId, int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.FindAsync(id, userId);

            if (address != null)
            {
                Address = address;
                _context.Addresses.Remove(Address);
                await _context.SaveChangesAsync();
            }

            return Redirect("/Users/Details/" + userId);
        }
    }
}

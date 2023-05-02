using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AddressManager.Data;
using AddressManager.Models;

namespace AddressManager.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly AddressManager.Data.ManagerContext _context;

        public DetailsModel(AddressManager.Data.ManagerContext context)
        {
            _context = context;
        }

      public User User { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            User = await _context.Users
                .Include(s => s.Addresses)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

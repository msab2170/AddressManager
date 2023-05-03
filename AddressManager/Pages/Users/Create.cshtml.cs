using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AddressManager.Data;
using AddressManager.Models;

namespace AddressManager.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly AddressManager.Data.ManagerContext _context;

        public CreateModel(AddressManager.Data.ManagerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }
            User.JoinDate = DateTime.Now; 
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Users/Index");
        }
    }
}

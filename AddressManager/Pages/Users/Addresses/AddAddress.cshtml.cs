

using AddressManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace AddressManager.Pages.Users.Addresses
{
    public class AddAddressModel : PageModel
    {
        private readonly AddressManager.Data.ManagerContext _context;

        public AddAddressModel(AddressManager.Data.ManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync( )
        {
            return Page();
        }

        [BindProperty]
        public Address Address { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (/*!ModelState.IsValid ||*/ _context.Addresses == null || Address == null)
            {
                return Page();
            }
            IQueryable<int> a = from address in _context.Addresses
                         where address.UserId == id
                         orderby address.Id descending
                         select address.Id;

            Address.Id = a.Count() + 1;
            Address.UserId = id;

            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();
            
            return Redirect($"/Users/Details/{id}");
        }
    }
  
}

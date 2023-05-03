using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AddressManager.Data;
using AddressManager.Models;
using Microsoft.Data.SqlClient;

namespace AddressManager.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AddressManager.Data.ManagerContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(AddressManager.Data.ManagerContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public int pagePerView { get; set; }

        public Pagination<User> Users { get; set; }
        
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null) {
                pageIndex = 1;
            } else {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<User> TempUsers = from s in _context.Users
                                                 select s;

            if(!String.IsNullOrEmpty(searchString) ) {
                TempUsers = TempUsers.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()) 
                || s.Email.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder) {
                case "Date":
                    TempUsers = TempUsers.OrderBy(s => s.JoinDate);
                    break;
                case "date_desc":
                    TempUsers = TempUsers.OrderByDescending(s => s.JoinDate);
                    break;
                case "name_desc":
                    TempUsers = TempUsers.OrderByDescending(s => s.Name);
                    break;
                default:
                    TempUsers = TempUsers.OrderBy(s => s.Name);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            pagePerView = Configuration.GetValue("pagePerView", 4);

            Users = await Pagination<User>.CreateAsync(
                TempUsers.AsNoTracking(), 
                pageIndex ?? 1, 
                pageSize
            );
        }
    }
}

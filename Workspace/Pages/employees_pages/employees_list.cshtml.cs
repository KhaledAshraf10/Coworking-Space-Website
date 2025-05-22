
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;
using System.Data;

namespace Workspace.Pages.employees_pages
{
    public class employees_listModel : PageModel
    {


        public string msgNotFound { get; set; }


        DB db;

        [BindProperty(SupportsGet = true)]
        public employee e { get; set; }

        [BindProperty(SupportsGet =true)]
        public DataTable dt { get; set; }

        public employees_listModel(DB db)
        {
            this.db = db;
        }


        public void OnGet()
        {
            this.dt = this.db.getallemployees();
        }



        public IActionResult OnPostSearch() { 
       this.dt= db.getemployee(e);
            if (this.dt.Rows.Count == 0)
            {
                msgNotFound = "Thers is no current employee with this name or ID try using another one or check the whole list again";
                return Page();

            }
            else
            {

                return Page();
            }
        }


    }
}

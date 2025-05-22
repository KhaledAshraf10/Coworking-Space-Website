
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;
namespace Workspace.Pages.Customer_pages
{
    public class customer_listModel : PageModel
    {

        DB db;


        [BindProperty(SupportsGet =true)]
        public CustomerUser Cust { get; set; }


        [BindProperty]
        public string MsgNotFound { get; set; }

        public DataTable dt { get; set; }


        public customer_listModel(DB db)
        {
            this.db= db;

        }



        public void OnGet()
        {
            dt=new DataTable();
            dt=db.getallCustomers();



            
        }

        public IActionResult OnPostSearch()
        {
            this.dt = db.getcustomerid(Cust);
            if (this.dt.Rows.Count == 0)
            {
                MsgNotFound = "Thers is no current Customer with this name or ID try using another one or check the whole list again";
                return Page();

            }
            else
            {

                return Page();
            }
        }





    }
}

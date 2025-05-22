using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;

namespace Workspace.Pages.employees_pages
{
    public class Hire_EmpModel : PageModel
    {

        public string msgPhoneNo { get; set; }  
        public string msgSucc { get; set; }
        public string msgid { get; set; }

        public string msgusername { get; set; }


        [BindProperty(SupportsGet =true)]
        public employee e { get; set; }

 DB db;

        public Hire_EmpModel(DB db)
        {
            this.db = db;
        }


       
        public void OnGet()
        {
        }


        public IActionResult OnPostButton2()
        {
            return RedirectToPage("/employees_pages/employees_list");



        }

        public IActionResult OnPostButton1()
        {

            if (!ModelState.IsValid)
            {
                return Page();

            }
            else
            {

                if (db.checkemployeeid(e))
                {
                    msgid = "This ID is already taken,Work IDs should be unique for each Employee";
                    return Page();
                }

                if (db.checkemployeeusername(e))
                {
                    msgusername = "This username is already taken,try a different one";
                    return Page();

                }

                if (db.checkemployePhoneNO(e))
                {
                    msgPhoneNo = "This phoneNO is already taken,Recheck the number entered and try again";
                    return Page();

                }

                else
                {
                    db.insertemployee(e);
                    msgSucc = e.Fname+" "+e.Lname+" has been succesfully hired with Work ID ("
                        +e.ID+") ";
                    return Page();


                }
            }






        }
    }
}

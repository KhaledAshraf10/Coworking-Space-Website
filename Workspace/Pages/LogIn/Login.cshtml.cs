using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;
using Workspace.Models;

namespace Workspace.Pages.LogIn
{
    public class LoginModel : PageModel
    {
        private readonly DB db;
        [BindProperty]
        public User u { get; set; }
        public string msg { get; set; } = "";
        public LoginModel(DB db)
        {
            this.db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
         
            if (db.CheckPassword(u.UserName, u.Password))
            {
                HttpContext.Session.SetInt32("UserType", db.GetUserType(u.UserName)); // kda enta ma3ak usertype lw 3ayz tgypo fe ay page hat3ml property fe page de => usertype = HttpContext.Session.GetInt32("UserType")
                HttpContext.Session.SetString("UserName",u.UserName);
                if (db.GetUserType(u.UserName) == 1)
                {
                    return RedirectToPage("/Admin/Welcome");
                }
                else if (db.GetUserType(u.UserName) == 2)
                {
                    return RedirectToPage("/Employee/Welcome");
                }
                else if (db.GetUserType(u.UserName) == 3)
                {
                    return RedirectToPage("/Customer/Welcome");
                }
                else { return Page(); }
                          
            }
            else { msg = "Incorrect Password"; return Page(); }
        }
    }
}

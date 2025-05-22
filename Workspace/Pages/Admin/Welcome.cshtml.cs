using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;
namespace Workspace.Pages.Admin
{
    
    public class WelcomeModel : PageModel
    {
        
        public string u { get; set; }
        public void OnGet()
        {
       
            u = HttpContext.Session.GetString("UserName");
        }
     

    }
}

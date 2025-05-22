using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Workspace.Pages.Emplyee
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

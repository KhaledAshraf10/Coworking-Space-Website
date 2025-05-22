using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;

namespace Workspace.Pages.MyTasks
{
    public class UpdateEventModel : PageModel
    {
        public string msg { get; set; } = "";
        private readonly DB dB;
        [BindProperty]
        public Event e { get; set; }

        public UpdateEventModel(DB db)
        {
           dB = db;

        }
        public void OnGet(int id)
        {
            e = dB.ReadEventt(id);
        }
        public IActionResult OnPost()
        {
            if (dB.updateEvent(e))
            {

                msg = "Update Successfully";
                return Page();
            }
            else
            {
                msg = "Invalid data";
                return Page();
            }
            
            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;

namespace Workspace.Pages.MyTasks
{
    public class BookEventModel : PageModel
    {


        private readonly DB dB;
        [BindProperty]
        public Event e { get; set; }
        public string msg { get; set; } = "";
        public BookEventModel(DB db)
        {
            dB = db;

        }
        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            if(dB.insertEvent(e))
            {

               msg = "Insert Successfully"; 
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

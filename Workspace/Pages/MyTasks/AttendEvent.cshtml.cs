using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection;
using Workspace.Models;

namespace Workspace.Pages.MyTasks
{
    public class AttendEventModel : PageModel
    {
        public int user_Type { get; set; }
        private readonly DB dB;

        public string msg { get; set; } = "";
        [BindProperty (SupportsGet =true)]

        public DataTable dt { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        

        public AttendEventModel(DB db)
        {
            dB = db;
            
        }
        public void OnGet(int eventID)
        {
            dt = dB.ReadEvent(eventID);
           

        }
        public IActionResult OnPost(int eventID)
        {
           if(dB.getNOAttendes(eventID) >= 60)
           {
                msg = "Fully BooKed";
                dt = dB.ReadEvent(eventID); 
                return Page();
           }
           else 
           {
                dB.incrementattendes(eventID);
                msg = "Booked Succesfully";
                dt = dB.ReadEvent(eventID); 
                return Page();
           }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Workspace.Models;

namespace Workspace.Pages.MyTasks
{
    public class EventsModel : PageModel
    {
        public int user_Type { get; set; }
        private readonly DB dB;
        public DataTable dt { get; set; }
        
        public EventsModel(DB db)
        {
            dB = db;
        }
        public void OnGet()
        {
            dt = dB.ReadEvent();

        }
        
    }
}

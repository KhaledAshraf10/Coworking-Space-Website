using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Workspace.Models;

namespace Workspace.Pages.MyTasks
{

    public class EventAdminModel : PageModel
    {
        private readonly DB dB;
        public DataTable dt { get; set; }

        public EventAdminModel( DB db)
        {
            
            dB = db;

        }
        public void OnGet()
        {
            dt = dB.ReadEvent();
        }
    }
}

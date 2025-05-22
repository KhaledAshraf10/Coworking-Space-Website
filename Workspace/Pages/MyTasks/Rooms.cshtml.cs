using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;
using Workspace.Models;
namespace Workspace.Pages.MyTasks
{
    public class RoomsModel : PageModel
    {
        public int user_Type { get; set; }
        public DataTable roomTable { get; set; }

        private readonly DB dbRoom;
        public RoomsModel(DB db)
        {
            
            dbRoom = db;
        }
        public void OnGet()
        {


            int? userTypeNullable = HttpContext.Session.GetInt32("UserType");

            if (userTypeNullable.HasValue)
            {
                user_Type = userTypeNullable.Value;
            }
            else
            {
                user_Type = 0;
            }
            roomTable = new DataTable();
            string queryGetRoomTable = "select * from Room";
            roomTable = dbRoom.GetCustomerTable(queryGetRoomTable);

        }
    }
}

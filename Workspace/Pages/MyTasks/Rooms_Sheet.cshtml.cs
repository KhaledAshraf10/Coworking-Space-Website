using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;
using System.Globalization;
using Workspace.Models;

namespace Workspace.Pages.MyTasks
{
    public class Rooms_SheetModel : PageModel
    {
        public int user_Type { get; set; }

        public DataTable sheetTable { get; set; }
        public DataTable sheetTable2 { get; set; }


        private readonly DB sheetDB;
        public Rooms_SheetModel(DB sheetDB)
        {
            this.sheetDB = sheetDB;
            sheetTable = new DataTable();
            sheetTable2 = new DataTable();

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

            DateTime today = DateTime.Today;
            string todayDate = today.ToString("yyyy-MM-dd");
            string todayDateTime = todayDate + " 12:00:00.000";
            DateTime todayDateTimeObj = DateTime.ParseExact(todayDateTime, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            DateTime lastweek = today.AddDays(-7);
            string lastweekDate = lastweek.ToString("yyyy-MM-dd");
            string lastweekDateTime = lastweekDate + " 12:00:00.000";
            DateTime lastweekDateTimeObj = DateTime.ParseExact(lastweekDateTime, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            string queryGetSheetTable1 = $"select CONCAT(Fname, ' ', Lname) AS FullName, Email, PhoneNO, B.Room_ID, R.type, R.Cost as 'Cost Per Hour', B.Date_time, B.time_Hours, B.Payment from Person P, Customer C, Book_Room B, Room R where C.Customer_ID = P.ID and B.Room_ID = R.Room_id and P.ID = B.Customer_ID and B.Date_time between '{lastweekDateTimeObj}' and '{todayDateTimeObj}' order by B.Date_time;";
            sheetTable = sheetDB.GetCustomerTable(queryGetSheetTable1);

            string queryGetSheetTable2 = $"select CONCAT(Fname, ' ', Lname) AS FullName, Email, PhoneNO, B.Chair_ID, R.Room_ID, R.type, R.Cost as 'Cost Per Hour', B.Date_time, B.time_Hours, B.Payment from Person P, Customer C, Book_Chair B, Chair CH, Room R where C.Customer_ID = P.ID and P.ID = B.Customer_ID and CH.Chair_ID = B.Chair_ID and CH.Room_ID = R.Room_id and B.Date_time between '{lastweekDateTimeObj}' and '{todayDateTimeObj}' order by B.Date_time;";
            sheetTable2 = sheetDB.GetCustomerTable(queryGetSheetTable2);
        }
    }
}

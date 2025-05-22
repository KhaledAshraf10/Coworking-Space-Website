using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;
using System.Globalization;
using Workspace.Models;

namespace Workspace.Pages.MyTasks
{
    public class RoomProfileModel : PageModel
    {
        public int user_Type { get; set; }
        public string user_Name { get; set; }
        public int CustomerID { get; set; }

        public int chairIDBook { get; set; }

        int[] array = new int[84];
        public List<int> capcityList { get; set; }

        public DataTable bookRoomTable { get; set; }
        public int ID_Room { get; set; }


        private readonly DB bookRoom;
        public RoomProfileModel(DB db)
        {
            bookRoom = db;
            capcityList = new List<int>(array);
        }


        public void checkCapacity(int IDR)
        {
            int index = 0;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time;

            int durationFromTable = 0;

            int countCapacity = 0;

            for (int g = 1; g <= 7; g++)
            {
                for (int k = 13; k <= 24; k++)
                {
                    if (k == 24)
                        time = date + " 00:00:00.000";
                    else
                        time = date + $" {k}" + ":00:00.000";

                    for (int i = 0; i < bookRoomTable.Rows.Count; i++)
                    {
                        DateTime dateTimeFromDatabase = (DateTime)bookRoomTable.Rows[i][2];
                        string formattedDateTime = dateTimeFromDatabase.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        if (formattedDateTime == time)
                        {
                            capcityList[index]++;
                            durationFromTable = (int)bookRoomTable.Rows[i][3];
                            for (int n = 1; n < durationFromTable; n++)
                            {
                                capcityList[index+n]++;
                            }

                        }
                        
                    }
                    index++;
                }
                DateTime currentDate = DateTime.Parse(date);
                currentDate = currentDate.AddDays(g);
                date = currentDate.ToString("yyyy-MM-dd");

            }

           
            
        }

        public void OnGet(int roomID)
        {

            string? userNameNullable = HttpContext.Session.GetString("UserName");

            if (userNameNullable!=null)
            {
                user_Name = userNameNullable;
            }
            else
            {
                user_Name = "";
            }
            string queryGetCustomerID = $"select ID from Person where Username='{user_Name}';";
            CustomerID = bookRoom.GetInt_Value(queryGetCustomerID); 


            int? userTypeNullable = HttpContext.Session.GetInt32("UserType");

            if (userTypeNullable.HasValue)
            {
                user_Type = userTypeNullable.Value;
            }
            else
            {
                user_Type = 0;
            }


            ID_Room = roomID;
            bookRoomTable = new DataTable();

            DateTime currentDate = DateTime.Now;
            DateTime dateAfterWeek = currentDate.AddDays(6);
            string formattedDate = currentDate.ToString("yyyy-MM-dd");
            string formattedDateAfterWeek = dateAfterWeek.ToString("yyyy-MM-dd");

            string timeWillCompare1 = formattedDate + " 00:00:00.000";
            DateTime datetimeStoredInDB1 = DateTime.ParseExact(timeWillCompare1, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            string timeWillCompare2 = formattedDateAfterWeek + " 00:00:00.000";
            DateTime datetimeStoredInDB2 = DateTime.ParseExact(timeWillCompare2, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);



            if (roomID > 6)
            {
                string queryBookRoom = $"SELECT R.Room_id, R.Type, B.Date_time, B.time_Hours FROM Room R INNER JOIN Book_Room B ON B.Room_ID = R.Room_id WHERE R.Room_id = '{ID_Room}'  AND B.Date_time >= '{datetimeStoredInDB1}' AND B.Date_time <= '{datetimeStoredInDB2}';";
                bookRoomTable = bookRoom.GetCustomerTable(queryBookRoom);
                checkCapacity(roomID);
            }
            else
            {       
                string queryBookRoom = $"SELECT C.Room_ID, C.Chair_ID, B.Date_time, B.time_Hours FROM Chair C INNER JOIN Book_Chair B ON C.Chair_ID = B.Chair_id WHERE C.Room_ID = '{ID_Room}' AND B.Date_time >= '{datetimeStoredInDB1}'  AND B.Date_time <= '{datetimeStoredInDB2}';";
                bookRoomTable = bookRoom.GetCustomerTable(queryBookRoom);
                checkCapacity(roomID);
            }


        }


        public int GetChairIDToBook(int roomnumber)
        {
            int chairToBook = 0;
            string queryGetChairID = $"SELECT Chair_ID " +
            $"FROM Chair " +
            $"WHERE Room_ID = '{roomnumber}' AND Chair_ID NOT IN " +
            $"(SELECT B.Chair_id FROM Book_Chair B WHERE B.Date_time > GETDATE() AND B.Date_time < DATEADD(WEEK, 1, GETDATE()))";

            DataTable tableOfEmptyChairs = bookRoom.GetCustomerTable(queryGetChairID);
           
             chairToBook = (int)tableOfEmptyChairs.Rows[0][0];

            return chairToBook;
        }
        public IActionResult OnPost()
        {
            int CUST_ID = int.Parse(Request.Form["CUSTID"]);
            int ROOM_NUM = int.Parse(Request.Form["RoomNUM"]);
            string dateUser = Request.Form["dateBook"];
            string timeUser = Request.Form["timeBook"];
            string durationUser = Request.Form["durationBook"];

            string dateTimeUser = dateUser + " " + timeUser + ":00.000";
            DateTime datetimeStoredInDB = DateTime.ParseExact(dateTimeUser, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            if(ROOM_NUM > 6)
            {
                string getIDBookingRoom = "select * from Book_Room";
                DataTable BookingRoom = bookRoom.GetCustomerTable(getIDBookingRoom);

                string queryBookRoom = $"INSERT INTO Book_Room(Booking_ID, Room_id, Customer_ID, Date_time, time_Hours, Payment) VALUES('{BookingRoom.Rows.Count + 1}' , '{ROOM_NUM}' ,'{CUST_ID}', '{datetimeStoredInDB}', '{durationUser}', 'Vodafone Cash');";
                bookRoom.InertIntoTable(queryBookRoom);
            }
            else
            {
                chairIDBook = GetChairIDToBook(ROOM_NUM);

                string getIDBookingChair = "select * from Book_Chair";
                DataTable BookingChair = bookRoom.GetCustomerTable(getIDBookingChair);

                string queryBookChair = $"INSERT INTO Book_Chair(Booking_Chair_ID, Chair_id, Customer_ID, Date_time, time_Hours, Payment) VALUES('{BookingChair.Rows.Count+1}' , '{chairIDBook}' ,'{CUST_ID}', '{datetimeStoredInDB}', '{durationUser}', 'Vodafone Cash');";
                bookRoom.InertIntoTable(queryBookChair);
            }
           

            return RedirectToPage("/MyTasks/RoomProfile", new { roomID = ROOM_NUM});
        }

        public IActionResult OnPostCancelBooking()
        {
            DataTable book_chair_tale = new DataTable();
            string queryGetBookChair = "select * from Book_Chair";
            book_chair_tale = bookRoom.GetCustomerTable(queryGetBookChair);

            string queryCancelBook = $"DELETE FROM Book_Chair WHERE Booking_Chair_ID = '{book_chair_tale.Rows.Count}';";
            bookRoom.InertIntoTable(queryCancelBook);

            return RedirectToPage("/MyTasks/RoomProfile", new { roomID = ID_Room });

        }

    }
}

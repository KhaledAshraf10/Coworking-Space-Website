using System.Data;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;

namespace Workspace.Pages.LogIn
{
    public class SignUPModel : PageModel
    {
        public int user_Type { get; set; }


        private readonly DB db;
        public DataTable Customer_table { get; set; }
        public SignUPModel(DB db)
        {
            this.db = db;
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

            Customer_table = db.GetCustomerTable("select * from Customer");
        }

        /*protected void Button_Click(object sender, EventArgs e)
        {

            db.InertIntoTable("INSERT INTO Bill(Bill_id, date_time, Cost_Products, Customer_id) VALUES(9, '2023-04-20 15:30:00', 100, 5);");

        }*/

        

        public IActionResult OnPost()
        {
            // Retrieve values from the form input fields
            string Fname = Request.Form["firstName"];
            string Lname = Request.Form["lastName"];
            string Email = Request.Form["email"];
            string phoneNumber = Request.Form["phoneNum"];
            string UserName = Request.Form["userName"];
            string Password = Request.Form["password"];
            string confirmPassword = Request.Form["confirmpassword"];

            string queryGetPerson = "select * from Person";
            DataTable PersonTable = db.GetCustomerTable(queryGetPerson);
            int Person_ID = PersonTable.Rows.Count + 1;

            string insertQuery = $"INSERT INTO PERSON(ID, Fname,Lname, Email, Username, Password, PhoneNO, UserType) VALUES('{Person_ID}' ,'{Fname}','{Lname}', '{Email}', '{UserName}', '{Password}', '{phoneNumber}', 3);";
            db.InertIntoTable(insertQuery);

            DateTime today = DateTime.Today;
            string todayDate = today.ToString("yyyy-MM-dd");
            string insertQuery1 = $"INSERT INTO Customer(Customer_ID, Status, registration_date) VALUES('{Person_ID}', 0,'{todayDate}');";
            db.InertIntoTable(insertQuery1);

            return RedirectToPage("/LogIn/Login");


        }

	}
}

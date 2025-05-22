using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Workspace.Models;


namespace Workspace.Pages.Customer_pages
{
    public class edit_CustModel : PageModel
    {
		// section of image
		public byte[] userImage { get; set; }
		public string baseImage { get; set; }

		public string fullName { get; set; }
		public string user_email { get; set; }
		public string user_phone { get; set; }
		public int idPerson { get; set; }
		public int subscription { get; set; }
		public string user_status { get; set; }



		private readonly DB dbProfile;

		public edit_CustModel(DB db)
		{
			dbProfile = db;
		}

		public void OnGet()
        {
			/*string id = Request.Query["IDPerson"];
			idPerson = int.Parse(id);*/

			string user_firstName = Request.Query["userFirstName"];

			// Retrieve the value of a query string parameter named "userName"
			string user_lastName = Request.Query["userLastName"];

			fullName = user_firstName + " " + user_lastName;
			user_email = Request.Query["userEmail"];
			user_phone = Request.Query["userPhone"];

			DataTable Customer_table;
			Customer_table = dbProfile.GetCustomerTable("select * from Person");
			int counter = Customer_table.Rows.Count;

			string query_status = $"select status from Customer where Customer_Id='{counter}'";
			user_status = dbProfile.GetString_Value(query_status);

			string query_subsxription = $"select s.Price from Customer c, Subscribtion s where c.Subscribtion_id = s.Subscribtion_id and c.Customer_ID = '{counter}';";
			subscription = dbProfile.GetInt_Value(query_subsxription);

		}


		public void OnPostCustomHandler()
		{
			var imageFile = Request.Form.Files["userImage"];
			if (imageFile != null && imageFile.Length > 0)
			{
				byte[] imageData;
				using (var stream = new MemoryStream())
				{
					imageFile.CopyTo(stream);
					imageData = stream.ToArray();
				}

				string Query = $"update Person set Person_Pic=CONVERT(varbinary(max), '{imageData}') where Email='{user_email}'";

				dbProfile.UpdatImage(Query);

				string Query2 = $"select Person_Pic from Person where Email='{user_email}'";

				userImage = dbProfile.GetImage(Query2);
				baseImage = Convert.ToBase64String(userImage);

			}

		}




	}
}

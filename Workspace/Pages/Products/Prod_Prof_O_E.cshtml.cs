using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Workspace.Models;
using System.Data;

namespace Workspace.Pages.Products
{
    public class Prod_Prof_O_EModel : PageModel
    {
        public int user_Type { get; set; }


        public DataTable Product_Table { get; set; }
        public int Product_ID { get; set; }





        private readonly DB ProductDB;
        public Prod_Prof_O_EModel(DB db)
        {
            Product_Table = new DataTable();
            ProductDB = db; 
        }
        public void OnGet(int productID)
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

            Product_ID = productID;

            string queryGetProducts = $"select * from Products where Product_id='{productID}';";
            Product_Table = ProductDB.GetCustomerTable(queryGetProducts);
        }

        public IActionResult OnPost()
        {

            string Current_Price = Request.Form["CurrentPrice"];
            string Current_Quantity = Request.Form["CurrentQuantity"];

            int ID = int.Parse(Request.Form["CurrentProduct"]);

            string queryUpdateProdut = $"update Products set Price = '{Current_Price}', Amount = '{Current_Quantity}' where Product_id = '{ID}'";
            ProductDB.InertIntoTable(queryUpdateProdut);

            return RedirectToPage("/Products/Prod_Prof_O_E", new { productID = ID });
        }
    }
}

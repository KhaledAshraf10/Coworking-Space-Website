using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Workspace.Models;
using System.Data;

namespace Workspace.Pages.Products
{
    public class ProductsModel : PageModel
    {
        public int user_Type { get; set; }


        public DataTable productTable { get; set; }

        private readonly DB productsDB;
        public ProductsModel(DB db)
        {
            productsDB = db;    
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

            string queryGetProductTable = "select * from Products";
            productTable = productsDB.GetCustomerTable(queryGetProductTable);

        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Workspace.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Workspace.Pages.Customer_pages
{
    public class Cust_HistoryModel : PageModel
    {
        private readonly DB _db;
        public int customerId { get; set; }
        public List<Dictionary<string, object>> SpendingData { get; set; }
        public Cust_HistoryModel(DB db)
        {
            _db = db;
        }
        public void OnGet(int Cust)
        {
            customerId = Cust;
            var spendingData = _db.GetCustomerSpending(customerId);
            SpendingData = spendingData;
        }
    }
}
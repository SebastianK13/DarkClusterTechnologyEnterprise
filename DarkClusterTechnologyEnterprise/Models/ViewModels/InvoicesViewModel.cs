using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class InvoicesViewModel
    {
        public IEnumerable<InvoiceDetail>? Invoices { get; set; }
        public PageInfo? PagesInfo { get; set; }
    }
    public class InvoiceDetail
    {
        public InvoiceDetail() { }
        public InvoiceDetail(List<Invoice> inv)
        {
            Invoices = new List<InvoiceDetail>();
            foreach(var i in inv)
            {
                Invoices.Add(new InvoiceDetail { 
                    CustomerName = i.Customer?.CustomerName,
                    //Email = i.Customer?.Email,
                    InvoiceID = i.InvoiceId,
                    Date = i.Date.ToString(),
                    Expire = i.ExpiresDate.ToString(),
                    ResponsibleEmployee = i.ResponsibleEmployee,
                    TotalPrice = i.InvoiceDetails?.TotalPrice + i.InvoiceDetails?.Currency
                });
            }
        }
        public string? CustomerName { get; set; }
        //public string? Email { get; set; }
        public string? InvoiceID { get; set; }
        public string? Date { get; set; }
        public string? Expire { get; set; }
        public string? ResponsibleEmployee { get; set; }
        public string? TotalPrice { get; set; }
        public List<InvoiceDetail> Invoices { get; set; }
    }
}

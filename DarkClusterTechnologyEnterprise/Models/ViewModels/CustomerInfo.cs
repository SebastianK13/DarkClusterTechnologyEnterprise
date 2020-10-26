using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class InvoiceView
    {
        public InvoiceView(Invoice inv)
        {
            InvoiceId = inv.InvoiceId;
            CustomerName = inv.Customer?.CustomerName;
            Email = inv.Customer?.Email;
            Country = inv.Customer?.CustomerLocation?.Country;
            City = inv.Customer?.CustomerLocation?.City;
            StreetAddress = inv.Customer?.CustomerLocation?.StreetAddress;
            ZipCode = inv.Customer?.CustomerLocation?.ZipCode;
            InvoiceDate = inv.Date;
            InvoiceExpires = inv.ExpiresDate;
        }
        public string? InvoiceId { get; set; }
        public string? CustomerName { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? InvoiceExpires { get; set; }
        public List<Service> Services { get; set; }
    }
    public class CustomerInfo
    {
        public CustomerInfo() { }
        public CustomerInfo(Customer c, CustomerLocation l)
        {
            CustomerName = c.CustomerName;
            Email = c.Email;
            Country = l.Country;
            City = l.City;
            StreetAddress = l.StreetAddress;
            ZipCode = l.ZipCode;
        }
        public CustomerInfo(List<Customer> results)
        {
            customers = new List<CustomerInfo>();
            foreach (var r in results)
            {
                SetInvoiceId();
                customers.Add(new CustomerInfo
                {

                    CustomerId = r.CustomerId,
                    CustomerName = r.CustomerName,
                    Email = r.Email,
                    Country = r.CustomerLocation?.Country,
                    City = r.CustomerLocation?.City,
                    StreetAddress = r.CustomerLocation?.StreetAddress,
                    ZipCode = r.CustomerLocation?.ZipCode,
                    InvoiceId = InvoiceId
                });
            }
        }
        public string InvoiceId { get; set; }
        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }
        public int CustomerId { get; set; }
        public List<CustomerInfo> customers { get; set; }

        private void SetInvoiceId()
        {
            Random r = new Random();
            string invoicePartOne = "";
            for (int i = 0; i < 4; i++)
            {
                char c = (char)r.Next(65, 90);
                invoicePartOne += c.ToString();
            }

            string invoiceID = "INV-" + invoicePartOne + (DateTime.Now).ToString();
            Regex rgx = new Regex("[^a-zA-Z0-9-]");
            invoiceID = rgx.Replace(invoiceID, "");
            InvoiceId = invoiceID.Substring(0, 21);
        }
    }
    public class InvoiceReceive
    {
        public int CustomerId { get; set; }
        public List<string>? InvoiceServices { get; set; }
        public string? InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceExpires { get; set; }
        public decimal? Summary { get; set; }
    }
    public class Services
    {
        public string? Service { get; set; }
        public int Quantity { get; set; }
        public float GrossV { get; set; }
    }
}

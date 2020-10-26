using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private AppCustomersDbContext context;
        public EFCustomerRepository(AppCustomersDbContext context)
        {
            this.context = context;
        }

        public List<CustomerInfo> Customer(string type)
        {
            if (type != null)
            {

            }
            var searchResult = context.Customers.Where(x => x.CustomerName.Contains(type)).Take(5).ToList();

            CustomerInfo ci = new CustomerInfo(searchResult);

            return ci.customers;
        }
        public void CreateCustomer(Customer customer, CustomerLocation location, string responsibleEmployee)
        {
            context.CustomersLocation.Add(location);
            customer.CLocationId = location.LocationId;
            customer.ResponsibleEmployee = responsibleEmployee;
            context.Customers.Add(customer);

            context.SaveChanges();
        }
        public async Task<bool> CreateInvoice(InvoiceReceive invoice, string responsibleEmployee)
        {

            Invoice inv = new Invoice()
            {
                CustomerId = invoice.CustomerId,
                ResponsibleEmployee = responsibleEmployee,
                Date = invoice.InvoiceDate,
                ExpiresDate = invoice.InvoiceExpires,
                InvoiceId = invoice.InvoiceId
            };

            inv.InvoiceDetailsId = await CreateInvDetailsAsync(invoice.Summary);

            context.Invoices.Add(inv);
            var services = CreateServicesForInv(invoice.InvoiceServices, invoice.InvoiceId);

            services.ForEach(s => context.Services.Add(s));

            return context.SaveChangesAsync().Result > 0;
        }
        public List<Service> CreateServicesForInv(List<string>? invServices, string? invoiceId)
        {
            var counter = 0;
            List<string> tempServiceList = new List<string>();
            List<Service> services = new List<Service>();
            for (int i = 0; i < invServices?.Count; i++)
            {
                tempServiceList.Add(invServices[i]);
                counter++;

                if (counter == 3)
                {
                    services.Add(new Service(tempServiceList, invoiceId));
                    counter = 0;
                    tempServiceList.Clear();
                }

            }
            return services;
        }
        public async Task<int> CreateInvDetailsAsync(decimal? summ)
        {
            InvoiceDetails invD = new InvoiceDetails()
            {
                Currency = "USD",
                TotalPrice = summ
            };

            context.InvoicesDetails.Add(invD);

            await context.SaveChangesAsync();

            return invD.DetailsId;
        }
        public List<Invoice> GetInvoices(string responsibleEmployee) =>
            context.Invoices
            .Where(r => r.ResponsibleEmployee == responsibleEmployee)
            .ToList();

        public Invoice GetInvoice(string invID) => 
            context.Invoices
            .Where(i => i.InvoiceId == invID)
            .FirstOrDefault();

        public List<Service> GetServices(string invID) => 
            context.Services
            .Where(i => i.InvoiceId == invID)
            .ToList();
    }
}

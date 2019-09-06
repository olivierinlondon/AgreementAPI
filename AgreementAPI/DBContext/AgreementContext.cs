
using System.Data.Entity;
using AgreementAPI.Models;

namespace AgreementAPI.DBContext
{
    public class AgreementContext : DbContext
    {
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AgreementContext() : base("AgreementDBConnectionString")
        {
            Database.SetInitializer(new AgreementDBInitializer());
        }
    }
}
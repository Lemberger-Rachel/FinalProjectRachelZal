using BrixBank.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrixBank.Data
{
    
    public class BrixBankContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<LoanRequest> LoanRequests { get; set; }

        public BrixBankContext()
        {
        }

        public BrixBankContext(DbContextOptions<BrixBankContext> options)
          : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}

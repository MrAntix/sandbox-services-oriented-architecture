using System.Data.Entity;

using Sandbox.SOA.Services.Data.Models;

namespace Sandbox.SOA.Services.Data
{
    public class DataContext : DbContext
    {
        public IDbSet<PersonData> People
        {
            get { return Set<PersonData>(); }
        }

        public IDbSet<PersonAddressData> PersonAddresses
        {
            get { return Set<PersonAddressData>(); }
        }

        protected override void OnModelCreating(
            DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonData>()
                        .HasKey(d => d.Id)
                        .Property(d => d.Identifier).IsRequired();

            modelBuilder.Entity<PersonData>()
                        .HasMany(p => p.Addresses)
                        .WithRequired(a => a.Person);

            modelBuilder.Entity<PersonAddressData>()
                        .HasKey(d => d.Id);
        }
    }
}
using System.Data.Entity.Migrations;
using System.Linq;

using Sandbox.SOA.Services.Data;
using Sandbox.SOA.Services.Data.Models;

namespace Sandbox.SOA.Services.Migrations
{
    sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            context.People
                   .AddOrUpdate(
                       p => p.Identifier,
                       new PersonData
                           {
                               Identifier = "69336C4C-07DB-45AA-ADCF-E14A8B9F30C7",
                               FirstName = "Anthony",
                               LastName = "Johnston",
                               Addresses = new[]
                                   {
                                       new PersonAddressData
                                           {
                                               Name = "Work",
                                               County = "West Sussex"
                                           }
                                   }.ToList()
                           },
                       new PersonData
                           {
                               Identifier = "29336C4C-07DB-45AA-ADCF-E14A8B9F30C7",
                               FirstName = "Sarah",
                               LastName = "Johnston",
                               Addresses = new[]
                                   {
                                       new PersonAddressData
                                           {
                                               Name = "Home",
                                               County = "West Sussex"
                                           },
                                       new PersonAddressData
                                           {
                                               Name = "Work"
                                           }
                                   }.ToList()
                           }
                );
        }
    }
}
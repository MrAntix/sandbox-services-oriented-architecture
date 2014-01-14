using System;
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
            var identifiers = new[]
                {
                    "49336C4C-07DB-45AA-ADCF-E14A8B9F30C7",
                    "63336C4C-07DB-45AA-ADCF-E14A8B9F30C7",
                    "69536C4C-07DB-45AA-ADCF-E14A8B9F30C7",
                    "69366C4C-07DB-45AA-ADCF-E14A8B9F30C7",
                    "69337C4C-07DB-45AA-ADCF-E14A8B9F30C7",
                    "6933684C-07DB-45AA-ADCF-E14A8B9F30C7",
                    "69336C9C-07DB-45AA-ADCF-E14A8B9F30C7",
                    "69336C41-07DB-45AA-ADCF-E14A8B9F30C7",
                    "69336C4C-27DB-45AA-ADCF-E14A8B9F30C7",
                    "69336C4C-03DB-45AA-ADCF-E14A8B9F30C7",
                    "69336C4C-074B-45AA-ADCF-E14A8B9F30C7",
                    "69336C4C-07D5-45AA-ADCF-E14A8B9F30C7",
                    "69336C4C-07DB-65AA-ADCF-E14A8B9F30C7",
                    "69336C4C-07DB-47AA-ADCF-E14A8B9F30C7",
                    "69336C4C-07DB-458A-ADCF-E14A8B9F30C7",
                    "69336C4C-07DB-45A9-ADCF-E14A8B9F30C7",
                    "69336C4C-07DB-45AA-1DCF-E14A8B9F30C7",
                    "69336C4C-07DB-45AA-A2CF-E14A8B9F30C7",
                    "69336C4C-07DB-45AA-AD3F-E14A8B9F30C7",
                    "69336C4C-07DB-45AA-ADC4-E14A8B9F30C7",
                    "69336C4C-07DB-45AA-ADCF-514A8B9F30C7",
                    "69336C4C-07DB-45AA-ADCF-E64A8B9F30C7",
                    "69336C4C-07DB-45AA-ADCF-E17A8B9F30C7",
                    "69336C4C-07DB-45AA-ADCF-E1488B9F30C7",
                };

            var firstNames = new[] {"Anthony", "Bob", "Gary", "Patrick", "Squidward", "Eugene", "Sandy", "Sheldon"};
            var lastNames = new[] {"Johnston", "Squarepants", "Starfish", "Tentacles", "Crabs", "Cheeks", "Plankton"};

            var random = new Random();
            var getRandom = (Func<string[], string>)
                            (list => list.ElementAt(random.Next(0, list.Length - 1)));

            context.People
                   .AddOrUpdate(
                       p => p.Identifier,
                       identifiers.Select(i =>
                                          new PersonData
                                              {
                                                  Identifier = Guid.Parse(i),
                                                  FirstName = getRandom(firstNames),
                                                  LastName = getRandom(lastNames)
                                              })
                                  .ToArray()
                );
        }
    }
}
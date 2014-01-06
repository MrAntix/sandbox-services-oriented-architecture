using System;
using System.Data.Entity;
using System.Linq;

using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;
using Sandbox.SOA.Services.Data.Models;

namespace Sandbox.SOA.Services.People.Addresses
{
    public class PersonAddressCreateService :
        ICommand<PersonAddressInfo, string>
    {
        readonly DataContext _dataContext;

        public PersonAddressCreateService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public string Execute(PersonAddressInfo model)
        {
            var person = _dataContext.People
                                     .Include(p => p.Addresses)
                                     .Single(p => p.Identifier == model.Person.Identifier);

            if(person.Addresses.Any(pa=>pa.Name==model.Name))
                throw new InvalidOperationException();

            var data = new PersonAddressData
                {
                    Name = model.Name
                };

            person.Addresses.Add(data);
            _dataContext.SaveChanges();

            return model.Name;
        }
    }
}
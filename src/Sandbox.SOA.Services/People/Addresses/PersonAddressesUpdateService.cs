using System.Data.Entity;
using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People.Addresses
{
    public class PersonAddressesUpdateService :
        ICommand<PersonAddresses>
    {
        readonly DataContext _dataContext;

        public PersonAddressesUpdateService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Execute(PersonAddresses model)
        {
            var data = _dataContext.People
                                   .Include(p => p.Addresses)
                                   .Single(p => p.Identifier == model.Person.Identifier);

            var i = 0;
            foreach (var addressData in data.Addresses.OrderBy(d => d.Index))
            {
                addressData.Index = model.Index[i++];
            }

            _dataContext.SaveChanges();
        }
    }
}
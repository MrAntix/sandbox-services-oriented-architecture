using System.Linq;

using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People.Addresses
{
    public class PersonAddressUpdateService :
        ICommand<PersonAddress>
    {
        readonly DataContext _dataContext;

        public PersonAddressUpdateService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Execute(PersonAddress model)
        {
            var data = (from p in _dataContext.People
                        where p.Identifier == model.Person.Identifier
                        from a in p.Addresses
                        where a.Name == model.Identifier
                              || a.Name == model.Name
                        select a).Single();

            data.Name = model.Name;

            data.Line1 = model.Line1;
            data.Line2 = model.Line2;
            data.Town = model.Town;
            data.County = model.County;
            data.Postcode = model.Postcode;
            data.Country = model.Country;

            _dataContext.SaveChanges();
        }
    }
}
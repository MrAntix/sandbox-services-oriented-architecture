using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People.Addresses
{
    public class PersonAddressGetService :
        ICommand<PersonAddressIdentifier, PersonAddress>
    {
        readonly DataContext _dataContext;

        public PersonAddressGetService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PersonAddress Execute(PersonAddressIdentifier model)
        {
            return (from p in _dataContext.People
                    where p.Identifier == model.Person.Identifier
                    from a in p.Addresses
                    where a.Name == model.Identifier
                    select new PersonAddress
                        {
                            Person = new PersonInfo
                                {
                                    Identifier = p.Identifier,
                                    Name = new PersonName
                                        {
                                            First = p.FirstName,
                                            Last = p.LastName
                                        }
                                },
                            Identifier = a.Name,
                            Name = a.Name,
                            Line1 = a.Line1,
                            Line2 = a.Line2,
                            Town = a.Town,
                            County = a.County,
                            Postcode = a.Postcode,
                            Country = a.Country
                        }).Single();
        }
    }
}
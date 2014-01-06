using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People.Addresses
{
    public class PersonAddressGetInfoService :
        ICommand<PersonAddressIdentifier, PersonAddressInfo>
    {
        readonly DataContext _dataContext;

        public PersonAddressGetInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PersonAddressInfo Execute(PersonAddressIdentifier model)
        {
            return (from p in _dataContext.People
                    where p.Identifier == model.Person.Identifier
                    from a in p.Addresses
                    where a.Name == model.Identifier
                    select new PersonAddressInfo
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
                            County = a.County,
                            Country = a.Country
                        }).Single();
        }
    }
}
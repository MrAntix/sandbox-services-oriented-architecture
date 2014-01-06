using System.Linq;

using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People.Addresses
{
    public class PersonAddressDeleteService :
        ICommand<PersonAddressDelete>
    {
        readonly DataContext _dataContext;

        public PersonAddressDeleteService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Execute(PersonAddressDelete model)
        {
            var entity = _dataContext.PersonAddresses
                                     .Single(pa =>
                                             pa.Name == model.Identifier
                                             && pa.Person.Identifier == model.Person.Identifier);

            _dataContext.PersonAddresses.Remove(entity);
            _dataContext.SaveChanges();
        }
    }
}
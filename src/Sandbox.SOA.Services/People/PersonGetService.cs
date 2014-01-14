using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People
{
    public class PersonGetService :
        ICommand<PersonIdentifier, Person>
    {
        readonly DataContext _dataContext;

        public PersonGetService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Person Execute(PersonIdentifier model)
        {
            return _dataContext
                .People
                .Where(p => p.Identifier == model.Identifier)
                .Select(p => new Person
                    {
                        Identifier = p.Identifier,
                        Name = new PersonName
                            {
                                First = p.FirstName,
                                Last = p.LastName
                            },
                        MobilePhone  = new PersonMobilePhone
                            {
                                Number = p.MobilePhone.Number
                            }
                    }).Single();
        }
    }
}
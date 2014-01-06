using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People
{
    public class PersonGetInfoService :
        ICommand<PersonIdentifier, PersonInfo>
    {
        readonly DataContext _dataContext;

        public PersonGetInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PersonInfo Execute(PersonIdentifier model)
        {
            return _dataContext
                .People
                .Where(p => p.Identifier == model.Identifier)
                .Select(p => new PersonInfo
                    {
                        Identifier = p.Identifier,
                        Name = new PersonName
                            {
                                First = p.FirstName,
                                Last = p.LastName
                            }
                    }).Single();
        }
    }
}
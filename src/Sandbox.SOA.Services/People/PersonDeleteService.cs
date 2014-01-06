using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People
{
    public class PersonDeleteService :
        ICommand<PersonDelete>
    {
        readonly DataContext _dataContext;

        public PersonDeleteService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Execute(PersonDelete model)
        {
            var entity = _dataContext.People
                                     .Single(p =>
                                             p.Identifier == model.Identifier);

            _dataContext.People.Remove(entity);
            _dataContext.SaveChanges();
        }
    }
}
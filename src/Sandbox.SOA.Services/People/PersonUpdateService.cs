using System.Data.Entity;
using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People
{
    public class PersonUpdateService :
        ICommand<Person>
    {
        readonly DataContext _dataContext;

        public PersonUpdateService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Execute(Person model)
        {
            var data = _dataContext.People
                                   .Include(p => p.Addresses)
                                   .Single(p => p.Identifier == model.Identifier);

            data.FirstName = model.Name.First;
            data.LastName = model.Name.Last;

            _dataContext.SaveChanges();
        }
    }
}
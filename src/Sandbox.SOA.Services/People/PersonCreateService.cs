using System;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;
using Sandbox.SOA.Services.Data.Models;

namespace Sandbox.SOA.Services.People
{
    public class PersonCreateService :
        ICommand<PersonInfo, PersonIdentifier>
    {
        readonly DataContext _dataContext;

        public PersonCreateService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PersonIdentifier Execute(PersonInfo model)
        {
            var data = new PersonData
                {
                    Identifier = Guid.NewGuid().ToString(),
                    FirstName = model.Name.First,
                    LastName = model.Name.Last
                };

            _dataContext.People.Add(data);
            _dataContext.SaveChanges();

            return new PersonIdentifier {Identifier = data.Identifier};
        }
    }
}
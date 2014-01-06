using System.Data.Entity;
using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People.Addresses
{
    public class PersonAddressSearchService :
        ICommand<PersonAddressSearchCriteria, PersonAddressSearchResult>
    {
        readonly DataContext _dataContext;
        readonly ICommand<PersonIdentifier, PersonInfo> _personGetInfoService;

        public PersonAddressSearchService(
            DataContext dataContext,
            ICommand<PersonIdentifier, PersonInfo> personGetInfoService)
        {
            _dataContext = dataContext;
            _personGetInfoService = personGetInfoService;
        }

        public PersonAddressSearchResult Execute(PersonAddressSearchCriteria criteria)
        {
            var query = _dataContext.PersonAddresses.AsNoTracking();

            query = query
                .Where(a =>
                       a.Person.Identifier == criteria.Person.Identifier);

            if (!string.IsNullOrEmpty(criteria.Text))
            {
                query = query
                    .Where(a =>
                           a.Name.StartsWith(criteria.Text)
                           || a.Line1.StartsWith(criteria.Text));
            }

            query = query
                .OrderBy(a => a.Index);

            var selected =
                from a in query
                select new PersonAddressInfo
                    {
                        Person = new PersonInfo
                            {
                                Identifier = a.Person.Identifier,
                                Name = new PersonName
                                    {
                                        First = a.Person.FirstName,
                                        Last = a.Person.LastName
                                    }
                            },
                        Identifier = a.Name,
                        Name = a.Name,
                        County = a.County,
                        Country = a.Country
                    };

            var person = _personGetInfoService.Execute(criteria.Person);

            return new PersonAddressSearchResult
                {
                    Person = person,
                    Criteria = criteria,
                    Items = selected
                        .Skip((criteria.Page - 1)*criteria.PageSize)
                        .Take(criteria.PageSize)
                        .ToArray()
                };
        }
    }
}
using System.Data.Entity;
using System.Linq;

using Sandbox.SOA.Common.Contracts.People;
using Sandbox.SOA.Common.Contracts.People.Addresses;
using Sandbox.SOA.Common.Services;
using Sandbox.SOA.Services.Data;

namespace Sandbox.SOA.Services.People
{
    public class PersonSearchService :
        ICommand<PersonSearchCriteria, PersonSearchResult>
    {
        readonly DataContext _dataContext;

        public PersonSearchService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PersonSearchResult Execute(
            PersonSearchCriteria criteria)
        {
            criteria = criteria ?? new PersonSearchCriteria();

            var query = _dataContext.People.AsNoTracking();

            if (!string.IsNullOrEmpty(criteria.Text))
            {
                query = query
                    .Where(p =>
                           p.FirstName.StartsWith(criteria.Text)
                           || p.LastName.StartsWith(criteria.Text));
            }

            query = query
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName);

            var selected = from p in query
                           select new PersonInfo
                               {
                                   Identifier = p.Identifier,
                                   Name = new PersonName
                                       {
                                           First = p.FirstName,
                                           Last = p.LastName,
                                       },
                                   DefaultAddress = (from a in p.Addresses
                                                     orderby a.Index
                                                     select new PersonAddressInfo
                                                         {
                                                             Name = a.Name,
                                                             County = a.County,
                                                             Country = a.Country
                                                         }).FirstOrDefault()
                               };

            return new PersonSearchResult
                {
                    Criteria = criteria,
                    Items = selected
                        .Skip((criteria.Page - 1)*criteria.PageSize)
                        .Take(criteria.PageSize)
                        .ToArray()
                };
        }
    }
}
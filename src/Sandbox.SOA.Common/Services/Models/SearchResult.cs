namespace Sandbox.SOA.Common.Services.Models
{
    public class SearchResult<T> 
    {
        public SearchCriteria Criteria { get; set; }
        public T[] Items { get; set; }
    }
}
namespace Sandbox.SOA.Common.Services.Models
{
    public class SearchCriteria
    {
        int _page;
        int _pageSize;

        public string Text { get; set; }

        public int Page
        {
            get { return _page < 1 ? 1 : _page; }
            set { _page = value; }
        }

        public int PageSize
        {
            get { return _pageSize < 10 ? 10 : _pageSize > 50 ? 50 : _pageSize; }
            set { _pageSize = value; }
        }
    }
}
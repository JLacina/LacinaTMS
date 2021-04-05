using System;

namespace LacinaTmsApi.Classes
{
    //TODD: this class will be used latter for pagination etc 
    public class QueryParameters
    {
        const int MaxSize = 100;
        private int _size = 50;
        public int Page { get; set; }

        public int Size
        {
            get { return _size; }
            set { _size = Math.Min(MaxSize, value); }
        }

        public string SortBy { get; set; } = "Id";

        private string _sortOrder = "asc";

        public string SortOrder
        {
            get { return _sortOrder; }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    _sortOrder = value;
                }
            }
        }
    }
}

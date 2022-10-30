namespace QueReal.PL.Models.Shared
{
    public abstract class PageSelectorViewModel
    {
        public int PageNumber { get; set; }

        public int TotalItemCount { get; set; }

        public int PageSize { get; set; }

        protected abstract string UrlFormat { get; }

        public int PageCount
        {
            get
            {
                var result = TotalItemCount / PageSize + 1;

                if (TotalItemCount % PageSize == 0 && TotalItemCount != 0)
                {
                    result--;
                }

                return result;
            }
        }

        public string GetPageUrl(int? pageNumber = null, int? pageSize = null) 
        {
            pageNumber ??= PageNumber;
            pageSize ??= PageSize;

            return string.Format(UrlFormat, pageNumber, pageSize);
        }
    }
}

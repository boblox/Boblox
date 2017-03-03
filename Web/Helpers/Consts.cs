namespace Web.Helpers
{
    public static class Consts
    {
        public static class NewsConfig
        {
            /// <summary>
            /// How many pages to show on the pager on the left and right side from the current page
            /// </summary>
            public const int PagerInterval = 3; 
            public const int ItemsPerPageOnOverviewPage = 5;
            public const int ItemsOnFrontPage = 3;

            public const int YearAllInt = 0;
            public const int PageAllInt = 0;

            public const int ThumbImgWidth = 400;
            public const int ThumbImgHeight = 300;
        }

        public static class GalleryConfig
        {
            /// <summary>
            /// How many pages to show on the pager on the left and right side from the current page
            /// </summary>
            public const int PagerInterval = 3;
            public const int ItemsPerPageOnOverviewPage = 12;
            public const int ItemsOnFrontPage = 12;

            public const int YearAllInt = 0;
            public const int PageAllInt = 0;
        }
    }
}
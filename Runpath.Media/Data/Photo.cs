using System;

namespace Runpath.Media.Data
{
    public class Photo
    {
        #region Properties

        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        public Uri Url { get; set; }

        public Uri ThumbnailUrl { get; set; }

        #endregion
    }
}
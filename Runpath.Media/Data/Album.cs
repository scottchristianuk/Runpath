using System.Collections.Generic;

namespace Runpath.Media.Data
{
    public class Album
    {
        #region Properties

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public IEnumerable<Photo> Photos { get; set; }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Runpath.Media.Data;

namespace Runpath.Media.Services
{
    public interface IPhotoAlbumService
    {
        Task<IEnumerable<Album>> GetAlbums(Func<Album, bool> filter = null);
    }
}
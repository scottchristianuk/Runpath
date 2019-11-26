using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Runpath.Media.Data;
using Runpath.Media.Data.Mapping;
using Runpath.Media.Net;

namespace Runpath.Media.Services
{
    public class PhotoAlbumService : IPhotoAlbumService
    {
        #region Nested Types

        internal static class PathConstants
        {
            public const string Albums = "albums";
            public const string Photos = "photos";
        }

        #endregion

        #region Members

        private readonly IApiClient _apiClient;

        #endregion

        #region Ctor.

        public PhotoAlbumService(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        #endregion

        public async Task<IEnumerable<Album>> GetAlbums(Func<Album, bool> filter = null)
        {
            var albumsAndPhotos = await FetchAlbumsAndPhotos();
            var photoAlbums     = albumsAndPhotos.ToAlbums();

            if (filter != null)
            {
                photoAlbums = photoAlbums.Where(filter);
            }

            return photoAlbums;
        }

        #region Helpers

        private async Task<(IReadOnlyCollection<AlbumDto> albums, IReadOnlyCollection<PhotoDto> photos)>
            FetchAlbumsAndPhotos()
        {
            var albumTask = _apiClient.Get<IReadOnlyCollection<AlbumDto>>(PathConstants.Albums);
            var photoTask = _apiClient.Get<IReadOnlyCollection<PhotoDto>>(PathConstants.Photos);

            await Task.WhenAll(albumTask, photoTask);

            return (albumTask.Result, photoTask.Result);
        }

        #endregion
    }
}
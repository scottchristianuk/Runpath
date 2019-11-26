using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;

namespace Runpath.Media.Data
{
    public static class DtoBuilder
    {
        /// <summary>
        ///     Creates a single photo DTO
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static PhotoDto CreatePhoto(Action<IOperable<PhotoDto>> configure = null)
        {
            return CreatePhotos(1, configure).Single();
        }

        /// <summary>
        ///     Creates a single album DTO
        /// </summary>
        /// <returns></returns>
        public static AlbumDto CreateAlbum()
        {
            return CreateAlbums(1).Single();
        }

        /// <summary>
        ///     Creates <paramref name="numberOfItems"/> number of photos
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IList<PhotoDto> CreatePhotos(int numberOfItems = 10, Action<IOperable<PhotoDto>> configure = null)
        {
            return Create<PhotoDto>(numberOfItems,
                                    c =>
                                    {
                                        c.With(x => x.Url          = "http://a-valid-url.com/")
                                         .With(x => x.ThumbnailUrl = "http://a-valid-thumbnail-url.com/");

                                        configure?.Invoke(c);
                                    });
        }

        /// <summary>
        ///     Creates <paramref name="numberOfItems"/> of albums
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        public static IList<AlbumDto> CreateAlbums(int numberOfItems = 10)
        {
            return Create<AlbumDto>(numberOfItems);
        }

        /// <summary>
        ///     Creates a list of album DTOs and photo DTOs such that there are <paramref name="numberOfAlbums"/> and for each album
        ///     there is <paramref name="numberOfPhotos"/> linked by the AlbumId on the photo.
        /// </summary>
        /// <param name="numberOfAlbums">The total number of albums to generate</param>
        /// <param name="numberOfPhotos">The number of photos for each album to generate</param>
        /// <returns></returns>
        public static (IList<AlbumDto>, IList<PhotoDto>) CreateAlbumsAndPhotos(
            int numberOfAlbums = 10,
            int numberOfPhotos = 10)
        {
            var albums = CreateAlbums(numberOfAlbums);
            var photos = albums
                        .SelectMany(a => CreatePhotos(numberOfPhotos,
                                                      configure => configure.With(x => x.AlbumId = a.Id)))
                        .ToList();

            return (albums, photos);
        }

        #region Helpers

        private static IList<T> Create<T>(int numberOfItems, Action<IOperable<T>> configure = null)
        {
            if (numberOfItems == 0)
            {
                return new List<T>();
            }

            var listBuilder = Builder<T>.CreateListOfSize(numberOfItems);
            configure?.Invoke(listBuilder.All());

            return listBuilder.Build();
        }

        #endregion
    }
}
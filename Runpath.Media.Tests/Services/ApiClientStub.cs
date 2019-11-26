using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Runpath.Media.Data;
using Runpath.Media.Net;

namespace Runpath.Media.Services
{
    public static class ApiClientStub
    {
        /// <summary>
        ///     Instantiates and configures an <see cref="IApiClient" /> stub for unit testing
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApiClient Create(Action<Mock<IApiClient>> configure)
        {
            var apiClientStub = new Mock<IApiClient>();
            configure?.Invoke(apiClientStub);

            return apiClientStub.Object;
        }

        /// <summary>
        ///     Stubs the api client to return <paramref name="albums" /> when called for the album path.
        /// </summary>
        /// <param name="mock"></param>
        /// <param name="albums">The album DTOs to return</param>
        /// <returns></returns>
        public static Mock<IApiClient> ReturnAlbums(this Mock<IApiClient> mock,
            IList<AlbumDto> albums)
        {
            mock.Setup(x => x.Get<IReadOnlyCollection<AlbumDto>>(PhotoAlbumService.PathConstants.Albums))
                .ReturnsAsync(albums.ToList().AsReadOnly());

            return mock;
        }

        /// <summary>
        ///     Stubs the api client to return <paramref name="photos" /> when called fo the photos path.
        /// </summary>
        /// <param name="mock"></param>
        /// <param name="photos">The photo DTOs to return</param>
        /// <returns></returns>
        public static Mock<IApiClient> ReturnPhotos(this Mock<IApiClient> mock,
            IList<PhotoDto> photos)
        {
            mock.Setup(x => x.Get<IReadOnlyCollection<PhotoDto>>(PhotoAlbumService.PathConstants.Photos))
                .ReturnsAsync(photos.ToList().AsReadOnly());

            return mock;
        }
    }
}
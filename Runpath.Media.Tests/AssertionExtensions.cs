using System.Collections.Generic;
using System.Linq;
using Runpath.Media.Data;
using Shouldly;

namespace Runpath.Media
{
    public static class AssertionExtensions
    {
        /// <summary>
        ///     Asserts that <paramref name="albumsAndPhotos" /> have been correctly grouped to form the <paramref name="result" />
        /// </summary>
        /// <param name="result"></param>
        /// <param name="albumsAndPhotos"></param>
        public static void ShouldBeGroupedCorrectly(this IEnumerable<Album> result,
            (IEnumerable<AlbumDto>, IEnumerable<PhotoDto>) albumsAndPhotos)
        {
            var (albums, photos) = albumsAndPhotos;

            result.ShouldMatchAlbumCount(albums);

            foreach (var album in result)
            {
                album.ShouldMatchPhotoCount(photos);
            }
        }

        /// <summary>
        ///     Asserts that a <paramref name="photo" /> is equal to a <paramref name="photoDto" />
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="photoDto"></param>
        public static void ShouldMatch(this Photo photo, PhotoDto photoDto)
        {
            photo.Id.ShouldBe(photoDto.Id);
            photo.AlbumId.ShouldBe(photoDto.AlbumId);
            photo.Title.ShouldBe(photoDto.Title);
            photo.ThumbnailUrl.AbsoluteUri.ShouldBe(photoDto.ThumbnailUrl);
            photo.Url.AbsoluteUri.ShouldBe(photoDto.Url);
        }

        /// <summary>
        ///     Confirms an <paramref name="album" /> is equal to a <paramref name="albumDto" />
        /// </summary>
        /// <param name="album"></param>
        /// <param name="albumDto"></param>
        public static void ShouldMatch(this Album album, AlbumDto albumDto)
        {
            album.Id.ShouldBe(albumDto.Id);
            album.UserId.ShouldBe(albumDto.UserId);
            album.Title.ShouldBe(albumDto.Title);
        }

        #region Helpers

        /// <summary>
        ///     Confirms that the <paramref name="result" /> contains and equal number of items as <paramref name="albums" />
        /// </summary>
        /// <param name="result"></param>
        /// <param name="albums"></param>
        private static void ShouldMatchAlbumCount(this IEnumerable<Album> result,
            IEnumerable<AlbumDto> albums)
        {
            result.Count().ShouldBe(albums.Count());
        }

        /// <summary>
        ///     Confirms that the <paramref name="album" /> contains the correct number of photos as matched in
        ///     <paramref name="photos" />
        /// </summary>
        /// <param name="album"></param>
        /// <param name="photos"></param>
        private static void ShouldMatchPhotoCount(this Album album,
            IEnumerable<PhotoDto> photos)
        {
            var photoCount = photos.Count(p => p.AlbumId == album.Id);
            album.Photos.Count().ShouldBe(photoCount);
        }

        #endregion
    }
}
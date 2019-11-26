using System;
using System.Linq;
using FizzWare.NBuilder;
using Shouldly;
using Xunit;

namespace Runpath.Media.Data.Mapping
{
    public class DtoMappingExtensionsTests
    {
        private static Action<IOperable<PhotoDto>> WithUpperMaxPlusOneAlbumId(int maxId)
        {
            return photo => { photo.With(p => p.AlbumId = maxId + 1); };
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(5, 10)]
        public void ToAlbums_WhereEachAlbumHasSetNumberOfPhotos_ShouldSucceedAndCompileAlbums(
            int numberOfAlbums,
            int numberOfPhotos)
        {
            // ARRANGE
            var albumsAndPhotos = DtoBuilder.CreateAlbumsAndPhotos(numberOfAlbums, numberOfPhotos);

            // ACT
            var result = albumsAndPhotos.ToAlbums();

            // ASSERT
            result.ShouldBeGroupedCorrectly(albumsAndPhotos);
        }

        [Fact]
        public void ToAlbum_FromAlbumDtoWithEmptyPhotoSet_ShouldSucceedAndMapToAlbumWithNoPhotos()
        {
            // ARRANGE
            var dto    = DtoBuilder.CreateAlbum();
            var photos = DtoBuilder.CreatePhotos(0);

            // ACT
            var album = dto.ToAlbum(photos);

            // ASSERT
            album.ShouldMatch(dto);
        }

        [Fact]
        public void ToAlbums_FromAlbumDtoWithEmptyPhotoSet_ShouldSucceedAndMapToAlbumWithNoPhotos()
        {
            // ARRANGE
            var albums = DtoBuilder.CreateAlbums(0);
            var photos = DtoBuilder.CreatePhotos();

            // ACT
            var result = (albums, photos).ToAlbums();

            // ASSERT
            result.ShouldBeEmpty();
        }

        [Fact]
        public void ToAlbums_WhereNoAlbumsOrPhotosExist_ShouldSucceedAndReturnEmptySet()
        {
            // ARRANGE
            var albums = DtoBuilder.CreateAlbums(0);
            var photos = DtoBuilder.CreatePhotos(0);

            // ACT
            var result = (albums, photos).ToAlbums();

            // ASSERT
            result.ShouldBeEmpty();
        }

        [Fact]
        public void ToAlbums_WhereNoPhotoExistsForAlbum_ShouldSucceedAndCompileAlbumsWithNoPhotos()
        {
            // ARRANGE
            var albums = DtoBuilder.CreateAlbums(1);
            var photos = DtoBuilder.CreatePhotos(1, WithUpperMaxPlusOneAlbumId(albums.Max(a => a.Id)));

            // ACT
            var result = (albums, photos).ToAlbums()
                                         .Single();

            // ASSERT
            result.Photos.ShouldBeEmpty();
        }

        [Fact]
        public void ToPhoto_FromPhotoDto_ShouldSucceedAndMapToPhoto()
        {
            // ARRANGE
            var dto = DtoBuilder.CreatePhoto();

            // ACT
            var photo = dto.ToPhoto();

            // ASSERT
            photo.ShouldMatch(dto);
        }
    }
}
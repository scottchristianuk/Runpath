using System;
using System.Linq;
using Runpath.Media.Data;
using Shouldly;
using Xunit;

namespace Runpath.Media.Services
{
    public class PhotoAlbumServiceTests
    {
        [Fact]
        public void Ctor_WhereApiClientIsNull_ShouldFailAndThrowException()
        {
            // ACT / ASSERT
            Assert.Throws<ArgumentNullException>(
                () => new PhotoAlbumService(null));
        }

        [Fact]
        public async void GetAlbums_WhereAlbumsAndPhotosReturnedByClient_ShouldReturnCompiledAlbums()
        {
            // ARRANGE
            var (albums, photos) = DtoBuilder.CreateAlbumsAndPhotos();
            var apiStub = ApiClientStub.Create(m =>
            {
                m.ReturnAlbums(albums)
                 .ReturnPhotos(photos);
            });
            var sut = new PhotoAlbumService(apiStub);

            // ACT
            var result = await sut.GetAlbums();

            // ASSERT
            result.Count().ShouldBe(albums.Count);
        }

        [Fact]
        public async void GetAlbums_WhereFilteredByUserId_ShouldReturnSingleAlbum()
        {
            // ARRANGE
            var (albums, photos) = DtoBuilder.CreateAlbumsAndPhotos();
            var apiStub = ApiClientStub.Create(m =>
            {
                m.ReturnAlbums(albums)
                 .ReturnPhotos(photos);
            });
            var sut = new PhotoAlbumService(apiStub);

            // ACT
            var result = await sut.GetAlbums(a => a.UserId == albums.First().UserId);

            // ASSERT
            result.Count().ShouldBe(1);
        }
    }
}
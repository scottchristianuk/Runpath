using System;
using System.Collections.Generic;
using System.Linq;

namespace Runpath.Media.Data.Mapping
{
    public static class DtoMappingExtensions
    {
        /// <summary>
        ///     Reconciles album and photo DTOs and compiles a list of albums
        /// </summary>
        /// <param name="albumsAndPhotos">The album and photo sets</param>
        /// <returns>A compiled list of albums</returns>
        public static IEnumerable<Album> ToAlbums(
            this (IEnumerable<AlbumDto> albums, IEnumerable<PhotoDto> photos) albumsAndPhotos)
        {
            var (albums, photos) = albumsAndPhotos;

            var photoAlbums = from a in albums
                              join p in photos.DefaultIfEmpty()
                                  on a.Id equals p.AlbumId into pa
                              select ToAlbum(a, pa);

            return photoAlbums;
        }

        internal static Album ToAlbum(this AlbumDto albumDto, IEnumerable<PhotoDto> photos)
        {
            return new Album
            {
                Id     = albumDto.Id, 
                UserId = albumDto.UserId, 
                Title  = albumDto.Title, 
                Photos = photos.Select(ToPhoto)
            };
        }

        internal static Photo ToPhoto(this PhotoDto photoDto)
        {
            return new Photo
            {
                Id           = photoDto.Id,
                AlbumId      = photoDto.AlbumId,
                Title        = photoDto.Title,
                ThumbnailUrl = new Uri(photoDto.ThumbnailUrl),
                Url          = new Uri(photoDto.Url)
            };
        }
    }
}
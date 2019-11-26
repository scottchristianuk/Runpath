using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Runpath.Media.Data;
using Runpath.Media.Services;

namespace Runpath.Media.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        #region Members

        private readonly IPhotoAlbumService _photoAlbumService;

        #endregion

        #region Ctor.

        public AlbumsController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService ?? throw new ArgumentNullException(nameof(photoAlbumService));
        }

        #endregion

        [HttpGet]
        public async Task<IEnumerable<Album>> Get(int? userId)
        {
            if (userId.HasValue)
            {
                return await _photoAlbumService.GetAlbums(a => a.UserId == userId.Value);
            }

            return await _photoAlbumService.GetAlbums();
        }
    }
}
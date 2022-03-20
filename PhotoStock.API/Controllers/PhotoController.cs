using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.API.Dtos;
using Shared.ControllerBases;
using Shared.Dtos;

namespace PhotoStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(List<IFormFile> photo, CancellationToken cancellationToken)
        {
            if (photo.Count <= 0) return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
            
            var photoDto = new List<PhotoDto>();

            foreach (var formFile in photo)
            {
                if (formFile is not { Length: > 0 })
                {
                    return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", formFile.FileName);
                await using var stream = new FileStream(path, FileMode.Create);
                await formFile.CopyToAsync(stream, cancellationToken);

                //http://www.photostock.api.com/photos/aasdasd.jpg
                var returnPath = "photos/" + formFile.FileName;

                photoDto.Add(new PhotoDto()
                {
                    Url = returnPath
                });
            }
            return CreateActionResultInstance(Response<List<PhotoDto>>.Success(photoDto, 200));

        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

            if (!System.IO.File.Exists(path))
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found", 404));

            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}

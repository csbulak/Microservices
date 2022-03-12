using Catalog.API.Dtos;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _courseService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return CreateActionResultInstance(await _courseService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            return CreateActionResultInstance(await _courseService.GetAllByUserId(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            return CreateActionResultInstance(await _courseService.CreateAsync(courseCreateDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            return CreateActionResultInstance(await _courseService.UpdateAsync(courseUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return CreateActionResultInstance(await _courseService.DeleteAsync(id));
        }
    }
}

using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Settings;
using MongoDB.Driver;
using Shared.Dtos;

namespace Catalog.API.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;

    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);

        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(course => true).ToListAsync();

        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }

        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
        try
        {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }

            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }
        catch (Exception e)
        {
            return Response<CourseDto>.Fail("Course not found", 404);
        }
    }

    public async Task<Response<List<CourseDto>>> GetAllByUserId(string userId)
    {
        var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }
        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);

    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
    {
        var newCourse = _mapper.Map<Course>(courseCreateDto);
        newCourse.CreatedTime = DateTime.Now;

        await _courseCollection.InsertOneAsync(newCourse);

        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
    }

    public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
    {
        try
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found.", 404);
            }

            return Response<NoContent>.Success(204);
        }
        catch (Exception e)
        {
            return Response<NoContent>.Fail("Course not found.", 404);
        }
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        try
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
        }
        catch (Exception e)
        {
            return Response<NoContent>.Fail("Course not found", 404);
        }
    }
}
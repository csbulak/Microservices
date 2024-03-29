﻿using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Settings;
using MongoDB.Driver;
using Shared.Dtos;

namespace Catalog.API.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);

        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(category => true).ToListAsync();

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryDto)
    {
        var createCategory = _mapper.Map<Category>(categoryDto);

        await _categoryCollection.InsertOneAsync(createCategory);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(createCategory), 200);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        try
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found.", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        catch (Exception exception)
        {
            return Response<CategoryDto>.Fail("Category not found.", 404);
        }
    }
}
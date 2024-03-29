﻿using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Catalog;

public class CourseCreateInput
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public string Description { get; set; }

    public string Picture { get; set; }

    public string CategoryId { get; set; }

    public FeatureViewModel Feature { get; set; }

    public IFormFile PhotoFormFile { get; set; }
}
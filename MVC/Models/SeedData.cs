using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC.Data;
using System;
using System.Linq;

namespace MVC.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MVCContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MVCContext>>()))
        {
            // Look for any movies.
            if (context.ExampleModel.Any())
            {
                return;   // DB has been seeded
            }
            context.ExampleModel.AddRange(
                new ExampleModel
                {
                    Name = "Name1",
                    Description = "Name1 Description",
                    Date = DateTime.Parse("1997-5-11"),
                },
                new ExampleModel
                {
                    Name = "Name2",
                    Description = "Name2 Description",
                    Date = DateTime.Parse("1989-2-12"),
                },
                new ExampleModel
                {
                    Name = "Name3",
                    Description = "Name3 Description",
                    Date = DateTime.Parse("2012-6-12"),
                },
                new ExampleModel
                {
                    Name = "Name4",
                    Description = "Name4 Description",
                    Date = DateTime.Parse("2024-7-11"),
                }
            );
            context.SaveChanges();
        }
    }
}
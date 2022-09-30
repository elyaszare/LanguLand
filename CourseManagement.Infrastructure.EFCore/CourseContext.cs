﻿using CourseManagement.Domain.CourseAgg;
using CourseManagement.Domain.CourseCategoryAgg;
using CourseManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.EFCore
{
    public class CourseContext : DbContext
    {
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Course> Courses { get; set; }

        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CourseMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            //modelBuilder.Entity<ArticleCategory>().ToTable("ArticleCategories");
            base.OnModelCreating(modelBuilder);
        }
    }
}

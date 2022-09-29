using CourseManagement.Domain.CourseAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.EFCore.Mapping
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(120);
            builder.Property(x => x.ShortDescription).HasMaxLength(150);
            builder.Property(x => x.Slug).IsRequired();
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Keywords).IsRequired().HasMaxLength(120);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.PageTitle).IsRequired().HasMaxLength(60);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(150);

            builder
                .HasOne(x => x.CourseCategory)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CategoryId);

            builder
                .HasOne(x => x.Teacher)
                .WithMany(x => x.TeacherCourses)
                .HasForeignKey(x => x.TeacherId);

            builder
                .HasMany(x => x.Students)
                .WithMany(x => x.StudentCourses);
        }
    }
}

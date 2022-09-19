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
            builder.Property(x => x.ShortDescription).HasMaxLength(500);
            builder.Property(x => x.Slug).IsRequired();
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Keywords).IsRequired().HasMaxLength(120);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Price).IsRequired();

            builder
                .HasOne(x => x.CourseCategory)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}

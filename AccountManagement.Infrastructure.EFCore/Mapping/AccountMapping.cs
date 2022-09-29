using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Mobile).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(100);

            builder
                .HasMany(x => x.Articles)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);

            builder
                .HasMany(x => x.TeacherCourses)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId);

            builder
                .HasMany(x => x.StudentCourses)
                .WithMany(x => x.Students);
        }
    }
}

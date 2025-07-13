using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Article");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Content)
                .IsRequired();

            builder.Property(a => a.Author)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Status)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.CategoryId)
                .IsRequired();
            
        }
    }
}
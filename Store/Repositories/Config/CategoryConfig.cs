using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);      // PK
            builder.Property(c => c.CategoryName).IsRequired();  //zorunlu alan
            
            builder.HasData(
                new Category() {CategoryId = 1, CategoryName="Book"},
                new Category() {CategoryId = 2, CategoryName="Elektronik"}
            );
        }
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class SenseConfiguration : IEntityTypeConfiguration<Sense>
{
    public void Configure(EntityTypeBuilder<Sense> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.HasMany(s => s.lsource)
            .WithOne(l => l.Sense)
            .HasForeignKey(l => l.SenseId)
            .IsRequired();
    }
}

public class LSourceConfiguration : IEntityTypeConfiguration<LSource>
{
    public void Configure(EntityTypeBuilder<LSource> builder)
    {
        builder.HasKey(k => k.Id);
    }
}
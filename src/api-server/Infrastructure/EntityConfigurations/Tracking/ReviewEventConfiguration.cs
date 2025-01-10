using Domain.Entities.Tracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Tracking;

public class ReviewEventConfiguration: IEntityTypeConfiguration<ReviewEvent>
{
    public void Configure(EntityTypeBuilder<ReviewEvent> builder)
    {
        builder.HasKey(re => new { re.ent_seq, UserId = re.UserId, re.Serial });

        builder.Property(re => re.Serial)
            .ValueGeneratedOnAdd();

        builder.HasOne(re => re.TrackedEntry)
            .WithMany(te => te.ReviewEvents)
            .HasForeignKey(re => new { re.ent_seq, TagId = re.UserId })
            .IsRequired();
        
        builder.Property(re => re.Created)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()");
    }
}
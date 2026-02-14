using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestFlow.Domain.Entities;

namespace RequestFlow.Persistence.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("Requests");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RequestNo)
            .IsRequired()
            .HasMaxLength(20);
        builder.HasIndex(r => r.RequestNo).IsUnique();

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .HasMaxLength(2000);

        builder.Property(r => r.CreatedByUserId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(r => r.Priority)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(r => r.RequestType)
            .WithMany(rt => rt.Requests)
            .HasForeignKey(r => r.RequestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.StatusHistory)
            .WithOne(h => h.Request)
            .HasForeignKey(h => h.RequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

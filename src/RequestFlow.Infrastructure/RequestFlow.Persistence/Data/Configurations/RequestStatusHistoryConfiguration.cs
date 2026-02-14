using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestFlow.Domain.Entities;

namespace RequestFlow.Persistence.Configurations;

public class RequestStatusHistoryConfiguration : IEntityTypeConfiguration<RequestStatusHistory>
{
    public void Configure(EntityTypeBuilder<RequestStatusHistory> builder)
    {
        builder.ToTable("RequestStatusHistories");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Comment)
            .HasMaxLength(500);

        builder.Property(h => h.ChangedByUserId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.FromStatus)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(h => h.ToStatus)
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}

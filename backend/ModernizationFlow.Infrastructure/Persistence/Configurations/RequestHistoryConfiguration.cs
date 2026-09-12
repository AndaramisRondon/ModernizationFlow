using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernizationFlow.Domain.Entities;

namespace ModernizationFlow.Infrastructure.Persistence.Configurations;

public sealed class RequestHistoryConfiguration
    : IEntityTypeConfiguration<RequestHistory>
{
    public void Configure(
        EntityTypeBuilder<RequestHistory> builder)
    {
        builder.ToTable("RequestHistories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RequestId)
            .IsRequired();

        builder.Property(x => x.FromStatus)
            .IsRequired();

        builder.Property(x => x.ToStatus)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}


﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Core.Entities;

namespace SetStats.Data.Configurations;

public class TrainingCycleConfiguration : IEntityTypeConfiguration<TrainingCycle>
{
    public void Configure(EntityTypeBuilder<TrainingCycle> builder)
    {
        _ = builder.ToTable("training_cycles");

        _ = builder.HasIndex(tc => new { tc.UserId, tc.CycleNumber })
            .IsUnique();

        _ = builder.Property(tc => tc.RoundingFactor)
            .HasPrecision(4, 2);
    }
}

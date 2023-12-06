﻿using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SiyinPractice.Infrastructure.DataStore.Maintenance.EntityConfigurations
{
    public class LoggerLogConfig : AbstractEntityTypeConfiguration<LoggerLog>
    {
        public override void Configure(EntityTypeBuilder<LoggerLog> builder)
        {
            base.Configure(builder);

            //builder.Property(x => x.Name).IsRequired().HasMaxLength(CfgConsts.Name_MaxLength);
            //builder.Property(x => x.Value).IsRequired().HasMaxLength(CfgConsts.Value_MaxLength);
            //builder.Property(x => x.Description).HasMaxLength(CfgConsts.Description_MaxLength);
        }
    }
}
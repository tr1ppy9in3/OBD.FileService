﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using OBD.FileService.Users.Core;

namespace OBD.FileService.DataAccess.Users.Auth.Cfg;

/// <summary>
/// Конфигурация для модели RegularUser.
/// </summary>
internal class RegularUserCfg : IEntityTypeConfiguration<RegularUser>
{
    public void Configure(EntityTypeBuilder<RegularUser> builder)
    {

        builder.Property(ru => ru.Name)
               .IsRequired()
               .HasMaxLength(32);

        builder.Property(ru => ru.Surname)
               .IsRequired()
               .HasMaxLength(32);

        builder.Property(ru => ru.ProfilePic).HasColumnType("bytea");
    }
}

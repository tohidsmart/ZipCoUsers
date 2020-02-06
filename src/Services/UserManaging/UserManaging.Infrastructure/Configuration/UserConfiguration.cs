using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UserManaging.Domain;

namespace UserManaging.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(o => o.UserId);

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.Property<string>("FirstName")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string>("LastName")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<string>("Email")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<decimal>("Salary")
                .HasColumnType("money")
                .IsRequired();

            builder.Property<decimal>("Expenses")
                .HasColumnType("money")
                .IsRequired();

            builder.HasIndex("Email").IsUnique();

            builder.Property<DateTime>("CreatedAt")
                .IsRequired();

            builder.HasOne(user => user.Account).WithOne(account => account.User)
                .HasForeignKey<Account>(account => account.UserId);
        }
    }
}

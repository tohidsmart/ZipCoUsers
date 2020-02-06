using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UserManaging.Domain;

namespace UserManaging.Infrastructure.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            
            builder.HasKey(acc => acc.AccountId);

            builder.Property<Guid>("AccountId")
                .IsRequired();

            builder.Property<decimal>("Balance")
                .HasColumnType("money")
                .IsRequired();

            builder.Property<DateTime>("CreatedAt")
                .IsRequired();

            builder.Property(account => account.Type).
                HasConversion(v => v.ToString(), v =>
                (AccountType)Enum.Parse(typeof(AccountType), v));

        }
    }
}

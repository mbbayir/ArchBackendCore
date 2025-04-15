using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchBackend.Repository.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {

            var Admin = new Users
            {
                FirstName = "Arif",
                LastName = "Turhal",
                UserName = "info@arifturhalmimarlik.com",
                Email = "info@arifturhalmimarlik.com",
                NormalizedEmail = "INFO@ARIFTURHALMIMARLIK.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEEryn1LCktchcqJXTV/1ItRsYyWYQARN+pJaCf4QabD2FiV2BO1fhzynoEP+NqYttQ==",
                SecurityStamp= "FWP32LOOGHI37OPDB734EIYSTDARKAYD",
                ConcurrencyStamp= "30f7972c-33e2-4cea-99b1-f47714e6b862",
                Id = 1,
            };
            builder.HasData(Admin);

        }
    }
}

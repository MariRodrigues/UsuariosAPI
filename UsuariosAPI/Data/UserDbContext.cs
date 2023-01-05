using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data
{
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {

        }

        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Endereco>()
                .HasOne(endereco => endereco.Usuario)
                .WithOne(usuario => usuario.Endereco)
                .HasForeignKey<CustomIdentityUser>(endereco => endereco.EnderecoId);


            modelBuilder.Entity<CustomIdentityUser>()
                .HasIndex(b => b.CPF)
                .IsUnique();
            modelBuilder.Entity<CustomIdentityUser>()
                .HasIndex(b => b.Email)
                .IsUnique();

            CustomIdentityUser admin = new CustomIdentityUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 99999,
                CPF = "123456789",
                DataCriacao = DateTime.Now,
                DataNascimento = DateTime.Now,
                Status = true,
                EnderecoId = 1
            };

            PasswordHasher<CustomIdentityUser> passwordHasher = new PasswordHasher<CustomIdentityUser>();

            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");

            modelBuilder.Entity<CustomIdentityUser>().HasData(admin);

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 99999,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>
            {
                Id = 99998,
                Name = "regular",
                NormalizedName = "REGULAR"
            });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 99999,
                UserId = 99999
            });
        }
    }
}

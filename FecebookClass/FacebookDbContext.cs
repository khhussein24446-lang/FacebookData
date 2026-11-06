using FecebookClass.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FecebookClass
{
    public class FacebookDbContext : DbContext
    {
        private string _connectionString = "Server=haytham-hussein\\SQLEXPRESS01;Database=FaceBook;Integrated Security=True;Encrypt=False;";

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var item in entries)
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreatedAt = DateTime.Now;
                }
                else if (item.State == EntityState.Modified) 
                { 
                   item.Entity.UpatedAt= DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }
        public DbSet<comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Like>  Likes { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Like>().HasOne(i=>i.post).WithMany(p=>p.likes).
                HasForeignKey(p=>p.PostId).OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(n=>n.name).IsUnique();
        }
    }
}

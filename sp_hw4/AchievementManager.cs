using sp_hw4.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace sp_hw4
{
    public partial class AchievementManager : DbContext
    {
        public AchievementManager()
            : base("name=AchievementManager")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Achievements)
                .WithRequired()
                .WillCascadeOnDelete(true);
        }
    }
}

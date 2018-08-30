namespace ProjectContentManager.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContentManagerModelContext : DbContext
    {
        public ContentManagerModelContext()
            : base("name=ContentManagerModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ContentType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ContentType>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}

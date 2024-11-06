using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoApp.Models;



namespace TodoApp.AppDataContext
{
    public class TodoDbContext : DbContext
    {
        static readonly string connectionString = "Server=localhost; User ID=emmanuel; Password=EmmanuelMuuo3!@#$; Database=todo";
        // private readonly DbSettings _dbsettings;

        // public TodoDbContext(IOptions<DbSettings> dbsettings)
        // {
        //     _dbsettings = dbsettings.Value;
        // }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("TodoApp");

                // Configure Id as CHAR(36) for MySQL to store UUID
                entity.Property(e => e.Id)
                    .HasColumnType("char(36)")
                    .IsRequired();

                entity.HasKey(e => e.Id);
            });
        }

        // Optional: If you need further model configuration
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Todo>()
        //                 .ToTable("TodoApp")
        //                 .HasBaseType("char(30)")
        //                 .HasKey(x => x.Id);
        // }
    //     private readonly DbSettings _dbsettings;

    //        // Constructor to inject the DbSettings model
    //     public TodoDbContext(IOptions<DbSettings> dbsettings)
    //     {
    //         _dbsettings = dbsettings.Value;
    //     }

    //     public DbSet<Todo> Todos{ get; set; }

    //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     {
    //             optionsBuilder.UseNpgsql(_dbsettings.ConnectionString);
    //     }

    //     // Configuring the model for the Todo entity
    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //         modelBuilder.Entity<Todo>()
    //                     .ToTable("TodoApp")
    //                     .HasKey(x => x.Id);
    //     }
    }
}
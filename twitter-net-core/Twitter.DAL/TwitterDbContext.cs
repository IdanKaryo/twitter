using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Twitter.DAL.Entities;

namespace Twitter.DAL
{
    public class TwitterDbContext: DbContext
    {
        private readonly string _connectionString;

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        public TwitterDbContext(string connectionString)
        {
            _connectionString = connectionString;

            Initialize();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cs = new MySqlConnection(_connectionString);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(cs, ServerVersion.AutoDetect(cs));
            }            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapEntitiesToTables(modelBuilder);

            ConfigurePrimaryKeys(modelBuilder);

            ConfigureIndexes(modelBuilder);

            ConfigureUserColumns(modelBuilder);

            ConfigureMessageColumns(modelBuilder);

            ConfigureRelationship(modelBuilder);
        }

        private static void ConfigureRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageEntity>().HasOne<UserEntity>().WithMany().HasPrincipalKey(message => message.Id)
                .HasForeignKey(message => message.UserId).OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Message_User");
        }

        private static void ConfigureMessageColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageEntity>().Property(message => message.Id).HasColumnType("int").UseMySqlIdentityColumn()
                .IsRequired();
            modelBuilder.Entity<MessageEntity>().Property(message => message.Message).HasColumnType("nvarchar(1000)")
                .IsRequired();
            modelBuilder.Entity<MessageEntity>().Property(message => message.InsertionTime).HasColumnType("datetime")
                .IsRequired();
            modelBuilder.Entity<MessageEntity>().Property(message => message.UserId).HasColumnType("int").IsRequired();
        }

        private static void ConfigureUserColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().Property(user => user.Id).HasColumnType("int").UseMySqlIdentityColumn()
                .IsRequired();
            modelBuilder.Entity<UserEntity>().Property(user => user.Name).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<UserEntity>().Property(user => user.Password).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<UserEntity>().Property(user => user.Salt).HasColumnType("nvarchar(100)").IsRequired();
        }

        private static void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasIndex(user => user.Id).IsUnique().HasDatabaseName("Idx_Id");
            modelBuilder.Entity<UserEntity>().HasIndex(user => user.Name).IsUnique().HasDatabaseName("Idx_Name");
            modelBuilder.Entity<MessageEntity>().HasIndex(message => message.UserId).HasDatabaseName("Idx_UserId");
        }

        private static void ConfigurePrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(user => user.Id).HasName("PK_User");
            modelBuilder.Entity<MessageEntity>().HasKey(message => message.Id).HasName("PK_Message");
        }

        private static void MapEntitiesToTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<MessageEntity>().ToTable("Messages");
        }

        private void Initialize()
        {
            Database.EnsureCreated();
        }
    }
}

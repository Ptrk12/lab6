using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class QuizDbContext : IdentityDbContext<UserEntity, UserRole, int>
{
    public DbSet<QuizEntity> Quizzes { get; set; }
    public DbSet<QuizItemEntity> QuizItems { get; set; }
    public DbSet<QuizItemUserAnswerEntity> UserAnswers { get; set; }
    public DbSet<QuizItemAnswerEntity> IncorrectAnswers { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "Data Source=BR-11\\SQLEXPRESS;Initial Catalog=lab777;Integrated Security=True;Pooling=False;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<QuizItemUserAnswerEntity>()
            .HasOne(e => e.QuizItem);

        modelBuilder.Entity<QuizItemAnswerEntity>()
            .HasData(
                new QuizItemAnswerEntity() { Id = 1, Answer = "1" },
                new QuizItemAnswerEntity() { Id = 2, Answer = "2" },
                new QuizItemAnswerEntity() { Id = 3, Answer = "3" },
                new QuizItemAnswerEntity() { Id = 4, Answer = "4" },
                new QuizItemAnswerEntity() { Id = 5, Answer = "5" },
                new QuizItemAnswerEntity() { Id = 6, Answer = "6" },
                new QuizItemAnswerEntity() { Id = 7, Answer = "7" },
                new QuizItemAnswerEntity() { Id = 8, Answer = "8" },
                new QuizItemAnswerEntity() { Id = 9, Answer = "9" },
                new QuizItemAnswerEntity() { Id = 10, Answer = "0" }
            );

        //modelBuilder.Entity<UserEntity>()
        //    .HasKey(x => x.UserId);

        modelBuilder.Entity<QuizItemUserAnswerEntity>()
            .HasOne<QuizEntity>()
            .WithMany()
            .HasForeignKey(x => x.QuizId);


        modelBuilder.Entity<QuizItemUserAnswerEntity>()
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        //modelBuilder.Entity<UserEntity>()
        //    .HasData(
        //        new UserEntity()
        //        {
        //            UserId = 1,
        //            Email = "testemail@gmail.com",
        //            Password = "123"
        //        });

        modelBuilder.Entity<QuizItemEntity>()
            .HasData(
                new QuizItemEntity()
                {
                    Id = 1,
                    Question = "2 + 3",
                    CorrectAnswser = "5"
                },
                new QuizItemEntity()
                {
                    Id = 2,
                    Question = "2 * 3",
                    CorrectAnswser = "6"
                },
                new QuizItemEntity()
                {
                    Id = 3,
                    Question = "8 - 3",
                    CorrectAnswser = "5"
                },
                new QuizItemEntity()
                {
                    Id = 4,
                    Question = "8 : 2",
                    CorrectAnswser = "4"
                }
            );

        modelBuilder.Entity<QuizEntity>()
            .HasData(
                new QuizEntity()
                {
                    Title = "Matematyka",
                    Id = 1
                },
                new QuizEntity()
                {
                    Title = "Arytmetyka",
                    Id = 2
                }
            );

        modelBuilder.Entity<QuizEntity>()
            .HasMany<QuizItemEntity>(q => q.Items)
            .WithMany(e => e.Quizes)
            .UsingEntity(
                join => join.HasData(
                    new { QuizesId = 1, ItemsId = 1 },
                    new { QuizesId = 1, ItemsId = 2 },
                    new { QuizesId = 1, ItemsId = 3 }
                )
            );

        modelBuilder.Entity<QuizItemEntity>()
            .HasMany<QuizItemAnswerEntity>(q => q.IncorrectAnswer)
            .WithMany(e => e.QuizItems)
            .UsingEntity(join => join.HasData(
                    // "2 + 3"
                    new { QuizItemsId = 1, IncorrectAnswerId = 1 },
                    new { QuizItemsId = 1, IncorrectAnswerId = 2 },
                    new { QuizItemsId = 1, IncorrectAnswerId = 3 },
                    // "2 * 3"
                    new { QuizItemsId = 2, IncorrectAnswerId = 3 },
                    new { QuizItemsId = 2, IncorrectAnswerId = 4 },
                    new { QuizItemsId = 2, IncorrectAnswerId = 7 },
                    // "8 - 3"
                    new { QuizItemsId = 3, IncorrectAnswerId = 1 },
                    new { QuizItemsId = 3, IncorrectAnswerId = 3 },
                    new { QuizItemsId = 3, IncorrectAnswerId = 9 },
                    // "8 : 2"
                    new { QuizItemsId = 4, IncorrectAnswerId = 2 },
                    new { QuizItemsId = 4, IncorrectAnswerId = 6 },
                    new { QuizItemsId = 4, IncorrectAnswerId = 8 }
                )
            );
    }
}
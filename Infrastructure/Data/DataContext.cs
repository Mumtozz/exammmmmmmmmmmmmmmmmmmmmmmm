using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options){}

    public DbSet<Course> Courses { get; set; } 
    public DbSet<Assignment> Assignments { get; set; } 
    public DbSet<Student> Students { get; set; } 
    public DbSet<Feedback> Feedbacks { get; set; } 
    public DbSet<Material> Materials { get; set; }
    public DbSet<Submission> Submissions { get; set; }    
    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     builder.Entity<Course>()
    //         .HasMany(x => x.Groups)
    //         .WithOne(x => x.Course)
    //         .HasForeignKey(x => x.CourseId)
    //         .OnDelete(DeleteBehavior.Cascade);
        
    //     base.OnModelCreating(builder);
    // }
}



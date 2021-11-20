//namespace BCX_ISAAC_BA_SOL.Models
//{
//    using System;
//    using System.Data.Entity;
//    using System.ComponentModel.DataAnnotations.Schema;
//    using System.Linq;

//    public partial class MyModel : DbContext
//    {
//        public MyModel()
//            : base("name=MyModel")
//        {
//        }

//        public virtual DbSet<Answer> Answers { get; set; }
//        public virtual DbSet<Job> Jobs { get; set; }
//        public virtual DbSet<JobApplication> JobApplications { get; set; }
//        public virtual DbSet<Question> Questions { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Job>()
//                .HasMany(e => e.JobApplications)
//                .WithOptional(e => e.Job)
//                .WillCascadeOnDelete();

//            modelBuilder.Entity<Job>()
//                .HasMany(e => e.Questions)
//                .WithOptional(e => e.Job)
//                .WillCascadeOnDelete();

//            modelBuilder.Entity<Question>()
//                .HasMany(e => e.Answers)
//                .WithOptional(e => e.Question)
//                .WillCascadeOnDelete();
//        }
//    }
//}

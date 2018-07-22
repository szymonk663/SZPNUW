using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SZPNUW.DBService.Model
{
    public partial class SZPNUWContext : DbContext
    {
        public virtual DbSet<Lecturers> Lecturers { get; set; }
        public virtual DbSet<Meetings> Meetings { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<Semesters> Semesters { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Studentsemester> Studentsemester { get; set; }
        public virtual DbSet<Studentssections> Studentssections { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Subjectssemesters> Subjectssemesters { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Syslog> Syslogs { get; set; }
        public static string ConnectionString { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecturers>(entity =>
            {
                entity.ToTable("lecturers");

                entity.HasIndex(e => e.Userid)
                    .HasName("ux_lecturers")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Lecturers)
                    .HasForeignKey<Lecturers>(d => d.Userid)
                    .HasConstraintName("fk_users");
            });

            modelBuilder.Entity<Meetings>(entity =>
            {
                entity.ToTable("meetings");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateofentry).HasColumnName("dateofentry");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Studentssectionsid).HasColumnName("studentssectionsid");

                entity.HasOne(d => d.Studentssections)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.Studentssectionsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_studentssections");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.ToTable("projects");

                entity.HasIndex(e => new { e.Lecturerid, e.Subjectid })
                    .HasName("ux_projects")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Lecturerid).HasColumnName("lecturerid");

                entity.Property(e => e.Subjectid).HasColumnName("subjectid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasColumnName("topic");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.Lecturerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lecturers");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.Subjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subjects");
            });

            modelBuilder.Entity<Reports>(entity =>
            {
                entity.ToTable("reports");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('raports_id_seq'::regclass)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("filename");

                entity.Property(e => e.Sectionid).HasColumnName("sectionid");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sections");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.ToTable("sections");

                entity.HasIndex(e => new { e.Subcjetsemesterid, e.Projectid })
                    .HasName("ux_sections")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Sectionnumber).HasColumnName("sectionnumber");

                entity.Property(e => e.Subcjetsemesterid).HasColumnName("subcjetsemesterid");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Projectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_projects");

                entity.HasOne(d => d.Subcjetsemester)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.Subcjetsemesterid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subjectssemesters");
            });

            modelBuilder.Entity<Semesters>(entity =>
            {
                entity.ToTable("semesters");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Academicyear)
                    .IsRequired()
                    .HasColumnName("academicyear");

                entity.Property(e => e.Fieldofstudy)
                    .IsRequired()
                    .HasColumnName("fieldofstudy");

                entity.Property(e => e.Semesternumber).HasColumnName("semesternumber");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.ToTable("students");

                entity.HasIndex(e => e.Userid)
                    .HasName("ux_students")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Albumnumber)
                    .IsRequired()
                    .HasColumnName("albumnumber");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Students)
                    .HasForeignKey<Students>(d => d.Userid)
                    .HasConstraintName("fk_users");
            });

            modelBuilder.Entity<Studentsemester>(entity =>
            {
                entity.HasKey(e => new { e.Studentid, e.Semesterid });

                entity.ToTable("studentsemester");

                entity.Property(e => e.Studentid).HasColumnName("studentid");

                entity.Property(e => e.Semesterid).HasColumnName("semesterid");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Studentsemester)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentsemester)
                    .HasForeignKey(d => d.Studentid)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Studentssections>(entity =>
            {
                entity.ToTable("studentssections");

                entity.HasIndex(e => new { e.Studentid, e.Sectionid })
                    .HasName("ux_studentssections")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateofentry).HasColumnName("dateofentry");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Sectionid).HasColumnName("sectionid");

                entity.Property(e => e.Studentid).HasColumnName("studentid");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Studentssections)
                    .HasForeignKey(d => d.Sectionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Studentssections)
                    .HasForeignKey(d => d.Studentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_students");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.ToTable("subjects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Leaderid).HasColumnName("leaderid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.Leader)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.Leaderid)
                    .HasConstraintName("fk_lecturers");
            });

            modelBuilder.Entity<Subjectssemesters>(entity =>
            {
                entity.ToTable("subjectssemesters");

                entity.HasIndex(e => new { e.Subjectid, e.Semesterid })
                    .HasName("ux_subjectssemesters")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Semesterid).HasColumnName("semesterid");

                entity.Property(e => e.Subjectid).HasColumnName("subjectid");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Subjectssemesters)
                    .HasForeignKey(d => d.Semesterid)
                    .HasConstraintName("fk_semesters");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Subjectssemesters)
                    .HasForeignKey(d => d.Subjectid)
                    .HasConstraintName("fk_subjects");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Pesel).HasColumnName("pesel");

                entity.Property(e => e.Usertype).HasColumnName("usertype");
            });

            modelBuilder.HasSequence("raports_id_seq");

            modelBuilder.Entity<Syslog>(entity =>
            {
                entity.ToTable("syslog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Details).HasColumnName("details");

                entity.Property(e => e.Name).IsRequired().HasColumnName("name");

                entity.Property(e => e.Date).IsRequired().HasColumnName("date");

            });
        }
    }
}

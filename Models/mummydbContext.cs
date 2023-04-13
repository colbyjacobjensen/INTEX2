using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INTEX2.Models
{
    public partial class mummydbContext : DbContext
    {
        public mummydbContext()
        {
        }

        public mummydbContext(DbContextOptions<mummydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MummyData> MummyData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=burialdb.c3jrpmjwmkyz.us-east-1.rds.amazonaws.com; Port=5432; Database=burialdb; Username=postgres; Password=intex2023;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MummyData>(entity =>
            {
                entity.ToTable("mummy");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AgeAtDeath)
                    .HasMaxLength(200)
                    .HasColumnName("age_at_death");

                entity.Property(e => e.BurialNumber)
                    .HasMaxLength(200)
                    .HasColumnName("burial_number");

                entity.Property(e => e.ColorValue)
                    .HasMaxLength(500)
                    .HasColumnName("color_value");

                entity.Property(e => e.FieldNotes)
                    .HasMaxLength(2000)
                    .HasColumnName("field_notes");

                entity.Property(e => e.HairColor)
                    .HasMaxLength(200)
                    .HasColumnName("hair_color");

                entity.Property(e => e.HeadDirection)
                    .HasMaxLength(200)
                    .HasColumnName("head_direction");

                entity.Property(e => e.Length)
                    .HasMaxLength(200)
                    .HasColumnName("length");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Photo)
                    .HasMaxLength(500)
                    .HasColumnName("photo");

                entity.Property(e => e.Sex)
                    .HasMaxLength(200)
                    .HasColumnName("sex");

                entity.Property(e => e.StructureValue)
                    .HasMaxLength(500)
                    .HasColumnName("structure_value");

                entity.Property(e => e.TextileValue)
                    .HasMaxLength(200)
                    .HasColumnName("textile_value");
            });

            modelBuilder.HasSequence("excelimporter$template_nr_mxseq");

            modelBuilder.HasSequence("system$filedocument_fileid_mxseq");

            modelBuilder.HasSequence("system$queuedtask_sequence_mxseq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
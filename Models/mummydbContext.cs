using System;
using System.Collections.Generic;
using INTEX2.Models;
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
                optionsBuilder.UseNpgsql("Server=mummydb.c3jrpmjwmkyz.us-east-1.rds.amazonaws.com; Port=5432; Database=mummydb; Username=postgres; Password=intex2023;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MummyData>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Data");

                entity.Property(e => e.Adultsubadult)
                    .HasMaxLength(200)
                    .HasColumnName("adultsubadult");

                entity.Property(e => e.Ageatdeath)
                    .HasMaxLength(200)
                    .HasColumnName("ageatdeath");

                entity.Property(e => e.Area)
                    .HasMaxLength(200)
                    .HasColumnName("area");

                entity.Property(e => e.Burialid).HasColumnName("burialid");

                entity.Property(e => e.Burialmaterials)
                    .HasMaxLength(5)
                    .HasColumnName("burialmaterials");

                entity.Property(e => e.Burialnumber)
                    .HasMaxLength(200)
                    .HasColumnName("burialnumber");

                entity.Property(e => e.CariesPeriodontalDisease)
                    .HasMaxLength(255)
                    .HasColumnName("Caries_Periodontal_Disease");

                entity.Property(e => e.Clusternumber)
                    .HasMaxLength(200)
                    .HasColumnName("clusternumber");

                entity.Property(e => e.Dataexpertinitials)
                    .HasMaxLength(200)
                    .HasColumnName("dataexpertinitials");

                entity.Property(e => e.DateofExamination).HasMaxLength(255);

                entity.Property(e => e.Dateofexcavation)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dateofexcavation");

                entity.Property(e => e.Depth)
                    .HasMaxLength(200)
                    .HasColumnName("depth");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Direction)
                    .HasMaxLength(200)
                    .HasColumnName("direction");

                entity.Property(e => e.DorsalPittingBoolean)
                    .HasMaxLength(255)
                    .HasColumnName("DorsalPitting (boolean)");

                entity.Property(e => e.Eastwest)
                    .HasMaxLength(200)
                    .HasColumnName("eastwest");

                entity.Property(e => e.Estimatedperiod)
                    .HasMaxLength(200)
                    .HasColumnName("estimatedperiod");

                entity.Property(e => e.Excavationrecorder)
                    .HasMaxLength(100)
                    .HasColumnName("excavationrecorder");

                entity.Property(e => e.Facebundles)
                    .HasMaxLength(200)
                    .HasColumnName("facebundles");

                entity.Property(e => e.Femur).HasMaxLength(255);

                entity.Property(e => e.FemurHeadDiameter).HasMaxLength(255);

                entity.Property(e => e.Fieldbookexcavationyear)
                    .HasMaxLength(200)
                    .HasColumnName("fieldbookexcavationyear");

                entity.Property(e => e.Fieldbookpage)
                    .HasMaxLength(200)
                    .HasColumnName("fieldbookpage");

                entity.Property(e => e.Gonion).HasMaxLength(255);

                entity.Property(e => e.Goods)
                    .HasMaxLength(200)
                    .HasColumnName("goods");

                entity.Property(e => e.Hair)
                    .HasMaxLength(5)
                    .HasColumnName("hair");

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(200)
                    .HasColumnName("haircolor");

                entity.Property(e => e.Headdirection)
                    .HasMaxLength(200)
                    .HasColumnName("headdirection");

                entity.Property(e => e.Humerus).HasMaxLength(255);

                entity.Property(e => e.HumerusHeadDiameter).HasMaxLength(255);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LamboidSuture).HasMaxLength(255);

                entity.Property(e => e.LeftRightBurialNumber)
                    .HasMaxLength(255)
                    .HasColumnName("Left_Right_BurialNumber");

                entity.Property(e => e.LeftRightId).HasColumnName("Left_Right_id");

                entity.Property(e => e.Length)
                    .HasMaxLength(200)
                    .HasColumnName("length");

                entity.Property(e => e.Locale)
                    .HasMaxLength(200)
                    .HasColumnName("locale");

                entity.Property(e => e.MainBurialmainid).HasColumnName("main$burialmainid");

                entity.Property(e => e.MedialIpRamus)
                    .HasMaxLength(255)
                    .HasColumnName("Medial_IP_Ramus");

                entity.Property(e => e.Northsouth)
                    .HasMaxLength(200)
                    .HasColumnName("northsouth");

                entity.Property(e => e.Notes).HasMaxLength(859);

                entity.Property(e => e.NuchalCrest).HasMaxLength(255);

                entity.Property(e => e.Observations).HasMaxLength(830);

                entity.Property(e => e.OrbitEdge).HasMaxLength(255);

                entity.Property(e => e.Osteophytosis).HasMaxLength(255);

                entity.Property(e => e.ParietalBossing).HasMaxLength(255);

                entity.Property(e => e.Photographeddate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("photographeddate");

                entity.Property(e => e.Photos)
                    .HasMaxLength(5)
                    .HasColumnName("photos");

                entity.Property(e => e.PreauricularSulcusBoolean)
                    .HasMaxLength(255)
                    .HasColumnName("PreauricularSulcus (Boolean)");

                entity.Property(e => e.Preservation)
                    .HasMaxLength(200)
                    .HasColumnName("preservation");

                entity.Property(e => e.PubicBone).HasMaxLength(255);

                entity.Property(e => e.RightArea)
                    .HasMaxLength(255)
                    .HasColumnName("Right_Area");

                entity.Property(e => e.RightBurialnumber)
                    .HasMaxLength(255)
                    .HasColumnName("Right_burialnumber");

                entity.Property(e => e.RightEastWest)
                    .HasMaxLength(255)
                    .HasColumnName("Right_EastWest");

                entity.Property(e => e.RightHairColor)
                    .HasMaxLength(255)
                    .HasColumnName("Right_HairColor");

                entity.Property(e => e.RightId).HasColumnName("Right_id");

                entity.Property(e => e.RightNorthSouth)
                    .HasMaxLength(255)
                    .HasColumnName("Right_NorthSouth");

                entity.Property(e => e.RightSquareEastWest).HasColumnName("Right_SquareEastWest");

                entity.Property(e => e.RightSquareNorthSouth).HasColumnName("Right_SquareNorthSouth");

                entity.Property(e => e.Robust).HasMaxLength(255);

                entity.Property(e => e.Sampledate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("sampledate");

                entity.Property(e => e.Samplescollected)
                    .HasMaxLength(200)
                    .HasColumnName("samplescollected");

                entity.Property(e => e.SciaticNotch).HasMaxLength(255);

                entity.Property(e => e.Sex)
                    .HasMaxLength(200)
                    .HasColumnName("sex");

                entity.Property(e => e.Shaftnumber)
                    .HasMaxLength(200)
                    .HasColumnName("shaftnumber");

                entity.Property(e => e.Southtofeet)
                    .HasMaxLength(200)
                    .HasColumnName("southtofeet");

                entity.Property(e => e.Southtohead)
                    .HasMaxLength(200)
                    .HasColumnName("southtohead");

                entity.Property(e => e.SphenooccipitalSynchrondrosis).HasMaxLength(255);

                entity.Property(e => e.SquamosSuture).HasMaxLength(255);

                entity.Property(e => e.Squareeastwest)
                    .HasMaxLength(200)
                    .HasColumnName("squareeastwest");

                entity.Property(e => e.Squarenorthsouth)
                    .HasMaxLength(200)
                    .HasColumnName("squarenorthsouth");

                entity.Property(e => e.SubpubicAngle).HasMaxLength(255);

                entity.Property(e => e.SupraorbitalRidges).HasMaxLength(255);

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.TextileFunction).HasMaxLength(200);

                entity.Property(e => e.ToothAttrition).HasMaxLength(255);

                entity.Property(e => e.ToothEruption).HasMaxLength(255);

                entity.Property(e => e.ToothEruptionAgeEstimate).HasMaxLength(255);

                entity.Property(e => e.VentralArc).HasMaxLength(255);

                entity.Property(e => e.Westtofeet)
                    .HasMaxLength(200)
                    .HasColumnName("westtofeet");

                entity.Property(e => e.Westtohead)
                    .HasMaxLength(200)
                    .HasColumnName("westtohead");

                entity.Property(e => e.Wrapping)
                    .HasMaxLength(200)
                    .HasColumnName("wrapping");

                entity.Property(e => e.ZygomaticCrest).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
using System;
using System.Collections.Generic;

namespace INTEX2.Models
{
    public partial class MummyData
    {
        public long Id { get; set; }
        public string? Location { get; set; }
        public string? HeadDirection { get; set; }
        public string? Sex { get; set; }
        public string? HairColor { get; set; }
        public string? BurialNumber { get; set; }
        public string? AgeAtDeath { get; set; }
        public string? StructureValue { get; set; }
        public string? ColorValue { get; set; }
        public string? TextileValue { get; set; }
        public string? FieldNotes { get; set; }
        public string? Length { get; set; }
        public string? Photo { get; set; }
    }
}
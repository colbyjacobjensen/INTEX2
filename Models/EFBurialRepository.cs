using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INTEX2.Models;

namespace INTEX2.Models
{
    public class EFBurialRepository : IBurialRepository
    {
        private BuffaloDbContext context { get; set; }

        public EFBurialRepository(BuffaloDbContext temp)
        {
            context = temp;
        }
        public IQueryable<Burialmain> Burials => context.Burialmain;
    }
}
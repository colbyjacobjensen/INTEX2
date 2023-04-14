using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INTEX2.Models;

namespace INTEX2.Models
{
    public class EFBurialRepository : IBurialRepository
    {
        private mummydbContext context { get; set; }

        public EFBurialRepository(mummydbContext temp)
        {
            context = temp;
        }
        public IQueryable<MummyData> Burials => context.MummyData;
    }
}
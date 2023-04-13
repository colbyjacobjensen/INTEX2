using System;
namespace INTEX2.Models.ViewModels

{
	public class PageInfo
	{
        public int TotalBurials { get; set; }
        public int BurialsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalBurials / BurialsPerPage);
	}
}
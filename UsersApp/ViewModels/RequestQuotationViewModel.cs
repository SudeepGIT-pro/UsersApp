using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    public class RequestQuotationViewModel
    {
        [Required(ErrorMessage = "Client Name is required")]
        [Display(Name = "Client Name")]
        public string? ClientName { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Type of Model is required")]
        [Display(Name = "Type of Model")]
        public string? TypeOfModel { get; set; }

        [Required(ErrorMessage = "Number of Units is required")]
        [Display(Name = "Number of Units")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Units must be at least 1")]
        public int NumberOfUnits { get; set; } = 1;

        [Display(Name = "Foundation Type")]
        public string? FoundationType { get; set; }

        [Display(Name = "Area (sq.m)")]
        public decimal? FoundationArea { get; set; }

        [Display(Name = "Amount")]
        public decimal? FoundationAmount { get; set; }

        [Display(Name = "Super Structure Type")]
        public string? SuperStructureType { get; set; }

        [Display(Name = "Wall Types")]
        public List<string>? WallTypes { get; set; } = new List<string>();

        [Display(Name = "Perimeters (m)")]
        public List<decimal>? Perimeters { get; set; } = new List<decimal>();
    }
}

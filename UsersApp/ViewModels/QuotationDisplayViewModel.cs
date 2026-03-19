using System.Collections.Generic;

namespace UsersApp.ViewModels
{
    public class QuotationDisplayViewModel
    {
        public string ClientName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string TypeOfModel { get; set; } = string.Empty;
        public int NumberOfUnits { get; set; } = 1;
        public List<QuotationItem> MainItems { get; set; } = new();
        public List<QuotationItem> OptionalItems { get; set; } = new();
        public string TotalInWords { get; set; } = string.Empty;
        
        public List<string> Exclusions { get; set; } = new()
        {
            "Transportation & Cranage at Actuals.",
            "Installer's Accomodation.",
            "Water Storage Tank & Waste Water Tank ( Can be provided at additional cost)"
        };

        public List<string> PaymentTerms { get; set; } = new()
        {
            "80% Advance payment along with purchase order.",
            "Balance 20% upon handing over",
            "Goods and Services Tax (GST) at a rate of 18% is applicable and will be charged in addition to the product cost."
        };

        public List<string> ClientResponsibilities { get; set; } = new()
        {
            "Soil Test",
            "Levelled land, Unlimited access to the site, Electricity & Water to be provided upto the unit.",
            "Completion of RR or RCC foundation before installation if foundation in client scope."
        };

        public string Warranty { get; set; } = "A one-year warranty will be provided, effective from the date of handing over.";
        public string ProductionSchedule { get; set; } = "The production time is estimated to be 30 days.";
        public string Installation { get; set; } = "30 days after readiness of the foundation.";

        public string CompanyName { get; set; } = "SMARDHOMES";
        public string SignatoryName { get; set; } = "Anil Rajan";
        public string SignatoryTitle { get; set; } = "Authorized Signatory";
        public string ContactNumber { get; set; } = "+91 90610 18880 ";

        public string BankAccountName { get; set; } = "SMARDCON INFRA PVT LTD";
        public string BankAccountNumber { get; set; } = "262805000529";
        public string BankName { get; set; } = "ICICI BANK";
        public string IfscCode { get; set; } = "ICIC0002628";
    }

    public class QuotationItem
    {
        public int SerialNo { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public string Rate { get; set; } = string.Empty;
    }
}

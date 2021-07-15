using System;

namespace ConsoleApp
{
    public class DataOutput
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }

        public DataOutput()
        {
            Successful = true;
            Message = string.Empty;
            Data = new Data();
        }
    }

    public class Data
    {
        public DateTime AdjudicationDate { get; set; }
        public string ClaimNumber { get; set; }
        public decimal CoveredTotalAmount { get; set; }
        public int CoveredPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal InsuranceResponsibilityAmount { get; set; }
        public decimal PatientResponsibilityAmount { get; set; }
    }
}
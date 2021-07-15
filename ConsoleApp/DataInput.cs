using System;

namespace ConsoleApp
{
    public class DataInput
    {
        public string[] Genders { get; set; }
        public string[] DiagnosisCodes { get; set; }
        public string[] PlanCodes { get; set; }
        public Claim Claim { get; set; }
    }

    public class Claim
    {
        public string Gender { get; set; }
        public DateTime DateOfService { get; set; }
        public string PlanCode { get; set; }
        public string DiagnosisCode { get; set; }
        public bool IsPregnant { get; set; }
        public bool IsSmoker { get; set; }
    }
}
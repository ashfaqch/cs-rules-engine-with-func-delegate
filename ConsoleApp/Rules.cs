using System;

namespace ConsoleApp
{
    public static class Rules
    {
        public static Tuple<bool, string> Exit(string message)
        {
            return new Tuple<bool, string>(false, message);
        }

        public static Tuple<bool, string> Next()
        {
            return new Tuple<bool, string>(true, string.Empty);
        }

        public static Tuple<bool, string> ValidateDateOfService(DateTime dateOfService)
        {
            if (dateOfService > DateTime.Today)
            {
                return Exit("Invalid Date Of Service");
            }
            return Next();
        }

        public static Tuple<bool, string> ValidateDiagnosisCode(string[] diagnosisCodes, Claim claim)
        {
            if (Array.IndexOf(diagnosisCodes, claim.DiagnosisCode) == -1)
            {
                return Exit("Invalid or Missing Diagnosis Code");
            }
            return Next();
        }

        public static Tuple<bool, string> ValidateGender(string[] genders, Claim claim)
        {
            if (Array.IndexOf(genders, claim.Gender) == -1)
            {
                return Exit("Invalid Gender");
            }

            if (claim.Gender.ToUpper() == "Male" && claim.IsPregnant)
            {
                return Exit("Patient's gender does not match the type of service");
            }
            return Next();
        }

        public static Tuple<bool, string> ApplySmokerDiscount(Claim claim, DataOutput output)
        {
            if (!claim.IsSmoker)
            {
                output.Data.DiscountAmount = 5;
            }
            return Next();
        }

        public static Tuple<bool, string> CalculateCoverage(Claim claim, DataOutput output)
        {
            switch (claim.PlanCode)
            {
                case "PC1":
                    if (claim.DiagnosisCode == "DC1")
                    {
                        output.Data.CoveredTotalAmount = 100;
                        output.Data.CoveredPercentage = 25;
                    }
                    else if (claim.DiagnosisCode == "DC2")
                    {
                        output.Data.CoveredTotalAmount = 100;
                        output.Data.CoveredPercentage = 20;
                    }
                    else if (claim.DiagnosisCode == "DC3")
                    {
                        output.Data.CoveredTotalAmount = 100;
                        output.Data.CoveredPercentage = 15;
                    }
                    else
                    {
                        output.Data.CoveredTotalAmount = 0;
                        output.Data.CoveredPercentage = 0;
                    }
                    break;

                case "PC2":
                    if (claim.DiagnosisCode == "DC1")
                    {
                        output.Data.CoveredTotalAmount = 100;
                        output.Data.CoveredPercentage = 50;
                    }
                    else if (claim.DiagnosisCode == "DC2")
                    {
                        output.Data.CoveredTotalAmount = 100;
                        output.Data.CoveredPercentage = 45;
                    }
                    else if (claim.DiagnosisCode == "DC3")
                    {
                        output.Data.CoveredTotalAmount = 100;
                        output.Data.CoveredPercentage = 40;
                    }
                    else
                    {
                        output.Data.CoveredTotalAmount = 0;
                        output.Data.CoveredPercentage = 0;
                    }
                    break;

                default:
                    return Exit("Invalid or Missing Plan Code");
            }

            return Next();
        }

        public static Tuple<bool, string> CalculateClaim(DataOutput output)
        {
            output.Data.AdjudicationDate = DateTime.Now;
            output.Data.ClaimNumber = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();

            output.Data.InsuranceResponsibilityAmount = Math.Round(100 * output.Data.CoveredPercentage / output.Data.CoveredTotalAmount);
            output.Data.PatientResponsibilityAmount = Math.Round(100 * (100 - output.Data.CoveredPercentage) / output.Data.CoveredTotalAmount);

            if (output.Data.PatientResponsibilityAmount > 0)
            {
                output.Data.PatientResponsibilityAmount = output.Data.PatientResponsibilityAmount - output.Data.DiscountAmount;
            }

            return Next();
        }
    }
}
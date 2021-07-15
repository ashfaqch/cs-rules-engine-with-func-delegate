using System;

namespace ConsoleApp
{
    public class RuleEngine
    {
        public DataOutput output;

        public RuleEngine()
        {
            output = new DataOutput();
        }

        public DataOutput Process(DataInput dataInput)
        {
            // list of rules
            Func<DataInput, Tuple<bool, string>>[] rules =
            {
                (input) => input.Claim == null ? Rules.Exit("Claim can not be null.") : Rules.Next(),
                (input) => Rules.ValidateDateOfService(input.Claim.DateOfService),
                (input) => Rules.ValidateDiagnosisCode(input.DiagnosisCodes, input.Claim),
                (input) => Rules.ValidateGender(input.Genders, input.Claim),
                (input) => input.Claim.IsSmoker ? Rules.Next() : Rules.ApplySmokerDiscount(input.Claim, output),
                (input) => Rules.CalculateCoverage(input.Claim, output),
                (input) => Rules.CalculateClaim(output),
            };

            // apply rules
            foreach (var rule in rules)
            {
                var result = rule(dataInput);
                if (!result.Item1)
                {
                    output.Successful = false;
                    output.Message = result.Item2;
                    break;
                }
            }

            // return output
            return output;
        }
    }
}
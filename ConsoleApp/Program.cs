using System;
using System.Reflection;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // input data
            var input = new DataInput
            {
                Genders = new string[] { "Female", "Male" },
                DiagnosisCodes = new string[] { "DC1", "DC2", "DC3" },
                PlanCodes = new string[] { "PC1", "PC2" },
                Claim = new Claim
                {
                    Gender = "Female",
                    DateOfService = DateTime.Today.AddDays(-5),
                    DiagnosisCode = "DC3",
                    PlanCode = "PC2",
                    IsPregnant = true,
                    IsSmoker = false
                }
            };

            // process data through rules driven engine
            var output = new RuleEngine().Process(input);

            // display output
            Console.WriteLine($"Claim Adjudicated Successful: {output.Successful}{(output.Successful ? string.Empty : ", Message: " + output.Message)}");
            if (output.Successful)
            {
                Console.WriteLine();
                foreach (PropertyInfo prop in output.Data.GetType().GetProperties())
                {
                    Console.WriteLine($"{prop.Name}: {prop.GetValue(output.Data, null)}");
                }
            }
            Console.ReadLine();
        }
    }
}
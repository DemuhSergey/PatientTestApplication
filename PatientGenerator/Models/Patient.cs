using Bogus;

namespace PatientGenerator.Models
{
    internal class Patient
    {
        public string? Use { get; set; }
        public string Family { get; set; }
        public string[] Given { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }

        internal static Faker<Patient> FakeData
        {
            get => new Faker<Patient>()
                .RuleFor(p => p.Use, f => f.Name.JobTitle())
                .RuleFor(p => p.Family, f => f.Name.LastName())
                .RuleFor(p => p.Given, f => new string[] { f.Name.FirstName(), f.Name.LastName() })
                .RuleFor(p => p.BirthDate, f => f.Date.Past(18))
                .RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
        }
    }
}

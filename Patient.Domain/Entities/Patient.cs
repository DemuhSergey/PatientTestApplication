using Patient.Domain.Abstractions.Interfaces;

namespace Patient.Domain.Entities
{
    public class Patient : BaseEntity, IDate
    {
        public string? Use { get; set; }
        public string Family { get; set; }
        public string[] Given { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

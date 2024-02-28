using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Patient.Application.Common.Mappers;

namespace Patient.Application.Logic.Patient.Commands.CreatePatient
{
    public class CreatePatientCommand : IRequest<Guid>, IMapTo<Domain.Entities.Patient>
    {
        [JsonIgnore]
        public Guid Id
        {
            get; set;
        }

        [JsonProperty("use")]
        public string Use
        {
            get; set;
        }

        [JsonProperty("family")]
        public string Family
        {
            get; set;
        }

        [JsonProperty("given")]
        public string[] Given
        {
            get; set;
        }

        [JsonProperty("gender")]
        public Domain.Enums.GenderEnum Gender
        {
            get; set;
        }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePatientCommand, Domain.Entities.Patient>()
                .ForMember(d => d.Use, opt => opt.MapFrom(s => s.Use))
                .ForMember(d => d.Family, opt => opt.MapFrom(s => s.Family))
                .ForMember(d => d.Given, opt => opt.MapFrom(s => s.Given))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate));

        }
    }
}

using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Patient.Application.Common.Mappers;

namespace Patient.Application.Logic.Patient.Models
{

    public class PatientNameInfo : IMapFrom<Domain.Entities.Patient>
    {
        [JsonProperty("id")]
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
    }

    public class PatientInfo : IRequest<Guid>, IMapFrom<Domain.Entities.Patient>
    {
        [JsonProperty("name")]
        public PatientNameInfo Name
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

        [JsonProperty("active")]
        public bool Active { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Patient, PatientNameInfo>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Use, opt => opt.MapFrom(s => s.Use))
                .ForMember(d => d.Family, opt => opt.MapFrom(s => s.Family))
                .ForMember(d => d.Given, opt => opt.MapFrom(s => s.Given));

            profile.CreateMap<Domain.Entities.Patient, PatientInfo>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate))
                .ForMember(d => d.Active, opt => opt.MapFrom(s => s.HasActive));
        }
    }
}

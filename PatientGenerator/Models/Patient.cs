﻿using Bogus;
using System.Text.Json.Serialization;

namespace PatientGenerator.Models
{
    internal class Patient
    {
        [JsonPropertyName("use")]
        public string? Use { get; set; }

        [JsonPropertyName("family")]
        public string Family { get; set; }

        [JsonPropertyName("given")]
        public string[] Given { get; set; }

        [JsonPropertyName("gender")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }


        internal static Faker<Patient> FakeData
        {
            get => new Faker<Patient>()
                .RuleFor(p => p.Use, f => f.Name.JobTitle())
                .RuleFor(p => p.Family, f => f.Name.LastName())
                .RuleFor(p => p.Given, f => new string[] { f.Name.FirstName(), f.Name.LastName() })
                .RuleFor(p => p.BirthDate, f => {
                    var pastDateTime = f.Date.PastOffset(70, DateTime.Now.AddYears(-18));
                    var result = DateTime.Parse(pastDateTime.ToString("yyyy-MM-ddTHH:mm:ss"));
                    return result;
                })
                .RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
        }
    }
}

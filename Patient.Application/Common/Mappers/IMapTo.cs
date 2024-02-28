using AutoMapper;

namespace Patient.Application.Common.Mappers
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(this.GetType(), typeof(T));
    }
}

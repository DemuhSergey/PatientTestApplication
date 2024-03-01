namespace PatientGenerator.Services.Abstractions
{
    internal interface IRestfullService<T>
    {
        Task<Guid> Post(string route, T data);
    }
}

namespace PatientGenerator.Services.Abstractions
{
    internal interface IRestfullService<T>
    {
        Task<IEnumerable<T>> GetAll(string route);

        Task<Guid> Post(string route, T data);
    }
}

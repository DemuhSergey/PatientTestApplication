using Microsoft.EntityFrameworkCore;

namespace Patient.Domain.Abstractions.Interfaces
{
    public interface IPatientDbContext
    {
        DbSet<Patient.Domain.Entities.Patient> Patients
        {
            get;
        }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

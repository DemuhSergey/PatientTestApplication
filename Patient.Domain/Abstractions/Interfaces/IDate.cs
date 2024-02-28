using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Domain.Abstractions.Interfaces
{
    public interface IDate
    {
        DateTime BirthDate { get; set; }
    }
}

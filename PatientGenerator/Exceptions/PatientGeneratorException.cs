using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientGenerator.Exceptions
{

    public class PatientGeneratorException : Exception
    {
        public PatientGeneratorException(Exception ex)
            : base("Entity was not created" + ex)
        {

        }
    }
}

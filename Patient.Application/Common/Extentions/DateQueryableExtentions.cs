using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Application.Common.Extentions
{
    public static class DateQueryableExtentions
    {
        public static IQueryable<T> SearchByDate<T>(this IQueryable<T> query, IEnumerable<string> date)
            where T : Domain.Abstractions.Interfaces.IDate
        {
            if (!date.Any()) { 
                return query;
            }


            return query;
        }
    }
}

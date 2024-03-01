using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Application.Common.Extentions
{
    public static class DateQueryableExtentions
    {
        //private static readonly IDictionary<string, Func<DateTime, DateTime, bool>> pairs = 
        //    new Dictionary<string, Func<DateTime, DateTime, bool>>
        //{
        //    { "eq", (first, second) => first == second },
        //    { "ne", (first, second) => first != second },
        //    { "gt", (first, second) => second > first },
        //    { "lt", (first, second) => first > second },
        //    { "ge", (first, second) => second >= first },
        //    { "le", (first, second) => first >= second },
        //    { "sa", (first, second) => second > first },
        //    { "eb", (first, second) => first < second },
        //    //{ "ap", (first, second) => first.Equals(second) }
        //};

        public static IQueryable<T> SearchByDate<T>(this IQueryable<T> query, IEnumerable<string> dates)
            where T : Domain.Abstractions.Interfaces.IDate
        {
            if (dates.Any()) {
                var operatorSymbolCount = 2;
                var dateFormat = "yyyy-MM-ddTHH:mm:ss.fff";

                Expression<Func<T,bool>> predicate = (query) => true;

                foreach (var stringDate in dates)
                {
                    var operation = String.Concat(stringDate.Take(operatorSymbolCount));
                    var dateString = stringDate.Substring(operatorSymbolCount).Trim();

                    if (!DateTime.TryParseExact(dateString, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var date))
                    {
                        continue;
                    }

                    //predicate = predicate.And(x => x.BirthDate == date);
                    query = query.GetFiltredQuery(operation, date);
                }

                query = query.Where(predicate);

            }

            return query;
        }

        public static IQueryable<T> GetFiltredQuery<T>(this IQueryable<T> query, string operation, DateTime date)
            where T : Domain.Abstractions.Interfaces.IDate
        {

            //TODO: don't have time make solution from expretions
            //TODO: make dictionary<string, Enum operator>
            switch (operation)
            {
                case "eq":
                    query = query.Where(x => x.BirthDate == date); 
                    break;
                case "ne":
                    query = query.Where(x => x.BirthDate != date);
                    break;
                case "gt":
                case "sa":
                    query = query.Where(x => x.BirthDate > date);
                    break;
                case "lt":
                case "eb":
                    query = query.Where(x => x.BirthDate < date);
                    break;
                case "ge":
                    query = query.Where(x => x.BirthDate >= date);
                    break;
                case "le":
                    query = query.Where(x => x.BirthDate <= date);
                    break;
                //case "ap":
                //    query = query.Where(x => x.BirthDate == date);
                //    break;

            }

            return query;
        }
    }
}

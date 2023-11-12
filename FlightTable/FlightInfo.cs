using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTable
{
    internal class FlightInfo
    {
        public static string GetFlightCode(Route route, FlightEntities context)
        {
            string code = "";
            code = context.AirlineCode
                                    .Where(x => x.airline_id == route.airline_id
                                                && x.type_id == context.CodeType.Where(type => type.id == 2).FirstOrDefault().id)
                                    .FirstOrDefault()
                                    .code;
            code += "-" + route.flight_number_numeric.ToString();

            return code;
        }
    }
}

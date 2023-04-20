using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.util
{
    internal static class Utils
    {
        public static double RoundWithInterval(double number, double roundingInterval)
        {
            if (roundingInterval == 0) return number;
            return (double)((decimal)roundingInterval * Math.Round((decimal)number /
                (decimal)roundingInterval, MidpointRounding.AwayFromZero));
        }
        public static double FloorWithInterval(double number, double roundingInterval)
        {
            if (roundingInterval == 0) return number;
            return (double)((decimal)roundingInterval * Math.Floor((decimal)number /
                (decimal)roundingInterval));
        }
    }
}

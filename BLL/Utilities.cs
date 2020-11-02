using System;
using System.Collections.Generic;
using System.Text;

namespace Jean_P2_AP1.BLL
{
    public class Utilities
    {
        public static int ToInt(string value)
        {
            int return_ = 0;

            int.TryParse(value, out return_);

            return return_;
        }

        public static float ToFloat(string value)
        {
            float return_ = 0;

            float.TryParse(value, out return_);

            return return_;
        }

        public static double ToDouble(string value)
        {
            double return_ = 0;

            double.TryParse(value, out return_);

            return return_;
        }

        public static decimal ToDecimal(string value)
        {
            decimal return_ = 0;

            decimal.TryParse(value, out return_);

            return return_;
        }
    }
}

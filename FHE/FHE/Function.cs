using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using xFunc.Maths;

namespace FHE
{
    public class Function
    {
        private String representationFunc;

        public Function(String representationFunc)
        {
            this.representationFunc = representationFunc;
        }

        public Function()
        {
            // TODO: Complete member initialization
        }

        public double calcResult(Dictionary<String, double> args)
        {
            double result = 0;

            Dictionary<String, double>.KeyCollection keys = args.Keys;

            foreach (string key in keys)
            {
                representationFunc = representationFunc.Replace(key, args[key].ToString());
            }
            representationFunc = representationFunc.Replace(",", ".");

            Parser parser = new Parser();
            object r = parser.Parse(representationFunc).Calculate();
            String str = r.ToString();
            str = str.Replace(",", ".");
            result = Convert.ToSingle(str, new CultureInfo("en-US"));

            return result;
        }
    }
}

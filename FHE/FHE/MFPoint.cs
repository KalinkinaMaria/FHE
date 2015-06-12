using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    class MFPoint
    {
        public String Unit;

        public double x
        {
            get;
            set;
        }
        public double y
        {
            get;
            private set;
        }
        public Dictionary<String, MFPoint> lambda
        {
            get;
            private set;
        }

        public MFPoint()
        {
            lambda = new Dictionary<string, MFPoint>();
        }

        public MFPoint(double x, double y, String unit)
        {
            lambda = new Dictionary<string, MFPoint>();
            this.x = x;
            this.y = y;
            this.Unit = unit;
        }

        public MFPoint(double x, double y, Dictionary<String, MFPoint> InputLambda, String unit)
        {
            lambda = new Dictionary<string, MFPoint>();
            this.x = x;
            this.y = y;
            this.Unit = unit;

            foreach (String nameX in InputLambda.Keys)
            {
                this.lambda.Add(nameX, InputLambda[nameX]);
            }
        }
    }
}

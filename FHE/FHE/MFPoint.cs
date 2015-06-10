using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    class MFPoint
    {
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
        public Dictionary<String, double> lambda
        {
            get;
            private set;
        }

        public MFPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public MFPoint(double x, double y, Dictionary<String, double> lambda)
        {
            this.x = x;
            this.y = y;
            this.lambda = lambda;
        }
    }
}

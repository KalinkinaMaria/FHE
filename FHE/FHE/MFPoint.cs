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

        public MFPoint()
        {
            lambda = new Dictionary<string, double>();
        }

        public MFPoint(double x, double y)
        {
            lambda = new Dictionary<string, double>();
            this.x = x;
            this.y = y;
        }

        public MFPoint(double x, double y, Dictionary<String, double> lambda)
        {
            lambda = new Dictionary<string, double>();
            this.x = x;
            this.y = y;
            this.lambda = lambda;
        }
    }
}

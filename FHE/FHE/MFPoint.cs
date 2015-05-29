using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    class MFPoint
    {
        public float x
        {
            get;
            set;
        }
        public float y
        {
            get;
            private set;
        }
        public Dictionary<String, float> lambda
        {
            get;
            private set;
        }

        public MFPoint(float x, float y, Dictionary<String, float> lambda)
        {
            this.x = x;
            this.y = y;
            this.lambda = lambda;
        }
    }
}

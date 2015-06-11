using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    abstract class Node
    {
        public Function communicationFunction
        {
            get;
            protected set;
        }
        public List<Node> children = new List<Node>();
        public String FullName;
        public int Level;

        public String name
        {
            get;
            protected set;
        }

        public Node()
        {
            communicationFunction = new Function();
        }

        public virtual void calcMembershipFunc()
        {

        }

        public void AddChild(Node Child)
        {
            children.Add(Child);
        }

        protected List<List<MFPoint>> merge(List<List<MFPoint>> first, MembershipFunction second)
        {
            List<List<MFPoint>> result = new List<List<MFPoint>>();
            if (first.Count != 0)
            {
                for (int i = 0; i < second.countPoints(); i++)
                {
                    for (int j = 0; j < first.Count; j++)
                    {
                        List<MFPoint> tmp = new List<MFPoint>();
                        for (int l = 0; l < first[j].Count; l++)
                        {
                            tmp.Add(first[j][l]);
                        }
                        tmp.Add(second.getMFPoint(i));
                        result.Add(tmp);
                    }
                }
            }
            else
            {
                for (int i = 0; i < second.countPoints(); i++)
                {
                    List<MFPoint> tmp = new List<MFPoint>();
                    tmp.Add(second.getMFPoint(i));
                    result.Add(tmp);
                }
            }
            return result;
        }

        protected MFPoint calcMFPoint(List<MFPoint> points)
        {
            Dictionary<String, double> vars = new Dictionary<string,double>();
            Dictionary<String, double> lambda = new Dictionary<string,double>();
            double x;
            double y = points[0].y;

            for (int i = 0; i < points.Count; i++)
            {
                vars.Add(this.children[i].name, points[i].x);
                if (points[i].y < y)
                {
                    y = points[i].y;
                }
                if (points[i].lambda.Count != 0)
                {
                    Dictionary<String, double>.KeyCollection keys = points[i].lambda.Keys;
                    foreach (string key in keys)
                    {
                        lambda.Add(key, points[i].lambda[key]);
                    }
                }
                else
                {
                    lambda.Add(this.children[i].name, points[i].x);
                }
            }
            x = this.communicationFunction.calcResult(vars);
            return new MFPoint(x, y, lambda);
        }
    }
}

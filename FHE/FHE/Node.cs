﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    abstract class Node
    {
        protected Function communicationFunction = new Function();
        protected List<Node> children = new List<Node>();
        public String name
        {
            get;
            protected set;
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
            List<List<MFPoint>> result = null;
            if (first.Count != 0)
            {
                for (int i = 0; i < second.countPoints(); i++)
                {
                    for (int j = 0; j < first.Count; j++)
                    {
                        List<MFPoint> tmp = null;
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
                    List<MFPoint> tmp = null;
                    tmp.Add(second.getMFPoint(i));
                    result.Add(tmp);
                }
            }
            return result;
        }

        protected MFPoint calcMFPoint(List<MFPoint> points)
        {
            Dictionary<String, double> vars = null;
            Dictionary<String, double> lambda = null;
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

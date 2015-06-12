using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FHE
{
    class MembershipFunction
    {
        private const double inaccuracy = 0.05d;
        public String Unit;
        public double StartX;
        public double EndX;

        public List<MFPoint> points = new List<MFPoint>();

        public MembershipFunction()
        {
        }

        public MembershipFunction(String Unit, double StartX, double EndX)
        {
            this.Unit = Unit;
            this.StartX = StartX;
            this.EndX = EndX;
        }

        public MembershipFunction(List<Point> Points, String Unit, double StartX, double EndX)
        {
            foreach (Point point in Points)
            {
                points.Add(new MFPoint(point.X, point.Y));
            }
            this.Unit = Unit;
            this.StartX = StartX;
            this.EndX = EndX;
        }

        public int countPoints()
        {
            return points.Count;
        }

        public void addMFPoint(MFPoint point)
        {
            points.Add(point);
            this.sortPoint(0, points.Count-1);
            this.deleteRepeat();
        }

        public MFPoint getMFPoint(int index)
        {
            return points[index];
        }

        private void sortPoint(int first, int last)
        {
	        int i = first, j = last;
            double middle = this.points[(first + last) / 2].x;

	        do {
                while (this.points[i].x < middle) i++;
                while (this.points[j].x > middle) j--;
 
                if(i <= j) {
                    if (this.points[i].x > this.points[j].x)
			        {
                        MFPoint tmp = this.points[i];
				        this.points[i] = this.points[j];
				        this.points[j] = tmp;
			        }
                    i++;
                    j--;
                }
            } while (i <= j);

	        if (i < last)
                sortPoint(i, last);
            if (first < j)
                sortPoint(first, j);
        }

        private void deleteRepeat()
        {
            //удаление точек, равных с погрешностью 0,05
            for (int i = 0; i < points.Count - 1; i++)
            {
                /*if (points[i].x < StartX || points[i].x > EndX)
                {
                    points.RemoveAt(i);
                    i--;
                }
                else */if (Math.Abs(points[i].x - points[i + 1].x) < inaccuracy)
                {
                    if (points[i].y > points[i + 1].y)
                    {
                        points.RemoveAt(i + 1);
                        i--;
                    }
                    else
                    {
                        points[i + 1].x = points[i].x;
                        points.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}

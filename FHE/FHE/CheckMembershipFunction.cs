using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FHE
{
    class CheckMembershipFunction
    {

        public static bool Check(bool viewError, String nameNode, List<Point> points, double startX, double endX, Window owner)
        {
            bool isDownturn = false;

            //Сортировка
            sortPoint(0, points.Count - 1, points);

            for (int i = 1; i < points.Count; i ++ )
            {
                if (points[i].Y < points[i - 1].Y)
                {
                    isDownturn = true;
                }
                if (isDownturn && points[i].Y > points[i - 1].Y)
                {
                    if (viewError)
                    {
                        System.Windows.MessageBox.Show(owner, "Вершина " + nameNode + ". Ошибка функции принадлежности: функция должн быть выпуклой",
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    }                    
                    return false;
                }
                if (points[i - 1].Y > 1 || points[i - 1].Y < 0) 
                {
                    if (viewError)
                    {
                        System.Windows.MessageBox.Show(owner, "Вершина " + nameNode + ". Ошибка функции принадлежности: точка лежит вне области определения Y",
                   "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return false;
                }
                if (points[i - 1].X > endX || points[i - 1].X < startX)
                {
                    if (viewError)
                    {
                        System.Windows.MessageBox.Show(owner, "Вершина " + nameNode + ". Ошибка функции принадлежности: точка лежит вне области определения Х",
                   "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private static void sortPoint(int first, int last, List<Point> points)
        {
            int i = first, j = last;
            double middle = points[(first + last) / 2].X;

            do
            {
                while (points[i].X < middle) i++;
                while (points[j].X > middle) j--;

                if (i <= j)
                {
                    if (points[i].X > points[j].X)
                    {
                        Point tmp = points[i];
                        points[i] = points[j];
                        points[j] = tmp;
                    }
                    i++;
                    j--;
                }
            } while (i <= j);

            if (i < last)
                sortPoint(i, last, points);
            if (first < j)
                sortPoint(first, j, points);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FHE
{
    class CheckMembershipFunction
    {

        public static bool Check(String nameNode, List<Point> points, double startX, double endX, Window owner)
        {
            bool isDownturn = false;

            for (int i = 1; i < points.Count; i ++ )
            {
                if (points[i].Y < points[i - 1].Y)
                {
                    isDownturn = true;
                }
                if (isDownturn && points[i].Y > points[i - 1].Y)
                {
                    System.Windows.MessageBox.Show(owner, "Вершина " + nameNode + ". Ошибка функции принадлежности: функция должн быть выпуклой",
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (points[i - 1].Y > 1 || points[i - 1].Y < 0) 
                {
                    System.Windows.MessageBox.Show(owner, "Вершина " + nameNode + ". Ошибка функции принадлежности: точка лежит вне области определения Y",
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (points[i - 1].X > endX || points[i - 1].X < startX)
                {
                    System.Windows.MessageBox.Show(owner, "Вершина " + nameNode + ". Ошибка функции принадлежности: точка лежит вне области определения Х",
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }

    }
}

using FHE.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    class Goal : Node
    {
        public MembershipFunction desirabilityGoal;
        public MembershipFunction achievementGoal;
        public MFPoint resultGoal;

        public Goal(MembershipFunction DesirabilityGoal, String Name, Function CommunicationFunction, String FullName)
            : base()
        {
            this.desirabilityGoal = DesirabilityGoal;
            this.name = Name;
            this.communicationFunction = CommunicationFunction;
            this.FullName = FullName;
            this.Level = 1;
        }

        private bool Intersection(double ax1, double ay1, double ax2, double ay2, double bx1, double by1, double bx2, double by2)
        {
            double v1,v2,v3,v4;

            if (ax1 == bx1 && ay1 == by1 ||
                ax1 == bx2 && ay1 == by2 ||
                ax2 == bx1 && ay2 == by1 ||
                ax2 == bx2 && ay2 == by2)
            {
                return true;
            }

            v1 = ( bx2 - bx1 ) * ( ay1 - by1 ) - ( by2 - by1 ) * ( ax1 - bx1 );
            v2 = ( bx2 - bx1 ) * ( ay2 - by1 ) - ( by2 - by1 ) * ( ax2 - bx1 );
            v3 = ( ax2 - ax1 ) * ( by1 - ay1 ) - ( ay2 - ay1 ) * ( bx1 - ax1 );
            v4 = ( ax2 - ax1 ) * ( by2 - ay1 ) - ( ay2 - ay1 ) * ( bx2 - ax1 );
            return (v1 * v2 < 0) && ( v3 * v4 < 0);
        }

        private void culcResultGoal()
        {
            List<MFPoint> results = new List<MFPoint>();

            //Найти пересечение
            for (int i = 0; i < desirabilityGoal.countPoints() - 1; i++)
            {
                for (int j = 0; j < achievementGoal.countPoints() - 1; j++)
                {
                    if (Intersection(desirabilityGoal.getMFPoint(i).x, desirabilityGoal.getMFPoint(i).y, desirabilityGoal.getMFPoint(i+1).x, desirabilityGoal.getMFPoint(i+1).y,
                        achievementGoal.getMFPoint(j).x, achievementGoal.getMFPoint(j).y, achievementGoal.getMFPoint(j+1).x, achievementGoal.getMFPoint(j+1).y))
                    {
                        if (Math.Abs(desirabilityGoal.getMFPoint(i).x - achievementGoal.getMFPoint(j).x) + Math.Abs(desirabilityGoal.getMFPoint(i + 1).x - achievementGoal.getMFPoint(j).x)
                        >= Math.Abs(desirabilityGoal.getMFPoint(i).x - achievementGoal.getMFPoint(i).x) + Math.Abs(desirabilityGoal.getMFPoint(i + 1).x - achievementGoal.getMFPoint(i).x))
                        {
                            results.Add(achievementGoal.getMFPoint(j));
                        }
                        else
                        {
                            results.Add(achievementGoal.getMFPoint(j+1));
                        }
                    }
                }
            }
            
            //Выбор максимального значения
            if (results.Count == 0)
            {
                resultGoal = null;
            }
            else
            {
                resultGoal = results[0];
                for (int i = 1; i < results.Count; i++)
                {
                    if (results[i].y >= resultGoal.y)
                    {
                        resultGoal = results[i];
                    }
                }
            }
        }

        public override void calcMembershipFunc()
        {
            List<List<MFPoint>> merged = new List<List<MFPoint>>();
            MembershipFunction result = new MembershipFunction();

            //Высчитать функции принадлежности для детей
            foreach (Node child in children)
            {
                child.calcMembershipFunc();
            }

            //создание таблицы сочетаний точек функций принадлежности всех детей узла
            foreach (Node child in children)
            {
                merged = this.merge(merged, (child as Characteristic).achievementCharacteristics);
            }

            //вычисление функции принадлежности узла
            for (int i = 0; i < merged.Count; i++)
            {
                result.addMFPoint(this.calcMFPoint(merged[i], desirabilityGoal.Unit));
            }

            achievementGoal = result;

            culcResultGoal();
        }
    }
}

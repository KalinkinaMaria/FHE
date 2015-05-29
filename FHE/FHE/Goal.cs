using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    class Goal : Node
    {
        private MembershipFunction desirabilityGoal;
        private MembershipFunction achievementGoal;
        private MFPoint resultGoal;
 
        public Goal()
        {
            this.level = 1;
        }

        private void culcResultGoal()
        {
            //this.calcMembershipFunc();

            //Найти пересечение
            for (int i = 0; i < desirabilityGoal.countPoints() - 1; i++)
            {
                int begin = -1;
                int end = -1;
                for (int j = 0; j < achievementGoal.countPoints() - 1; j++)
                {
                    if (achievementGoal.getMFPoint(j).x >= desirabilityGoal.getMFPoint(i).x)
                    {
                        begin = j;
                        break;
                    }
                }
                for (int j = 0; j < achievementGoal.countPoints() - 1; j++)
                {
                    if (achievementGoal.getMFPoint(j).x > desirabilityGoal.getMFPoint(i).x)
                    {
                        end = j - 1;
                        break;
                    }
                }
                if (end != -1 && begin != -1 && (((desirabilityGoal.getMFPoint(i).y <= achievementGoal.getMFPoint(begin).y)
                    && (desirabilityGoal.getMFPoint(i + 1).y >= achievementGoal.getMFPoint(end).y))
                    || ((desirabilityGoal.getMFPoint(i).y >= achievementGoal.getMFPoint(begin).y)
                    && (desirabilityGoal.getMFPoint(i + 1).y <= achievementGoal.getMFPoint(end).y))))
                {
                    resultGoal = achievementGoal.getMFPoint((end + begin) / 2);
                    return;
                }
            }
        }

        public override void calcMembershipFunc()
        {
            List<List<MFPoint>> merged = null;
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
                result.addMFPoint(this.calcMFPoint(merged[i]));
            }

            achievementGoal = result;

            culcResultGoal();
        }
    }
}

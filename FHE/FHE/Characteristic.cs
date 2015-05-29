using System;
using System.Collections.Generic;
using System.Text;

namespace FHE
{
    class Characteristic : Node
    {
        public MembershipFunction achievementCharacteristics
        {
            get;
            private set;
        }

        public override void calcMembershipFunc()
        {
            List<List<MFPoint>> merged = null;
            MembershipFunction result = new MembershipFunction();

            if(achievementCharacteristics.countPoints() == 0)
	        {
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

                achievementCharacteristics = result;
	        }
        }
    }
}

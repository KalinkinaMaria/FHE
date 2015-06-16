using FHE.Windows;
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
            set;
        }

        public Characteristic(MembershipFunction AchievementCharacteristics, String Name, Function CommunicationFunction, String FullName, int Level)
            : base()
        {
            this.achievementCharacteristics = AchievementCharacteristics;
            this.name = Name;
            this.communicationFunction = CommunicationFunction;
            this.FullName = FullName;
            this.Level = Level;
        }

        public override void calcMembershipFunc()
        {
            List<List<MFPoint>> merged = new List<List<MFPoint>>();
            MembershipFunction result = new MembershipFunction(achievementCharacteristics.Unit, achievementCharacteristics.StartX, achievementCharacteristics.EndX);

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
                    MFPoint NewPoint = this.calcMFPoint(merged[i], "");
                    result.addMFPoint(NewPoint);
                }

                achievementCharacteristics = result;
	        }
        }
    }
}

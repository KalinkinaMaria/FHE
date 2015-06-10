using FHE.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHE
{
    class ConvertModel
    {
        public static List<Node> FromViewToModel(List<HierarchyGoal> GoalsView)
        {
            List<Node> Results = new List<Node>();

            foreach (HierarchyGoal goal in GoalsView)
            {
                Results.Add(GoalFromViewToModel(goal));
            }

            return Results;
        }

        public static List<HierarchyGoal> FromModelToView(List<Node> GoalsView)
        {
            List<HierarchyGoal> Results = new List<HierarchyGoal>();

            return Results;
        }

        private static void ChildrenFromViewToModel(Node ParentNode, AbstractHierarchyNode ViewNode)
        {
            foreach (HierarchyNode ViewChild in ViewNode.childrenNode)
            {
                Characteristic ChildNode = CharacteristicFromViewToModel(ViewChild);
                ParentNode.AddChild(ChildNode);
                ChildrenFromViewToModel(ChildNode, ViewChild);
            }
        }

        private static Goal GoalFromViewToModel(HierarchyGoal ViewGoal)
        {
            Goal NewGoal = new Goal(new MembershipFunction(ViewGoal.MembershipFunction), ViewGoal.Name, new Function(ViewGoal.LinkFunc));

            ChildrenFromViewToModel(NewGoal, ViewGoal);

            return NewGoal;
        }

        private static Characteristic CharacteristicFromViewToModel(HierarchyNode ViewNode)
        {
            Characteristic NewCharacteristic = new Characteristic(new MembershipFunction(ViewNode.MembershipFunction), ViewNode.Name, new Function(ViewNode.LinkFunc));

            return NewCharacteristic;
        }
    }
}

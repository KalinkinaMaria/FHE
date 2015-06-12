using FHE.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FHE
{
    class ConvertModel
    {
        public static List<Goal> FromViewToModel(List<HierarchyGoal> GoalsView)
        {
            List<Goal> Results = new List<Goal>();

            foreach (HierarchyGoal goal in GoalsView)
            {
                Results.Add(GoalFromViewToModel(goal));
            }

            return Results;
        }

        public static void FromModelToView(List<Goal> GoalsModel, StackPanel StackLevel)
        {
            StackLevel.Orientation = Orientation.Vertical;
            HierarchyLevelForGoal addingCanvas = new HierarchyLevelForGoal(1);
            StackLevel.Children.Insert(0, addingCanvas);

            foreach (Goal goal in GoalsModel)
            {
                GoalFromModelToView(goal, StackLevel);
            }

        }

        private static void GoalFromModelToView(Goal ModelGoal, StackPanel StackLevel)
        {
            HierarchyLevelForGoal addingCanvas = StackLevel.Children[0] as HierarchyLevelForGoal;
            addingCanvas.add(ModelGoal.name);
            HierarchyGoal NewViewGoal = addingCanvas.stackNode.Children[addingCanvas.stackNode.Children.Count - 1] as HierarchyGoal;

            NewViewGoal.LinkFunc = ModelGoal.communicationFunction.representationFunc;
            NewViewGoal.name = ModelGoal.FullName;
            NewViewGoal.textNode.Text = ModelGoal.name;
            NewViewGoal.UnitMF = ModelGoal.desirabilityGoal.Unit;
            NewViewGoal.StartXMF = ModelGoal.desirabilityGoal.StartX;
            NewViewGoal.EndXMF = ModelGoal.desirabilityGoal.EndX;
            NewViewGoal.MembershipFunction = new List<Point>();

            foreach (MFPoint point in ModelGoal.desirabilityGoal.points)
            {
                NewViewGoal.MembershipFunction.Add(new Point(point.x, point.y));
            }

            ChildrenFromModelToView(NewViewGoal, ModelGoal, StackLevel);
        }

        private static void ChildrenFromModelToView(AbstractHierarchyNode NewViewNode, Node ModelGoal, StackPanel StackLevel)
        {
            foreach (Characteristic ModelChild in ModelGoal.children)
            {
                int Index = Convert.ToInt32(ModelChild.name.Replace("x", "").Replace("X", ""));
                if (!NewViewNode.containsChild(Index))
                {
                    HierarchyNode ChildNode = NodeFromModelToView(ModelChild, StackLevel);
                    NewViewNode.childrenNode.Add(ChildNode);
                    ChildNode.ParentNode.Add(NewViewNode);
                    ChildrenFromModelToView(ChildNode, ModelChild, StackLevel);
                }
            }
        }

        private static HierarchyNode NodeFromModelToView(Characteristic ModelChild, StackPanel StackLevel)
        {
            HierarchyNode NewNode = null;
            int Level = ModelChild.Level;

            if (StackLevel.Children.Count > Level && StackLevel.Children[Level - 1] is HierarchyLevelForNode)
            {
                int index = ContainsNode((StackLevel.Children[Level - 1] as HierarchyLevelForNode), ModelChild.name);
                if (index == -1)
                {
                    (StackLevel.Children[Level - 1] as HierarchyLevelForNode).add(ModelChild.name);
                    NewNode = ((StackLevel.Children[Level - 1] as HierarchyLevelForNode).stackNode.Children[(StackLevel.Children[Level - 1] as HierarchyLevelForNode).stackNode.Children.Count - 1] as HierarchyNode);
                }
                else
                {
                    return (StackLevel.Children[Level - 1] as HierarchyLevelForNode).stackNode.Children[index] as HierarchyNode;
                }
            }
            else 
            {
                HierarchyLevelForNode addingCanvas = new HierarchyLevelForNode(Level);
                StackLevel.Children.Insert(Level - 1, addingCanvas);
                addingCanvas.add(ModelChild.name);
                NewNode = addingCanvas.stackNode.Children[addingCanvas.stackNode.Children.Count - 1] as HierarchyNode;
            }

            NewNode.name = ModelChild.FullName;
            if (ModelChild.communicationFunction != null)
            {
                NewNode.LinkFunc = ModelChild.communicationFunction.representationFunc;
            }
            if (ModelChild.achievementCharacteristics != null)
            {
                NewNode.UnitMF = ModelChild.achievementCharacteristics.Unit;
                NewNode.StartXMF = ModelChild.achievementCharacteristics.StartX;
                NewNode.EndXMF = ModelChild.achievementCharacteristics.EndX;
                NewNode.MembershipFunction = new List<Point>();

                foreach (MFPoint point in ModelChild.achievementCharacteristics.points)
                {
                    NewNode.MembershipFunction.Add(new Point(point.x, point.y));
                }
            }

            return NewNode;
        }

        private static int ContainsNode(HierarchyLevel Level, String NameNode)
        {
            int result = -1;

            for (int i = 0; i < Level.stackNode.Children.Count; i++ )
            {
                if ((Level.stackNode.Children[i] as AbstractHierarchyNode).textNode.Text == NameNode)
                {
                    result = i;
                }
            }

            return result;
        }

        private static void ChildrenFromViewToModel(Node ParentNode, AbstractHierarchyNode ViewNode, int Level)
        {
            foreach (HierarchyNode ViewChild in ViewNode.childrenNode)
            {
                Characteristic ChildNode = CharacteristicFromViewToModel(ViewChild, Level);
                ParentNode.AddChild(ChildNode);
                ChildrenFromViewToModel(ChildNode, ViewChild, Level+1);
            }
        }

        private static Goal GoalFromViewToModel(HierarchyGoal ViewGoal)
        {
            Goal NewGoal = new Goal(new MembershipFunction(ViewGoal.MembershipFunction, ViewGoal.UnitMF, ViewGoal.StartXMF, ViewGoal.EndXMF), ViewGoal.textNode.Text, new Function(ViewGoal.LinkFunc), ViewGoal.name);

            ChildrenFromViewToModel(NewGoal, ViewGoal, 2);

            return NewGoal;
        }

        private static Characteristic CharacteristicFromViewToModel(HierarchyNode ViewNode, int Level)
        {
            Characteristic NewCharacteristic = new Characteristic(new MembershipFunction(ViewNode.MembershipFunction, ViewNode.UnitMF, ViewNode.StartXMF, ViewNode.EndXMF), ViewNode.textNode.Text, new Function(ViewNode.LinkFunc), ViewNode.name, Level);

            return NewCharacteristic;
        }
    }
}

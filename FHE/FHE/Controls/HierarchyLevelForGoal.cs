using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace FHE.Controls
{
    class HierarchyLevelForGoal : HierarchyLevel
    {
        public HierarchyLevelForGoal(int number) : base(number)
        {
            InitializeComponent();
            this.deleteButton.Visibility = System.Windows.Visibility.Hidden;
        }

        protected override void add()
        {
            HierarchyGoal addingNode = new HierarchyGoal();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "Q" + (this.stackNode.ColumnDefinitions.Count + 1);
            Grid.SetColumn(addingNode, this.stackNode.ColumnDefinitions.Count);
            this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
            this.stackNode.Children.Add(addingNode);

            addingNode.onChange += this.fairOnChange;
        }

        public List<HierarchyGoal> GetGoals()
        {
            List<HierarchyGoal> Results = new List<HierarchyGoal>();

            foreach (HierarchyGoal goal in this.stackNode.Children)
            {
                Results.Add(goal);
            }

            return Results;
        }
    }
}

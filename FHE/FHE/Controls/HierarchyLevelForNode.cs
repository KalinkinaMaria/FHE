using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FHE.Controls
{
    class HierarchyLevelForNode : HierarchyLevel
    {
        public HierarchyLevelForNode(int number) : base(number)
        {
        }

        private void addNode(int number)
        {
            HierarchyNode addingNode = new HierarchyNode(number);
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "X" + number;
            Grid.SetColumn(addingNode, this.stackNode.ColumnDefinitions.Count);
            this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
            this.stackNode.Children.Add(addingNode);

            addingNode.onChange += this.fairOnChange;
        }

        protected override void add()
        {
            HierarchyNode addingNode = new HierarchyNode();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "X" + addingNode.id;
            Grid.SetColumn(addingNode, this.stackNode.ColumnDefinitions.Count);
            this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
            this.stackNode.Children.Add(addingNode);
            
            addingNode.onChange += this.fairOnChange;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Handled == false && MainWindow.mode == MainWindow.Mode.EDIT_HIERARCHY)
            {
                HierarchyNode _element = (HierarchyNode)e.Data.GetData("Object");

                if (_element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        int id = _element.id;
                        _element.delete();
                        this.addNode(id);
                        e.Effects = DragDropEffects.Move;
                    }
                }
                //onChange();
            }
        }
    }
}

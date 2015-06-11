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

        private void addNode(HierarchyNode node)
        {
            node.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            node.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            node.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            node.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            Grid.SetColumn(node, this.stackNode.ColumnDefinitions.Count);
            this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
            this.stackNode.Children.Add(node);
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

        public override void add(String Name)
        {
            HierarchyNode addingNode = new HierarchyNode();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = Name;
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
                        _element.delete();
                        addNode(_element);

                        foreach (AbstractHierarchyNode parentNode in _element.ParentNode)
                        {
                            if ((((parentNode.Parent as Grid).Parent as Grid).Parent as HierarchyLevel).Number >=
                                (((_element.Parent as Grid).Parent as Grid).Parent as HierarchyLevel).Number)
                            {
                                parentNode.removeChild(_element);
                            }
                        }

                        e.Effects = DragDropEffects.Move;
                    }
                }
                fairOnChange();
            }
        }
    }
}

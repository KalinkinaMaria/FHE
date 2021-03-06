﻿using System;
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
            if (this.stackNode.Children.Count < 10)
            {
                node.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                node.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                node.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                node.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                Grid.SetColumn(node, this.stackNode.ColumnDefinitions.Count);
                this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
                this.stackNode.Children.Add(node);
            }
        }

        protected override void add()
        {
            if (this.stackNode.Children.Count < 10)
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
        }

        public override void add(String Name)
        {
            if (this.stackNode.Children.Count < 10)
            {
                String Index = Name.Replace("x", "");
                Index = Index.Replace("X", "");
                HierarchyNode addingNode = new HierarchyNode(Convert.ToInt32(Index));
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
        }

        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Handled == false && MainWindow.mode == MainWindow.Mode.EDIT_HIERARCHY)
            {
                HierarchyNode _element = (HierarchyNode)e.Data.GetData("Object");

                if (_element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null && this.stackNode.Children.Count < 10)
                    {
                        _element.delete();
                        addNode(_element);

                        for (int i = 0; i < _element.ParentNode.Count; i ++ )
                        {
                            if ((((_element.ParentNode[i].Parent as Grid).Parent as Grid).Parent as HierarchyLevel).Number >=
                                (((_element.Parent as Grid).Parent as Grid).Parent as HierarchyLevel).Number)
                            {
                                _element.ParentNode[i].removeChild(_element);
                                _element.ParentNode.Remove(_element.ParentNode[i]);
                                i--;
                            }
                        }

                        //удалить дуги
                        int currentLevel = this.Number;
                        for (int i = 0; i < _element.childrenNode.Count; i ++ )
                        {
                            if ((((_element.childrenNode[i].Parent as Grid).Parent as Grid).Parent as HierarchyLevel).Number <=
                                (((_element.Parent as Grid).Parent as Grid).Parent as HierarchyLevel).Number)
                            {
                                _element.childrenNode[i].ParentNode.Remove(_element);
                                _element.removeChild(_element.childrenNode[i]);
                                i--;
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

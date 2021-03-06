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
    /// <summary>
    /// Interaction logic for HierarchyLevel.xaml
    /// </summary>
    abstract public partial class HierarchyLevel : UserControl
    {
        public delegate void ChangeEventHandler();

        public event ChangeEventHandler onChange;

        public int Number
        {
            get;
            private set;
        }

        public HierarchyLevel(int number)
        {
            InitializeComponent();
            this.Number = number;
        }

        public bool containsDependence()
        {
            bool result = true;
            for (int i = 0; i < this.stackNode.Children.Count; i++)
            {
                if (!(this.stackNode.Children[i] as AbstractHierarchyNode).containsDependence())
                {
                    result = false;
                    return result;
                }
            }
            return result;
        }

        public List<HierarchyNode> getNodes()
        {
            List<HierarchyNode> result = new List<HierarchyNode>();

            foreach (HierarchyNode child in this.stackNode.Children)
            {
                result.Add(child);
            }

            return result;
        }

        public void fairOnChange()
        {
            onChange();
        }

        private void Delete_Level(object sender, RoutedEventArgs e)
        {
            foreach (HierarchyNode node in this.stackNode.Children)
            {
                foreach (AbstractHierarchyNode parentNode in node.ParentNode)
                {
                    parentNode.removeChild(node);
                }
            }

            (this.Parent as StackPanel).Children.Remove(this);

            onChange();
        }

        abstract protected void add();

        abstract public void add(String Name);

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.mode == MainWindow.Mode.EDIT_HIERARCHY)
            {
                this.add();
                onChange();
            }
        }

        public void paint_node_for_func_link()
        {
            for (int i = 0; i < this.stackNode.Children.Count; i++)
            {
                AbstractHierarchyNode currentNode = (this.stackNode.Children[i] as AbstractHierarchyNode);
                List<String> args = new List<String>();
                foreach (HierarchyNode node in currentNode.childrenNode)
                {
                    args.Add(node.textNode.Text);
                }               
                String[] args_copy = args.ToArray();
                if (!CheckFunctionLinc.check(false, currentNode.textNode.Text, currentNode.LinkFunc, args_copy, null))
                {
                    currentNode.formNode.Fill = Brushes.MediumBlue;
                    if (currentNode.LinkFunc != "")
                    {
                        currentNode.formNode.Stroke = Brushes.Red;
                    }
                }
                currentNode.IsNeedFuncLink = true;
            }
        }

        internal bool containsFuncLink()
        {
            bool result = true;
            for (int i = 0; i < this.stackNode.Children.Count; i++)
            {
                if ((this.stackNode.Children[i] as AbstractHierarchyNode).LinkFunc == ""
                    || (this.stackNode.Children[i] as AbstractHierarchyNode).LinkFunc == null)
                {
                    result = false;
                    return result;
                }
            }
            return result;
        }

        internal void paint_node_for_start()
        {
            for (int i = 0; i < this.stackNode.Children.Count; i++)
            {
                (this.stackNode.Children[i] as AbstractHierarchyNode).setColorForm();
                (this.stackNode.Children[i] as AbstractHierarchyNode).formNode.Stroke = Brushes.White;
            }
        }

        internal bool containsParentNode()
        {
            bool result = true;
            for (int i = 0; i < this.stackNode.Children.Count; i++)
            {
                if ((this.stackNode.Children[i] as AbstractHierarchyNode).ParentNode.Count == 0)
                {
                    result = false;
                    return result;
                }
            }
            return result;
        }

        internal bool correctFuncLink(Window owner)
        {
            AbstractHierarchyNode currentNode; 
            List<String> args = new List<String>();
            bool result = true;
            
            for (int i = 0; i < this.stackNode.Children.Count; i++)
            {
                currentNode = (this.stackNode.Children[i] as AbstractHierarchyNode);
                foreach (HierarchyNode node in currentNode.childrenNode)
                {
                    args.Add(node.textNode.Text);
                }
                if (!CheckFunctionLinc.check(false, currentNode.textNode.Text, currentNode.LinkFunc, args.ToArray(), owner))
                {
                    result = false;
                    return result;
                }
                args.Clear();
            }
            return result;
        }
    }
}

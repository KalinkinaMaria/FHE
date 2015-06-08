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
                (this.stackNode.Children[i] as AbstractHierarchyNode).formNode.Fill = Brushes.MediumBlue;
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
    }
}

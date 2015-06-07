using FHE.Windows;
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
    /// Interaction logic for AbstractHierarchyNode.xaml
    /// </summary>
    abstract public partial class AbstractHierarchyNode : UserControl
    {
        public delegate void ChangeEventHandler();

        public event ChangeEventHandler onChange;

        private string LinkFunc;

        public Point Position
        {
            get
            {
                int level = (((this.Parent as Grid).Parent as Grid).Parent as HierarchyLevel).Number;
                int column = Grid.GetColumn(this);
                int columns = (this.Parent as Grid).Children.Count;
                double width = (this.Parent as Grid).ActualWidth;
                double x = width / (columns * 2) * ((column + 1) * 2 - 1);
                double y = 50*level-25;
                return new Point(x, y);
            }
        }

        public string name
        {
            get;
            private set;
        }

        public List<HierarchyNode> childrenNode
        {
            get;
            private set;
        }

        public List<AbstractHierarchyNode> ParentNode
        {
            get;
            set;
        }

        public void addChild(HierarchyNode node)
        {
            this.childrenNode.Add(node);
        }

        public void removeChild(int index)
        {
            foreach (HierarchyNode node in childrenNode)
            {
                if (node.id == index)
                {
                    childrenNode.Remove(node);
                    return;
                }
            }
        }

        public void removeChild(AbstractHierarchyNode node)
        {
            childrenNode.Remove(node as HierarchyNode);
        }

        public bool containsChild (int index)
        {
            foreach (HierarchyNode node in childrenNode)
            {
                if (node.id == index)
                    return true;
            }
            return false;
        }

        public AbstractHierarchyNode()
        {
            InitializeComponent();
            childrenNode = new List<HierarchyNode>();
            ParentNode = new List<AbstractHierarchyNode>();
        }

        public void delete()
        {
            int currentcolumn = Grid.GetColumn(this);

            foreach (AbstractHierarchyNode Node in (this.Parent as Grid).Children)
            {
                if (Grid.GetColumn(Node) > currentcolumn)
                {
                    Grid.SetColumn(Node, Grid.GetColumn(Node) - 1);
                }
            }

            (this.Parent as Grid).ColumnDefinitions.RemoveAt((this.Parent as Grid).ColumnDefinitions.Count - 1);

            (this.Parent as Grid).Children.Remove(this);
        }

        public bool containsDependence()
        {
            return childrenNode.Count != 0;
        }

        public void fairOnChange()
        {
            onChange();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (MainWindow.mode == MainWindow.Mode.EDIT_HIERARCHY)
            {
                this.deleteNode.Visibility = System.Windows.Visibility.Visible;
                this.addEdge.Visibility = System.Windows.Visibility.Visible;
                this.renameNode.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MainWindow.mode == MainWindow.Mode.EDIT_FUNC_LINK)
            {
                this.addFuncLink.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (MainWindow.mode == MainWindow.Mode.EDIT_HIERARCHY)
            {
                this.deleteNode.Visibility = System.Windows.Visibility.Hidden;
                this.addEdge.Visibility = System.Windows.Visibility.Hidden;
                this.renameNode.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (MainWindow.mode == MainWindow.Mode.EDIT_FUNC_LINK)
            {
                this.addFuncLink.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void deleteNode_Click(object sender, RoutedEventArgs e)
        {
            foreach (AbstractHierarchyNode parentNode in this.ParentNode)
            {
                parentNode.removeChild(this);
            }
            this.delete();

            onChange();
        }

        private void addEdge_Click(object sender, RoutedEventArgs e)
        {
            EditEdge text = null;
            if (this is HierarchyNode)
                text = new EditEdge(this as HierarchyNode);
            else
                text = new EditEdge(this as HierarchyGoal);
            text.onChange += this.fairOnChange;
            text.ShowDialog();
        }

        private void renameNode_Click(object sender, RoutedEventArgs e)
        {
            EnterName text = new EnterName();
            text.ShowDialog();
            name = text.getName();
            this.parent.ToolTip = this.name;
        }

        private void addFuncLink_Click(object sender, RoutedEventArgs e)
        {
            EditFuncLink window = new EditFuncLink(this);
            window.ShowDialog();

            this.LinkFunc = window.getFuncLink();
            setColorForm();
        }

        abstract public void setColorForm();

        private void addMembershipFunc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

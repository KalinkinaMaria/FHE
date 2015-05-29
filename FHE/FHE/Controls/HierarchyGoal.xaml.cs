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
    /// Interaction logic for HierachyGoal.xaml
    /// </summary>
    public partial class HierarchyGoal : UserControl
    {
        public delegate void ChangeEventHandler();

        public event ChangeEventHandler onChange;

        public static SolidColorBrush defaultColor = new SolidColorBrush(Color.FromRgb(255,215,0));

        public List<HierarchyNode> childrenNode
        {
            get;
            private set;
        }
        public string name
        {
            get;
            private set;
        }

        public int getId()
        {
            return Convert.ToInt32(this.textNode.Text.Remove(0, 1));
        }

        public HierarchyGoal()
        {
            InitializeComponent();
            childrenNode = new List<HierarchyNode>();
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (MainWindow.mode == MainWindow.Mode.EDIT_HIERARCHY)
            {
                this.deleteNode.Visibility = System.Windows.Visibility.Hidden;
                this.addEdge.Visibility = System.Windows.Visibility.Hidden;
                this.renameNode.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (MainWindow.mode == MainWindow.Mode.EDIT_HIERARCHY)
            {
                this.deleteNode.Visibility = System.Windows.Visibility.Visible;
                this.addEdge.Visibility = System.Windows.Visibility.Visible;
                this.renameNode.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void delete()
        {
            int currentcolumn = Grid.GetColumn(this);

            foreach (HierarchyGoal Node in (this.Parent as Grid).Children)
            {
                if (Grid.GetColumn(Node) > currentcolumn)
                {
                    Grid.SetColumn(Node, Grid.GetColumn(Node) - 1);
                }
            }

            (this.Parent as Grid).ColumnDefinitions.RemoveAt((this.Parent as Grid).ColumnDefinitions.Count - 1);

            (this.Parent as Grid).Children.Remove(this);
        }

        private void deleteNode_Click(object sender, RoutedEventArgs e)
        {
            this.delete();

            onChange();
        }

        private void addEdge_Click(object sender, RoutedEventArgs e)
        {
            EditEdge text = new EditEdge(this);
            text.ShowDialog();  
        }

        private void renameNode_Click(object sender, RoutedEventArgs e)
        {
            EnterName text = new EnterName();
            text.ShowDialog();
            name = text.getName();
            this.parent.ToolTip = this.name;
        }
    }
}

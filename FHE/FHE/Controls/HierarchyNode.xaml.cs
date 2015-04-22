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
    /// Interaction logic for HierarchyNode.xaml
    /// </summary>
    public partial class HierarchyNode : UserControl
    {
        public delegate void addEdgeClick(object sender, RoutedEventArgs e);

        public event addEdgeClick onAddEdgeClick;

        public HierarchyNode()
        {
            InitializeComponent();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            this.deleteNode.Visibility = System.Windows.Visibility.Visible;
            this.addEdge.Visibility = System.Windows.Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.deleteNode.Visibility = System.Windows.Visibility.Hidden;
            this.addEdge.Visibility = System.Windows.Visibility.Hidden;
        }

        private void deleteNode_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).ColumnDefinitions.RemoveAt(Grid.GetColumn(this));
            (this.Parent as Grid).Children.Remove(this);
        }

        private void addEdge_Click(object sender, RoutedEventArgs e)
        {
            if (this.onAddEdgeClick != null)
            {
                this.onAddEdgeClick(this, e);
            }
        }
    }
}

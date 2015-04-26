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
        private static int global_id = 1;
        private int id;

        public delegate void addEdgeClick(object sender, RoutedEventArgs e);

        public event addEdgeClick onAddEdgeClick;

        public HierarchyNode()
        {
            InitializeComponent();
            id = global_id;
            global_id += 1;
        }

        public HierarchyNode(int id)
        {
            InitializeComponent();
            id = id;
        }

        public int getId()
        {
            return id;
        }

        public void delete()
        {
            int currentcolumn = Grid.GetColumn(this);
            
            foreach(HierarchyNode Node in (this.Parent as Grid).Children)
            {
                if (Grid.GetColumn(Node) > currentcolumn)
                {
                    Grid.SetColumn(Node, Grid.GetColumn(Node) - 1);
                }
            }

            (this.Parent as Grid).ColumnDefinitions.RemoveAt((this.Parent as Grid).ColumnDefinitions.Count - 1);

            (this.Parent as Grid).Children.Remove(this);
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
            this.delete();
        }

        private void addEdge_Click(object sender, RoutedEventArgs e)
        {
            if (this.onAddEdgeClick != null)
            {
                this.onAddEdgeClick(this, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && !this.textNode.Text.Contains("Q"))
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }
    }
}

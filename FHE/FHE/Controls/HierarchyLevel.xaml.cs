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
    public partial class HierarchyLevel : UserControl
    {
        public HierarchyLevel()
        {
            InitializeComponent();
        }

        private void Delete_Level(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void addNode(int number)
        {
            HierarchyNode addingNode = new HierarchyNode();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "X" + number;
            Grid.SetColumn(addingNode, this.stackNode.ColumnDefinitions.Count);
            this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
            this.stackNode.Children.Add(addingNode);
        }

        private void addNode()
        {
            HierarchyNode addingNode = new HierarchyNode();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "X" + addingNode.getId();
            Grid.SetColumn(addingNode, this.stackNode.ColumnDefinitions.Count);
            this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
            this.stackNode.Children.Add(addingNode);
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.addNode();
        }

        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                e.Effects = DragDropEffects.Move;
            }
        }

        private void panel_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                HierarchyNode _element = (HierarchyNode)e.Data.GetData("Object");

                if (_panel != null && _element != null)
                {
                    // Get the panel that the element currently belongs to,
                    // then remove it from that panel and add it the Children of
                    // the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        int id = _element.getId();
                        _element.delete();
                        this.addNode(id);
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
        }
    }
}

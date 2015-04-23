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

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HierarchyNode addingNode = new HierarchyNode();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "X" + ((this.Parent as StackPanel).Children.Count - 2) + (this.stackNode.ColumnDefinitions.Count + 1);
            Grid.SetColumn(addingNode, this.stackNode.ColumnDefinitions.Count);
            this.stackNode.ColumnDefinitions.Add(new ColumnDefinition());
            this.stackNode.Children.Add(addingNode);
        }
    }
}

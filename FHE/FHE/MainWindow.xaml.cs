using FHE.Controls;
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

namespace FHE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Level_Click(object sender, RoutedEventArgs e)
        {
            HierarchyLevel addingCanvas = new HierarchyLevel();
            this.stackLevel.Children.Insert(this.stackLevel.Children.Count - 1, addingCanvas);
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HierarchyNode addingNode = new HierarchyNode();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "X0" + (this.targetLevel.ColumnDefinitions.Count + 1);
            Grid.SetColumn(addingNode, this.targetLevel.ColumnDefinitions.Count);
            this.targetLevel.ColumnDefinitions.Add(new ColumnDefinition());
            this.targetLevel.Children.Add(addingNode);
        }
    }
}

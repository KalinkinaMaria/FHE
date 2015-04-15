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
        private int _indexLevel;
        public HierarchyLevel()
        {
            InitializeComponent();
        }

        private void Delete_Level(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}

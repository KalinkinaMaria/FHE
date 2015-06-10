using FHE.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for IdealValueMF.xaml
    /// </summary>
    public partial class IdealValueMF : UserControl
    {
        private EditMembershipFunc Parent;
        public IdealValueMF(EditMembershipFunc parent)
        {
            InitializeComponent();
            this.Parent = parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Parent.PointsMF.Add(new Point(Convert.ToDouble(this.IdealX1.Text), 1));
            this.Parent.PointsMF.Add(new Point(Convert.ToDouble(this.IdealX2.Text), 1));

            this.Parent.MF.ItemsSource = this.Parent.PointsMF;
            this.IsEnabled = false;

            PointsMF Points = new PointsMF(this.Parent);
            this.Parent.StackStep.Children.Insert(this.Parent.StackStep.Children.Count, Points);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

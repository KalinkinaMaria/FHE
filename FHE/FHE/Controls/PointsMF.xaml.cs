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
    /// Interaction logic for PointsMF.xaml
    /// </summary>
    public partial class PointsMF : UserControl
    {
        private EditMembershipFunc Parent;

        public PointsMF(EditMembershipFunc parent)
        {
            InitializeComponent();
            this.Parent = parent;
        }

        private void AddPoint_Click(object sender, RoutedEventArgs e)
        {
            List<Point> list = new List<Point>();
            foreach (Point point in this.Parent.PointsMF)
            {
                list.Add(point);
            }
            Point NewPoint = new Point(Convert.ToDouble(this.NameX.Text), Convert.ToDouble(this.NameY.Text));
            list.Add(NewPoint);
            this.Parent.PointsMF.Add(NewPoint);
            this.Parent.MF.ItemsSource = list;

            DescriptionPoint Point = new DescriptionPoint(this, this.Parent, new Point(Convert.ToDouble(this.NameX.Text), Convert.ToDouble(this.NameY.Text)));
            this.StackPointMF.Children.Add(Point);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Point point in this.Parent.PointsMF)
            {
                this.Parent.CurrentNode.MembershipFunction.Add(point);
            }
        }
    }
}

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
    /// Interaction logic for DescriptionPoint.xaml
    /// </summary>
    public partial class DescriptionPoint : UserControl
    {
        private EditMembershipFunc Parent;
        private PointsMF ParentControl;
        public DescriptionPoint(PointsMF parentControl, EditMembershipFunc parent, Point point)
        {
            InitializeComponent();
            this.Parent = parent;
            this.ParentControl = parentControl;
            this.NameX.Text = point.X.ToString();
            this.NameY.Text = point.Y.ToString();
        }

        private void DeletePoint_Click(object sender, RoutedEventArgs e)
        {
            List<Point> list = new List<Point>();
            Point NewPoint = new Point(Convert.ToDouble(this.NameX.Text), Convert.ToDouble(this.NameY.Text));
            this.Parent.PointsMF.Remove(NewPoint);
            foreach (Point point in this.Parent.PointsMF)
            {
                list.Add(point);
            }
            this.Parent.MF.ItemsSource = list;

            //Удалить из стека
            this.ParentControl.StackPointMF.Children.Remove(this);
        }
    }
}

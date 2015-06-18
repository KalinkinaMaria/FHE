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
        private bool isDownturn = false;

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
            double newX = 0, newY = 0;

            try
            {
                newX = Convert.ToDouble(this.NameX.Text.Replace('.', ','));
                newY = Convert.ToDouble(this.NameY.Text.Replace('.', ','));
            }
            catch (FormatException exept)
            {
                System.Windows.MessageBox.Show(Parent, "Вершина " + Parent.CurrentNode.textNode.Text + ". Значение точки - ожидалось число",
            "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newX > this.Parent.AxisX.Maximum || newX < this.Parent.AxisX.Minimum)
            {
                System.Windows.MessageBox.Show(Parent, "Вершина " + Parent.CurrentNode.textNode.Text + ". Значение X вне области определения Х",
            "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (newY > 1 || newY < 0)
            {
                System.Windows.MessageBox.Show(Parent, "Вершина " + Parent.CurrentNode.textNode.Text + ". Значение Y вне области определения Y",
            "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }            

            Point NewPoint = new Point(newX, newY);
            list.Add(NewPoint);

            if (!CheckMembershipFunction.Check(true, this.Parent.CurrentNode.textNode.Text, list, Convert.ToDouble(this.Parent.AxisX.Minimum), Convert.ToDouble(this.Parent.AxisX.Maximum), this.Parent))
            {
                list.Remove(NewPoint);
                return;
            }

            this.Parent.PointsMF.Add(NewPoint);
            this.Parent.MF.ItemsSource = list;

            DescriptionPoint Point = new DescriptionPoint(this, this.Parent, new Point(newX, newY));
            this.StackPointMF.Children.Add(Point);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Parent.CurrentNode.MembershipFunction.Clear();
            foreach (Point point in this.Parent.PointsMF)
            {
                this.Parent.CurrentNode.MembershipFunction.Add(point);
            }
            this.Parent.CurrentNode.UnitMF = (this.Parent.StackStep.Children[0] as DomainMF).Unit.Text;
            this.Parent.CurrentNode.StartXMF = Convert.ToDouble((this.Parent.StackStep.Children[0] as DomainMF).MinAxisX.Text);
            this.Parent.CurrentNode.EndXMF = Convert.ToDouble((this.Parent.StackStep.Children[0] as DomainMF).MaxAxisX.Text);

            this.Parent.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            this.Parent.StackStep.Children[1].IsEnabled = true;
        }
    }
}

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
using System.Windows.Shapes;

namespace FHE.Windows
{
    /// <summary>
    /// Interaction logic for EditMembershipFunc.xaml
    /// </summary>
    public partial class EditMembershipFunc : Window
    {
        public AbstractHierarchyNode CurrentNode
        {
            get;
            private set;
        }

        public List<Point> PointsMF = new List<Point>();

        public EditMembershipFunc(AbstractHierarchyNode node)
        {
            InitializeComponent();
            CurrentNode = node;
            
            //Проверить наличие функции принадлежности
            if (node.MembershipFunction.Count != 0)
            {
                this.MF.ItemsSource = node.MembershipFunction;
                //Отрисовать форму
                print_form(true);
            }
            else
            {
                //Отрисовать форму
                print_form(false);
            }
        }

        private void print_form(bool full)
        {
            DomainMF domain = new DomainMF(this);
            this.StackStep.Children.Insert(this.StackStep.Children.Count, domain);
            this.Graphics.Title = CurrentNode.FullName;

            if (full)
            {
                double start = -1, end = -1;
                domain.Unit.Text = CurrentNode.UnitMF;
                domain.MinAxisX.Text = Convert.ToString(CurrentNode.StartXMF);
                domain.MaxAxisX.Text = Convert.ToString(CurrentNode.EndXMF);
                domain.IsEnabled = false;

                IdealValueMF Ideal = new IdealValueMF(this);
                this.StackStep.Children.Insert(this.StackStep.Children.Count, Ideal);

                foreach (Point point in CurrentNode.MembershipFunction)
                {
                    if (point.Y == 1)
                    {
                        if (start == -1 && end == -1)
                        {
                            start = end = point.X;
                        }
                        if (point.X < start)
                        {
                            start = point.X;
                        }
                        if (point.X > end)
                        {
                            end = point.X;
                        }
                    }
                }

                Ideal.IdealX1.Text = Convert.ToString(start);
                Ideal.IdealX2.Text = Convert.ToString(end);
                Ideal.IsEnabled = false;

                PointsMF Points = new PointsMF(this);
                this.StackStep.Children.Insert(this.StackStep.Children.Count, Points);

                foreach (Point point in CurrentNode.MembershipFunction)
                {
                    if (point.Y != 1)
                    {
                        DescriptionPoint Point = new DescriptionPoint(Points, this, new Point(point.X, point.Y));
                        Points.StackPointMF.Children.Add(Point);
                    }
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Clear();

            String filename;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Файл описания системы (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return;
            }

            MembershipFunction mf = ParserXML.ParseXMLFileToMF(filename);

            List<Point> points = new List<Point>();
            foreach (MFPoint mfpoint in mf.points)
            {
                points.Add(new Point(mfpoint.x, mfpoint.y));
            }

            if (!CheckMembershipFunction.Check(this.CurrentNode.textNode.Text, points, mf.StartX, mf.EndX, this))
            {
                DomainMF curDomain = new DomainMF(this);
                this.StackStep.Children.Insert(this.StackStep.Children.Count, curDomain);
                this.Graphics.Title = CurrentNode.FullName;
                return;
            }

            foreach (Point point in points)
            {
                this.PointsMF.Add(new Point(point.X, point.X));
            }
            this.MF.ItemsSource = points;

            DomainMF domain = new DomainMF(this);
            this.StackStep.Children.Insert(this.StackStep.Children.Count, domain);
            this.Graphics.Title = CurrentNode.FullName;
            double start = -1, end = -1;
            domain.Unit.Text = mf.Unit;
            domain.MinAxisX.Text = Convert.ToString(mf.StartX);
            domain.MaxAxisX.Text = Convert.ToString(mf.EndX);
            domain.IsEnabled = false;

            IdealValueMF Ideal = new IdealValueMF(this);
            this.StackStep.Children.Insert(this.StackStep.Children.Count, Ideal);

            foreach (Point point in points)
            {
                if (point.Y == 1)
                {
                    if (start == -1 && end == -1)
                    {
                        start = end = point.X;
                    }
                    if (point.X < start)
                    {
                        start = point.X;
                    }
                    if (point.X > end)
                    {
                        end = point.X;
                    }
                }
            }

            Ideal.IdealX1.Text = Convert.ToString(start);
            Ideal.IdealX2.Text = Convert.ToString(end);
            Ideal.IsEnabled = false;

            PointsMF Points = new PointsMF(this);
            this.StackStep.Children.Insert(this.StackStep.Children.Count, Points);

            foreach (Point point in points)
            {
                if (point.Y != 1)
                {
                    DescriptionPoint Point = new DescriptionPoint(Points, this, new Point(point.X, point.Y));
                    Points.StackPointMF.Children.Add(Point);
                }
            }
        }

        private void Clear()
        {
            PointsMF.Clear();
            this.MF.ItemsSource = PointsMF;
            this.StackStep.Children.Clear();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            String filename;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Файл описания системы (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return;
            }

            ParserXML.SaveMFToFile(filename, this.PointsMF, (this.StackStep.Children[0] as DomainMF).Unit.Text,
                Convert.ToDouble((this.StackStep.Children[0] as DomainMF).MinAxisX.Text), Convert.ToDouble((this.StackStep.Children[0] as DomainMF).MaxAxisX.Text));
        }
    }
}

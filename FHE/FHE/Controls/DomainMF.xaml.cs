using FHE.Windows;
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
    /// Interaction logic for DomainMF.xaml
    /// </summary>
    public partial class DomainMF : UserControl
    {
        private EditMembershipFunc Parent;
        public DomainMF(EditMembershipFunc parent)
        {
            InitializeComponent();
            this.Parent = parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double minimum = 0, maximum = 0;
            this.Parent.AxisX.Title = this.Unit.Text;
            try
            {
                minimum = Convert.ToDouble(this.MinAxisX.Text);
            }
            catch (FormatException exep)
            {
                System.Windows.MessageBox.Show(Parent, "Вершина " + Parent.CurrentNode.textNode.Text + ". Начальная точка области определения X - ожидалось число",
            "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                maximum = Convert.ToDouble(this.MaxAxisX.Text);
            }
            catch (FormatException exep)
            {
                System.Windows.MessageBox.Show(Parent, "Вершина " + Parent.CurrentNode.textNode.Text + ". Конечная точка области определения X - ожидалось число",
            "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }    
        
            if (minimum > maximum)
            {
                System.Windows.MessageBox.Show(Parent, "Вершина " + Parent.CurrentNode.textNode.Text + ". Начальная точка области определения X не может быть больше конечной точи области определения X",
            "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                this.Parent.AxisX.Minimum = minimum;
                this.Parent.AxisX.Maximum = maximum;
            }
            this.IsEnabled = false;

            IdealValueMF Ideal = new IdealValueMF(this.Parent);
            this.Parent.StackStep.Children.Insert(this.Parent.StackStep.Children.Count, Ideal);
        }
    }
}

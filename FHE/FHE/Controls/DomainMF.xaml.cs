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
            this.Parent.AxisX.Title = this.Unit.Text;
            this.Parent.AxisX.Minimum = Convert.ToDouble(this.MinAxisX.Text);
            this.Parent.AxisX.Maximum = Convert.ToDouble(this.MaxAxisX.Text);
            this.IsEnabled = false;

            IdealValueMF Ideal = new IdealValueMF(this.Parent);
            this.Parent.StackStep.Children.Insert(this.Parent.StackStep.Children.Count, Ideal);
        }
    }
}

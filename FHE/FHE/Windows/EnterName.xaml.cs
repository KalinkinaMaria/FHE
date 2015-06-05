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

namespace FHE.Controls
{
    /// <summary>
    /// Interaction logic for EnterName.xaml
    /// </summary>
    public partial class EnterName : Window
    {
        private string name;

        public string getName()
        {
            return name;
        }

        public EnterName()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            name = this.nameNode.Text;
            this.Close();
        }
    }
}

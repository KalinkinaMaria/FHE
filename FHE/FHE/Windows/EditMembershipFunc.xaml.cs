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

            //Отрисовать форму
            print_form();
        }

        private void print_form()
        {
            DomainMF domain = new DomainMF(this);

            this.StackStep.Children.Insert(this.StackStep.Children.Count, domain);

            this.Graphics.Title = CurrentNode.name;
            
        }
    }
}

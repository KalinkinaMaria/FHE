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
    /// Interaction logic for HierachyGoal.xaml
    /// </summary>
    public partial class HierarchyGoal : AbstractHierarchyNode
    {

        public int getId()
        {
            return Convert.ToInt32(this.textNode.Text.Remove(0, 1));
        }

        public HierarchyGoal()
        {
            InitializeComponent();
            this.formNode.Fill = Brushes.Gold;
        }

        public override void setColorForm()
        {
            this.formNode.Fill = Brushes.Gold;
        }
    }
}

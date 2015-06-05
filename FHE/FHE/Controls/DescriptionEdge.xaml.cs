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
    /// Interaction logic for descriptionEdge.xaml
    /// </summary>
    public partial class DescriptionEdge : UserControl
    {
        public delegate void ChangeEventHandler();

        public event ChangeEventHandler onChange;
        private AbstractHierarchyNode _parentNode;
        private EditEdge _parentWindow;

        public DescriptionEdge(string nodeName, AbstractHierarchyNode parentNode, EditEdge parent)
        {
            InitializeComponent();

            this.node.Content = nodeName;
            _parentNode = parentNode;
            _parentWindow = parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _parentNode.removeChild(Convert.ToInt32(this.node.Content.ToString().Remove(0, 1)));
            _parentWindow.listNode.Items.Add(this.node.Content);
            _parentWindow.stackEdge.Children.Remove(this);
            onChange();
        }
    }
}

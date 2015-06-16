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
    /// Interaction logic for EditFuncLink.xaml
    /// </summary>
    public partial class EditFuncLink : Window
    {
        private AbstractHierarchyNode _currentNode;
        private List<String> args = new List<String>();
        private bool _isCorrect = false;

        public EditFuncLink(AbstractHierarchyNode currentNode)
        {
            InitializeComponent();
            this._currentNode = currentNode;

            //Написать вршину
            this.nameCurrentNode.Text = currentNode.textNode.Text + " (" + currentNode.FullName + ")";

            //Написать от кого она зависит
            foreach (HierarchyNode node in currentNode.childrenNode)
            {
                this.nameChildren.Text += node.textNode.Text + " (" + node.FullName + ")\n";
                args.Add(node.textNode.Text);
            }
            this.nameChildren.Text = this.nameChildren.Text.Remove(this.nameChildren.Text.Length - 1);

            if (currentNode.LinkFunc != "" && currentNode.LinkFunc != null)
            {
                this.nameFunc.Text = currentNode.LinkFunc;
            }
        }

        public string getFuncLink()
        {
            return this.nameFunc.Text;
        }

        public bool isCorrect()
        {
            return this._isCorrect;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<String> current_args = new List<string>();
            String[] args_copy = args.ToArray();

            if (CheckFunctionLinc.check(_currentNode.textNode.Text, this.nameFunc.Text, args_copy, this))
            {
                _isCorrect = true;
                this.Close();
            }

        }
    }
}

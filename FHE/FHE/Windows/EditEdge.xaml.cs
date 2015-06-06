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
    /// Interaction logic for EditEdge.xaml
    /// </summary>
    public partial class EditEdge : Window
    {
        public delegate void ChangeEventHandler();

        public event ChangeEventHandler onChange;

        HierarchyGoal _goal = null;
        HierarchyNode _node = null;
        private const string _foreword = "Редактирование дуг вершины ";
        private Dictionary<int, HierarchyNode> mapNode = new Dictionary<int, HierarchyNode>();
        public EditEdge(HierarchyNode node)
        {
            int i;
            InitializeComponent();

            _node = node;

            this.nameWindow.Content = _foreword + "X" + node.id + " (" + node.name + ")";
            this.fillEdge(node.childrenNode);
            Grid t = (node.Parent as Grid);
            t = t.Parent as Grid;
            HierarchyLevel r = t.Parent as HierarchyLevel;
            StackPanel stackLevel = (r.Parent as StackPanel);
            for (i = stackLevel.Children.IndexOf(r) + 1; i < stackLevel.Children.Count - 1; i++)
            {
                List<HierarchyNode> list = ((HierarchyLevel)stackLevel.Children[i]).getNodes();
                foreach (HierarchyNode index in list)
                {
                    mapNode.Add(index.id, index);
                }
            }
            int[] keys = new int[mapNode.Keys.Count];
            mapNode.Keys.CopyTo(keys, 0);
            foreach (int index in keys)
            {
                if (!node.containsChild(index))
                    this.listNode.Items.Add("X" + index);
            }
            this.listNode.SelectedIndex = 0;
        }

        public EditEdge(HierarchyGoal goal)
        {
            int i;
            InitializeComponent();

            _goal = goal;

            this.nameWindow.Content = _foreword + "Q" + goal.getId() + " (" + goal.name + ")";
            this.fillEdge(goal.childrenNode);
            Grid t = (goal.Parent as Grid);
            t = t.Parent as Grid;
            HierarchyLevel r = t.Parent as HierarchyLevel;
            StackPanel stackLevel = (r.Parent as StackPanel);
            for (i = 1; i < stackLevel.Children.Count - 1; i++ )
            {
                List<HierarchyNode> list = ((HierarchyLevel)stackLevel.Children[i]).getNodes();
                foreach (HierarchyNode node in list)
                {
                    mapNode.Add(node.id, node);
                }
            }
            int[] keys = new int[mapNode.Keys.Count];
            mapNode.Keys.CopyTo(keys, 0);
            foreach (int index in keys)
            {
                if (!goal.containsChild(index))
                    this.listNode.Items.Add("X" + index);
            }
            this.listNode.SelectedIndex = 0;
        }

        //????????????????????????????????
        private void fillEdge(List<HierarchyNode> childrenNode)
        {
            foreach (HierarchyNode child in childrenNode)
            {
                DescriptionEdge addingCanvas = null;
                if (_node != null)
                {
                    addingCanvas = new DescriptionEdge(child.textNode.Text, _node, this);
                }
                else if (_goal != null)
                {
                    addingCanvas = new DescriptionEdge(child.textNode.Text, _goal, this);
                }

                addingCanvas.onChange += this.fairOnChange;
                this.stackEdge.Children.Insert(this.stackEdge.Children.Count, addingCanvas);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.listNode.SelectedValue == null)
                return;
            
            DescriptionEdge descEdge = null;
            int index = Convert.ToInt32((this.listNode.SelectedValue as string).Remove(0, 1));

            if (_node != null)
            {
                _node.addChild(mapNode[index]);
                mapNode[index].ParentNode.Add(_node);
                descEdge = new DescriptionEdge(this.listNode.SelectedValue as string, _node, this);
            }
            else if (_goal != null)
            {
                _goal.addChild(mapNode[index]);
                mapNode[index].ParentNode.Add(_goal);
                descEdge = new DescriptionEdge(this.listNode.SelectedValue as string, _goal, this);
            }
            else
                return;

            this.listNode.Items.Remove(this.listNode.SelectedItem);
            this.listNode.SelectedIndex = 0;
            this.stackEdge.Children.Insert(this.stackEdge.Children.Count, descEdge);

            descEdge.onChange += this.fairOnChange;
            onChange();

        }

        public void fairOnChange()
        {
            onChange();
        }
    }
}

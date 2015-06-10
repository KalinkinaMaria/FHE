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
    /// Interaction logic for HierarchyNode.xaml
    /// </summary>
    public partial class HierarchyNode : AbstractHierarchyNode
    {
        private static int global_id = 1;
        public int id
        {
            get;
            private set;
        }

        public HierarchyNode()
            : base()
        {
            InitializeComponent();
            this.formNode.Fill = Brushes.LightGreen;
            id = global_id;
            global_id += 1;
        }

        public HierarchyNode(int id)
            : base()
        {
            InitializeComponent();
            this.formNode.Fill = Brushes.LightGreen;
            this.id = id;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && !this.textNode.Text.Contains("Q"))
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        public override void setColorForm()
        {
            this.formNode.Fill = Brushes.LightGreen;
        }
    }
}

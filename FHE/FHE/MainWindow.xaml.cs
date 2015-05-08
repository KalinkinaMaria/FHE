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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FHE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Mode {EDIT_HIERARCHY, EDIT_FUNC_LINK, EDIT_FUNK_MEMBERSHIP, CALC_RESULT};

        public static Mode _mode
        {
            get;
            private set;
        }

        public MainWindow()
        {
            _mode = Mode.EDIT_HIERARCHY;
            InitializeComponent();
        }

        private void Add_Level_Click(object sender, RoutedEventArgs e)
        {
            HierarchyLevel addingCanvas = new HierarchyLevel();
            this.stackLevel.Children.Insert(this.stackLevel.Children.Count - 1, addingCanvas);
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HierachyGoal addingNode = new HierachyGoal();
            addingNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            addingNode.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            addingNode.textNode.Text = "Q" + (this.targetLevel.ColumnDefinitions.Count + 1);
            Grid.SetColumn(addingNode, this.targetLevel.ColumnDefinitions.Count);
            this.targetLevel.ColumnDefinitions.Add(new ColumnDefinition());
            this.targetLevel.Children.Add(addingNode);
        }

        private void Mode_Up(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case Mode.EDIT_HIERARCHY:
                    _mode = Mode.EDIT_FUNC_LINK;
                    this.back.IsEnabled = true;
                    break;
                case Mode.EDIT_FUNC_LINK:
                    _mode = Mode.EDIT_FUNK_MEMBERSHIP;
                    this.forward.Content = "Рассчитать";
                    break;
                case Mode.EDIT_FUNK_MEMBERSHIP:
                    _mode = Mode.CALC_RESULT;
                    this.forward.IsEnabled = false;
                    //TO DO открыть окно с рассчетом
                    break;
                case Mode.CALC_RESULT:
                    break;
            }
        }

        private void Mode_Down(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case Mode.EDIT_HIERARCHY:
                    break;
                case Mode.EDIT_FUNC_LINK:
                    _mode = Mode.EDIT_HIERARCHY;
                    this.back.IsEnabled = false;
                    break;
                case Mode.EDIT_FUNK_MEMBERSHIP:
                    _mode = Mode.EDIT_FUNC_LINK;
                    this.forward.Content = "Вперед";
                    break;
                case Mode.CALC_RESULT:
                    this.forward.IsEnabled = true;
                    _mode = Mode.EDIT_FUNK_MEMBERSHIP;
                    break;
            }
        }
    }
}

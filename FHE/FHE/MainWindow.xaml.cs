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
using xFunc.Maths;

namespace FHE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Mode { EDIT_HIERARCHY, EDIT_FUNC_LINK, EDIT_FUNK_MEMBERSHIP, CALC_RESULT };

        public static Mode mode
        {
            get;
            private set;
        }

        public MainWindow()
        {
            mode = Mode.EDIT_HIERARCHY;

            InitializeComponent();
            HierarchyLevelForGoal addingCanvas = new HierarchyLevelForGoal(this.stackLevel.Children.Count);
            addingCanvas.onChange += this.repaintEdge;
            this.stackLevel.Children.Insert(0, addingCanvas);
            //StackPanel stackLevel = (this.targetLevel.Parent as StackPanel);
        }

        public void repaintEdge()
        {
            //удалить старые линии
            this.canvasLines.Children.Clear();

            //получить и отрисовать новые дуги
            for (int i = 0; i < this.stackLevel.Children.Count; i++)
            {
                if (this.stackLevel.Children[i] is HierarchyLevel)
                {
                    foreach (AbstractHierarchyNode node in (this.stackLevel.Children[i] as HierarchyLevel).stackNode.Children)
                    {
                        foreach (HierarchyNode _node in node.childrenNode)
                        {
                            Line line = new Line();
                            line.X1 = node.Position.X;
                            line.Y1 = node.Position.Y;
                            line.X2 = _node.Position.X;
                            line.Y2 = _node.Position.Y;
                            line.StrokeThickness = 1;
                            line.Stroke = Brushes.Black;
                            this.canvasLines.Children.Add(line);
                        }
                    }
                }
            }
        }

        private void Add_Level_Click(object sender, RoutedEventArgs e)
        {
            HierarchyLevel addingCanvas = new HierarchyLevelForNode(this.stackLevel.Children.Count);
            this.stackLevel.Children.Insert(this.stackLevel.Children.Count - 1, addingCanvas);

            addingCanvas.onChange += this.repaintEdge;
        }

        private void paint_node_for_func_link()
        {

        }

        private void close_function()
        {
            int count = this.stackLevel.Children.Count;

            for (int i = 1; i < count - 1; i++)
            {
                (this.stackLevel.Children[i] as HierarchyLevel).deleteButton.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private bool is_correct()
        {
            bool result = true;

            int count = this.stackLevel.Children.Count;

            for (int i = 0; i < count - 1; i++)
            {
                if ((this.stackLevel.Children[i] is Grid))
                {
                    //проверить цели
                }
                else if (!(this.stackLevel.Children[i] as HierarchyLevel).containsDependence())
                {
                    result = false;
                    return result;
                }
            }

            return result;
        }

        private void Mode_Up(object sender, RoutedEventArgs e)
        {
            switch (mode)
            {
                case Mode.EDIT_HIERARCHY:
                    //TO DO Проверка корректности введенного графа
                    if (!is_correct())
                    {
                        //переделать сообщ.
                        System.Windows.MessageBox.Show(this, "Характеристики средних уровней не имеют зависимсотей. Переместите характеристику на нижний уровень или добавте зависимости.",
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    mode = Mode.EDIT_FUNC_LINK;
                    this.nameMode.Text = "Определение зависимости между характеристиками путем задания функций связи";
                    this.back.IsEnabled = true;

                    //TO DO Закрасить вершины, для которых неодходимо добавить функции связи
                    close_function();
                    paint_node_for_func_link();
                    break;
                case Mode.EDIT_FUNC_LINK:
                    mode = Mode.EDIT_FUNK_MEMBERSHIP;
                    this.nameMode.Text = "Определение желательности достижения цели и возможности реализации характеристик";
                    this.forward.Content = "Рассчитать";
                    //TO DO Закрасить вершины, для которых неодходимо добавить функции принадлежности
                    break;
                case Mode.EDIT_FUNK_MEMBERSHIP:
                    mode = Mode.CALC_RESULT;
                    this.forward.IsEnabled = false;
                    this.nameMode.Text = "Рассчитать оптимальное достижение цели";
                    //TO DO открыть окно с рассчетом
                    break;
                case Mode.CALC_RESULT:
                    break;
            }
        }

        private void Mode_Down(object sender, RoutedEventArgs e)
        {
            switch (mode)
            {
                case Mode.EDIT_HIERARCHY:
                    break;
                case Mode.EDIT_FUNC_LINK:
                    mode = Mode.EDIT_HIERARCHY;
                    this.back.IsEnabled = false;
                    this.nameMode.Text = "Создание иерархии характеристик";
                    //TO DO Отменить закрашивание
                    break;
                case Mode.EDIT_FUNK_MEMBERSHIP:
                    mode = Mode.EDIT_FUNC_LINK;
                    this.nameMode.Text = "Определение зависимости между характеристиками путем задания функций связи";
                    this.forward.Content = "Вперед";
                    //TO DO Закрасить вершины, для которых неодходимо добавить функции связи
                    break;
                case Mode.CALC_RESULT:
                    this.forward.IsEnabled = true;
                    this.nameMode.Text = "Определение желательности достижения цели и возможности реализации характеристик";
                    mode = Mode.EDIT_FUNK_MEMBERSHIP;
                    //TO DO Закрасить вершины, для которых неодходимо добавить функции принадлежности
                    break;
            }
        }
    }
}

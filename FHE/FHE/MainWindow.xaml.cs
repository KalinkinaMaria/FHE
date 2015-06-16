using FHE.Controls;
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
using System.Xml;
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
        }

        public void Clear()
        {
            for( int i = 0; i < this.stackLevel.Children.Count; i ++ )
            {
                if (this.stackLevel.Children[i] is HierarchyLevelForNode)
                {
                    this.stackLevel.Children.RemoveAt(i);
                    i--;
                }
            }

            (this.stackLevel.Children[0] as HierarchyLevelForGoal).stackNode.Children.Clear();

            //Парсер
            ParserXML.Clear();

            //Перерисовать дуги
            repaintEdge();

            //Обнулить id
            HierarchyNode.clearGlobalId();
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
            int count = this.stackLevel.Children.Count;

            for (int i = 0; i < count - 2; i++)
            {
                (this.stackLevel.Children[i] as HierarchyLevel).paint_node_for_func_link();
            }
        }

        private void close_function()
        {
            int count = this.stackLevel.Children.Count;

            for (int i = 1; i < count - 1; i++)
            {
                (this.stackLevel.Children[i] as HierarchyLevel).deleteButton.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void open_function()
        {
            int count = this.stackLevel.Children.Count;

            for (int i = 1; i < count - 1; i++)
            {
                (this.stackLevel.Children[i] as HierarchyLevel).deleteButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private bool is_correct()
        {
            bool result = true;
            int count = this.stackLevel.Children.Count;
            switch (mode)
            {
                case Mode.EDIT_HIERARCHY:

                    for (int i = 0; i < count - 2; i++)
                    {
                        if (!(this.stackLevel.Children[i] as HierarchyLevel).containsDependence())
                        {
                            result = false;
                            return result;
                        }
                    }
                    if (!(this.stackLevel.Children[this.stackLevel.Children.Count-2] as HierarchyLevel).containsParentNode())
                    {
                        result = false;
                        return result;
                    }
                break;
                case Mode.EDIT_FUNC_LINK:

                    for (int i = 0; i < count - 2; i++)
                    {
                        if (!(this.stackLevel.Children[i] as HierarchyLevel).containsFuncLink())
                        {
                            //переделать сообщ.
                            System.Windows.MessageBox.Show(this, "Не для всех характеристик были введены ф-ции связи.",
                   "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                            result = false;
                            return result;
                        }
                        if (!(this.stackLevel.Children[i] as HierarchyLevel).correctFuncLink(this))
                        {
                            result = false;
                            return result;
                        }
                    }
                break;
            }

            return result;
        }

        private void Mode_Up(object sender, RoutedEventArgs e)
        {
            int count = this.stackLevel.Children.Count;
            int countNode = 0;
            switch (mode)
            {
                case Mode.EDIT_HIERARCHY:
                    //Проверка корректности введенного графа
                    if (!is_correct())
                    {
                        //переделать сообщ.
                        System.Windows.MessageBox.Show(this, "Неверная структура системы: имеется несвязная вершина",
               "Для перехода требуется верная структура иерархии", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    for (int i = 0; i < count - 1; i++)
                    {
                        countNode += (this.stackLevel.Children[i] as HierarchyLevel).stackNode.Children.Count;
                        if (countNode == 0)
                        {
                            System.Windows.MessageBox.Show(this, "Не добавлена ни одна вершина.",
               "Для перехода требуется верная структура иерархии", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    mode = Mode.EDIT_FUNC_LINK;
                    this.nameMode.Text = "Определение зависимости между характеристиками путем задания функций связи (для выделенных вершин)";
                    this.back.IsEnabled = true;
                    this.addLevel.IsEnabled = false;

                    //TO DO Закрасить вершины, для которых неодходимо добавить функции связи
                    close_function();
                    paint_node_for_func_link();
                    break;
                case Mode.EDIT_FUNC_LINK:
                    //Проверка заполненности всех ф-ций связи
                    if (!is_correct())
                    {
                        return;
                    }
                    mode = Mode.EDIT_FUNK_MEMBERSHIP;
                    this.nameMode.Text = "Определение желательности достижения цели и возможности реализации характеристик";
                    this.forward.Content = "Рассчитать";
                    //TO DO Закрасить вершины, для которых неодходимо добавить функции принадлежности
                    print_node_for_MF();
                    break;
                case Mode.EDIT_FUNK_MEMBERSHIP:
                    mode = Mode.CALC_RESULT;
                    this.forward.IsEnabled = false;
                    this.nameMode.Text = "Рассчитать оптимальное достижение цели";
                    //TO DO открыть окно с рассчетом
                    ResultingWindow Window = new ResultingWindow((this.stackLevel.Children[0] as HierarchyLevelForGoal).GetGoals());
                    Window.ShowDialog();
                    break;
                case Mode.CALC_RESULT:
                    break;
            }
        }

        private void print_node_for_MF()
        {
            HierarchyLevel FirstLevel = (this.stackLevel.Children[0] as HierarchyLevel);
            for (int i = 0; i < FirstLevel.stackNode.Children.Count; i++)
            {
                (FirstLevel.stackNode.Children[i] as AbstractHierarchyNode).formNode.Fill = Brushes.Orange;
                (FirstLevel.stackNode.Children[i] as AbstractHierarchyNode).IsNeedMF = true;
            }

            HierarchyLevel LastLevel = (this.stackLevel.Children[this.stackLevel.Children.Count - 2] as HierarchyLevel);
            for (int i = 0; i < LastLevel.stackNode.Children.Count; i++)
            {
                (LastLevel.stackNode.Children[i] as AbstractHierarchyNode).formNode.Fill = Brushes.Orange;
                (LastLevel.stackNode.Children[i] as AbstractHierarchyNode).IsNeedMF = true;
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
                    this.addLevel.IsEnabled = true;
                    //TO DO Отменить закрашивание
                    open_function();
                    paint_node_for_start();
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

        private void paint_node_for_start()
        {
            int count = this.stackLevel.Children.Count;

            for (int i = 0; i < count - 2; i++)
            {
                (this.stackLevel.Children[i] as HierarchyLevel).paint_node_for_start();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Clear();

            String filename;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Файл описания системы (.xml)|*.xml"; 

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return;
            }

            List<Goal> Nodes = ParserXML.ParseXMLFile(filename);

            if (Nodes == null)
            {
                System.Windows.MessageBox.Show(this, "Некорректный входной файл.",
            "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Перевод в графическое представление
            this.stackLevel.Children.RemoveAt(0);
            ConvertModel.FromModelToView(Nodes, this.stackLevel);

            for (int i = 0; i < this.stackLevel.Children.Count - 1; i ++)
            {
                (this.stackLevel.Children[i] as HierarchyLevel).onChange += this.repaintEdge;
            }

            //Замена 
            repaintEdge();

            this.Title = this.Title.Remove(0, this.Title.LastIndexOf("-")).Insert(0, filename);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            String filename = this.Title.Remove(this.Title.LastIndexOf("-"));

            List<HierarchyLevel> levels = new List<HierarchyLevel>();
            for (int i = 0; i < this.stackLevel.Children.Count - 1; i++)
            {
                levels.Add(this.stackLevel.Children[i] as HierarchyLevel);
            }

            ParserXML.SaveToFile(filename, levels);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            String filename;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Файл описания системы (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return;
            }

            List<HierarchyLevel> levels = new List<HierarchyLevel>();
            for (int i = 0; i < this.stackLevel.Children.Count - 1; i ++ )
            {
                levels.Add(this.stackLevel.Children[i] as HierarchyLevel);
            }

            ParserXML.SaveToFile(filename, levels);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Clear();

            this.Title = "Новый документ - FHE";
        }
    }
}

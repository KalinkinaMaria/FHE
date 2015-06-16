using FHE.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ResultingWindow.xaml
    /// </summary>
    public partial class ResultingWindow : Window
    {
        private static ProgressBar CalculatePB;
        private static int FactorIncProgressBar;
        private Process CalculateProcess;
        private List<HierarchyGoal> Goals;

        public ResultingWindow(List <HierarchyGoal> Goals)
        {
            InitializeComponent();
            this.Goals = Goals;
            ResultingWindow.CalculatePB = this.ProgressCalculation;

            //Вывод желательности достижения цели
            this.AxisX.Minimum = this.Goals[0].StartXMF;
            this.AxisX.Maximum = this.Goals[0].EndXMF;
            this.AxisX.Title = this.Goals[0].UnitMF;
            this.MF.ItemsSource = this.Goals[0].MembershipFunction;

            //Расчет коэффициента прирощения progress bar
            ResultingWindow.FactorIncProgressBar = 0;
            foreach (HierarchyGoal goal in this.Goals)
            {
                ResultingWindow.FactorIncProgressBar += goal.CountEdgesWithChild();
            }
        }

        private void StartCalculate_Click(object sender, RoutedEventArgs e)
        {
            this.StartCalculate.Visibility = System.Windows.Visibility.Hidden;

            CalculateProcess = new Process(Goals);
            CalculateProcess.StartCalculation();
            this.ProgressCalculation.IsIndeterminate = true;

            //Вывод результата
            List<Point> result =  CalculateProcess.GetResultMembershipFunction(0);
            this.SMF.ItemsSource = result;

            //Поиск пересечения
            this.SaveResults.Visibility = System.Windows.Visibility.Visible;
            this.StackDefinitionResults.Visibility = System.Windows.Visibility.Visible;
            this.ProgressCalculation.Visibility = System.Windows.Visibility.Hidden;
            this.StartCalculate.Visibility = System.Windows.Visibility.Hidden;
            List<MFPoint> results = CalculateProcess.GetResults();
            PrintResult(results[0], Goals[0]);
        }

        private void PrintResult(MFPoint ResultPoint, HierarchyGoal ResultGoal)
        {
            TextBlock strResult = new TextBlock();
            strResult.Text = "Оптимальное решение: ";
            strResult.TextWrapping = TextWrapping.WrapWithOverflow;
            this.StackDefinitionResults.Children.Add(strResult);

            TextBlock goalResult = new TextBlock();
            goalResult.TextWrapping = TextWrapping.WrapWithOverflow;
            goalResult.Text = ResultGoal.textNode.Text;
            goalResult.Text += " (";
            goalResult.Text += ResultGoal.FullName;
            goalResult.Text += ") = ";
            goalResult.Text += Convert.ToString(ResultPoint.x);
            goalResult.Text += " ";
            goalResult.Text += ResultPoint.Unit;
            this.StackDefinitionResults.Children.Add(goalResult);

            TextBlock strResult2 = new TextBlock();
            strResult2.Text = "Цель достигается при следующих значениях характеристик: ";
            strResult2.TextWrapping = TextWrapping.WrapWithOverflow;
            this.StackDefinitionResults.Children.Add(strResult2);

            foreach (String nameX in ResultPoint.lambda.Keys)
            {
                TextBlock characteristic = new TextBlock();
                characteristic.Text = nameX;
                characteristic.Text += " = ";
                characteristic.Text += Convert.ToString(ResultPoint.lambda[nameX].x);
                characteristic.Text += " ";
                characteristic.Text += ResultPoint.lambda[nameX].Unit;
                characteristic.TextWrapping = TextWrapping.WrapWithOverflow;
                this.StackDefinitionResults.Children.Add(characteristic);
            }
        }
    }
}

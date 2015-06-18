using FHE.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FHE.Windows
{
    /// <summary>
    /// Interaction logic for ResultingWindow.xaml
    /// </summary>
    public partial class ResultingWindow : Window
    {
        private Process CalculateProcess;
        private List<HierarchyGoal> Goals;
        private BackgroundWorker backgroundWorker;
        private List<Point> result;

        public ResultingWindow(List <HierarchyGoal> Goals)
        {
            InitializeComponent();
            backgroundWorker = (BackgroundWorker)this.FindResource("backgroundWoker");

            this.Goals = Goals;

            //Вывод желательности достижения цели
            this.Graphics.Title = this.Goals[0].FullName;
            this.AxisX.Minimum = this.Goals[0].StartXMF;
            this.AxisX.Maximum = this.Goals[0].EndXMF;
            this.AxisX.Title = this.Goals[0].UnitMF;
            this.MF.ItemsSource = this.Goals[0].MembershipFunction;
        }

        private void StartCalculate_Click(object sender, RoutedEventArgs e)
        {
            this.StartCalculate.Visibility = System.Windows.Visibility.Hidden;
            this.ProgressCalculation.IsIndeterminate = true;
            CalculateProcess = new Process(Goals);
            backgroundWorker.RunWorkerAsync();
        }

        private void PrintResult(MFPoint ResultPoint, HierarchyGoal ResultGoal)
        {
            int countStr = 3;
            if (ResultPoint != null)
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
                    countStr++;
                }
                this.Height += countStr * 10;
            }
            else
            {
                TextBlock NotResult = new TextBlock();
                NotResult.Text = "Нет точки пересечения.";
                NotResult.TextWrapping = TextWrapping.WrapWithOverflow;
                this.StackDefinitionResults.Children.Add(NotResult);
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CalculateProcess.StartCalculation();            
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.ProgressCalculation.Visibility = Visibility.Hidden;
                
                //Вывод результата
                List<Point> result = CalculateProcess.GetResultMembershipFunction(0);
                this.SMF.ItemsSource = result;

                //Поиск пересечения
                this.StackDefinitionResults.Visibility = System.Windows.Visibility.Visible;
                this.StartCalculate.Visibility = System.Windows.Visibility.Hidden;
                List<MFPoint> results = CalculateProcess.GetResults();
                PrintResult(results[0], Goals[0]);
            }
        }
    }
}

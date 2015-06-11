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

            //Вывод результата
            List<Point> result =  CalculateProcess.GetResultMembershipFunction(0);
            this.SMF.ItemsSource = result;
        }

        public static void IncProgressBar()
        {
            ResultingWindow.CalculatePB.Value += 100 / ResultingWindow.FactorIncProgressBar;
        }
    }
}

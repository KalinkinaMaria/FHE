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
        private enum CurrentValue {NONE, OPERATION, X, CONST, POINT, NULL, CLOSE_BRACKET, VAR, FLOAT};
        private AbstractHierarchyNode _currentNode;
        private List<String> args = new List<String>();
        private bool _isCorrect = false;

        public EditFuncLink(AbstractHierarchyNode currentNode)
        {
            InitializeComponent();
            this._currentNode = currentNode;

            //Написать вршину
            this.nameCurrentNode.Text = currentNode.textNode.Text + " (" + currentNode.name + ")";

            //Написать от кого она зависит
            foreach (HierarchyNode node in currentNode.childrenNode)
            {
                this.nameChildren.Text += node.textNode.Text + " (" + node.name + ")\n";
                args.Add(node.textNode.Text);
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
            String[] args_copy = args.ToArray();
            List<String> current_args = new List<string>();

            //Проверить введенную функцию
            if (!check())
                return;
            
            //Количество скобок
            string str = this.nameFunc.Text;
            int countBracket = 0;
            for (int i = 0; i < str.Length; i ++ )
            {
                if (str[i] == '(')
                {
                    countBracket++;
                }
                if (str[i] == ')')
                {
                    countBracket--;
                }
                if (countBracket < 0)
                {
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: неправильная расстановка скобок",
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (countBracket != 0)
            {
                System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: неправильная расстановка скобок",
           "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Заполнить массив текущих аргументов
            char[] delimiters = new char[] { '+', '-', '*', '/', '^', '(', ')' };
            string[] parts = str.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i ++ )
            {
                if (parts[i][0] == 'x' || parts[i][0] == 'X')
                {
                    current_args.Add(parts[i]);
                }
            }
            for (int i = 0; i < current_args.Count; i ++ )
            {
                current_args[i] = current_args[i].ToUpper();
            }

            //Проверить аргументы
            //Проверить, не забыли ли кого-нибудь
            bool available = false;
            for (int i = 0; i < args_copy.Length; i ++ )
            {
                foreach (string s in current_args)
                {
                    if (s == args_copy[i])
                    {
                        available = true;
                    }
                }
                if (!available)
                {
                    System.Windows.MessageBox.Show(this, "В функции описаны не все переменные, от которых зависит вершина: " + args_copy[i],
                "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                while (current_args.Remove(args_copy[i])) ;
            }

            //Проверить, не ввели ли лишнего
            if (current_args.Count > 0)
            {
                string argsOver = "";
                foreach (string s in current_args)
                {
                    argsOver += s + " ";
                }
                System.Windows.MessageBox.Show(this, "Введены лишние переменные: " + argsOver,
           "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _isCorrect = true;
            this.Close();
        }

        private bool check()
        {
            CurrentValue value = CurrentValue.NONE;
            string str = this.nameFunc.Text;
            str = str.Replace(" ", "");
            char chr;

            for (int i = 0; i < str.Length; i++)
            {
                chr = str[i];

                //Проверка первого символа
                if (value == CurrentValue.NONE && (chr == '.' || chr == '+' || chr == '*' 
                    || chr == '/' || chr == '^' || chr == '^'))
                {
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: первым символом выражения не может быть "+chr,
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после .
                else if (value == CurrentValue.POINT && (chr == '.' || chr == '+' || chr == '*'
                    || chr == '/' || chr == '^' || chr == '-' || chr == '(' || chr == ')' || chr == 'x' || chr == 'X'))
                {
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после символа . не может быть " + chr + " место: " + (i+1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после 0
                else if (value == CurrentValue.NULL && chr != '.')
                {
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после символа 0 не может быть " + chr + " место: " + (i + 1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после float
                else if (value == CurrentValue.FLOAT && (chr == '.' || chr == '(' || chr == 'x' || chr == 'X'))
                {
                    //ошиба
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после числа с плавающей точкой не может быть " + chr + " место: " + (i + 1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после const
                else if (value == CurrentValue.CONST && (chr == 'x' || chr == 'X' || chr == '('))
                {
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после целочисленной константы не может быть " + chr + " место: " + (i + 1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после x
                else if (value == CurrentValue.X && (chr == 'x' || chr == 'X' || chr == '(' || chr == '.' || chr == '+' || chr == '*'
                    || chr == '/' || chr == '^' || chr == '-' || chr == '0' || chr == ')'))
                {
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после символа x не может быть " + chr + " место: " + (i + 1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после операции 
                else if (value == CurrentValue.OPERATION && (chr == ')' || chr == '.' || chr == '+' || chr == '*'
                    || chr == '/' || chr == '^' || chr == '-'))
                {
                    //Проверка символа после (
                    if (value == CurrentValue.OPERATION && str[i-1] == '(' && (chr == '-'))
                    {
                        return true;
                    }
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после операции не может быть " + chr + " место: " + (i + 1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после )
                else if (value == CurrentValue.CLOSE_BRACKET && (chr == '0' || chr == '1' || chr == '2' || chr == '3' || chr == '4'
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9' || chr == '(' || chr == 'x' || chr == 'X'))
                {
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после символа ) не может быть " + chr + " место: " + (i + 1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //Проверка символа после var
                else if (value == CurrentValue.VAR && (chr == '.' || chr == 'x' || chr == 'X' || chr == '('))
                {
                    //ошибка
                    System.Windows.MessageBox.Show(this, "Синтаксическая ошибка: после переменной не может быть " + chr + " место: " + (i + 1),
               "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else 
                {
                    value = determineValue(value, chr);
                }
            }
            return true;
        }

        private CurrentValue determineValue(CurrentValue value, char chr)
        {
            //Первый символ
            if (value == CurrentValue.NONE && chr == '0')
                return CurrentValue.NULL;
            if (value == CurrentValue.NONE && (chr == '1' || chr == '2' || chr == '3'|| chr == '4' 
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9'))
            {
                return CurrentValue.CONST;
            }
            if (value == CurrentValue.NONE && chr == '(')
            {
                return CurrentValue.OPERATION;
            }

            //Символ после .
            if (value == CurrentValue.POINT && (chr == '0' || chr == '1' || chr == '2' || chr == '3' || chr == '4'
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9'))
            {
                return CurrentValue.FLOAT;
            }

            //Символ после )
            if (value == CurrentValue.CLOSE_BRACKET && (chr == '+' || chr == '-' || chr == '*'
                || chr == '/' || chr == '^'))
            {
                return CurrentValue.OPERATION;
            }
            if (value == CurrentValue.CLOSE_BRACKET && (chr == ')'))
            {
                return CurrentValue.CLOSE_BRACKET;
            }

            //После константы
            if (value == CurrentValue.CONST && (chr == '0' || chr == '1' || chr == '2' || chr == '3' || chr == '4'
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9'))
            {
                return CurrentValue.CONST;
            }
            if (value == CurrentValue.CONST && chr == '.')
            {
                return CurrentValue.POINT;
            }
            if (value == CurrentValue.CONST && chr == ')')
            {
                return CurrentValue.CLOSE_BRACKET;
            }
            if (value == CurrentValue.CONST && (chr == '+' || chr == '-' || chr == '*'
                || chr == '/' || chr == '^'))
            {
                return CurrentValue.OPERATION;
            }

            //После float
            if (value == CurrentValue.FLOAT && (chr == '+' || chr == '-' || chr == '*'
                || chr == '/' || chr == '^'))
            {
                return CurrentValue.OPERATION;
            }
            if (value == CurrentValue.FLOAT && (chr == '0' || chr == '1' || chr == '2' || chr == '3' || chr == '4'
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9'))
            {
                return CurrentValue.FLOAT;
            }
            if (value == CurrentValue.FLOAT && chr == ')')
            {
                return CurrentValue.CLOSE_BRACKET;
            }

            //После 0
            if (value == CurrentValue.NULL && chr == '.')
            {
                return CurrentValue.POINT;
            }

            //После операции
            if (value == CurrentValue.OPERATION && (chr == '1' || chr == '2' || chr == '3' || chr == '4'
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9'))
            {
                return CurrentValue.CONST;
            }
            if (value == CurrentValue.OPERATION && chr == '0')
            {
                return CurrentValue.NULL;
            }
            if (value == CurrentValue.OPERATION && (chr == '-' || chr == '('))
            {
                return CurrentValue.OPERATION;
            }
            if (value == CurrentValue.OPERATION && (chr == 'x' || chr == 'X'))
            {
                return CurrentValue.X;
            }

            //После переменной
            if (value == CurrentValue.VAR && (chr == '0' || chr == '1' || chr == '2' || chr == '3' || chr == '4'
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9'))
            {
                return CurrentValue.VAR;
            }
            if (value == CurrentValue.VAR && (chr == '+' || chr == '-' || chr == '*'
                || chr == '/' || chr == '^'))
            {
                return CurrentValue.OPERATION;
            }
            if (value == CurrentValue.VAR && chr == ')')
            {
                return CurrentValue.CLOSE_BRACKET;
            }

            //После X
            if (value == CurrentValue.X && (chr == '1' || chr == '2' || chr == '3' || chr == '4'
                 || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9'))
            {
                return CurrentValue.VAR;
            }

            return CurrentValue.NONE;
        }
    }
}

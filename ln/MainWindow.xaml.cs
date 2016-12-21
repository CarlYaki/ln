using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ln
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            num1.Text = (-7 / 4).ToString();
            num2.Text = (-7 % 4).ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bigNum bn1 = new bigNum(num1.Text), bn2 = new bigNum(num2.Text);
            bigNum ans = bn1 * bn2;
            string toshow = ans.neg ? "-" : "";
            for (int i = ans.cnt - 1; i >= ans.dot; --i)
                toshow += ans.num[i].ToString();
            toshow += ".";
            for (int i = ans.dot - 1; i >= 0; --i)
                toshow += ans.num[i].ToString();
            MessageBox.Show(toshow);
        }
    }
}

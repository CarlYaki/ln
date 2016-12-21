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
        }

        /*
         *      a. Taylor展开（最佳或近似最佳逼近）；
         *      b. 数值积分；
         *      c. 非Taylor展开的函数逼近方法（选作）；
         */

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //--------------减半Taylor展开---------------
            bigNum input = new bigNum(num.Text);
            bigNum E = new bigNum(Math.E.ToString());
            bigNum two = new bigNum("2");
            bigNum temp = two;
            int r;
            for (int i = 1; ; ++i)
            {
                if (temp > input)
                {
                    r = i - 1;
                    break;
                }
                temp = temp * two;
            }
            temp = temp / two;
            bigNum a = input / temp - new bigNum(1.ToString());
            a.show();
            bigNum R = new bigNum(r.ToString());
            R.show();

            bigNum ans = new bigNum("0"), an = new bigNum(a);
            bigNum minusone = new bigNum("-1");
            int n=1;
            while (true)
            {
                ans = ans + (an / (new bigNum(n.ToString())));
                n++;
                an = an * minusone * a;
                if (n > 20)
                    break;
            }
            ans.show();
        }
    }
}

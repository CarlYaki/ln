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
        int acc = 20;
        public MainWindow()
        {
            InitializeComponent();
            calc.E = new bigNum(Math.E.ToString());
            calc.one = new bigNum("1");
            calc.two = new bigNum("2");

            calc.sqrt2 = bigNum.sqrt(2);
            //calc.sqrt2.show();


            bigNum sqrt2_1 = calc.sqrt2 - calc.one;
            calc.ln2 = new bigNum("0");
            bigNum an=new bigNum(sqrt2_1);
            for (int i = 1; i <= 100; ++i)
            {
                calc.ln2 = calc.ln2 + an / (new bigNum(i.ToString()));
                an = an * sqrt2_1;
                an.neg = (i % 2 == 1 ? true : false);
            }
            calc.ln2 = calc.ln2 * calc.two;
            //calc.ln2.show();
        }

        /*
         *      a. Taylor展开（最佳或近似最佳逼近）；
         *      b. 数值积分；
         *      c. 非Taylor展开的函数逼近方法（选作）；
         */

        private void button_Click(object sender, RoutedEventArgs e)
        {
            acc = Convert.ToInt32(accuracy.Text);
            bigNum halfTaylorAns = calc.halfTaylor(num.Text, acc);
            halfTaylor.Text = (calc.round(halfTaylorAns, acc)).show(acc);


        }
    }
}

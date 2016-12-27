using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            calc.times = new int[3];
            //初始化，存1、2、4、根号2的大数表示
            calc.one = new bigNum("1");
            calc.two = new bigNum("2");
            calc.four = new bigNum("4");

            calc.sqrt2 = bigNum.sqrt(2);
            //calc.sqrt2.show();

            /*
             * 计算ln2
             */
            bigNum sqrt2_1 = calc.sqrt2 - calc.one;
            calc.ln2 = new bigNum("0");
            bigNum an=new bigNum(sqrt2_1);
            for (int i = 1; i <= 300; ++i)
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
            if (accuracy.Text == "")
            {
                MessageBox.Show("请输入精度");
                return;
            }
            if (num.Text == "")
            {
                MessageBox.Show("请输入x");
                return;
            }
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < accuracy.Text.Length; ++i)
            {
                if (accuracy.Text[i] < '0' || accuracy.Text[i] > '9')
                {
                    MessageBox.Show("精度请输入有效正整数");
                    return;
                }
            }
            acc = Convert.ToInt32(accuracy.Text);

            if (acc >= 50)
            {
                MessageBox.Show("精度建议小于50");
                return;
            }
            if (acc < 0)
            {
                MessageBox.Show("结果小数位数大于等于0");
                return;
            }

            int cntdot = 0;
            for (int i = 0; i < num.Text.Length; ++i)
            {
                if (num.Text[i] < '0' || num.Text[i] > '9')
                {
                    if (num.Text[i] == '.')
                    {
                        cntdot++;
                        if (cntdot > 1)
                        {
                            MessageBox.Show("请输入有效x");
                            return;
                        }
                    }
                    else
                    { 
                        MessageBox.Show("请输入有效x");
                        return;
                    }

                }
            }
            bigNum input = new bigNum(num.Text);
            if (input < calc.one)
            {
                MessageBox.Show("请输入不小于1的x");
                return;
            }


            /*
             * Taylor展开段
             */
            stopwatch.Reset();
            stopwatch.Start();
            bigNum halfTaylorAns = calc.halfTaylor(num.Text, acc);
            halfTaylor.Text = (calc.round(halfTaylorAns, acc)).show(acc);
            stopwatch.Stop();
            halfTaylorTime.Text= stopwatch.ElapsedMilliseconds.ToString()+"ms";
            halfTaylorTimes.Text = calc.times[0].ToString();

            /*
             * Romberg数值积分段
             */
            stopwatch.Reset();
            stopwatch.Start();
            bigNum rombergAns = calc.romberg(num.Text, acc);
            romberg.Text = (calc.round(rombergAns, acc)).show(acc);
            stopwatch.Stop();
            rombergTime.Text = stopwatch.ElapsedMilliseconds.ToString() + "ms";
            rombergTimes.Text = calc.times[1].ToString();

            /*
             * 有理逼近段
             */
            stopwatch.Reset();
            stopwatch.Start();
            bigNum rationalAns = calc.rational(num.Text, acc);
            rational.Text = (calc.round(rationalAns, acc)).show(acc);
            stopwatch.Stop();
            rationalTime.Text = stopwatch.ElapsedMilliseconds.ToString() + "ms";
            rationalTimes.Text = calc.times[2].ToString();
            MessageBox.Show("Done!");
        }

        private void image_MouseEnter(object sender, MouseEventArgs e)
        {
            image.Source = new BitmapImage(new Uri("Resources/start_enter.png", UriKind.Relative));
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            image.Source = new BitmapImage(new Uri("Resources/start.png", UriKind.Relative));
        }
    }
}

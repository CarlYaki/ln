using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ln
{
    class calc
    {
        public static bigNum E, one, two, four, sqrt2, ln2;
        public static int[] times;
        public static bigNum halfTaylor(string inp, int acc)
        {
            //--------------减半Taylor展开---------------
            bigNum.maxlen = acc + 10;
            bigNum input = new bigNum(inp);
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
            //temp.show();
            bigNum a = input / temp - one;
            //a.show();
            bigNum R = new bigNum(r.ToString());
            //R.show();


            bigNum ans = new bigNum("0"), an = new bigNum(a);
            bigNum delta;
            int n = 1;

            bigNum offset = new bigNum("0");

            bool flag;
            do
            {
                flag = false;
                delta = an / (new bigNum(n.ToString()));

                for (int i = delta.dot - 1; i >= delta.dot - acc - 1 && i >= 0; --i)
                {
                    if (delta.num[i] != 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    break;

                ans = ans + delta;
                n++;
                an = an * a;
                an.neg = (n % 2 == 0 ? true : false);
            } while (flag) ;

            times[0] = n;
            //MessageBox.Show("taylor迭代到" + n.ToString() + "次");

            ans = ans + ln2 * R;

            return ans;
        }


        public static bigNum romberg(string inp, int acc)
        {
            //--------------Romberg数值积分---------------
            bigNum.maxlen = acc + 5;
            bigNum input = new bigNum(inp);
            bigNum[] power4 = new bigNum[50];
            bigNum[] stack_T = new bigNum[50];
            int cnt_x = 1;

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
            //temp.show();
            bigNum a = input / temp;
            //a.show();
            bigNum R = new bigNum(r.ToString());
            //R.show();

            bigNum h = (a - one);
            bigNum half_h = h / two;
            stack_T[0] = half_h * (one + f(a));

            power4[0] = new bigNum(one);

            bigNum x;
            bigNum delta;
            int n;
            bool returnflag;
            for (n = 1; ; ++n)
            {
                //MessageBox.Show(n.ToString());
                power4[n] = power4[n - 1] * four;
                temp = new bigNum("0");
                x = one + half_h;
                for (int i = 0; i < cnt_x; ++i)
                {
                    temp = temp + f(x);
                    x = x + h;
                }

                h = half_h;
                half_h = h / two;

                cnt_x <<= 1;
                stack_T[n] = (stack_T[n - 1] / two) + (h * temp);//T0(n)
                
                returnflag = true;
                for (int i = n - 1; i >= 0; --i)
                {
                    temp = ((power4[n - i] * stack_T[i + 1]) - stack_T[i]) / (power4[n - i] - one);
                    if (i == 0)
                    {
                        delta = temp - stack_T[0];
                        for (int j = delta.cnt - 1; j >= delta.dot - acc - 1; --j)
                        {
                            if (delta.num[j] > 0)
                            {
                                returnflag = false;
                                break;
                            }
                        }
                        if (returnflag)
                        {
                            times[1] = n;
                            //MessageBox.Show("romberg迭代到" + n.ToString() + "次");
                            return temp + R * ln2;
                        }
                    }
                    stack_T[i] = temp;
                }
            }
        }
        private static bigNum f(bigNum inp)
        {
            return one / inp;
        }

        public static bigNum rational(string inp, int acc)
        {
            //--------------有理逼近---------------
            bigNum.maxlen = acc+10;
            bigNum input = new bigNum(inp);
            int loop = acc * 2;
            bigNum countDown = new bigNum(loop.ToString());
            bigNum countDownPlus = countDown + one;
            bigNum powerRoot=new bigNum(((loop+1)/2).ToString());

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
            //temp.show();
            bigNum a = input / temp - one;
            //a.show();
            bigNum R = new bigNum(r.ToString());
            //R.show();

            times[2] = loop;
            temp = new bigNum("0");
            while (loop-- > 0)
            {
                temp = ((powerRoot * powerRoot * a) / (countDownPlus + temp));
                countDownPlus = countDown;
                countDown = new bigNum(loop.ToString());
                powerRoot = new bigNum(((loop + 1) / 2).ToString());
            }
            return (a / (one + temp)) + (R * ln2);
        }
        
        public static bigNum round(bigNum input, int acc)
        {
            bigNum ans = new bigNum(input);
            if (ans.num[ans.dot - acc - 1] > 5)
            {
                ans.num[ans.dot - acc]++;
                for (int i = ans.dot - acc; i < ans.cnt; ++i)
                {
                    if (ans.num[i] >= bigNum.mod)
                    {
                        ans.num[i + 1] += ans.num[i] / bigNum.mod;
                        ans.num[i] %= bigNum.mod;
                        if (i == ans.cnt - 1)
                        {
                            ans.cnt++;
                        }
                    }
                }
            }
            return ans;
        }
    }
}

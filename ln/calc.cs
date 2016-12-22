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
        public static bigNum E, one, two, sqrt2, ln2;
        public static bigNum halfTaylor(string inp, int acc)
        {
            //--------------减半Taylor展开---------------
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

            bool flag = true;
            while (flag)
            {
                flag = false;
                delta = an / (new bigNum(n.ToString()));

                for (int i = delta.dot - 1; i >= delta.dot - acc - 10 && i >= 0; --i)
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
            }

            MessageBox.Show("迭代到" + n.ToString() + "次");

            ans = ans + ln2 * R;

            return ans;
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

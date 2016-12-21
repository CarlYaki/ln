using System;
using System.Windows;

namespace ln
{
    class bigNum
    {
        private static int mod = 10;
        private static int maxlen = 500;
        public bool neg;
        public int[] num;//每个存四位
        public int cnt, dot;
        bigNum()
        {
            neg = false;
            cnt = dot = 0;
            num = new int[1010];//上限用到maxlen组（maxlen位有效数字），以防乘法爆
        }
        bigNum(string s)
        {
            string[] nums = s.Split('.');
            if(nums.Length==2)
            {
                dot = nums[1].Length;
                nums[0] += nums[1];
            }
            cnt = nums[0].Length;
            for (int i = cnt - 1; i >= 0; --i)
            {
                num[i] += (short)(nums[0][i] - '0');
            }
        }

        private static bigNum plus(bigNum b1, bigNum b2)
        {
            bigNum bn1 = b1, bn2 = b2, ans = new bigNum();

            if (bn1.dot > bn2.dot)
            {
                for (int i = bn2.cnt - 1; i >= 0; --i)
                {
                    bn2.num[i + bn1.dot - bn2.dot] = bn2.num[i];
                    bn2.num[i] = 0;
                }
                bn2.cnt += bn1.dot - bn2.dot;
                bn2.dot = bn1.dot;
            }
            else if (bn1.dot < bn2.dot)
            {
                for (int i = bn1.cnt - 1; i >= 0; --i)
                {
                    bn1.num[i + bn2.dot - bn1.dot] = bn1.num[i];
                    bn1.num[i] = 0;
                }
                bn1.cnt += bn2.dot - bn1.dot;
                bn1.dot = bn2.dot;
            }
            else
            { }
            ans.cnt = bn1.cnt > bn2.cnt ? bn1.cnt : bn2.cnt;
            ans.dot = bn1.dot;
            for (int i = 0; i < ans.cnt; ++i)
            {
                ans.num[i] = bn1.num[i] + bn2.num[i];
            }
            for (int i = 0; i < ans.cnt; ++i)
            {
                if (ans.num[i] > mod)
                {
                    ans.num[i + 1] += ans.num[i] / mod;
                    ans.num[i] %= mod;
                    if (i == ans.cnt - 1)
                        ++ans.cnt;
                }

            }
            if (ans.cnt > maxlen)
            {
                int delta = ans.cnt - maxlen;
                for (int i = 0; i < maxlen; ++i)
                {
                    ans.num[i] = ans.num[i + delta];
                }
            }
            return ans;
        }

        private static bigNum minus(bigNum b1, bigNum b2)
        {
            bigNum bn1 = b1, bn2 = b2, ans = new bigNum();

            if (bn1.dot > bn2.dot)
            {
                for (int i = bn2.cnt - 1; i >= 0; --i)
                {
                    bn2.num[i + bn1.dot - bn2.dot] = bn2.num[i];
                    bn2.num[i] = 0;
                }
                bn2.cnt += bn1.dot - bn2.dot;
                bn2.dot = bn1.dot;
            }
            else if (bn1.dot < bn2.dot)
            {
                for (int i = bn1.cnt - 1; i >= 0; --i)
                {
                    bn1.num[i + bn2.dot - bn1.dot] = bn1.num[i];
                    bn1.num[i] = 0;
                }
                bn1.cnt += bn2.dot - bn1.dot;
                bn1.dot = bn2.dot;
            }
            else
            { }
            ans.cnt = bn1.cnt > bn2.cnt ? bn1.cnt : bn2.cnt;
            ans.dot = bn1.dot;
            for (int i = 0; i < ans.cnt; ++i)
            {
                ans.num[i] = bn1.num[i] + bn2.num[i];
            }
            for (int i = 0; i < ans.cnt; ++i)
            {
                if (ans.num[i] > mod)
                {
                    ans.num[i + 1] += ans.num[i] / mod;
                    ans.num[i] %= mod;
                    if (i == ans.cnt - 1)
                        ++ans.cnt;
                }

            }
            if (ans.cnt > maxlen)
            {
                int delta = ans.cnt - maxlen;
                for (int i = 0; i < maxlen; ++i)
                {
                    ans.num[i] = ans.num[i + delta];
                }
            }
            return ans;
        }


        public static bigNum operator +(bigNum b1, bigNum b2)
        {
            
        }
        public static bigNum operator -(bigNum b1, bigNum b2)
        {
            
        }
    }
}

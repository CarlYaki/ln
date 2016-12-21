﻿using System;
using System.Windows;

namespace ln
{
    class bigNum
    {
        private static int mod = 10;
        private static int maxlen = 500;
        public bool neg;
        public int[] num;//每个存一位
        public int cnt, dot;
        public bigNum()
        {
            neg = false;
            cnt = dot = 0;
            num = new int[1010];//上限用到maxlen组（maxlen位有效数字），以防乘法爆
        }
        public bigNum(string s)
        {
            if (s[0] == '-')
            {
                neg = true;
                s = s.Remove(0, 1);
            }
            num = new int[1010];//上限用到maxlen组（maxlen位有效数字），以防乘法爆
            string[] nums = s.Split('.');
            if (nums.Length == 2)
            {
                dot = nums[1].Length;
                nums[0] += nums[1];
            }
            cnt = nums[0].Length;
            for (int i = 0; i < cnt; ++i)
            {
                num[i] += (nums[0][cnt - 1 - i] - '0');
            }
            while (cnt > dot + 1 && num[cnt - 1] == 0)
            {
                --cnt;
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
                if (ans.num[i] >= mod)
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
                ans.dot -= delta;
                ans.cnt = maxlen;
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
                ans.num[i] = bn1.num[i] - bn2.num[i];
            }
            for (int i = 0; i < ans.cnt; ++i)
            {
                if (ans.num[i] < 0)
                {
                    ans.num[i + 1] += ans.num[i] / mod;
                    ans.num[i] %= mod;
                    if (ans.num[i] < 0)
                    {
                        ans.num[i + 1]--;
                        ans.num[i] += mod;
                    }
                }
            }
            while (ans.num[ans.cnt - 1] == 0 && ans.cnt > ans.dot + 1)
            {
                ans.cnt--;
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
            bigNum ans = new bigNum();

            if (!(b1.neg ^ b2.neg))
            {
                ans = plus(b1, b2);
                ans.neg = b1.neg;
            }
            else
            {
                if (b1 >= b2)
                {
                    ans = minus(b1, b2);
                    ans.neg = b1.neg;
                }
                else
                {
                    ans = minus(b2, b1);
                    ans.neg = b2.neg;
                }
            }

            return ans;
        }
        public static bigNum operator -(bigNum b1, bigNum b2)
        {
            bigNum ans = new bigNum();

            if ((b1.neg ^ b2.neg))//符号不同，数字相加，符号跟随被减数
            {
                ans = plus(b1, b2);
                ans.neg = b1.neg;
            }
            else//符号相同，数字相减，
            {
                if (b1 >= b2)
                {
                    ans = minus(b1, b2);
                    ans.neg = b1.neg;
                }
                else
                {
                    ans = minus(b2, b1);
                    ans.neg = !b1.neg;
                }
            }


            return ans;
        }
        public static bigNum operator *(bigNum b1, bigNum b2)
        {
            bigNum ans = new bigNum();

            for (int i = 0; i < b1.cnt; ++i)
            {
                for (int j = 0; j < b2.cnt; ++j)
                {
                    ans.num[i + j] += b1.num[i] * b2.num[j];
                }
            }
            ans.cnt = b1.cnt + b2.cnt - 1;
            ans.dot = b1.dot + b2.dot;
            for (int i = 0; i < ans.cnt; ++i)
            {
                if (ans.num[i] >= mod)
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
                ans.dot -= delta;
                ans.cnt = maxlen;
            }
            return ans;
        }
        public static bigNum operator /(bigNum b1, bigNum b2)
        {
            bigNum ans = new bigNum();
            int[] num_a = b1.num, num_b = b2.num;
            int len1 = b1.cnt, len2 = b2.cnt;

            for (int i = 0; i < len1; ++i)
            {
                num_a[i + maxlen] = num_a[i];
                num_a[i] = 0;
            }
            len1 += maxlen;
            int nTimes = len1 - len2;
            for (int i = len2 - 1; i >= 0; --i)
            {
                num_b[i+nTimes] = num_b[i];
                num_b[i] = 0;
            }
            len2 = len1;
            int nTemp;
            for (int i = 0; i <= nTimes; ++i)
            {
                while ((nTemp = SubStract(num_a, num_b, len1, len2, i)) >= 0)
                {
                    len1 = nTemp;
                    ans.num[nTimes - i]++;
                }
            }
            ans.dot = b1.dot + maxlen;
            ans.cnt = nTimes + 1;
            while (ans.num[ans.cnt-1] == 0 && ans.cnt > ans.dot + 1)
            {
                ans.cnt--;
            }
            if (ans.cnt > maxlen)
            {
                int delta = ans.cnt - maxlen;
                for (int i = 0; i < maxlen; ++i)
                {
                    ans.num[i] = ans.num[i + delta];
                }
                ans.dot -= delta;
                ans.cnt = maxlen;
            }
            ans.neg = b1.neg ^ b2.neg;
            return ans;
        }
        private static int SubStract(int[] p1, int[] p2, int len1, int len2,int j)
        {
            int i;
            if (len1 < len2-j)
                return -1;
            if (len1 == len2-j)
            {                        //判断p1 > p2
                for (i = len1 - 1; i >= 0; i--)
                {
                    if (p1[i] > p2[i+j])   //若大，则满足条件，可做减法
                        break;
                    else if (p1[i] < p2[i+j]) //否则返回-1
                        return -1;
                }
            }
            for (i = 0; i <= len1 - 1; i++)  //从低位开始做减法
            {
                p1[i] -= p2[i+j];
                if (p1[i] < 0)          //若p1<0，则需要借位
                {
                    p1[i] += 10;         //借1当10
                    p1[i + 1]--;           //高位减1
                }
            }
            for (i = len1 - 1; i >= 0; i--)       //查找结果的最高位
                if (p1[i]!=0)                  //最高位第一个不为0
                    return (i + 1);       //得到位数并返回
            return 0;                  //两数相等的时候返回0
        }












        public static bool operator <(bigNum bn1, bigNum bn2)
        {
            if (bn1.cnt - bn1.dot < bn2.cnt - bn2.dot)
            {
                return true;
            }
            if (bn1.cnt - bn1.dot > bn2.cnt - bn2.dot)
            {
                return false;
            }
            int i, j;
            for (i = bn1.cnt - 1, j = bn2.cnt - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (bn1.num[i] < bn2.num[j])
                    return true;
                if (bn1.num[i] > bn2.num[j])
                    return false;
            }
            if (j > 0)
                return true;
            if (i > 0)
                return false;
            return false;
        }

        public static bool operator >(bigNum bn1, bigNum bn2)
        {
            if (bn1.cnt - bn1.dot > bn2.cnt - bn2.dot)
            {
                return true;
            }
            if (bn1.cnt - bn1.dot < bn2.cnt - bn2.dot)
            {
                return false;
            }
            int i, j;
            for (i = bn1.cnt - 1, j = bn2.cnt - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (bn1.num[i] > bn2.num[j])
                    return true;
                if (bn1.num[i] < bn2.num[j])
                    return false;
            }
            if (i > 0)
                return true;
            if (j > 0)
                return false;
            return false;
        }

        public static bool operator <=(bigNum bn1, bigNum bn2)
        {
            if (bn1.cnt - bn1.dot < bn2.cnt - bn2.dot)
            {
                return true;
            }
            if (bn1.cnt - bn1.dot > bn2.cnt - bn2.dot)
            {
                return false;
            }
            int i, j;
            for (i = bn1.cnt - 1, j = bn2.cnt - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (bn1.num[i] < bn2.num[j])
                    return true;
                if (bn1.num[i] > bn2.num[j])
                    return false;
            }
            if (j > 0)
                return true;
            if (i > 0)
                return false;
            return true;
        }

        public static bool operator >=(bigNum bn1, bigNum bn2)
        {
            if (bn1.cnt - bn1.dot > bn2.cnt - bn2.dot)
            {
                return true;
            }
            if (bn1.cnt - bn1.dot < bn2.cnt - bn2.dot)
            {
                return false;
            }
            int i, j;
            for (i = bn1.cnt - 1, j = bn2.cnt - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (bn1.num[i] > bn2.num[j])
                    return true;
                if (bn1.num[i] < bn2.num[j])
                    return false;
            }
            if (i > 0)
                return true;
            if (j > 0)
                return false;
            return true;
        }
        /*
        public static bool operator ==(bigNum bn1, bigNum bn2)
        {

            if (bn1.cnt != bn2.cnt)
                return false;
            if (bn1.dot != bn2.dot)
                return false;

            for (int i = bn1.cnt - 1; i >= 0; --i)
            {
                if (bn1.num[i] != bn2.num[i])
                    return false;
            }
            return true;
        }

        public static bool operator !=(bigNum bn1, bigNum bn2)
        {

            if (bn1.cnt != bn2.cnt)
                return true;
            if (bn1.dot != bn2.dot)
                return true;

            for (int i = bn1.cnt - 1; i >= 0; --i)
            {
                if (bn1.num[i] != bn2.num[i])
                    return true;
            }
            return false;
        }
        */
    }
}

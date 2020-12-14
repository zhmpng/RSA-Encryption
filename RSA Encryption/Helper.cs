using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSA_Encryption
{
    public partial class Helper
    {
        public static char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                        'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                        'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' };

        public static bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        public static List<string> RSA_Endoce(string s, BigInteger e, long n)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi %= n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        public static string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi %= n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        public static long Calculate_d(long m)
        {
            long d = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0))
                {
                    d--;
                    i = 1;
                }

            return d;
        }

        public static BigInteger Calculate_e(long m)
        {
            BigInteger i = (BigInteger)Math.Truncate((decimal)Math.Sqrt((ulong)m));
            while (true)
            {
                if (BigInteger.GreatestCommonDivisor(m, i) != 1)
                    i--;
                else break;
            }

            return i;
        }

        public static BigInteger modInverse(BigInteger e, BigInteger m)
        {
            BigInteger i = m, v = 0, d = 1;
            while (e > 0)
            {
                BigInteger t = i / e, x = e;
                e = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= m;
            if (v < 0) v = (v + m) % m;
            return v;
        }

        public static string ConvertTo1251(string text)
        {
            Encoding srcEncodingFormat = Encoding.UTF8;
            Encoding dstEncodingFormat = Encoding.GetEncoding("windows-1251");
            byte[] originalByteString = srcEncodingFormat.GetBytes(text);
            byte[] convertedByteString = Encoding.Convert(srcEncodingFormat,
            dstEncodingFormat, originalByteString);
            string finalString = dstEncodingFormat.GetString(convertedByteString);
            return finalString;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicherkivania
{
    public static class Ext
    {
        public static string AsString(this char[] arr)
        {
            return string.Join("", arr);
        }
    }

    class Program
    {
        static bool CompareStringsIgnoreMinus(string f, string f0)
        {
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == '-')
                {
                    continue;
                }

                if (f[i] != f0[i])
                {
                    return false;
                }
            }

            return true;
        }

        static List<string> ParseLine(string line, int variables)
        {
            List<string> list = new List<string>();
            foreach (var token in line.Split(','))
            {
                if (token.Split('-').Length > 1)
                {
                    int left = int.Parse(token.Split('-')[0]);
                    int right = int.Parse(token.Split('-')[1]);
                    for (int i = left; i <= right; i++)
                    {
                        list.Add(Convert.ToString(i, 2).PadLeft(variables, '0'));
                    }
                }
                else
                {
                    int i = int.Parse(token);
                    list.Add(Convert.ToString(i, 2).PadLeft(variables, '0'));
                }
            }

            return list;
        }

        static string ToVariables(string f, string variables)
        {
            string s = "";
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == '-') continue;
                s += (f[i] == '0' ? "!" : "") + variables[i];
            }

            return s;
        }

        static void Main(string[] args)
        {
            string variables = "xyzwpq";
            string m1 = "3,8,22,24";
            string m0 = "2,12,14,17,19,21";

            List<string> f1List = ParseLine(m1, variables.Length);
            List<string> f0List = ParseLine(m0, variables.Length);

            HashSet<string> uniqueF1 = new HashSet<string>();

            foreach (var f1 in f1List)
            {
                char[] f1Copy = f1.ToCharArray();
                for (int j = 0; j < f1Copy.Length; j++)
                {
                    char c = f1Copy[j];
                    f1Copy[j] = '-';
                    Console.Write(string.Join("", f1Copy));

                    if (f0List.Any(t => CompareStringsIgnoreMinus(f1Copy.AsString(), t)))
                    {
                        f1Copy[j] = c;
                        Console.Write("!");
                    }

                    Console.WriteLine();
                }

                uniqueF1.Add(f1Copy.AsString());

                Console.WriteLine(string.Join("", f1Copy) + " " + ToVariables(f1Copy.AsString(), variables));
                Console.WriteLine("--------");
            }

            Console.WriteLine(string.Join(" + ", uniqueF1.Select(f1 => ToVariables(f1, variables))));

            Console.ReadKey();
        }
    }
}
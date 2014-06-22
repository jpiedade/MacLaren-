using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectPalindromes
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please insert a word:");
                string word = Console.ReadLine();

                if (word == string.Empty)
                {
                    Console.WriteLine("Please insert a word...");
                    return;
                }

                IEnumerable<KeyValuePair<int, string>> list = GetAllPalindromes(word);

                if(list == null)
                {
                    Console.WriteLine(string.Format("No Palindromes detect in: {0}", word));
                    return;
                }

                foreach (var item in list)
                {
                    Console.WriteLine(string.Format("Text: {0}, Index:{1}, Length: {2}", item.Value, item.Key, item.Value.Length));
                }

                Console.WriteLine("Press any key to exit the program...");
                Console.ReadKey();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static IEnumerable<KeyValuePair<int, string>> GetAllPalindromes(string input)
        {
            List<KeyValuePair<int, string>> Palindromes = new List<KeyValuePair<int, string>>();

            int len = input.Length;

            //Detect all ODD occuring palindromes
            OddPalindromes(input, Palindromes, len);
            //Detect all EVEN occuring palindromes
            EvenPalindromes(input, Palindromes, len);

            UniquePalindromes(Palindromes);

            return Palindromes.Count > 0 ? Palindromes.OrderByDescending(x => x.Value.Length).Take(3) : null;
        }

        private static void UniquePalindromes(List<KeyValuePair<int, string>> Palindromes)
        {
            //Auxiliar List to clean all the palindromes that are not the longuest unique
            List<KeyValuePair<int, string>> aux = new List<KeyValuePair<int, string>>();

            foreach(var item1 in Palindromes)
            {
                foreach(var item2 in Palindromes)
                {
                    if(item1.Value.Contains(item2.Value) && item1.Key != item2.Key)
                    {
                        aux.Add(item2);
                    }
                }
            }

            foreach(var item in aux)
            {
                Palindromes.Remove(item);
            }
        }

        private static void EvenPalindromes(string input, List<KeyValuePair<int, string>> Palindromes, int len)
        {
            for (int i = 1; i < len - 1; i++)
            {
                for (int j = i, k = i + 1; j >= 0 && k < len; j--, k++)
                {
                    if (input.ElementAt(j) == input.ElementAt(k))
                    {
                        var subString = input.Substring(j, k - j + 1);
                        Palindromes.Add(new KeyValuePair<int, string>(j, subString));
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void OddPalindromes(string input, List<KeyValuePair<int, string>> Palindromes, int len)
        {
            for (int i = 1; i < len - 1; i++)
            {
                for (int j = i - 1, k = i + 1; j >= 0 && k < len; j--, k++)
                {
                    if (input.ElementAt(j) == input.ElementAt(k))
                    {
                        var subString = input.Substring(j, k - j + 1);
                        Palindromes.Add(new KeyValuePair<int, string>(j, subString));
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

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

                //Verification to see if any word was insert in the cmd line
                if (word == string.Empty)
                {
                    /* If there is not any word in cmd line exit program 
                    with specify message */
                    Console.WriteLine("Please insert a word...");
                    return;
                }

                IEnumerable<KeyValuePair<int, string>> list = GetAllPalindromes(word);

                //Verification to see if there is any palindrome
                if(list == null)
                {
                    /* If there is not any palindrome exit program 
                    with specify message */
                    Console.WriteLine(string.Format("No Palindromes detect in: {0}", word));
                    return;
                }

                /* Write each retrieved palindrome that is in the list
                in the cmd line */
                foreach (var item in list)
                {
                    Console.WriteLine(string.Format("Text: {0}, Index: {1}, Length: {2}", item.Value, item.Key, item.Value.Length));
                }

                //Press any key to exit the program.
                Console.WriteLine("Press any key to exit the program...");
                Console.ReadKey();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Method that get all the palindromes in a input string
        public static IEnumerable<KeyValuePair<int, string>> GetAllPalindromes(string input)
        {
            List<KeyValuePair<int, string>> Palindromes = new List<KeyValuePair<int, string>>();

            int len = input.Length;

            //Detect all ODD length occuring palindromes
            OddPalindromes(input, Palindromes, len);
            //Detect all EVEN length occuring palindromes
            EvenPalindromes(input, Palindromes, len);
            //Remove all the Palindromes that are not unique from the list
            UniquePalindromes(Palindromes);

            /* Return the 3 longest palindromes in the list
               else return it null */
            return Palindromes.Count > 0 ? Palindromes.OrderByDescending(x => x.Value.Length).Take(3) : null;
        }

        private static void UniquePalindromes(List<KeyValuePair<int, string>> Palindromes)
        {
            //Aux List to save palindromes that are contained in others palindromes
            List<KeyValuePair<int, string>> aux = new List<KeyValuePair<int, string>>();

            //Add all palindromes that are contained in others to aux list
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

            //Remove all not unique palindromes from Palindromes list
            foreach(var item in aux)
            {
                Palindromes.Remove(item);
            }
        }

        //Method to detect EVEN length palindromes
        private static void EvenPalindromes(string input, List<KeyValuePair<int, string>> Palindromes, int len)
        {
            for (int i = 1; i < len - 1; i++)
            {
                /* Find every EVEN length palindrome with center points
                   as i and i + 1 */
                for (int j = i, k = i + 1; j >= 0 && k < len; j--, k++)
                {
                    if (input.ElementAt(j) == input.ElementAt(k))
                    {
                        var subString = input.Substring(j, k - j + 1);
                        Palindromes.Add(new KeyValuePair<int, string>(j, subString));
                    }
                }
            }
        }

        //Method to detect ODD length palindromes
        private static void OddPalindromes(string input, List<KeyValuePair<int, string>> Palindromes, int len)
        {
            for (int i = 1; i < len - 1; i++)
            {
                // Find every ODD length palindrome with center point i
                for (int j = i - 1, k = i + 1; j >= 0 && k < len; j--, k++)
                {
                    if (input.ElementAt(j) == input.ElementAt(k))
                    {
                        var subString = input.Substring(j, k - j + 1);
                        Palindromes.Add(new KeyValuePair<int, string>(j, subString));
                    }
                }
            }
        }
    }
}

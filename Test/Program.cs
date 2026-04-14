namespace Test
{
    internal class Program
    {

        public int MyAtoi(string s)
        {
            var word = s.Trim();
            if (word.Length == 0) return 0;

            if (!char.IsDigit(word[0]) && !word[0].Equals('-') && !word[0].Equals('+')) return 0;

            bool isNegative = word[0].Equals('-');
            if (!char.IsDigit(word[0])) word = word[1..];

            long sum = 0;

            foreach (var c in word)
            {
                if (!char.IsDigit(c)) break;
                sum = sum * 10 + (c - '0');
                if (sum > int.MaxValue || -1 * sum < int.MinValue) return isNegative ? int.MinValue : int.MaxValue;
            }

            return isNegative ? (int)(-1 * sum) : (int)sum;


        }


        public int LengthOfLastWord(string s)
        {
            return s.Trim().Split(' ').Last().Length;


        }
        static void Main(string[] args)
        {
            Console.WriteLine(int.MaxValue);
            Console.WriteLine(int.MinValue);
            var program = new Program();
            Console.WriteLine(program.MyAtoi("10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000522545459"));
        }
    }
}

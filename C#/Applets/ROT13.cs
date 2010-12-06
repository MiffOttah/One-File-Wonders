using System;
using System.IO;

namespace ROT13
{
    class ROT13
    {
        static void Main(string[] args)
        {
            try
            {
                int n;
                while ((n = Console.In.Read()) > 0)
                {
                    if ((n >= 65 && n <= 77) || (n >= 97) && (n <= 109)) n += 13;
                    else if ((n >= 78 && n <= 90) || (n >= 110) && (n <= 122)) n -= 13;

                    Console.Out.Write((char)n);
                }
                
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}

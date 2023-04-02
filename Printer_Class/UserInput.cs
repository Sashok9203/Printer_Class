using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp_Class
{
    internal static class UserInput
    {
        public static string getString(string message)
        {
            string? tmp = null;
            Console.Write(message);
            while (string.IsNullOrWhiteSpace(tmp))
                tmp = Console.ReadLine();
            return tmp;
        }

        public static uint getUint(string message)
        {
            uint tmp;
            Console.Write(message);
            while (!uint.TryParse(Console.ReadLine(), out tmp)) ;
            return tmp;
        }
        
        public static T getEnum<T>(string message, T en) where T : Enum
        {
            uint tmp;
            Console.WriteLine(message);
            string[] names = Enum.GetNames(en.GetType());
            for (int i = 0; i < names.Length; i++)
                Console.WriteLine($" {i+1}) {names[i]}");
            do{tmp = getUint(" -> ");}
            while (tmp < 1 || tmp > names.Length);
            return (T)Enum.Parse(en.GetType(), names[tmp - 1]);
        }
    }
}

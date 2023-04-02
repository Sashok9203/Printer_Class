using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp_Class
{
    internal partial class Printer
    {
        public static void Swap(ref Printer pr1, ref Printer pr2)
        {
            Printer tmp = pr1;
            pr1 = pr2;
            pr2 = tmp;
        }

        public static void InputProperties(out string? name, out uint colorsCount, out Cartridge?[] cartridges)
        {
            Console.Clear();
            name = UserInput.getString(" Enter printer name : ");
            colorsCount = UserInput.getUint(" Enter printer colors count : ");
            cartridges = new Cartridge?[colorsCount];
            for (int i = 0; i < colorsCount; i++)
            {
                bool found = false;
                uint max;
                Colors color;
                Console.Clear();
                Console.WriteLine($" Enter cartridge {i + 1} parametres : ");
                do
                {
                    found = false;
                    color = UserInput.getEnum(" Choose cartridge color ", Colors.Red);
                    for (int k = 0; k < colorsCount; k++)
                        if (cartridges[k]?.Color == color) found = true;
                } while (found);
                do { max = UserInput.getUint($" Enter cartridge {i+1} max level (>0): "); }
                while (max == 0);
                cartridges[i] = new Cartridge(color, max);
            }
        }
    }
}

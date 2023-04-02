using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Comp_Class
{
    enum Colors
    {
        Red,
        Green,
        Blue,
        Black,
        Yellow,
        Magenta,
        Cyan
    }

    struct Cartridge
    {
        public uint MaxLevel;
        public uint CurentLevel;
        public Colors Color;
        public Cartridge(Colors color, uint maxLevel)
        {
            Color = color;
            MaxLevel = maxLevel;
            CurentLevel = maxLevel;
        }
        public override string ToString()
        {
            return $" {Color} : {CurentLevel} / {MaxLevel}";
        }
    }

    struct Document
    {
        public string Name;
        public uint pages;
    }

    internal partial class Printer
    {
        private Thread thread;
        private static uint _totalDocsPrinted;
        private const string _manufacturer = "Canon";
        private Cartridge?[] _cartridges;
        private Queue<Document> _doc_queue;
        private  void print()
        {
            
             while (_doc_queue.Count > 0)
             {
                Random rnd = new Random();  
                Document doc = _doc_queue.Dequeue();
                uint count = 1;
                while (doc.pages >= count)
                {
                    for(int i = 0;i<_cartridges.Length;i++)
                    {
                        if (!(_cartridges[i]?.CurentLevel > 0))
                        {
                            Ready = false;
                            return;
                        }
                        Cartridge car = _cartridges[i].Value;
                        car.CurentLevel -= 5;
                        _cartridges[i] = car;
                    }
                    Console.WriteLine($" \"{Name}\" printing \"{doc.Name}\" page {doc.pages}/{count}\n");
                    Thread.Sleep(rnd.Next(500,1500));
                    ++count;
                }
             }
        }

        public uint ColorsCount { get; init; }
        public string Name { get; init; }
        public Guid Guid { get; init; }
        public bool Ready { get; private set; }
        public bool OnOff { get; private set; }
        public static uint TotalDocsPrinted { get { return _totalDocsPrinted; } }

       
        public Printer(string name, uint colorsCount,params Cartridge?[] cartridges): this(name,colorsCount)
        {
            setСartridges(cartridges);
        }

        public Printer(string name,uint colorsCount)
        {
            Name = name;
            ColorsCount = colorsCount;
            _cartridges = new Cartridge?[ColorsCount];
            _doc_queue = new Queue<Document>();
            Guid = Guid.NewGuid();
            thread = new(new ThreadStart(print));
        }
        
        static Printer() { _totalDocsPrinted = 0; }

        public void setСartridges(params Cartridge?[] сartridges)
        {
            if (OnOff) throw new ApplicationException(" Printer power on ...");
            if (сartridges.Length != ColorsCount) throw new ApplicationException(" Invalid cartridges count...");
            for (int i = 0; i < сartridges.Length; i++)
            {
                if(сartridges[i]?.MaxLevel == 0) throw new ApplicationException($" Invalid max color value {сartridges[i]?.Color} ...");
                for (int k = i + 1; k < сartridges.Length; k++)
                    if (сartridges[i]?.Color == сartridges[k]?.Color) throw new ApplicationException($" Dublicate colors {сartridges[i]?.Color} and {сartridges[k]?.Color} ...");
            }
            _cartridges = сartridges;
        }

        public void RechargeCartridge(Colors color)
        {
            for (int i = 0; i < _cartridges.Length; i++)
            {
                if (_cartridges[i]?.Color == color)
                {
                    Cartridge tmp = _cartridges[i].GetValueOrDefault();
                    tmp.CurentLevel = tmp.MaxLevel;
                    _cartridges[i] = tmp;
                    return;

                }
            }
            throw new ApplicationException($" Color {color} does not exist...");
        }

        public void Print(in Document doc)
        {
            if (OnOff && Ready)
            {
                _doc_queue.Enqueue(doc);
                if (thread.IsAlive) return;
                else  thread.Start();
            }
        }

        public void PrinterOnOff() 
        {
            OnOff = !OnOff;
            if (OnOff)
            {
                foreach (var item in _cartridges)
                {
                    if (!(item?.CurentLevel > 0))
                    {
                        Ready = false;
                        return;
                    }
                }
                Ready = true;
            }
            else Ready = false;
        }
        
        public override string ToString()
        { 
            StringBuilder sb = new StringBuilder($" -= \"{_manufacturer}\" Printer =-\n");
            sb.AppendLine($" Name         : {Name}");
            sb.AppendLine($" Colors Count : {ColorsCount}");
            sb.AppendLine($" Colors  : ");
            foreach (var item in _cartridges)
                sb.AppendLine($" {item?.ToString() ?? " Cartridge not installed"}");
            sb.AppendLine($" UUID          : {Guid}");
            sb.AppendLine($" Status        : {(Ready ? "Ready to print" : "Not redy to print")}");
            sb.AppendLine($" Power status  : {(OnOff ? "Power On":"Power Off")}");
            return sb.ToString();
        }
    }
}

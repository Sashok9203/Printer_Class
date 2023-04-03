namespace Comp_Class
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Printer?[] printers = new Printer[5]
                {
                    new Printer("E414", 4,  new Cartridge(Colors.Magenta, 200),
                                            new Cartridge(Colors.Cyan, 200) { CurentLevel = 150 },
                                            new Cartridge(Colors.Black, 200),
                                            new Cartridge(Colors.Yellow, 200)),

                    new Printer("G2411",  4,new Cartridge(Colors.Magenta, 150),
                                            new Cartridge(Colors.Cyan, 150) ,
                                            new Cartridge(Colors.Black, 150),
                                            new Cartridge(Colors.Yellow, 150)),

                    new Printer("G1411", 4, new Cartridge(Colors.Magenta, 120),
                                            new Cartridge(Colors.Cyan, 120) ,
                                            new Cartridge(Colors.Black, 120),
                                            new Cartridge(Colors.Yellow, 120)),

                    null,

                    new Printer("SX-250", 4)

                };
                foreach (var item in printers)
                {
                    item?.PrinterOnOff();
                    Console.WriteLine(item?.ToString() ?? "  -= No printer =-\n");

                }
                Console.ReadKey();
                Printer.InputProperties(out string? name, out uint count, out Cartridge?[] crt);
                printers[3] = new Printer(name, count, crt);
                printers[3]?.PrinterOnOff();

                printers[0]?.PrinterOnOff();
                try { printers[0]?.RechargeCartridge(Colors.Cyan); } catch (Exception ex) { Console.WriteLine(ex.Message); }
                printers[0]?.PrinterOnOff();

                try
                {
                    printers[4]?.PrinterOnOff();
                    printers[4]?.setСartridges(new Cartridge(Colors.Magenta, 120),
                                                 new Cartridge(Colors.Cyan, 120),
                                                 new Cartridge(Colors.Black, 120),
                                                 new Cartridge(Colors.Yellow, 120));
                    printers[4]?.PrinterOnOff();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Printer.Swap(ref printers[4], ref printers[0]);

                Console.Clear();
                foreach (var item in printers)
                    Console.WriteLine(item?.ToString() ?? "  -= No printer =-\n");
                Console.ReadKey();

                Console.Clear();

                printers[4]?.Print(new Document { Name = " Text1.txt", pages = 3 });
                printers[4]?.Print(new Document { Name = " Text2.txt", pages = 1 });
                printers[4]?.Print(new Document { Name = " Text3.txt", pages = 2 });

                printers[0]?.Print(new Document { Name = " Text4.txt", pages = 2 });
                printers[0]?.Print(new Document { Name = " Text5.txt", pages = 1 });
                printers[0]?.Print(new Document { Name = " Text6.txt", pages = 2 });

                printers[2]?.Print(new Document { Name = " Text7.txt", pages = 4 });
                printers[2]?.Print(new Document { Name = " Text8.txt", pages = 4 });
                printers[2]?.Print(new Document { Name = " Text9.txt", pages = 4 });

                Console.WriteLine(" All documents in queues...\n");
               
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
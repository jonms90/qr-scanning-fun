using QRScanningFun.Application;

namespace QRScanningFun.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Hello, World!");
            var x = 5;
            var y = 3;
            System.Console.WriteLine("The sum of {0} and {1} is {2}", x, y,
                new Class1().Sum(x, y));
        }
    }
}

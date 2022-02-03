using System;
using System.Threading.Tasks;

namespace Soap
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ServiceReference1.SOAPDemoSoapClient client = new();
            
            var responseInterger = await client.AddIntegerAsync(50, 55);
            Console.WriteLine($"AddIntegerAsync {responseInterger}");

            Console.WriteLine();
            
            var responseDivide = await client.DivideIntegerAsync(10, 5);
            Console.WriteLine($"DivideIntegerAsync {responseDivide}");
            
            Console.WriteLine();
            Console.WriteLine();

            var responsePerson = await client.FindPersonAsync("2");

            Console.WriteLine("InfoPerson");
            Console.WriteLine(responsePerson.Name);
            Console.WriteLine(responsePerson.Age);
            Console.WriteLine(responsePerson.Home.Street);
            Console.WriteLine(responsePerson.Office.Street);
        }
    }
}
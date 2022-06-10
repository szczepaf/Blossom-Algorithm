using System;
using System.Linq;

namespace Blossom_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "", input = "";
            while (!String.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                input += line + "\n";
            }

            Graph k3 = new Graph("K3");
            k3.ParseGraph(input);
            Console.WriteLine(k3.ToString());

            


        }

    }




}

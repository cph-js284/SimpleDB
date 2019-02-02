using System;
using simpledb2;

namespace GetData
{
    class Program
    {
        static void Main(string[] args)
        {   
            SimpleDB sdb = new SimpleDB();
            sdb.CreateMap();
            sdb.printMap();
            System.Console.WriteLine("---------------Retrive data-----------------------");
            sdb.GetData(2);
        }
    }
}

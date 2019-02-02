using System;
using simpledb2;

namespace SetData
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleDB sdb = new SimpleDB();
            System.Console.WriteLine("---------------Set data-----------------------");
            sdb.SetData("1", "First data entry");
            sdb.SetData(2, new int[]{1,2,3,4,5,6,7,8,9,0});
            sdb.SetData('3',"more regular text - key 3 entry");
            sdb.SetData("4", "This value will soon be deleted in the hashmap");
            sdb.SetData("weird_key", "Any type of key is acceptable");
            sdb.SetData("4", 123456);
            System.Console.WriteLine("---------------Set data done-----------------------");


            System.Console.WriteLine("---------------Print offSets-----------------------");
            sdb.CreateMap();
            sdb.printMap();

        }
    }
}

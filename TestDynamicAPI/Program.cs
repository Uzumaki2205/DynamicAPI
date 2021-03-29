using System;
using static TestDynamicAPI.CreateHelper;

namespace TestDynamicAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            InfoVuln info = new InfoVuln();
            info.LoadJson();
            //foreach (var item in info.infoObject.test)
            //{
            //    Console.WriteLine(item);
            //}
            
        }
    }
}

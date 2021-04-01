using System;
using System.IO;
using static TestDynamicAPI.CreateHelper;

namespace TestDynamicAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            InfoVuln info = new InfoVuln();
            info.TimeStamp = info.GetTimestamp(DateTime.Now);

            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists($"{rootPath}Static/{info.TimeStamp}"))
                Directory.CreateDirectory($"{rootPath}Static/{info.TimeStamp}");
            
            info.LoadJson();
            
        }
    }
}

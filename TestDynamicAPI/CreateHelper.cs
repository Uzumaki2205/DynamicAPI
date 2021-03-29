using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NGS.Templater;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Text;

namespace TestDynamicAPI
{
    class CreateHelper
    {
        public class InfoVuln 
        {
            private string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            public dynamic infoObject { get; set; }
            public dynamic tableObject { get; set; }

            public void LoadJson()
            {
                infoObject = new List<Object>();
                tableObject = new List<Object>();

                Dictionary<string, string> obj_Property = new Dictionary<string, string>();
                List<Dictionary<string, string>> obj_Array = new List<Dictionary<string, string>>();
                List<Dictionary<string, string>> temp = new List<Dictionary<string, string>>(); // Xử lý bảng

                File.Copy($"{rootPath}/test.docx", $"{rootPath}/testDocx.docx", true);

                using (StreamReader r = new StreamReader($"{rootPath}/test.json"))
                {
                    string json = r.ReadToEnd();
                    JObject jObject = JObject.Parse(json);

                    using (var doc = Configuration.Factory.Open("testDocx.docx"))
                    {
                        foreach (JProperty property in jObject.Properties())
                        {
                            foreach (var item in property)
                            {
                                if (item.Type == JTokenType.String)
                                {
                                    obj_Property.Add(property.Name, property.Value.ToString());
                                }
                                else if (item.Type == JTokenType.Array)
                                {
                                    temp = ProcessTable(item, doc);

                                    infoObject.Add(obj_Property);

                                    doc.Process(infoObject);
                                    doc.Process(temp); //bảng
                                }
                            }
                        }
                    }
                    
                    //using (var doc = Configuration.Factory.Open("testDocx.docx"))
                    //{
                    //    doc.Process(infoObject);
                    //    doc.Process(temp); //bảng
                    //}
                    Process.Start(new ProcessStartInfo("testDocx.docx") { UseShellExecute = true });
                }
            }

            List<Dictionary<string, string>> ProcessTable(JToken item, ITemplateDocument doc)
            {
                List<Dictionary<string, string>> temp = new List<Dictionary<string, string>>();
                foreach (var Subitem in item)
                {
                    if(Subitem.Type != JTokenType.Array)
                    {
                        Dictionary<string, string> tb = new Dictionary<string, string>();
                        foreach (JProperty Subitem2 in Subitem)
                        {
                            if(Subitem2.Type != JTokenType.Array)
                                tb.Add(Subitem2.Name, Subitem2.Value.ToString());
                            
                        }
                        temp.Add(tb);
                    }
                    //else
                    //{
                    //    List<Dictionary<string, string>> temp2 = new List<Dictionary<string, string>>();
                    //    foreach (var Subitem3 in item)
                    //    {
                    //        Dictionary<string, string> tb = new Dictionary<string, string>();
                    //        foreach (JProperty Subitem3 in Subitem)
                    //        {
                    //            tb.Add(Subitem3.Name, Subitem3.Value.ToString());
                    //        }
                    //        temp2.Add(tb);
                    //    }
                    //    doc.Process(temp2);
                    //} UNDONE
                }
                return temp;
            }
           
        }
    }
}

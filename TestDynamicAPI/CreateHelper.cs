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

            //public void LoadJson()
            //{
            //    infoObject = new List<Object>();
            //    tableObject = new List<Object>();

            //    Dictionary<string, string> obj_Property = new Dictionary<string, string>();
            //    List<Dictionary<string, string>> obj_Array = new List<Dictionary<string, string>>();
            //    List<Dictionary<string, string>> temp = new List<Dictionary<string, string>>(); // Xử lý bảng

            //    File.Copy($"{rootPath}/test.docx", $"{rootPath}/testDocx.docx", true);

            //    using (StreamReader r = new StreamReader($"{rootPath}/test.json"))
            //    {
            //        string json = r.ReadToEnd();
            //        JObject jObject = JObject.Parse(json);

            //        using (var doc = Configuration.Factory.Open("testDocx.docx"))
            //        {
            //            foreach (JProperty property in jObject.Properties())
            //            {
            //                foreach (var item in property)
            //                {
            //                    if (item.Type == JTokenType.String)
            //                    {
            //                        obj_Property.Add(property.Name, property.Value.ToString());
            //                    }
            //                    else if (item.Type == JTokenType.Array)
            //                    {
            //                        temp = ProcessTable(item, doc);

            //                        infoObject.Add(obj_Property);

            //                        doc.Process(infoObject);
            //                        doc.Process(temp); //bảng
            //                    }
            //                }
            //            }
            //        }

            //        Process.Start(new ProcessStartInfo("testDocx.docx") { UseShellExecute = true });
            //    }
            //}

            public void LoadJson()
            {
                infoObject = new List<Object>();
                tableObject = new List<Object>();

                Dictionary<string, string> obj_Property = new Dictionary<string, string>();
                List<Dictionary<string, string>> obj_Array = new List<Dictionary<string, string>>();
                List<Dictionary<string, Object>> temp = new List<Dictionary<string, Object>>(); // Xử lý bảng

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
                                    temp = ProcessTable(item);

                                    infoObject.Add(obj_Property);

                                    doc.Process(infoObject);
                                    doc.Process(temp); //bảng
                                }
                            }
                        }
                    }

                    Process.Start(new ProcessStartInfo("testDocx.docx") { UseShellExecute = true });
                }
            }

            List<Dictionary<string, Object>> ProcessTable(JToken array)
            {
                List<Dictionary<string, Object>> Dictionary_Looping = new List<Dictionary<string, Object>>();

                foreach (JToken item in array)
                {
                    // Dictionary string là key, object có thể là string key có thể là List của Dictionary<string, string> khác để xác định array trong array
                    Dictionary<string, Object> dic = new Dictionary<string, Object>();
                    foreach (JProperty subItem in item)
                    {
                        foreach (var tempArr in subItem)
                        {
                            // Nếu mảng bên trong mảng thì đưa mảng ở bên trong vào list dictionary<string, string> 
                            //rồi add vào dictionary<string, Dictionary<string,string>>
                            if (tempArr.Type == JTokenType.Array)
                            {
                                List<Dictionary<string, string>> lstArr2 = new List<Dictionary<string, string>>(); // List của mảng json bên trong
                                foreach (JToken itemArray2p in tempArr)
                                {
                                    Dictionary<string, string> array2p = new Dictionary<string, string>();
                                    foreach (JProperty subItemArray2p in itemArray2p)
                                    {
                                        array2p.Add(subItemArray2p.Name, subItemArray2p.Value.ToString());
                                    }
                                    // Tạo list dictionary của mảng trong mảng :)))
                                    lstArr2.Add(array2p);
                                }
                                dic.Add(subItem.Name, lstArr2);
                            }
                            else
                            {
                                dic.Add(subItem.Name, subItem.Value.ToString());
                            }
                        }
                    }
                    Dictionary_Looping.Add(dic);
                }

                return Dictionary_Looping;
            }

            //List<Dictionary<string, string>> ProcessTable(JToken item, ITemplateDocument doc)
            //{
            //    List<Dictionary<string, string>> temp = new List<Dictionary<string, string>>();
            //    List<Object> temp2 = new List<Object>();


            //    foreach (var Subitem in item)
            //    {
            //        if(Subitem.Type != JTokenType.Array)
            //        {
            //            foreach (var test in Subitem)
            //            {
            //                foreach (JToken test2 in test)
            //                {
            //                    if (test2.Type == JTokenType.Array)
            //                    {
            //                        //List<Object> tbtest = new List<Object>();
            //                        foreach (var restest in test2)
            //                        {
            //                            Dictionary<string, string> dic = new Dictionary<string, string>();
            //                            foreach (JProperty r in restest)
            //                            {

            //                                dic.Add(r.Name, r.Value.ToString());

            //                               // tbtest.Add(dic);            
            //                            }
            //                            temp.Add(dic);
            //                        }

            //                        //temp2 = tbtest;
            //                    }
            //                }
            //            }


            //            Dictionary<string, string> tb = new Dictionary<string, string>();
            //            foreach (JProperty Subitem2 in Subitem)
            //            {
            //                //if (Subitem2.Type == JTokenType.Property)
            //                tb.Add(Subitem2.Name, Subitem2.Value.ToString());

            //            }
            //            temp.Add(tb);
            //        }
            //        //else
            //        //{
            //        //    List<Dictionary<string, string>> temp2 = new List<Dictionary<string, string>>();
            //        //    foreach (var Subitem3 in item)
            //        //    {
            //        //        Dictionary<string, string> tb = new Dictionary<string, string>();
            //        //        foreach (JProperty Subitem3 in Subitem)
            //        //        {
            //        //            tb.Add(Subitem3.Name, Subitem3.Value.ToString());
            //        //        }
            //        //        temp2.Add(tb);
            //        //    }
            //        //    doc.Process(temp2);
            //        //} UNDONE
            //    }
            //    return temp;
            //}

        }
    }
}

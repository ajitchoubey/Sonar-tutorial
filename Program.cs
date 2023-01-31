using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> ls = new List<string>();
            List<String> ls1 = new List<string>();
            ls.Add("90316943");
            ls.Add("90316943_");
            ls.Add("90316943_dpi66268");
            ls.Add("90316943_66268");
            ls.Add("90316943_DPI66268");
            string name;
            string name1;
            string str1 = "";

            foreach (var emp in ls)
            {
                string empcode = "";
                string substr = "";
                if (!emp.ToUpper().Contains("_"))
                {
                    empcode = (emp + "_StoreCode");
                }
                if (empcode == "")
                {
                    int indx = emp.IndexOf("_");
                    if (indx != -1)
                    {
                        substr = emp.Substring(indx+1);
                        if (substr != "")
                        {
                            empcode = emp.ToUpper();
                            
                        }
                        else
                        {
                            empcode = (emp + "StoreCode");
                        }
                    }
                    
                }
                ls1.Add(empcode);
                //else if(!emp.ToUpper().Contains("_"))
                //else if (!emp.ToUpper().Contains("_DPI"))
                //{
                //    empcode = (emp + "_StoreCode").ToUpper();
                //}
                //ls1.Add();
            }
            foreach(var p in ls1)
            {
                Console.WriteLine(p);
            }
            Console.ReadLine();
        }
    }
}

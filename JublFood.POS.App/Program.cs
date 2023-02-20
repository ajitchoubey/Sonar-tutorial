using JublFood.POS.App.BusinessLayer;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using Jublfood.AppLogger;
using JublFood.POS.App.Class;
using JublFood.POS.App.Cache;
using System.Management;
using System.Threading;

namespace JublFood.POS.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {                
                UserFunctions.AppendToLog("Step 1", "", "");
                Console.WriteLine("TimeStamp1 :" + DateTime.Now);
                if (CheckCurrentProcessRunning()) return;
                UserFunctions.AppendToLog("Step 2", "", "");
                //if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1) return;
                Console.WriteLine("TimeStamp2 :" + DateTime.Now);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SplashThread.StartSplashThread();

                Console.WriteLine("TimeStamp3 :" + DateTime.Now);
                if (!PreDocker.LoadPOS())
                {
                    Application.Exit();
                }
                else
                {
                    Console.WriteLine("TimeStamp4 :" + DateTime.Now);
                    //new MemoryStore().LoadMemory();
                    MemoryStore memoryStore = new MemoryStore();
                    memoryStore.LoadMemoryAsync();
                    Console.WriteLine("TimeStamp5 :" + DateTime.Now);
                    memoryStore.LoadMemory();
                    Console.WriteLine("TimeStamp6 :" + DateTime.Now);
                    Application.Run(new MultiFormContext(null, new frmCustomer(), new frmLogin()));
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "Program-Main(): " + ex.Message, ex, true);
                CustomMessageBox.Show(MessageConstant.InternalErrorApplication, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Error);
            }
        }

        public class MultiFormContext : ApplicationContext
        {
            private int openForms;
            public MultiFormContext(Form currentForm,params Form[] forms)
            {
                openForms = forms.Length;
                forms[0].Show();
                if (currentForm != null) currentForm.Close();
                forms[1].ShowDialog();
            }
        }

        public static bool CheckCurrentProcessRunning()
        {
            int count = 0;
            foreach(Process process in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
            {
                string UserName = GetProcessOwner(process.Id);
                if (Environment.UserName.ToLower() == UserName.ToLower())
                {
                    if (count > 0)
                    {
                        UserFunctions.AppendToLog("CheckCurrentProcessRunning() - ", process.ProcessName + " is running for user " + UserName, "");
                        return true;
                    }
                    else
                        count++;
                }
            }
            return false;
        }

        public static string GetProcessOwner(int processId)
        { 
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query);
            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection moCollection = moSearcher.Get();

            foreach (ManagementObject mo in moCollection)
            {
                string[] args = new string[] { string.Empty };
                int returnVal = Convert.ToInt32(mo.InvokeMethod("GetOwner", args));
                if (returnVal == 0)
                    return args[0];
            }

            return "N/A";
        }       
    }
}

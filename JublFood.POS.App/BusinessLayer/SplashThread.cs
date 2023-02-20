using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JublFood.POS.App.BusinessLayer
{
    public static class SplashThread
    {
        private static Thread thread;
        public static void StartSplashThread()
        {
            try
            {
                ThreadStart threadStart = new ThreadStart(ShowSplashScreen);
                thread = new Thread(threadStart);
                thread.Start();
            }
            catch (Exception ex)
            {

            }
        }

        public static void StopSplashThread()
        {
            try
            {
                thread.Abort();
                //thread.Join();
            }
            catch (ThreadAbortException ex)
            {

            }
        }

        private static void ShowSplashScreen()
        {
            try
            {
                frmSplash frmSplash = new frmSplash();
                frmSplash.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }
    }


}

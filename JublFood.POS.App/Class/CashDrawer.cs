using Jublfood.AppLogger;
using JublFood.POS.App.Cache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace JublFood.POS.App.Class
{
    class CashDrawer
    {
        

        #region Application Constant Definitions
        public static SerialPort ConnectedComPort = new SerialPort();
        #endregion


        #region Private Variables
        private byte[] RS232SendBuffer = new byte[2048];
        private int RS232SendBufferLength = 0;
        #endregion

        public static bool SendDataToWorkStation(string workstationIP,string workstation)
        {
            bool sendDataStatus = false;
            try
            {
                TcpClient tcpClient = new TcpClient(workstationIP, 2021);
                StreamReader reader = new StreamReader(tcpClient.GetStream());
                StreamWriter writer = new StreamWriter(tcpClient.GetStream());
                
                writer.WriteLine(workstation);
                writer.Flush();

                reader.Close();
                writer.Close();
                tcpClient.Close();
                sendDataStatus = true;
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "SendDataToWorkStation: " + ex.Message, ex, true);
                sendDataStatus = false; 
            }

            return sendDataStatus;
        }


        public static bool ReceiveDataFromWrokStation()
        {
            TcpListener tcpListener = null;
            bool recStatus = false;
            try
            {
                string workstation = Environment.MachineName;
                //Console.WriteLine(workstation);
                IPAddress localhost = IPAddress.Parse(Session.WorkstationIP);
                tcpListener = new TcpListener(localhost, 2021);
                tcpListener.Start();
                //Console.WriteLine("Server Started...");
                while (true)
                {
                    //Console.WriteLine("Waiting for incomming client connection...");
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    //Console.WriteLine("Accepted new client connection...");
                    StreamReader reader = new StreamReader(tcpClient.GetStream());
                    StreamWriter writer = new StreamWriter(tcpClient.GetStream());
                    string s = string.Empty;
                    s = reader.ReadLine();
                    //while (!s.Equals("Exit"))
                    //{
                        //Console.WriteLine("From client ->" + s);
                        if (Session.WorkstationIP == s)
                        {
                            recStatus = true;
                        }
                        //Console.WriteLine("From client ->" + s);
                        //Console.WriteLine("From service ->" + s);
                        writer.Flush();
                    //}
                    reader.Close();
                    writer.Close();
                    tcpClient.Close();



                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "ReceiveDataFromWrokStation: " + ex.Message, ex, true);
                recStatus = false;
            }
            return recStatus;
        }


        public void OpenDrawer()
        {

            SerialPort ConnectedComPort = new SerialPort();
            byte[] RS232SendBuffer = new byte[2048];
            int RS232SendBufferLength = 0;

            if (ConnectedComPort.IsOpen)
            {

                bool error = false;
                try
                {
                    ConnectedComPort.Close();
                }
                catch (Exception)
                {
                    error = true;
                }
                finally
                {
                    //lblStatusBar.Text = cmbPortName.Text + " is closed!!";
                }

                if (error)
                {
                    throw new ApplicationException("ExceptionOnCloseSerialPort");
                }
            }
            else
            {
                // Set the port's settings
                ConnectedComPort.BaudRate = 9600;
                ConnectedComPort.DataBits = 8;
                ConnectedComPort.StopBits = System.IO.Ports.StopBits.One;
                ConnectedComPort.Parity = System.IO.Ports.Parity.None;
                ConnectedComPort.PortName = "COM1";

                bool error = false;
                try
                {

                    ConnectedComPort.Open();
                    //CR Port don't need check RTS/DTR
                    ConnectedComPort.RtsEnable = false;
                    ConnectedComPort.DtrEnable = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    error = true;
                }

                if (error)
                {
                    string DisplayMessage = "MessageString_OpenSerialPortError";
                    string MessageCaption = "MessageString_SerialPortUnavalible";
                    //MessageBox.Show(DisplayMessage, MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if (ConnectedComPort.IsOpen)
                    {
                        ConnectedComPort.Close();
                    }
                    return;
                }
                else
                {
                    //lblStatusBar.Text = cmbPortName.Text + " is opened!!";
                }
            }

            //EnableControlsBySerialPortStatus();
            //OpenDrawer();

            byte[] SendBytes = new byte[2];
            SendBytes[0] = 0x07;
            SendBytes[1] = 0x17;

            Array.Copy(SendBytes, 0, RS232SendBuffer, 0, SendBytes.Length);
            RS232SendBufferLength = SendBytes.Length;
            //port_DataSend();
            ConnectedComPort.Write(RS232SendBuffer, 0, RS232SendBufferLength);
            ConnectedComPort.Dispose(); 
        }

      

        //private void OpenDrawer()
        //{
        //    //Console.WriteLine("10");
        //    //Console.ReadLine();
        //    //if (ConnectedComPort.IsOpen == false)
        //    //{
        //    //    return;
        //    //}

        //    byte[] SendBytes = new byte[2];
        //    SendBytes[0] = 0x07;
        //    SendBytes[1] = 0x17;

        //    Array.Copy(SendBytes, 0, RS232SendBuffer, 0, SendBytes.Length);
        //    RS232SendBufferLength = SendBytes.Length;
        //    port_DataSend();
        //}

        private bool port_DataSend()
        {
            //Console.WriteLine("11");
            //Console.ReadLine();
            //if (!ConnectedComPort.IsOpen)
            //{
            //    return false;
            //}
            // Send the binary data out the port
            //ConnectedComPort.Write(RS232SendBuffer, 0, RS232SendBufferLength);
            return true;
        }

        public static bool CashDrawerClose()
        {
            if (ConnectedComPort.IsOpen)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        public void MakeWindowActive(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);
            if (p.Count() > 0) SetForegroundWindow(p[0].MainWindowHandle);
        }
        public void OpenDrawerCommon()
        {
            Process p = System.Diagnostics.Process.Start("cmd.exe");
            MakeWindowActive(p.ProcessName);

            System.Threading.Thread.Sleep(1000);

            SendKeys.Send("echo ");
            SendKeys.Send("^{G}");
            SendKeys.Send(" > COM1");
            SendKeys.Send("~");
            p.Kill();
        }


    }

}

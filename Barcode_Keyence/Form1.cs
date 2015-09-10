using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Keyence.AutoID;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Barcode_Keyence
{
    public partial class Form1 : Form
    {
        //Integer variable to store command port
        private int commandport;
        //Integer variable to store data port
        private int dataport;
        //StreamWriter variables to write data into file and logs into log file
        private StreamWriter streamWriter,streamLogWriter;
        //Holds the path where the data file should be
        private string filePath="";
        //Holds the path where the image file should be
        private string folderPath="";
        //Holds the name of the data file should be
        private string fileName="";
        //Holds the newly generated name of the data file should be
        private string fileNameNew="";
        private string imageString = "";
        /// <summary>
        /// A default constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Method to create a StreamWriter object to write barcode data
        /// </summary>
        /// <param name="filePath">Location of the file</param>
        /// <returns></returns>
        private StreamWriter getWriter(string filePath)
        {
            if (streamWriter == null)
            {
                streamWriter = new StreamWriter(filePath,true);
            }
            if (streamWriter.BaseStream == null)
            {
                streamWriter = null;
                streamWriter = new StreamWriter(filePath, true);
            }
            return streamWriter;
        }
        /// <summary>
        /// Method to create a StreamWriter object to write log data
        /// </summary>
        /// <param name="filePath">Location of the file</param>
        /// <returns></returns>
        private StreamWriter getLogWriter(string filePath)
        {
            if (streamLogWriter == null)
            {
                streamLogWriter = new StreamWriter(filePath, true);
            }
            if (streamLogWriter.BaseStream == null)
            {
                streamLogWriter = null;
                streamLogWriter = new StreamWriter(filePath, true);
            }
            return streamLogWriter;
        }
        /// <summary>
        /// Method to write logs into log file
        /// </summary>
        /// <param name="status">Log Details</param>
        private void WriteLog(string status)
        {
            getLogWriter("log.log").WriteLine(status);
            getLogWriter("log.log").Flush();
        }
        private void CloseLog()
        {
            getLogWriter("log.log").Close();
        }
        /// <summary>
        /// Method to write data into data file
        /// </summary>
        /// <param name="data">Barcode data details</param>
        private void WriteData(string data,string date)
        {
            getWriter(filePath).WriteLine(string.Format("{0},{1}"),data,date);
            getWriter(filePath).Flush();
        }
        private void CloseData()
        {
            getWriter(filePath).Close();
        }
        private void OnFocusCommunicationPort(object Sender,EventArgs e)
        {
            textBox_communicationport.Text = "";
        }
        private void OnLostFocusCommunicationPort(object Sender, EventArgs e)
        {
            if (textBox_communicationport.Text.Equals(""))
            {
                textBox_communicationport.Text = "9003";
            }
        }
        private void OnFocusDataPort(object Sender, EventArgs e)
        {
            textBox_dataport.Text = "";
        }
        private void OnLostFocusDataPort(object Sender, EventArgs e)
        {
            if (textBox_dataport.Text.Equals(""))
            {
                textBox_dataport.Text = "9004";
            }
        }
        private void OnFocusIPAddress1(object Sender, EventArgs e)
        {
            textBox_ipaddress1.Text = "";
        }
        private void OnLostFocusIPAddress1(object Sender, EventArgs e)
        {
            if (textBox_ipaddress1.Text.Equals(""))
            {
                textBox_ipaddress1.Text = "192";
            }
            else if (Convert.ToInt32(textBox_ipaddress1.Text) > 255)
            {
                textBox_ipaddress1.Text = "192";
            }
        }
        private void OnFocusIPAddress2(object Sender, EventArgs e)
        {
            textBox_ipaddress2.Text = "";
        }
        private void OnLostFocusIPAddress2(object Sender, EventArgs e)
        {
            if (textBox_ipaddress2.Text.Equals(""))
            {
                textBox_ipaddress2.Text = "168";
            }
            else if (Convert.ToInt32(textBox_ipaddress2.Text) > 255)
            {
                textBox_ipaddress2.Text = "168";
            }
        }
        private void OnFocusIPAddress3(object Sender, EventArgs e)
        {
            textBox_ipaddress3.Text = "";
        }
        private void OnLostFocusIPAddress3(object Sender, EventArgs e)
        {
            if (textBox_ipaddress3.Text.Equals(""))
            {
                textBox_ipaddress3.Text = "1";
            }
            else if (Convert.ToInt32(textBox_ipaddress3.Text) > 255)
            {
                textBox_ipaddress3.Text = "1";
            }
        }
        private void OnFocusIPAddress4(object Sender, EventArgs e)
        {
            textBox_ipaddress4.Text = "";
        }
        private void OnLostFocusIPAddress4(object Sender, EventArgs e)
        {
            if (textBox_ipaddress4.Text.Equals(""))
            {
                textBox_ipaddress4.Text = "1";
            }
            else if(Convert.ToInt32(textBox_ipaddress4.Text)>255)
            {
                textBox_ipaddress4.Text = "1";
            }
        }
        /// <summary>
        /// This is the method to initialize controls when form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_ipaddress1.Text = "192";
            textBox_ipaddress2.Text = "168";
            textBox_ipaddress3.Text = "1";
            textBox_ipaddress4.Text = "1";
            textBox_communicationport.Text = "9003";
            textBox_dataport.Text = "9004";
            textBox_communicationport.GotFocus += OnFocusCommunicationPort;
            textBox_communicationport.LostFocus += OnLostFocusCommunicationPort;
            textBox_dataport.GotFocus += OnFocusDataPort;
            textBox_dataport.LostFocus += OnLostFocusDataPort;
            textBox_ipaddress1.GotFocus += OnFocusIPAddress1;
            textBox_ipaddress1.LostFocus += OnLostFocusIPAddress1;
            textBox_ipaddress2.GotFocus += OnFocusIPAddress2;
            textBox_ipaddress2.LostFocus += OnLostFocusIPAddress2;
            textBox_ipaddress3.GotFocus += OnFocusIPAddress3;
            textBox_ipaddress3.LostFocus += OnLostFocusIPAddress3;
            textBox_ipaddress4.GotFocus += OnFocusIPAddress4;
            textBox_ipaddress4.LostFocus += OnLostFocusIPAddress4;
            button_close.Enabled = false;
        }
        private void Connect()
        {
            try
            {
                if (checkBox_Monitor.Checked)
                {
                    timer_Monitor.Start();
                    WriteLog("Date Time Monitor Started..."+"   at" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss:fff") + "\n");
                    textBox_status.AppendText("Date Time Monitor Started...");
                }
                Byte[] ipaddress = new byte[4];
                ipaddress[0] = Convert.ToByte(textBox_ipaddress1.Text.Trim());
                ipaddress[1] = Convert.ToByte(textBox_ipaddress2.Text.Trim());
                ipaddress[2] = Convert.ToByte(textBox_ipaddress3.Text.Trim());
                ipaddress[3] = Convert.ToByte(textBox_ipaddress4.Text.Trim());
                commandport = Convert.ToInt32(textBox_communicationport.Text.Trim());
                dataport = Convert.ToInt32(textBox_dataport.Text.Trim());
                IPAddress ip = new IPAddress(ipaddress);
                this.barcodeReaderControl1.IpAddress = ip.ToString();
                WriteLog("IP Address is " + ip.ToString() + "\n");
                //textBox_status.Text = "IP Address is " + ip.ToString() + "\n";
                this.barcodeReaderControl1.Ether.CommandPort = commandport;
                WriteLog("Command Port is " + commandport.ToString() + "\n");
                //textBox_status.Text = "Command Port is " + commandport.ToString() + "\n";
                this.barcodeReaderControl1.Ether.DataPort = dataport;
                WriteLog("Data Port is " + dataport.ToString() + "\n");
                //textBox_status.Text = "Data Port is " + dataport.ToString() + "\n";
                this.barcodeReaderControl1.Comm.Interface = Interface.Ethernet;
                WriteLog("Interface is " + Interface.Ethernet.ToString() + "\n");
                //textBox_status.Text = "Interface is " + Interface.Ethernet.ToString() + "\n";
                this.barcodeReaderControl1.ReaderType = ReaderType.SR_1000;
                WriteLog("Reader Type is " + ReaderType.SR_1000.ToString() + "\n");
                //textBox_status.Text = "Reader Type is " + ReaderType.SR_1000.ToString() + "\n";
                textBox_status.AppendText("Socket Connecting...\n");
                this.barcodeReaderControl1.Connect();
                textBox_status.AppendText("Socket Connected...\n");
                this.barcodeReaderControl1.OnDataReceived += dataReceived;
                textBox_status.AppendText("Live View Starting...\n");
                this.barcodeReaderControl1.StartLiveView();
                textBox_status.AppendText("Live View Started...\n");
                button_connect.Enabled = false;
                button_close.Enabled = true;
            }
            catch (SocketException ex)
            {
                //timer_Monitor.Stop();
                WriteLog(ex.ToString() + Environment.NewLine + "   at " + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                textBox_status.AppendText("Socket Exception Occured. Please refer log file." + "\n");
                button_connect.Enabled = true;
                button_close.Enabled = false;
            }
            catch (Exception ex)
            {
                //timer_Monitor.Stop();
                WriteLog(ex.ToString() + Environment.NewLine + "   at " + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                textBox_status.AppendText("Exception Occured. Please refer log file." + "\n");
                button_connect.Enabled = true;
                button_close.Enabled = false;
            }
            finally
            {
                CloseLog();
            }
        }
        /// <summary>
        /// This is the method to handle close button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_connect_Click(object sender, EventArgs e)
        {
            if (!groupBox6.Controls.OfType<RadioButton>().Any(x => x.Checked))
            {
                WriteLog("Choose One File Storing Type..." + "   at" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss:fff") + "\n");
                textBox_status.AppendText("Choose One File Storing Type...");
                return;
            }
            if (File.Exists(filePath)&&Directory.Exists(folderPath))
            {
                Connect();
            }
            else
            {
                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Enter File Name...";
                saveFileDialog1.Filter = "Comma Separated Values (*.csv)|*.csv";
                if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
                {
                    filePath = saveFileDialog1.FileName;
                    fileName = Path.GetFileNameWithoutExtension(filePath);
                    fileNameNew = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    textBox_FileName.Text = filePath;
                    WriteLog("Data will be written to " + filePath + "   at" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "\n");
                    textBox_status.AppendText("Data will be written to " + filePath + "     \n");
                }
                else
                {
                    return;
                }
                folderBrowserDialog1 = new FolderBrowserDialog();
                if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
                {
                    folderPath = folderBrowserDialog1.SelectedPath;
                    textBox_ImagePath.Text = folderPath;
                    WriteLog("Images will be stored to " + folderPath + "   at" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "\n");
                    textBox_status.AppendText("Images will be stored to " + folderPath + "     \n");
                }
                else
                {
                    return;
                }
                Connect();
            }
        }
        private delegate void updateData(byte[] data);
        private void updateDataValue(byte[] data)
        {
            try
            {
                string dataString = Encoding.GetEncoding("Shift_JIS").GetString(data);
                string dateString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                if (radioButton_Individual.Checked)
                {
                    fileNameNew = fileName + "_"+DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    filePath = Path.Combine(Path.GetDirectoryName(filePath),fileNameNew);
                }
                if (radioButton_Combined.Checked)
                {
                    filePath = Path.Combine(Path.GetDirectoryName(filePath), fileNameNew);
                }
                WriteData(dataString, dateString);
                if (!dataString.StartsWith("ERROR"))
                    imageString = barcodeReaderControl1.LSIMG();
                textBox_status.AppendText(dataString+"\n");
                textBox_status.AppendText("Image Stored at "+imageString);
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString() + "   at" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "\n");
                textBox_status.AppendText("Exception Occured. Please refer log file."+"\n");
            }
            finally
            {
                CloseData();
                CloseLog();
            }
        }
        /// <summary>
        /// This is the function to write barcode data into file and it is called whenever there is an OnDataReceived event occurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataReceived(object sender,OnDataReceivedEventArgs e)
        {
                textBox_status.Invoke(new updateData(updateDataValue),e.data);
        }
        /// <summary>
        /// This is the method to handle close button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            try
            {
                this.barcodeReaderControl1.OnDataReceived -= dataReceived;
                this.barcodeReaderControl1.StopLiveView();
                this.barcodeReaderControl1.Disconnect();
                CloseLog();
                CloseData();
                textBox_status.AppendText("Socked Closed...\n");
                button_close.Enabled = false;
                button_connect.Enabled = true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString() + Environment.NewLine + "   at " + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                textBox_status.AppendText("Exception Occured. Please refer log file." + "\n");
                button_close.Enabled = true;
                button_connect.Enabled = false;
            }
        }

        private void timer_Monitor_Tick(object sender, EventArgs e)
        {
            label_DateTime.Text = DateTime.Now.ToString("HH:mm:ss:fff");
        }

    }
}

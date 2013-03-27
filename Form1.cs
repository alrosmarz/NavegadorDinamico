using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.IO;

namespace NavegadorDinamico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Uri currentUri;
        private void button1_Click(object sender, EventArgs e)
        {
            ////for (int i = 0; i < 100; i++)
            ////{
            //string key_enable = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            //string serverName_enable = "123.129.240.174";//your proxy server name;
            //string port_enable = "8081"; //your proxy port;
            //string proxy_enable = serverName_enable + ":" + port_enable;

            //RegistryKey RegKey_enable = Registry.CurrentUser.OpenSubKey(key_enable, true);

            //RegKey_enable.SetValue("ProxyServer", proxy_enable);
            //RegKey_enable.SetValue("ProxyEnable", 1);

            //webBrowser1.Navigate("http://zuperhosting.net/link.php?u=1102");
            //    //System.Threading.Thread.Sleep(5000);
            ////}

            System.Threading.Thread t = new System.Threading.Thread(checa);
            t.Start();
        }

        void checa()
        {

            StreamReader sr = new StreamReader(@"C:\p.txt");
            while (sr.Peek() >= 0)
            {
                string proxy = sr.ReadLine();
                //textBox1.Text = sr.ReadLine();
                try
                {
                    

                    //validacion de lineas haber si ya paso el proxy
                    bool abrirconexion = true;
                    StreamReader sr2 = new StreamReader(@"C:\p2.txt");
                    while (sr2.Peek() >= 0)
                    {
                        //string linea2 = sr2.ReadLine();
                        if (proxy == sr2.ReadLine())
                            abrirconexion = false;
                    }
                    sr2.Close();
                    sr2.Dispose();
                    
                    if (abrirconexion)
                    {
                        StreamWriter sw = File.AppendText(@"c:\p2.txt");
                        sw.WriteLine(proxy);
                        sw.Flush();
                        sw.Close();

                        currentUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["url"].ToString());
                        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["url"].ToString());
                        WebProxy myProxy = new WebProxy(proxy);
                        myRequest.Proxy = myProxy;

                        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

                        webBrowser1.ScriptErrorsSuppressed = true;

                        webBrowser1.DocumentStream = myResponse.GetResponseStream();

                        webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);



                        System.Threading.Thread.Sleep(10000);

                        //t2.Abort();
                    }
                }
                catch (Exception ex)
                {
                    //label1.Text = "Fallo de conexión en: " + textBox1.Text;
                    GC.Collect();
                }
            }

            MessageBox.Show("Ya acabe");
        }

        void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {

                if (e.Url.AbsolutePath != "blank")
                {
                    currentUri = new Uri(currentUri, e.Url.AbsolutePath);
                    HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(currentUri);

                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

                    webBrowser1.DocumentStream = myResponse.GetResponseStream();
                    e.Cancel = true;
                }

            }
            catch (Exception ex)
            {
                //label1.Text = "Fallo de conexión en: " + textBox1.Text;
                GC.Collect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}

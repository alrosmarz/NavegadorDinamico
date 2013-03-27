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

            StreamReader sr = new StreamReader(@"C:\p\p.txt");
            while (sr.Peek() >= 0)
            {
                string proxy = sr.ReadLine();
                //textBox1.Text = sr.ReadLine();
                try
                {
                    

                    //validacion de lineas haber si ya paso el proxy
                    bool abrirconexion = true;
                    StreamReader sr2 = new StreamReader(@"C:\p\p2.txt");
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
                        StreamWriter sw = File.AppendText(@"c:\p\p2.txt");
                        sw.WriteLine(proxy);
                        sw.Flush();
                        sw.Close();

                        currentUri = new Uri(@"http://www.googleadservices.com/pagead/aclk?sa=L&ai=CiUauaShTUfrjOqS6wQHeiYCwC4zTp_gC7N2Ry0-M566TxAEQASD2y9kRULPZkK4CYOXz_YTYFqAB9tul9QPIAQGoAwHIA9MEqgSDAU_QD3P3bVgfCMrPnbUNTjWlJHBxja70yeiD1vT_xBMz1zV92_OqnM1ZHke8DYR-5FXikhk5yLxmb69W-UB5NoP6J6EtnsdDNt3B6_GzlWnY2aUs3oIXzF3dSPeY0Lxm1WXTa4hQXBAIoG2b968_V9D2FTIZ_AcOh_bCASRx_tsz2Az7iAYBgAfyo9oK&num=1&cid=5GjscZc68gZW8bUzHSTxYx_m&sig=AOD64_0QEHpHBjPLp4wJkuL2XRqyfCj6NQ&client=ca-pub-7601976974643505&adurl=http://pixel.everesttech.net/3098/cq%3Fev_sid%3D3%26ev_cmpid%3D118499004%26ev_ln%3D%26ev_crx%3D21284726844%26ev_mt%3D%26ev_n%3Dd%26ev_ltx%3D%26ev_pl%3Dcancunet.blogspot.mx%26ev_pos%3Dnone%26ev_dvc%3Dc%26ev_dvm%3D%26url%3Dhttp%253A//es.godaddy.com/deals2/%253Fisc%253Dgtng2cla02%2526currencytype%253DUSD&nm=12&mb=2");
                        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://www.googleadservices.com/pagead/aclk?sa=L&ai=CiUauaShTUfrjOqS6wQHeiYCwC4zTp_gC7N2Ry0-M566TxAEQASD2y9kRULPZkK4CYOXz_YTYFqAB9tul9QPIAQGoAwHIA9MEqgSDAU_QD3P3bVgfCMrPnbUNTjWlJHBxja70yeiD1vT_xBMz1zV92_OqnM1ZHke8DYR-5FXikhk5yLxmb69W-UB5NoP6J6EtnsdDNt3B6_GzlWnY2aUs3oIXzF3dSPeY0Lxm1WXTa4hQXBAIoG2b968_V9D2FTIZ_AcOh_bCASRx_tsz2Az7iAYBgAfyo9oK&num=1&cid=5GjscZc68gZW8bUzHSTxYx_m&sig=AOD64_0QEHpHBjPLp4wJkuL2XRqyfCj6NQ&client=ca-pub-7601976974643505&adurl=http://pixel.everesttech.net/3098/cq%3Fev_sid%3D3%26ev_cmpid%3D118499004%26ev_ln%3D%26ev_crx%3D21284726844%26ev_mt%3D%26ev_n%3Dd%26ev_ltx%3D%26ev_pl%3Dcancunet.blogspot.mx%26ev_pos%3Dnone%26ev_dvc%3Dc%26ev_dvm%3D%26url%3Dhttp%253A//es.godaddy.com/deals2/%253Fisc%253Dgtng2cla02%2526currencytype%253DUSD&nm=12&mb=2");

                        //currentUri = new Uri(@"http://cancunet.blogspot.mx/2013/03/algo-que-siempre-es-necesario-en-un.html");
                        //HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(@"http://cancunet.blogspot.mx/2013/03/algo-que-siempre-es-necesario-en-un.html");
                        
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

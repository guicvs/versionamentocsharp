using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;
namespace Aplicao
{
    public partial class Form1 : Form
    {
        string minha_versao = "1.0.0";
        public Form1()
        {
            InitializeComponent();
        }

        public void baixar_nova_versao(string url)
        {
            WebClient baixar = new WebClient();
            try {

                baixar.DownloadFile(url, Application.StartupPath + "//versao_nova.exe");
            } catch (Exception ex) {
                MessageBox.Show("link inexistente");
            }
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int random = new Random().Next(0, 999999);
            WebClient url = new WebClient();
            url.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            string xml_data = url.DownloadString("https://raw.githubusercontent.com/guicvs/versionamentocsharp/master/version.xml?r=" + random);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xml_data);

            string version = xml.GetElementsByTagName("version").Item(0).InnerText;
            string binary = xml.GetElementsByTagName("binary").Item(0).InnerText;
            if (version != minha_versao)
            {
                MessageBox.Show("Esse programa está desatualizado");
                baixar_nova_versao(binary);
                return;
            }

            MessageBox.Show("Seu programa está atualizado");
        }
    }
}

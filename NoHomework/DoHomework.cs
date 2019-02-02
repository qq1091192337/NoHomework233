using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static NoHomework.Global;
using HtmlAgilityPack;
namespace NoHomework
{
    public partial class DoHomework : Form
    {
        public DoHomework()
        {
            InitializeComponent();
        }
        List<Image> List_images = new List<Image>();

        private void DoHomework_Load(object sender, EventArgs e)
        {
            this.Text = questions.task.taskName;
            
            foreach (var item in questions.data)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                doc.LoadHtml(item.teaTitle);

                HtmlNodeCollection hrefList = doc.DocumentNode.SelectNodes(".//img[@src]");

                if (hrefList != null)
                {
                    foreach (HtmlNode href in hrefList)
                    {
                        HtmlAttribute att = href.Attributes["src"];
                        if(att!=null)
                        {
                            string url = att.Value;

                            if(url!=null)
                            {
                                Image _image = Image.FromStream(WebRequest.Create(url).GetResponse().GetResponseStream());

                                imageList1.Images.Add(_image);

                                List_images.Add(_image);
                            }
                            break;
                        }
                    }
                }
            }
            listView1.LargeImageList = imageList1;
            listView1.BeginUpdate();
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.ImageIndex = i;
                listViewItem.Text = "题目" + i.ToString();
                listView1.Items.Add(listViewItem);
                
            }
            listView1.EndUpdate();
        }

        private void DoHomework_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.ShutDown();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count!=0)
            {
                pictureBox1.Image = List_images[listView1.SelectedItems[0].Index];
            }

        }
    }
}

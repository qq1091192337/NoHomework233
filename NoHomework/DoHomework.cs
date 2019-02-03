using Baidu.Aip.Ocr;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Tesseract;
using static NoHomework.Global;
namespace NoHomework
{
    public partial class DoHomework : Form
    {
        public int taskId { get; set; }

        public string tmpFilePath { get { return Path.Combine(Path_Data, taskId.ToString()); } }

        public DoHomework()
        {
            InitializeComponent();
        }

        private List<Image> List_images = new List<Image>();

        private List<string> List_Questions = new List<string>();

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
                        if (att != null)
                        {
                            string url = att.Value;

                            if (url != null)
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
            //var ImageString = JsonConvert.SerializeObject(List_images);
            //using (StreamWriter streamWriter = new StreamWriter(Global.Path_Image, true))
            //{
            //    streamWriter.Write(ImageString);
            //}
            Global.ShutDown();
        }

        private void OCR()
        {
            var ocr = new TesseractEngine("./tessdata", "chi_sim", EngineMode.TesseractOnly);

            var page = ocr.Process((Bitmap)List_images[listView1.SelectedItems[0].Index]);

            textBox1.Text = page.GetText();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count != 0)
            {
                pictureBox1.Image = List_images[listView1.SelectedItems[0].Index];

                if (!Directory.Exists(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
                {
                    Directory.CreateDirectory(tmpFilePath);
                }

                if (File.Exists(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
                {
                    using (StreamReader streamReader = new StreamReader(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
                    {
                        textBox1.Text=streamReader.ReadToEnd();
                        button5.Enabled = false;
                        return;
                    }
                }
                button5.Enabled = true;
                //var ocr = new TesseractEngine("./tessdata", "chi_sim", EngineMode.TesseractOnly);

                //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(OCR));

                //var page = ocr.Process((Bitmap)List_images[listView1.SelectedItems[0].Index]);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("注意！您现在是用本软件进行提交，由于作者能力问题，不能够上传主观题，请谨慎发送", "警告！！！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                MessageBox.Show("请求被取消", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("因为这个功能我觉得大部分人也不一定会用，所以就没有做");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            List_images[listView1.SelectedItems[0].Index].Save(AppDomain.CurrentDomain.BaseDirectory + listView1.SelectedItems[0].Index.ToString() + ".jpg");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if(List_images[listView1.SelectedItems[0].Index]!=null)
            {
                textBox1.Text = string.Empty;

                Ocr ocr = new Ocr(ApiKey, SecretToken);

                var _bytes = imageToByte(List_images[listView1.SelectedItems[0].Index]);

                var jObj= ocr.GeneralBasic(_bytes);

                var Answer = jObj.ToObject<Answer_Root>();


                if (!Directory.Exists(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
                {
                    Directory.CreateDirectory(tmpFilePath);
                }
                using (StreamWriter streamWriter =new StreamWriter(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
                {
                    foreach (var item in Answer.words_result)
                    {
                        streamWriter.Write(item.words);
                    }
                }
                foreach (var item in Answer.words_result)
                {
                    textBox1.Text += item.words;
                }
                button5.Enabled = false;
            }
            else
            {
                MessageBox.Show("没有选择照片");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            main.Show();
        }
    }
}

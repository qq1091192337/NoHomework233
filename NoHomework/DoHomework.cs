using Baidu.Aip.Ocr;
using HtmlAgilityPack;
using NoHomework.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using Tesseract;
using static NoHomework.Global;

namespace NoHomework
{
    public partial class DoHomework : Form
    {
        public int taskId { get; set; }

        public string tmpFilePath { get { return Path.Combine(Path_Data, questions.task.taskName); } }



        public DoHomework()
        {
            
            //webBrowser1.Navigate("http://www.baidu.com/s?wd=" + HttpUtility.UrlEncode(input, Encoding.GetEncoding("gb2312")));
            InitializeComponent();
        }

        private List<Image> List_images = new List<Image>();

        private string[] List_Questions = new string[55];

        
        private void DoHomework_Load(object sender, EventArgs e)
        {
            this.Text = questions.task.taskName;
            int cnt = 0;
            foreach (var item in questions.data)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                doc.LoadHtml(item.teaTitle);

                HtmlNodeCollection hrefList = doc.DocumentNode.SelectNodes(".//img[@src]");

                if (hrefList != null&& doc.DocumentNode.SelectNodes("/p/strong/span")==null)
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
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    HtmlNodeCollection _hrefList = doc.DocumentNode.SelectNodes("/p/strong/span");
                    //*[@id="sogou_wrap_id"]
                    if (_hrefList != null)
                    {

                        foreach (HtmlNode href in _hrefList)
                        {

                            stringBuilder.Append(href.InnerText);
                        }
                        stringBuilder.Replace("&nbsp;", "");
                    }

                    Image _image = Resources.NO;/*Image.FromFile(Path.Combine(Path_Data, "NO.png"));*/
 
                    if(_image!=null)
                    {
                        imageList1.Images.Add(_image);

                        List_images.Add(_image);

                        List_Questions[cnt]=stringBuilder.ToString();

                    }
                }
                cnt++;
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
                if (!File.Exists(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString()+".html")))
                {
                    if (!Directory.Exists(tmpFilePath)) Directory.CreateDirectory(tmpFilePath);
                    using (StreamWriter streamWriter = new StreamWriter(File.Create(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString() + ".html")),Encoding.UTF8))
                    {
                        streamWriter.Write(questions.data[listView1.SelectedItems[0].Index].teaTitle);
                    }
                }
                using (StreamReader streamReader = new StreamReader(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString() + ".html")))
                {

                    webBrowser1.Url = new Uri(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString() + ".html"));
                    //button5.Enabled = false;
                }
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
                if(List_Questions.Length!=0)
                {
                    textBox1.Text = List_Questions[listView1.SelectedItems[0].Index];
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
            if (textBox1.Text != string.Empty)
            {
                CallWebBrowser("http://www.baidu.com/s?wd=" + HttpUtility.UrlEncode(textBox1.Text.Trim(), Encoding.GetEncoding("gb2312")));
            }
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
            if (listView1.SelectedItems[0].Index < 0) return;

            if(List_images[listView1.SelectedItems[0].Index]!=null)
            {
                textBox1.Text = string.Empty;

                Ocr ocr = new Ocr(ApiKey, SecretToken);

                var _bytes = imageToByte(List_images[listView1.SelectedItems[0].Index]);

                var jObj= this.Text.IndexOf("数学")<0?ocr.GeneralBasic(_bytes):ocr.AccurateBasic(_bytes);

                var Answer = jObj.ToObject<Answer_Root>();


                if (!Directory.Exists(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
                {
                    Directory.CreateDirectory(tmpFilePath);
                }
                using (StreamWriter streamWriter =new StreamWriter(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
                {
                    foreach (var item in Answer.words_result)
                    {
                        streamWriter.Write(item.words+'\n');
                    }
                }
                foreach (var item in Answer.words_result)
                {
                    textBox1.Text += (item.words+'\n');
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

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //if (File.Exists(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
            //{
            //    using (StreamWriter streamWriter = new StreamWriter(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString())))
            //    {
            //        streamWriter.Write(textBox1.Text);

            //        List_Questions[listView1.SelectedItems[0].Index] = textBox1.Text;

            //        return;
            //    }
            //}
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

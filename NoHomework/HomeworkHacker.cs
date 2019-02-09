using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static NoHomework.Global;
namespace NoHomework
{
    public partial class HomeworkHacker : Form
    {
        public class Data
        {
            /// <summary>
            /// TeaScore
            /// </summary>
            public float teaScore { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string teaResolve { get; set; }
            /// <summary>
            /// TeaId
            /// </summary>
            public int teaId { get; set; }
            /// <summary>
            /// <p><img src="http://zuoye2.xinkaoyun.com/uploadImg/image/20190122/6368375231334284764766669.png" title="blob.png" alt="blob.png"/></p>
            /// </summary>
            public string teaAnswer { get; set; }
            /// <summary>
            /// <p><img src="http://zuoye2.xinkaoyun.com/uploadImg/image/20190122/6368375229800241482383612.png" title="blob.png" alt="blob.png"/></p>
            /// </summary>
            public string teaTitle { get; set; }
            /// <summary>
            /// TaskId
            /// </summary>
            public int taskId { get; set; }
            /// <summary>
            /// 4
            /// </summary>
            public string teaCode { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 获取互评作业详细信息成功
            /// </summary>
            public string msg { get; set; }
            /// <summary>
            /// Data
            /// </summary>
            public List<Data> data { get; set; }
            /// <summary>
            /// ok
            /// </summary>
            public string state { get; set; }
        }

        public HomeworkHacker()
        {
            InitializeComponent();
        }
        private void AddColumn(string Text, int Width, HorizontalAlignment TextAlign = HorizontalAlignment.Center)
        {
            ColumnHeader ch = new ColumnHeader();
            ch.Text = Text;   //设置列标题

            ch.Width = Width;    //设置列宽度

            ch.TextAlign = TextAlign;   //设置列的对齐方式

            //this.listView1.Columns.Add(ch);
        }

        private Dictionary<string, string> PhoneNumberAndOwner = new Dictionary<string, string>();

        private List<string> Phones = new List<string>();

        private string Token = string.Empty;

        private string stuId = string.Empty;

        private string ClassId = string.Empty;

        public string TaskId = string.Empty;

        public Questions_Root Handle_Question;

        private Root Handle_Root = new Root();

        //private string tmpFilePath = Path.Combine(Path_Hack, questions.task.taskName);
        private string Hack_tmpFilePath
        {
            get
            {
                return Path.Combine(Path_Hack, Handle_Question.task.taskName);
            }
        }


        private void HomeworkHacker_Load(object sender, EventArgs e)
        {
            int successCNT = 0;Root_1 root_Now = new Root_1();var Detail_Now = new Root_2();
            this.Text = Handle_Question.task.taskName;
            if (!Directory.Exists(Hack_tmpFilePath)) Directory.CreateDirectory(Hack_tmpFilePath);
            ////LoginInstance loginInstance = new LoginInstance();
            ////AddColumn("ID", 50);
            ////AddColumn("Phone", 200);
            ////AddColumn("Name", 150);
            if (File.Exists(Path.Combine(Path_Hack, "phone_vaild.txt")))
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(Path.Combine(Path_Hack, "phone_vaild.txt")))
                    {
                        PhoneNumberAndOwner = JsonConvert.DeserializeObject<Dictionary<string, string>>(streamReader.ReadToEnd());
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                MessageBox.Show("未获取到PhoneList");

                main.Show();
                this.Dispose();
            }
            ////this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            if (PhoneNumberAndOwner == null) throw new Exception("Phone是空的");
            foreach (var item in PhoneNumberAndOwner)
            {
                Phones.Add(item.Key);
            }
            //for (int i = 0; i < PhoneNumberAndOwner.Count; i++)   //添加10行数据
            //{
            //    ListViewItem lvi = new ListViewItem();

            //    lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

            //    lvi.Text = i.ToString();

            //    //lvi.SubItems.Add("第2列,第" + i + "行");
            //    lvi.SubItems.Add(Phones[i]);

            //    lvi.SubItems.Add(PhoneNumberAndOwner[Phones[i]]);

            //    //this.listView1.Items.Add(lvi);
            //}

            //this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            //if(File.Exists())
            foreach (var item in Phones)
            {
                var Root= Helper.Login(item);

                Token = Root.data.andToken;

                var Detail = Helper.GetDetail(Token);

                stuId = Detail.data.stuId.ToString();

                ClassId = Detail.data.classId.ToString();

                System.Net.WebClient WebClientObj = new System.Net.WebClient();

                System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

                PostVars.Add("token", Token);

                PostVars.Add("stuId",stuId);

                PostVars.Add("classId",ClassId);

                PostVars.Add("taskId", TaskId);

                WebClientObj.Encoding = Encoding.UTF8;

                string sRemoteInfo = string.Empty;

                byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/getMutualTaskInfo?0.17584490646948914", "POST", PostVars);

                sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

                jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                var Tmp_Root= JsonConvert.DeserializeObject<Root>(sRemoteInfo);

                if (Tmp_Root.msg != "获取互评作业详细信息成功") continue;

                else if (Tmp_Root.msg == "获取互评作业详细信息成功")
                {
                    successCNT++;

                    root_Now=Root;

                    Detail_Now = Detail;

                    Handle_Root = Tmp_Root;

                    break;
                }

                //if (!File.Exists(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString() + ".html")))
                //{
                //    if (!Directory.Exists(tmpFilePath)) Directory.CreateDirectory(tmpFilePath);
                //    using (StreamWriter streamWriter = new StreamWriter(File.Create(Path.Combine(tmpFilePath, listView1.SelectedItems[0].Index.ToString() + ".html")), Encoding.UTF8))
                //    {
                //        streamWriter.Write(questions.data[listView1.SelectedItems[0].Index].teaTitle);
                //    }
                //}
                //webBrowser1.Url = Tmp_Root.data[listBox1.SelectedIndex].teaTitle;
                //msg = 获取互评作业详细信息成功
            }
            
            if (successCNT>0)
            {
                MessageBox.Show($"题目预加载成功！现在用的账号是：{root_Now.data.realName}-----{Detail_Now.data.userName}");
                for (int i = 0; i < Handle_Root.data.Count; i++)
                {
                    listBox1.Items.Add("题目：" + (i + 1).ToString());
                }
            }
            else
            {
                MessageBox.Show("可能您的小伙伴作业还没有写完，暂时不能提供答案", "啊欧~Sorry！");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sendor = (sender as ListBox);
            if(sendor.SelectedIndex>=0)
            {
                if (!File.Exists(Path.Combine(Hack_tmpFilePath, listBox1.SelectedIndex.ToString() + ".html")))
                {
                    if (!Directory.Exists(Hack_tmpFilePath)) Directory.CreateDirectory(Hack_tmpFilePath);

                    using (StreamWriter streamWriter = new StreamWriter(File.Create(Path.Combine(Hack_tmpFilePath, listBox1.SelectedIndex.ToString() + ".html")), Encoding.UTF8))
                    {
                        streamWriter.Write(Handle_Root.data[listBox1.SelectedIndex].teaAnswer);
                    }
                }
                if (!File.Exists(Path.Combine(Hack_tmpFilePath, listBox1.SelectedIndex.ToString() + "_Resolve.html")))
                {
                    if (!Directory.Exists(Hack_tmpFilePath)) Directory.CreateDirectory(Hack_tmpFilePath);

                    using (StreamWriter streamWriter = new StreamWriter(File.Create(Path.Combine(Hack_tmpFilePath, listBox1.SelectedIndex.ToString() + "_Resolve.html")), Encoding.UTF8))
                    {
                        streamWriter.Write(Handle_Root.data[listBox1.SelectedIndex].teaResolve);
                    }
                }

                using (StreamReader streamReader = new StreamReader(Path.Combine(Hack_tmpFilePath,listBox1.SelectedIndex.ToString() + ".html")))
                {
                    webBrowser1.Url = new Uri(Path.Combine(Hack_tmpFilePath, listBox1.SelectedIndex.ToString() + ".html"));
                    //button5.Enabled = false;
                }
                using (StreamReader streamReader = new StreamReader(Path.Combine(Hack_tmpFilePath, listBox1.SelectedIndex.ToString() + "_Resolve.html")))
                {
                    webBrowser2.Url = new Uri(Path.Combine(Hack_tmpFilePath, listBox1.SelectedIndex.ToString() + "_Resolve.html"));
                    //button5.Enabled = false;
                }
                //webBrowser1.Url = Handle_Root.data[listBox1.SelectedIndex].teaTitle;
            }
        }

        private void HomeworkHacker_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.Show();

            this.Dispose();
        }
    }
    public static class Helper
    {
        public static NoHomework.Root_1 Login(string Users)
        {
            try
            {
                var tmp_Root = new NoHomework.Root_1();

                System.Net.WebClient WebClientObj = new System.Net.WebClient();

                System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

                PostVars.Add("userPass", "a123456");

                PostVars.Add("userName", Users);

                PostVars.Add("platform", "android");


                WebClientObj.Encoding = Encoding.UTF8;

                string sRemoteInfo = string.Empty;

                WebClientObj.DownloadStringCompleted += (sender, e) =>
                {
                    //Console.WriteLine(sender.ToString());   //输出 System.Net.WebClient   触发事件的对象
                    //Console.WriteLine(e.Result);    //输出页面源代码
                    sRemoteInfo = e.Result;
                };
                //WebClientObj.UploadValuesAsync(new Uri("http://xxzy.xinkaoyun.com:8081/holidaywork/login"),PostVars);
                byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/login", "POST", PostVars);


                //下面都没用啦，就上面一句话就可以了 

                sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

                jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                tmp_Root = JsonConvert.DeserializeObject<NoHomework.Root_1>(sRemoteInfo, jsonSerializerSettings);

                return tmp_Root;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static NoHomework.Root_2 GetDetail(string Token)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            PostVars.Add("token", Token);

            var byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/getStudent", "POST", PostVars);

            var sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

            //MessageBox.Show(LoginObj.msg);

            var tmpLogin = JsonConvert.DeserializeObject<Root_2>(sRemoteInfo);

            return tmpLogin;
        }
    }
    internal class LoginInstance
    {
        private string user;
        
        public LoginInstance(string User)
        {
            user = User;
        }
        public Root_1 Login()
        {
            return Helper.Login(user);
        }
    }
}

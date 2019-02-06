using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static NoHomework.Global;

namespace NoHomework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            PostVars.Add("userPass", textBox2.Text.Trim());

            PostVars.Add("userName", textBox1.Text.Trim());

            PostVars.Add("platform", "android");

            try

            {

                WebClientObj.Encoding = Encoding.UTF8;

                byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/login", "POST", PostVars);

                //下面都没用啦，就上面一句话就可以了 

                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

                LoginObj = JsonConvert.DeserializeObject<Root_1>(sRemoteInfo);

                PostVars.Clear();

                PostVars.Add("token", LoginObj.data.andToken);

                byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/getStudent", "POST", PostVars);

                sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

                MessageBox.Show(LoginObj.msg);

                LoginObj_2 = JsonConvert.DeserializeObject<Root_2>(sRemoteInfo);

                if (LoginObj.msg == "登录成功")
                {
                    this.Hide();
                    main.Show();
                }
                //这是获取返回信息 
            }

            catch
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}

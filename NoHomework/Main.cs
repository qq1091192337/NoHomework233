using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NoHomework.Global;
namespace NoHomework
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public static List<string> HomeworksPointer=new List<string>();
        public static Dictionary<string, HomeWork_Data> Homeworks=new Dictionary<string, HomeWork_Data>();
        
        private void Main_Load(object sender, EventArgs e)
        {
            lastLabel = label1;
            lastButton = button1;
            B_cnt = 0;

            this.Text = $"欢迎你：“{LoginObj.data.realName}”";

            label1.Text = LoginObj_2.data.classId==39?$"{LoginObj_2.data.classId}（老黑的班）":$"{LoginObj_2.data.classId}";

            //CreateLabel($"{LoginObj_2.data.}")
        }
        int cnt = 2, deltaY = 25 - 9;
        Label lastLabel;
        private void CreateLabel(string Text)
        {

            Label label = new Label();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(12, lastLabel.Location.Y+deltaY);
            label.Name = "label"+cnt.ToString();
            label.Size = new System.Drawing.Size(41, 12);
            label.TabIndex = cnt-1;
            label.Text = Text;
            cnt++;lastLabel = label;
        }
        Button lastButton;
        int B_cnt;

        //private Button CreateButton(string Text)
        //{
        //    Button button = new Button();

        //    button.Location = new System.Drawing.Point(6, lastButton.Location.Y+(50-17));

        //    button.Name = "button"+cnt.ToString();

        //    button.Size = new System.Drawing.Size(378, 27);

        //    button.TabIndex = cnt;

        //    button.Text = Text;

        //    button.UseVisualStyleBackColor = true;

        //    button.Click += new System.EventHandler(this.NormalButtonClick);

        //    B_cnt++;lastButton = button;

        //    groupBox1.Controls.Add(button);

        //    if(button.Location.Y>438)
        //    {
        //        groupBox1.AutoScrollOffset = new Point(button.Location.Y + (50 - 17));
        //    }

        //    return button;
            
        //}
        private void RefreshList()
        {
            foreach (var item in HomeworksPointer)
            {
                listBox1.Items.Add(item);
            }
        }
        private void Subject_Changed(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string tag = (sender as RadioButton).Name;
            int sid = 0;
            if (!(sender as RadioButton).Checked) return;
            switch (tag)
            {
                case "Chinese":
                    sid = 1;
                    break;
                case "Math":
                    sid = 2;
                    break;
                case "English":
                    sid = 3;
                    break;
                case "Politics":
                    sid = 7;
                    break;
                case "History":
                    sid = 8;
                    break;
                case "Physics":
                    sid = 4;
                    break;
                case "Chemical":
                    sid = 5;
                    break;
                case "Biology":
                    sid = 6;
                    break;
                case "Geography":
                    sid = 9;
                    break;
                default:
                    break;
            }
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            PostVars.Add("classId", LoginObj_2.data.classId.ToString());

            PostVars.Add("stuId", LoginObj_2.data.stuId.ToString());

            PostVars.Add("limit", "30");

            PostVars.Add("page", "1");

            PostVars.Add("token", LoginObj.data.andToken);

            PostVars.Add("sid", sid.ToString());


            try

            {

                WebClientObj.Encoding = Encoding.UTF8;

                byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/getTasks", "POST", PostVars);

                //下面都没用啦，就上面一句话就可以了 

                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

                homeWork = JsonConvert.DeserializeObject<HomeWork_Root>(sRemoteInfo);

                if(homeWork==null)
                {
                    MessageBox.Show(homeWork.msg);
                }
                //StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in homeWork.data)
                {
                    HomeworksPointer.Add(item.taskName);
                    Homeworks.Add(item.taskName, item);
                    //stringBuilder.Append(item.taskName+'\n');
                    //buttons.Add(CreateButton($"{item.taskName}\n{item.finishTime}之前完成")); 
                }
                RefreshList();
                //MessageBox.Show(stringBuilder.ToString());
            } 

            
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "引发异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HomeWork_Data data = Homeworks[HomeworksPointer[listBox1.SelectedIndex]];
            StringBuilder sb = new StringBuilder();
            sb.Append("提交状态"+data.submitState);
            sb.Append('\n');
            sb.Append("完成时间：" + data.finishTime);
            MessageBox.Show(sb.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            PostVars.Add("token",LoginObj.data.andToken);

            PostVars.Add("taskId", Homeworks[HomeworksPointer[listBox1.SelectedIndex]].taskId.ToString());

            PostVars.Add("stuId", LoginObj_2.data.stuId.ToString());

            PostVars.Add("classId", LoginObj_2.data.classId.ToString());

            WebClientObj.Encoding = Encoding.UTF8;

            byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/getTaskInfo?0.6596327046934594", "POST", PostVars);

            string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

            questions= JsonConvert.DeserializeObject<Questions_Root>(sRemoteInfo);

            if(questions!=null)
            {
                DoHomework doHomework = new DoHomework();

                this.Hide();

                doHomework.Show();
            }

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDown();
        }

    }
}

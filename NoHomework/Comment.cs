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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using static NoHomework.Helper;
namespace NoHomework
{
    public partial class Comment : Form
    {
        public Questions_Root Handle_Question;

        private Root Handle_Root = new Root();

        private Dictionary<string, string> PhoneNumberAndOwner = new Dictionary<string, string>();

        private List<string> Phones = new List<string>();

        private List<Root_1> puppets_1 = new List<Root_1>();

        private List<Root_2> puppets_2 = new List<Root_2>();

        private List<Root> NoCorrects = new List<Root>();

        private Dictionary<Root, int> CorrectsPointToTaskId = new Dictionary<Root, int>();

        private string Hack_tmpFilePath
        {
            get
            {
                return Path.Combine(Path_Hack, Handle_Question.task.taskName);
            }
        }
        #region Comment
        public class DataItem
        {
            /// <summary>
            /// 蔡雨桐
            /// </summary>
            public string realName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int stuId { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 获取待批作业学生信息成功
            /// </summary>
            public string msg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<DataItem> data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string state { get; set; }
        }
        #endregion
        #region Answers
        public class _DataItem
        {
            /// <summary>
            /// 
            /// </summary>
            public float teaScore { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string teaResolve { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int teaId { get; set; }
            /// <summary>




            /// </summary>
            public string teaAnswer { get; set; }
            /// <summary>





            /// </summary>
            public string teaTitle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int taskId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string teaCode { get; set; }
        }

        public class AnsRoot
        {
            /// <summary>
            /// 获取互评作业详细信息成功
            /// </summary>
            public string msg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<_DataItem> data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string state { get; set; }
        }
        #endregion
        private int sid = -1;//科目
        public Comment(int SID)
        {
            sid = SID;
            InitializeComponent();
        }

        private void Comment_Load(object sender, EventArgs e)
        {
            //int successCNT = 0;

            Root_1 root_Now = new Root_1();

            var Detail_Now = new Root_2();

            //this.Text = Handle_Question.task.taskName;

            //if (!Directory.Exists(Hack_tmpFilePath)) Directory.CreateDirectory(Hack_tmpFilePath);


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
            //foreach (var item in Phones)
            //{
            //    puppets_1.Add(Login(item));

            //    puppets_2.Add(GetDetail());
            //}
            for (int i = 0; i < Phones.Count; i++)
            {
                puppets_1.Add(Login(Phones[i]));
                puppets_2.Add(GetDetail(puppets_1[i].data.andToken));
            }
            if (puppets_1.Count == Phones.Count && puppets_2.Count == Phones.Count)
            {
                MessageBox.Show("傀儡安装完毕！个数：" + Phones.Count.ToString());
            }
            //MessageBox.Show(Phones.Count.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int suc = 0;
            if (MessageBox.Show("你当前要批改的作业是：" + sid2Subject(sid), "确定吗？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) MessageBox.Show("您取消了这个操作！");
            else
            {
                if (NoCorrects.Count == 0)
                {
                    MessageBox.Show("还没有获取到待批改名单！");
                    return;
                }
                //foreach (var item in NoCorrects)
                //{
                //    var r = random.Next(0, Math.Min(puppets_1.Count, puppets_2.Count) - 1);
                //    foreach (var item_ in item.data)
                //    {
                //        if (puppets_1[r] != null)
                //        {
                //            if(Submit(puppets_1[r].data.andToken, item_.stuId, puppets_2[r].data.classId, item_, item_.realName))
                //            {
                //                suc++;
                //            }
                //        }
                //    }
                //}
                for (int i = 0; i < homeWork.data.Count; i++)
                {
                    var TId = homeWork.data[i].taskId;

                    for (int j = 0; j < NoCorrects[i].data.Count; j++)
                    {
                        var r = random.Next(0, Math.Min(puppets_1.Count, puppets_2.Count) - 1);
                        if (puppets_1[r].data.realName == NoCorrects[i].data[j].realName)
                        {
                            Submit(puppets_1[(r + 1) >= puppets_1.Count ? (r - 1) : (r + 1)].data.andToken, NoCorrects[i].data[j].stuId, puppets_2[(r + 1) >= puppets_1.Count ? (r - 1) : (r + 1)].data.classId, TId, NoCorrects[i].data[j].realName);
                        }
                        if (Submit(puppets_1[r].data.andToken, NoCorrects[i].data[j].stuId, puppets_2[r].data.classId, TId, NoCorrects[i].data[j].realName))
                        {
                            suc++;
                        }
                        listBox1.BeginUpdate();

                        listBox1.Items.Add($"{homeWork.data[i].taskName}-----{NoCorrects[i].data[j].realName}已批改完毕-----批改人：{puppets_2[r].data.realName}");

                        listBox1.EndUpdate();
                    }
                    listBox1.BeginUpdate();

                    listBox1.Items.Add($"{homeWork.data[i].taskName}-----已批改完毕！");

                    listBox1.EndUpdate();

                }

                
        
            }

        }
        private string sid2Subject(int sid)
        {
            switch (sid)
            {
                case 1: return "语文";
                case 2: return "数学";
                case 3: return "英语";
                case 4: return "物理";
                case 5: return "化学";
                case 6: return "生物";
                case 7: return "政治";
                case 8: return "历史";
                case 9: return "地理";
                default:
                    return "失败";
                    break;
            }
        }
        private Root GetStuNoCorrect(string token, int stuId, int classId, int taskId)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            PostVars.Add("token", token);

            //PostVars.Add("allTeaType", _root.task.allTeaType.ToString());

            PostVars.Add("stuId", stuId.ToString());

            PostVars.Add("classId", classId.ToString());

            PostVars.Add("taskId", taskId.ToString());

            WebClientObj.Encoding = Encoding.UTF8;

            byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/getStuNotCorrect", "POST", PostVars);

            string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

            var Tmp = JsonConvert.DeserializeObject<Root>(sRemoteInfo);

            return Tmp;
        }

        private bool Submit(string token, int stuId, int classId, int taskId, string stuName)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            PostVars.Add("token", token);

            //PostVars.Add("allTeaType", _root.task.allTeaType.ToString());

            PostVars.Add("stuId", stuId.ToString());

            PostVars.Add("classId", classId.ToString());

            PostVars.Add("taskId", taskId.ToString());

            WebClientObj.Encoding = Encoding.UTF8;

            byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/getMutualTaskInfo?0.8891892034316116", "POST", PostVars);

            string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

            var Ans = JsonConvert.DeserializeObject<AnsRoot>(sRemoteInfo);


            StringBuilder Tscore = new StringBuilder();

            foreach (var item in Ans.data)
            {
                Tscore.Append($"{item.teaId}-{item.teaScore}-{item.teaScore},");
            }
            if (Tscore[Tscore.Length - 1] == ',') Tscore.Remove(Tscore.Length - 1, 1);

            PostVars.Add("taskScore", Tscore.ToString());

            byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/submitMutualTask?0.3035299531512341", "POST", PostVars);

            sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

            if (sRemoteInfo.IndexOf("提交成功") > 0)
            {
                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int successcnt = 0;
            foreach (var item in homeWork.data)
            {

                var tmp = GetStuNoCorrect(puppets_1[2].data.andToken, puppets_2[2].data.stuId, puppets_2[2].data.classId, item.taskId);
                NoCorrects.Add(tmp);
                CorrectsPointToTaskId.Add(tmp, item.taskId);
                if (tmp.msg == "获取待批作业学生信息成功")
                {
                    successcnt++;
                }
            }
            if (successcnt == homeWork.data.Count)
            {
                MessageBox.Show("获取成功！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(Path_Data, "日志.txt")))
            {
                foreach (var item in listBox1.Items)
                {
                    sw.WriteLine(item);
                }
            }

        }
        private int GetLeftCorrect(Root root)
        {
           return root.data.Count;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int[] characters = { 5, 2, 0, 1, 3, 1, 4 };

            Random random = new Random();
            int Selector = 0;
            for (int i = 0; i < homeWork.data.Count; i++)
            {
                var TId = homeWork.data[i].taskId;
                int left = -1;
                for (int j = 0; j < NoCorrects[i].data.Count; j++)
                {
                    var r = random.Next(0, Math.Min(puppets_1.Count, puppets_2.Count) - 1);

                    left = GetLeftCorrect(NoCorrects[i]);

                    if (left == characters[Selector]) break;

                    if(puppets_1[r].data.realName== NoCorrects[i].data[j].realName)
                    {
                        Submit(puppets_1[(r + 1) >= puppets_1.Count ? r - 1 : r + 1].data.andToken, NoCorrects[i].data[j].stuId, puppets_2[(r + 1) >= puppets_1.Count ? r - 1 : r + 1].data.classId, TId, NoCorrects[i].data[j].realName);
                    }
                    if (Submit(puppets_1[r].data.andToken, NoCorrects[i].data[j].stuId, puppets_2[r].data.classId, TId, NoCorrects[i].data[j].realName))
                    {
                        
                    }
                    listBox1.BeginUpdate();

                    listBox1.Items.Add($"{homeWork.data[i].taskName}-----{NoCorrects[i].data[j].realName}已批改完毕-----批改人：{puppets_2[r].data.realName}");

                    listBox1.EndUpdate();
                }
                if(Selector!=characters.Length-1)
                {
                    Selector++;
                }
                listBox1.BeginUpdate();

                listBox1.Items.Add($"{homeWork.data[i].taskName}-----已批改完毕！");

                listBox1.EndUpdate();
            }
        }
    }
}

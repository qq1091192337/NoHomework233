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
    public partial class Submit : Form
    {
        Questions_Root _root = new Questions_Root();
        int T_ID;
        public Submit(Questions_Root root, int TaskId)
        {
            _root = root;
            T_ID = TaskId;
            InitializeComponent();
        }

        private void Submit_Load(object sender, EventArgs e)
        {
            Inputs.Add(textBox_Template);
            Inputs.Add(textBox1);
            Inputs.Add(textBox2);
            Inputs.Add(textBox3);
            Inputs.Add(textBox4);
            Inputs.Add(textBox5);
            foreach (var item in Inputs)
            {
                item.TextChanged += TextChanged;
            }
        }
        GroupBox LastGroupBox = null;
        TextBox LastTextBox = null;
        List<TextBox> Inputs = new List<TextBox>();
        private void AddInput()
        {
            if (LastGroupBox == null) LastGroupBox = groupBox_Template;
            if (LastTextBox == null) LastTextBox = textBox_Template;
            TextBox textBox = new TextBox() { Size = LastTextBox.Size, TextAlign = LastTextBox.TextAlign, Dock = LastTextBox.Dock, CharacterCasing = LastTextBox.CharacterCasing, Font = LastTextBox.Font, MaxLength = LastTextBox.MaxLength, Name = ("textBox" + (Inputs.Count).ToString()), Location = (LastTextBox.Location.X + 162) <= Size.Width ? new Point(LastTextBox.Location.X + 162, LastTextBox.Location.Y) : new Point() };
        }
        private void TextChanged(object sender, EventArgs e)
        {
            var obj = sender as TextBox;
            if (obj.Text.Length == 5)
            {
                SendKeys.Send("{TAB}");
            }
            if (obj.Text != "")
            {
                var CurInput = obj.Text[obj.Text.Length - 1];
                if (CurInput != 'A' && CurInput != 'B' && CurInput != 'C' && CurInput != 'D')
                {
                    SendKeys.Send("{BACKSPACE}");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            PostVars.Add("token", LoginObj.data.andToken);

            PostVars.Add("allTeaType", _root.task.allTeaType.ToString());

            PostVars.Add("stuId", LoginObj_2.data.stuId.ToString());

            PostVars.Add("classId", LoginObj_2.data.classId.ToString());

            PostVars.Add("taskId", T_ID.ToString());

            StringBuilder Answer = new StringBuilder();

            List<char> Ans = new List<char>();

            foreach (var item in Inputs)
            {
                var Maxlen = item.Text.Length;
                for (int i = 0; i < Maxlen; i++)
                {
                    if (item.Text != "")
                        if (item.Text[i] != '\0')
                            Ans.Add(item.Text[i]);
                }
            }

            int b = 0;
            foreach (var item in Ans)
            {
                Answer.Append($"{_root.data[b].teaId},{item}|");
                b++;
            }
            if (Answer[Answer.Length - 1] == '|')
            {
                Answer.Remove(Answer.Length - 1, 1);
            }
            PostVars.Add("taskAnswer", Answer.ToString());



            WebClientObj.Encoding = Encoding.UTF8;

            byte[] byRemoteInfo = WebClientObj.UploadValues("http://xxzy.xinkaoyun.com:8081/holidaywork/student/submitTask?0.28205744858631876", "POST", PostVars);

            string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

            if (sRemoteInfo.IndexOf("提交作业成功") > 0)
            {
                MessageBox.Show("提交作业成功！");
            }
            else
            {
                MessageBox.Show("提交失败！");
            }
        }
    }
}

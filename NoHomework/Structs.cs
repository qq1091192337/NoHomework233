using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoHomework
{
    public class Login_1
    {
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sqlName { get; set; }
        /// <summary>
        /// 李嘉俊
        /// </summary>
        public string realName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string andToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int schoolId { get; set; }
        /// <summary>
        /// 武邑中学
        /// </summary>
        public string schName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userRole { get; set; }
        /// <summary>
        /// 武邑中学-张晓燕-11000-分流
        /// </summary>
        public string schMark { get; set; }
    }
    public class Root_1
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Login_1 data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
    }

    public class Login_2
    {
        /// <summary>
        /// 
        /// </summary>
        public string jxappOnlycode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gradeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int gradeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int stuId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jgid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string className { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// 李嘉俊
        /// </summary>
        public string realName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int classId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int schoolId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string studyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userRole { get; set; }
        /// <summary>
        /// 未分
        /// </summary>
        public string classType { get; set; }
    }

    public class Root_2
    {
        /// <summary>
        /// 获取学生信息成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Login_2 data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
    }


    #region HomeWork    

    public class HomeWork_Data
    {
        /// <summary>
        /// 2019-02-02 21:00:00
        /// </summary>
        public DateTime finishTime { get; set; }
        /// <summary>
        /// 2月2日高一语文寒假作业2
        /// </summary>
        public string taskName { get; set; }
        /// <summary>
        /// SubmitCode
        /// </summary>
        public int submitCode { get; set; }
        /// <summary>
        /// TaskId
        /// </summary>
        public int taskId { get; set; }
        /// <summary>
        /// 未提交
        /// </summary>
        public string submitState { get; set; }
    }

    public class HomeWork_Root
    {
        /// <summary>
        /// 获取作业成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public List<HomeWork_Data> data { get; set; }
        /// <summary>
        /// ok
        /// </summary>
        public string state { get; set; }
    }
    #endregion

    public class Subject_DataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int stype { get; set; }
        /// <summary>
        /// 语文
        /// </summary>
        public string subname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sid { get; set; }
    }

    public class Subject_Root
    {
        /// <summary>
        /// 获取科目成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Subject_DataItem> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
    }
    #region Questions
    public class Question_Task
    {
        /// <summary>
        /// 2月1日高一化学寒假作业1
        /// </summary>
        public string taskName { get; set; }
        /// <summary>
        /// AllTeaType
        /// </summary>
        public int allTeaType { get; set; }
    }
    public class Question_Children
    {
        /// <summary>
        /// 单选题
        /// </summary>
        public string teaType { get; set; }
        /// <summary>
        /// TeaOption
        /// </summary>
        public List<string> teaOption { get; set; }
        /// <summary>
        /// TeaScore
        /// </summary>
        public float teaScore { get; set; }
        /// <summary>
        /// TeaId
        /// </summary>
        public int teaId { get; set; }
        /// <summary>
        /// TeaChildId
        /// </summary>
        public int teaChildId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string teaTitle { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string teaCode { get; set; }
    }
    public class Question_Data
    {
        /// <summary>
        /// 客观题
        /// </summary>
        public string teaType { get; set; }
        /// <summary>
        /// TeaOption
        /// </summary>
        public List<string> teaOption { get; set; }
        /// <summary>
        /// 一般
        /// </summary>
        public string teaDifficulty { get; set; }
        /// <summary>
        /// Children
        /// </summary>
        public List<Question_Children> children { get; set; }
        /// <summary>
        /// TeaScore
        /// </summary>
        public float teaScore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string voicePath { get; set; }
        /// <summary>
        /// TeaId
        /// </summary>
        public int teaId { get; set; }
        /// <summary>
        /// 单选题
        /// </summary>
        public string teaQueType { get; set; }
        /// <summary>
        /// <p><img title="image.png" alt="image.png" src="http://zuoye2.xinkaoyun.com/uploadImg/image/20190122/6368377149812397507548687.png"/></p>
        /// </summary>
        public string teaTitle { get; set; }
        /// <summary>
        /// TaskId
        /// </summary>
        public int taskId { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string teaCode { get; set; }
    }

    public class Questions_Root
    {
        /// <summary>
        /// 获取作业信息成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// Task
        /// </summary>
        public Question_Task task { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public List<Question_Data> data { get; set; }
        /// <summary>
        /// ok
        /// </summary>
        public string state { get; set; }
    }

    #endregion
    #region Answer
    public class Words_resultItem
    {
        /// <summary>
        /// 1.下列条约中,均有割地、赔款、开埠通商条款的有
        /// </summary>
        public string words { get; set; }
    }

    public class Answer_Root
    {
        /// <summary>
        /// 
        /// </summary>
        public string log_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int words_result_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Words_resultItem> words_result { get; set; }
    }
    #endregion
}

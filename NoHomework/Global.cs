using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Windows.Forms;
namespace NoHomework
{
    class Global
    {
        public static Root_1 LoginObj=new  Root_1();

        public static Root_2 LoginObj_2 = new Root_2();

        public static HomeWork_Root homeWork = new HomeWork_Root();

        public static Questions_Root questions = new Questions_Root();

        public static DoHomework doHomework = new DoHomework(new DoHomework.StructSubmit(),new Questions_Root());

        public static Main main = new Main();

        //public static List<string> PhoneNumbers
        //{
        //    get
        //    {
        //        if (File.Exists(Path.Combine(Path_Hack, "PhoneNumbers.hck")))
        //        {
        //            using (StreamReader streamReader =new StreamReader(Path.Combine(Path_Hack, "PhoneNumbers.hck")))
        //            {
        //                string str= streamReader.ReadToEnd();
        //                string[] tmp = str.Split('\n');
        //                foreach (var item in tmp)
        //                {
        //                    if (item != "") PhoneNumbers.Add(item.Remove(item.Length - 1));
        //                }

        //            }
        //        }
        //    }
        //    set
        //    {

        //    }
        //}

        public static string Path_Data = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        public static string Path_Hack = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Hack");

        public static string Path_Image = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\Image.hmk");

        public static string Path_Question = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\Question");

        public static string AccessToken = "24.3eec1f1615da335faa149b8c9e34546d.2592000.1551771574.282335-15529672";

        public static string SecretToken = "IiXSB9Emca84lqlGU3kFSlQW9VAeMDcf";

        public static string ApiKey = "oojd7mh98lTehquAk9cr0VzC";

        public static string GetOutLineIPAddress()
        {
            string url = "https://www.ipip.net/ip/223.91.11.73.html";
            
            HtmlWeb htmlWeb = new HtmlWeb();

            string str= GetWebContent(url);

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(GetWebContent(url));
            
            string XPath = "/html/body/div[3]/div/table[1]/tbody/tr[2]/td[2]/span[1]/a";

            HtmlNodeCollection hrefList = htmlDocument.DocumentNode.SelectNodes(XPath);
            if (hrefList != null)
            {
                foreach (HtmlNode href in hrefList)
                {
                    return href.InnerText;
                }

            }
            return string.Empty;
        }

        public static void CallWebBrowser(string URL)
        {
            System.Diagnostics.Process.Start(URL);
        }
        
        public static byte[] imageToByte(System.Drawing.Image _image)
        {
            MemoryStream ms = new MemoryStream();

            _image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }

        public static void ShutDown()
        {
            Environment.Exit(0);
        }
        public static string GetWebContent(string Url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                //声明一个HttpWebRequest请求 

                request.Timeout = 30000;

                //设置连接超时时间 

                request.Headers.Set("Pragma", "no-cache");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream streamReceive = response.GetResponseStream();

                Encoding encoding = Encoding.GetEncoding("GB2312");

                StreamReader streamReader = new StreamReader(streamReceive, encoding);

                strResult = streamReader.ReadToEnd();

            }
            catch
            {
                throw;
            }
            return strResult;
        }
        //public static DateTime GetNowTime()
        //{

        //    string url = "http://time.tianqi.com/beijing/";
        //    HtmlWeb htmlWeb = new HtmlWeb();
        //    HtmlAgilityPack.HtmlDocument htmlDocument = htmlWeb.Load(url);
        //    string XPath = "//*[@id=\"clock\"]";

        //    HtmlNodeCollection hrefList = htmlDocument.DocumentNode.SelectNodes(XPath);
        //    if (hrefList != null)
        //    {
        //        foreach (HtmlNode href in hrefList)
        //        {
        //            //DateTime dateTime=DateTime.ParseExact(href.InnerText, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        //            return dateTime;
        //        }

        //    }
        //    return DateTime.MaxValue;
        //}
        public static string Post( string URL,Dictionary<string,string> _PostVars)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();

            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            foreach (var item in _PostVars)
            {
                PostVars.Add(item.Key, item.Value);
            }

            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues(URL, "POST", PostVars);

                //下面都没用啦，就上面一句话就可以了 

                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);

                return sRemoteInfo;
                //这是获取返回信息 

            }

            catch

            {
                throw;
            }
        }
    }
}

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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkPicker
{
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
    }
}

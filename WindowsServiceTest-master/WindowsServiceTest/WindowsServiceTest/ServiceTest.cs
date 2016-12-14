using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace WindowsServiceTest
{
    public partial class ServiceTest : ServiceBase
    {
        System.Threading.Timer time;
        public ServiceTest()
        {
            InitializeComponent();

        }


        protected override void OnStart(string[] args)
        {
            using (StreamWriter sw = new StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "ServiceTest服务Start.");

                TimerCallback getOnDutyCallback = new TimerCallback(UploadEvent);
                time = new System.Threading.Timer(getOnDutyCallback, new AutoResetEvent(false), 0, 1000 * 60);

            }
        }

        protected override void OnStop()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "ServiceTest服务Stop.");
            }
        }

        private void UploadEvent(object setinfo)
        {
            using (StreamWriter sw = new StreamWriter("D:\\log.txt", true))
            {

                if (DateTime.Now.Minute >= 30 && DateTime.Now.Minute < 40)
                {
                    string strurl = "http://api.xiaoxxx.com/tjsy/hello";
                    string filePath = @"D:\杭州海关.xls";
                    try
                    {
                        WebClient webclient = new WebClient();

                        webclient.UploadFile(strurl, "POST", filePath);

                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "成功");
                    }
                    catch (Exception ex)
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + ex.Message);
                    }
                }
                else
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "还没到时");
                }
            }

        }
    }
}

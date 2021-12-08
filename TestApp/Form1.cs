using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Authing.ApiClient.Domain.Model.Management.WhiteList;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public ManagementClient ManagementClient { get; set; }

        protected static string UserPoolId { get; set; } = "617280674680a6ca2b1f6317";
        protected static string Secret { get; set; } = "6671136fa932eb692735a6f82af3b67b";
        protected static string AppId { get; set; } = "6172807001258f603126a78a";
        protected static string TestUserId = "61a82941979c96c04ed9e920";
        protected static IEnumerable<string> phones { get; set; } = new List<string>() { "18000000000", "19000000000" };
        public static string Host { get; set; } = "https://core.authing.cn";


        public Form1()
        {
            InitializeComponent();
            ManagementClient = new ManagementClient((c) =>
            {
                c.AppId = AppId;
                c.Host = Host;
                c.Secret = Secret;
                c.UserPoolId = UserPoolId;
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var taskWork = Task.Factory.StartNew(DoWork);
            //Do other work
            //Then wait for thread finish
            //taskWork.Wait();
            ManagementClient.Whitelist.Enable(WhitelistType.EMAIL | WhitelistType.PHONE | WhitelistType.USERNAME).ContinueWith(
                (r) =>
                {
                    listBox1.Invoke(new Action(() =>
                    {
                        listBox1.Items.Add(r.Result.Result.Whitelist.EmailEnabled.ToString());
                        listBox1.Items.Add(r.Result.Result.Whitelist.PhoneEnabled.ToString());
                        listBox1.Items.Add(r.Result.Result.Whitelist.UsernameEnabled.ToString());
                    }));
                }
                );
            //Console.WriteLine(result.ToString());
            //listBox1.Items.Add(taskWork.Result.Result.Whitelist.EmailEnabled.ToString());
            //listBox1.Items.Add(taskWork.Result.Result.Whitelist.PhoneEnabled.ToString());
            //listBox1.Items.Add(taskWork.Result.Result.Whitelist.UsernameEnabled.ToString());
        }

        private UpdateUserpoolResponse DoWork()
        {
            return ManagementClient.Whitelist.Enable(WhitelistType.EMAIL | WhitelistType.PHONE |
                                                     WhitelistType.USERNAME).Result;
        }
    }
}

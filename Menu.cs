using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace DelfiApp
{
    public partial class Menu : KryptonForm
    {
        string ver = "0.2915";
        WebClient client = new WebClient();
        string fullPath = Application.StartupPath.ToString();

        void setup_update(bool in_st)
        {
            if (client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/version.txt").Contains(ver))
            {
                if (!in_st)
                {
                    MessageBox.Show("Версия приложения:" + ver + "\n" + "Версия приложения на сервере: " + client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/version.txt"), "Уведомление", MessageBoxButtons.OK);
                }
            }
            else
            {
                if (!in_st) { MessageBox.Show("Версия приложения:" + ver + "\n" + "Версия приложения на сервере: " + client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/version.txt"), "Уведомление", MessageBoxButtons.OK); }
                MessageBox.Show("Обнаружена новая версия! Идет установка файлов...", "Update", MessageBoxButtons.OK);
                File.Move(fullPath + "\\DeWorld.exe", fullPath + "\\DeWorld_old.exe");
                string requestString = @"https://github.com/Delfi1/DeWorld/blob/master/bin/Release/DelfiApp.exe?raw=true";
                HttpClient httpClient = new HttpClient();
                var GetTask = httpClient.GetAsync(requestString);
                GetTask.Wait(1000); // WebCommsTimeout is in milliseconds

                if (!GetTask.Result.IsSuccessStatusCode)
                {
                    // write an error
                    return;
                }

                using (var fs = new FileStream(fullPath + "\\DeWorld.exe", FileMode.CreateNew))
                {
                    var ResponseTask = GetTask.Result.Content.CopyToAsync(fs);
                    ResponseTask.Wait(1000);
                }
                System.Diagnostics.Process.Start(fullPath + "\\DeWorld.exe");
                Application.Exit();
            }
        }
        async void TextExtraEdit()
        {
            await Task.Delay(300);
            string DeWorld = "Delfi:World";
            int step = 0;
            string PrintVer = "Version: " + ver;
            string Check = "Checking for a new version...";
            setup_update(true);
            while (true){
                while (step <= DeWorld.Length)
                {
                    TextExtra = DeWorld.Substring(0, step++);

                    await Task.Delay(175);
                }
                await Task.Delay(2000);
                step = 0;
                while (step <= PrintVer.Length)
                {
                    TextExtra = PrintVer.Substring(0, step++);

                    await Task.Delay(175);
                }
                await Task.Delay(2000);
                step = 0;
                while (step <= Check.Length)
                {
                    TextExtra = Check.Substring(0, step++);

                    await Task.Delay(175);
                }
                setup_update(true);
                step = 0;
                await Task.Delay(2000);
            }
        }
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            TextExtraEdit();
        }
    }
}

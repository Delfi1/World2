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
        int get_hg1;
        int get_wd1;
        string ver = "0.2919";
        WebClient client = new WebClient();
        string fullPath = Application.StartupPath.ToString();
        DeLog Log = new DeLog();
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
                System.Threading.Thread.Sleep(100);
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

        async void Set_locations(){
            await Task.Delay(1);
            kryptonButton1.Location = new Point(kryptonButton1.Location.X, kryptonButton1.Location.Y + 0);
        }
        async void Get_prop(){
            await Task.Delay(1);
            get_hg1 = kryptonButton1.Size.Height;
            get_wd1 = kryptonButton1.Size.Width;
            
        }
        async void Set_Prop(){

            await Task.Delay(20);
            kryptonButton1.Width = 0;
            kryptonButton1.Height = 0;
        }
        async void Btn_ch() {
            while (true)
            {
                if (kryptonButton1.Size.Width <= get_wd1)
                {
                    kryptonButton1.Width = kryptonButton1.Width + 1;
                    await Task.Delay(1);
                }
                else { break; }
                if (kryptonButton1.Size.Height <= get_hg1)
                {
                    kryptonButton1.Height = kryptonButton1.Height + 1;
                    await Task.Delay(1);
                }
            }
        }
        async void TextExtraEdit()
        {
            await Task.Delay(3300);
            string DeWorld = "Delfi:World";
            int step = 0;
            string PrintVer = "Version: " + ver;
            string Check = "Checking for a new version...";
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
        async void Initilise()
        {
            await Task.Delay(10);
            if (File.Exists(fullPath + "\\DeWorld_old.exe"))
            {
                File.Delete(fullPath + "\\DeWorld_old.exe");
            }
            notifyIcon1.Text = "De:World";
        }
        async void change_op()
        {
            while (this.Opacity != 1)
            {
                Opacity = Opacity + 0.01F;
                await Task.Delay(12);
            }
        }
        async void Load_(){
            Set_locations();
            this.Visible = false;
            this.Opacity = 0F;
            this.Visible = true;
            Get_prop();
            await Task.Delay(10);
            Set_Prop();
            await Task.Delay(10);
            change_op();
            Btn_ch();
            await Task.Delay(10);
            TextExtraEdit();
            Initilise();
            setup_update(true);
        }
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Load_();
        }
        
        protected override void OnResize(EventArgs e)
        {
            // Если программу свернули, то убрать ее из панели задач и показать в трее иконку
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(
                  1,
                  "De:World",
                  "Программа свёрнута в трей",
                  ToolTipIcon.Info
                );
            }
            else
            {
                ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
            base.OnResize(e);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (Log != null && !Log.IsDisposed && Log.Visible)
                return;

            if (Log == null)
                Log = new DeLog();

            if (Log.IsDisposed)
                Log = new DeLog();

            Log.Show();
        }
    }
}

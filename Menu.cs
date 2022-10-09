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
using System.IO.Compression;

namespace DelfiApp
{
    public partial class Menu : KryptonForm
    {
        int get_hg1;
        int get_wd1;
        string ver = "0.2929";
        WebClient client = new WebClient();
        string fullPath = Application.StartupPath.ToString();
        DeLog Log = new DeLog();
        DialogResult result;
        void setup_update(bool in_st)
        {
            if (client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/version.txt").Contains(ver))
            {
                if (!in_st)
                {
                    result = MessageBox.Show("Версия приложения:" + ver + "\n" + "Версия приложения на сервере: " + client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/version.txt"), "Уведомление", MessageBoxButtons.OK);
                }
            }
            else
            {
                if (!in_st) { result = MessageBox.Show("Версия приложения:" + ver + "\n" + "Версия приложения на сервере: " + client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/version.txt"), "Уведомление", MessageBoxButtons.OK); }
                MessageBox.Show("Обнаружена новая версия! Идет установка файлов...", "Update", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    File.Move(fullPath + "\\DeWorld.exe", fullPath + "\\DeWorld_old.exe");
                    System.Threading.Thread.Sleep(200);
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
        }

        void Download_file(string requestString, string path)
        {
            HttpClient httpClient = new HttpClient();
            var GetTask = httpClient.GetAsync(requestString);
            GetTask.Wait(1000); // WebCommsTimeout is in milliseconds

            if (!GetTask.Result.IsSuccessStatusCode)
            {
                // write an error
                return;
            }

            using (var fs = new FileStream(path, FileMode.CreateNew))
            {
                var ResponseTask = GetTask.Result.Content.CopyToAsync(fs);
                ResponseTask.Wait(1000);
            }
            System.Threading.Thread.Sleep(200);
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
            kryptonButton2.Width = 0;
            kryptonButton2.Height = 0;
        }
        async void Btn_ch(KryptonButton btn) {
            while (true)
            {
                if (btn.Size.Width <= get_wd1)
                {
                    btn.Width = btn.Width + 3;
                    await Task.Delay(1);
                }
                else { break; }
                if (btn.Size.Height <= get_hg1)
                {
                    btn.Height = btn.Height + 1;
                    await Task.Delay(1);
                }
            }
        }
        async void TextExtraEdit()
        {
            await Task.Delay(1000);
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
        async void reality(){
            await Task.Delay(2000);
            Real.Visible = true;
            int step = 0;
            string Realit = "World";
            string Ent = "Enter";
            while (step <= Ent.Length)
            {
                Real.Values.ExtraText = Ent.Substring(0, step++);

                await Task.Delay(40);
            }
            step = 0;
            await Task.Delay(100);
            while (step <= Realit.Length)
            {
                Real.Values.Text = Realit.Substring(0, step++);

                await Task.Delay(90);
            }
        }
        async void Load_(){
            Real.Visible = false;
            Set_locations();
            kryptonColorButton1.Visible = false;
            kryptonColorButton2.Visible = false;
            this.Visible = false;
            this.Opacity = 0F;
            this.Visible = true;
            Get_prop();
            await Task.Delay(20);
            Set_Prop();
            await Task.Delay(20);
            change_op();
            await Task.Delay(1800);
            Btn_ch(kryptonButton1);
            Btn_ch(kryptonButton2);
            await Task.Delay(20);
            TextExtraEdit();
            Initilise();
            setup_update(true);
            await Task.Delay(1000);
            reality();
        }
        private static String RGBConverter(Color col)
        {
            String rtn = String.Empty;
            try
            {
                rtn = col.R.ToString() + ", " + col.G.ToString() + ", " + col.B.ToString();
            }
            catch (Exception ex)
            {
                //doing nothing
            }

            return rtn;
        }

        async void Change_col(KryptonButton btn, KryptonColorButton cbt, int line){
            StreamWriter sw = new StreamWriter(fullPath + "\\Design.txt");
            string sc = RGBConverter(btn.StateNormal.Back.Color1);
            await Task.Delay(10);
            sw.WriteLine(sc);
            sw.Close();
            await Task.Delay(10);
            btn.StateNormal.Back.Color1 = cbt.SelectedColor;
            btn.StateCommon.Back.Color1 = cbt.SelectedColor;
            btn.StateTracking.Back.Color1 = cbt.SelectedColor;
            btn.StatePressed.Back.Color1 = cbt.SelectedColor;
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

        private void kryptonColorButton1_MouseClick(object sender, MouseEventArgs e)
        {
            Change_col(kryptonButton1, kryptonColorButton1, 0);
            
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            kryptonColorButton1.Visible = !kryptonColorButton1.Visible;
            kryptonColorButton2.Visible = !kryptonColorButton2.Visible;
        }

        private void kryptonColorButton2_MouseClick(object sender, MouseEventArgs e)
        {
            Change_col(kryptonButton2, kryptonColorButton2, 1);
        }

        async private void Real_MouseClick(object sender, MouseEventArgs e)
        {
            if(File.Exists(fullPath + "\\World\\Files\\World.exe")){
                StreamReader sr = new StreamReader(fullPath + "\\World\\World_ver.txt");
                string getver = await sr.ReadToEndAsync();
                sr.Close();
                if (getver.Contains(client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/World_version.txt")))
                {
                    System.Diagnostics.Process.Start(fullPath + "\\World\\Files\\World.exe");
                }
                else{
                    Directory.Delete(fullPath + "\\World\\Files", true);
                    await Task.Delay(200);
                    Download_file(@"https://github.com/Delfi1/DeWorld/blob/master/World.zip?raw=true", fullPath + "\\World\\World.zip");
                    ZipFile.ExtractToDirectory(fullPath + "\\World\\World.zip", fullPath + "\\World\\Files\\");
                    System.Diagnostics.Process.Start(fullPath + "\\World\\Files\\World.exe");
                    File.Delete(fullPath + "World\\World.zip");
                }
                StreamWriter sw = new StreamWriter(fullPath + "\\World\\World_ver.txt");
                sw.WriteLine(client.DownloadString("https://raw.githubusercontent.com/Delfi1/DeWorld/master/World_version.txt"));
                sw.Close();
            }
            else{
                Directory.CreateDirectory(fullPath + "\\World\\Files");
                File.Create(fullPath + "\\World\\World_ver.txt");
                await Task.Delay(200);
                Download_file(@"https://github.com/Delfi1/DeWorld/blob/master/World.zip?raw=true", fullPath + "\\World\\World.zip");
                ZipFile.ExtractToDirectory(fullPath + "\\World\\World.zip", fullPath + "\\World\\Files\\");
                System.Diagnostics.Process.Start(fullPath + "\\World\\Files\\World.exe");
                File.Delete(fullPath + "\\World\\World.zip");
            }
        }
    }
}

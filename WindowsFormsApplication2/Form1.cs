using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TL.AutoIE;
using WMPLib;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private KeyHook Tastatur = new KeyHook();
        // private InterceptMouse Maus = new InterceptMouse();

        private blue.Form1 Mediaplayer = new blue.Form1();
        /* [DllImport("winmm.dll")]
         public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

         [DllImport("winmm.dll")]
         public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);*/

        [DllImport("user32.dll")]
        private static extern
            bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern
            bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern
            bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        private static extern System.IntPtr LoadCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        /* [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
         private static extern IntPtr LoadLibrary(string fileName);*/

        // [DllImport("user32.dll")]
        // static extern int ShowCursor(bool bShow);

        private ControlIE Iexplorer = new ControlIE();

        private ControlIE Browser = new ControlIE();

        private const int SW_RESTORE = 9;

        private int over = 0;
        private int modus = 0;
        private bool clicked = false;

        // Uhr
        private bool uhraktiv = false;

        private bool uhreingabegestartet = false;
        private int uhrover = 0;
        private int Sekunden = 0;
        private int Minuten = 0;
        private int Stunden = 0;
        private int ISekunden = 0;
        private int IMinuten = 0;
        private int IStunden = 0;
        private bool letztenansage = false;
        private bool alarmaktiv = false;
        private int spezialalarm = 0;

        private bool second15 = false;
        private bool second30 = false;
        private bool minute1 = false;
        private bool minute2 = false;
        private bool minute3 = false;
        private bool minute5 = false;
        private bool minute10 = false;
        private bool minute15 = false;
        private bool minute20 = false;
        private bool minute30 = false;
        private bool minute45 = false;
        private bool minute60 = false;

        // TV
        private int SenderPos = 0;

        private int SenderOver = 0;
        private bool ZeigeSender = false;
        private bool tveingabegestartet = false;

        // Radio
        private int RSenderPos = 0;

        private int RSenderOver = 0;
        private bool RZeigeSender = false;
        private bool radioeingabegestartet = false;

        // private int MediaVolume = (int)((float)int.MaxValue * 0.1f);
        private int MediaVolume = 100;

        private void TerminateProcess(string processName)
        {
            Process[] MatchingProcesses = Process.GetProcessesByName(processName);

            foreach (Process p in MatchingProcesses)
            {
                p.CloseMainWindow();
            }

            System.Threading.Thread.Sleep(500);

            MatchingProcesses = Process.GetProcessesByName(processName);

            foreach (Process p in MatchingProcesses)
            {
                p.Kill();
            }
        }

        /* private bool CheckIfBrowserIsRunning()
         {
             Process[] MatchingProcesses = Process.GetProcessesByName("iexplore");

             foreach (Process p in MatchingProcesses)
             {
                 if (p.MainModule.FileName == "C:\\Program Files\\Internet Explorer2\\iexplore.exe") return true;
             }
             return false;
         }*/

        private bool CheckIfAProcessIsRunning(string processname)
        {
            return Process.GetProcessesByName(processname).Length > 0;
        }

        //  private Process Browser = null;
        private void TextSenden(string Text, Process Prozess)
        {
            //   System.IntPtr MainHandle = Process.GetProcessById(Prozess.Id).MainWindowHandle;
            // SetForegroundWindow(MainHandle);
            // SendKeys.
            //  SendKeys.Send(Text);
        }

        private Process StarteProzess(String Name, String parameter)
        {
            return System.Diagnostics.Process.Start(Name, parameter);
        }


        public Form1()
        {
            InitializeComponent();

            // hang on events
            Gma.UserActivityMonitor.HookManager.MouseClick += new MouseEventHandler(MouseMoved);
            Gma.UserActivityMonitor.HookManager.MouseDown += new MouseEventHandler(MouseDown);

            // TerminateProcess("start");
            //  TerminateProcess("explorer");
            //   TerminateProcess("iexplore");
            //   TerminateProcess("firefox");
            //   TerminateProcess("taskmgr");

           // IntPtr cursor = LoadCursorFromFile(@"./blank.cur");
         //   SetSystemCursor(cursor, 32512);

            Color trans = Color.Black;
            this.ForeColor = trans;
            this.BackColor = trans;
            this.TransparencyKey = trans;
            Iexplorer.ieStart();
            Iexplorer.iexplore.Visible = false;
            timer2.Enabled = true;
            timer3.Enabled = true;
            timer3_Tick(null, null);
            Sounds.Load("C:\\Sounds\\", "DE", "F");

            Browser.ieNavigate(true, "www.google.de");
            Browser.iexplore.FullScreen = true;
            Browser.iexplore.Silent = true;
            Browser.iexplore.AddressBar = false;
            Browser.iexplore.MenuBar = false;
            Browser.iexplore.Resizable = false;
            Browser.iexplore.StatusBar = false;
            Browser.iexplore.Visible = false;

            Iexplorer.ieNavigate(true, "about:blank");
            Iexplorer.iexplore.FullScreen = true;
            Iexplorer.iexplore.Silent = true;
            Iexplorer.iexplore.AddressBar = false;
            Iexplorer.iexplore.MenuBar = false;
            Iexplorer.iexplore.Resizable = false;
            Iexplorer.iexplore.StatusBar = false;
            Iexplorer.iexplore.Visible = false;
            Mediaplayer.Mediaplayer.settings.volume = MediaVolume;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        public static POINT mausklick = null;
        public static bool IsDown = false;
        public static int DownCounter = 0;
        public static POINT DownPoint = null;
        public void MouseMoved(object sender, MouseEventArgs e)
        {

            if (e.Clicks > 0)
            {
                mausklick = new POINT();
                mausklick.x = e.X; mausklick.y = e.Y;
                IsDown = false;
                DownCounter = 0;
            }
            else
                mausklick = null;
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (IsDown == false)
            {
                DownPoint = new POINT();
                DownPoint.x = e.X; DownPoint.y = e.Y;
            }
            IsDown = true;
        }

        ~Form1()
        {
            if (Iexplorer != null)
                if (Iexplorer.launched)
                    Iexplorer.ieClose();

            if (Browser != null)
                if (Browser.launched)
                    Browser.ieClose();

            SetSystemCursor(LoadCursor((IntPtr)null, 32651), 32512);
        }

        public void focuse()
        {
            if (!this.Visible) return;

            if (IsIconic(this.Handle))
                ShowWindowAsync(this.Handle, SW_RESTORE);

            SetForegroundWindow(this.Handle);
        }

        public void Starte_Browser()
        {
            Browser.iexplore.Visible = true;

            this.Hide();
            Visible = false;
            if (Mediaplayer.URL != "") Mediaplayer.Hide();
        }

        public void Starte_TV()
        {
            Iexplorer.ieNavigate(true, "http://peer-stream.com/api/embedplayer/?id=viva&e=3&width=100%&height=100%");

            Iexplorer.iexplore.Visible = true;
            timer1.Enabled = true;
            focuse();
        }

        public void Starte_Radio(int id)
        {
            String zo = RadioA[id];
            Mediaplayer.Starte(RadioA[id]);
            focuse();
        }

        public void RSenderWechsel(int id)
        {
            Starte_Radio(id);
        }

        private String[] SenderAnzeige = { "VIVA", "ARD", "ZDF", "ZDF Neo", "3SAT", "ARTE", "WDR", "BR", "HR", "SWR", "EURONEWS", "EUROSPORT", "KABEL 1", "VOX", "TELE5", "RTL", "ORF Eins", "ORF2" };
        private String[] Sender = { "viva", "ard", "zdf", "zdf%20neo", "3sat", "arte", "wdr", "br", "hr", "swr", "euronews", "EUROSPORT", "kabel%201", "vox", "tele5", "rtl", "orf%20eins", "orf2" };

        public void SenderWechsel(int id)
        {
            Iexplorer.ieNavigate(true, "http://peer-stream.com/api/embedplayer/?id=" + Sender[id] + "&e=3&width=100%&height=100%");
            timer1.Enabled = true;
        }

        private void SenderAnzeigenStart()
        {
            senderzeigen.Stop();
            senderzeigen.Start();
            ZeigeSender = true;
        }

        private void uhrEingabeStart()
        {
            uhreingabe.Stop();
            uhreingabe.Start();
            uhreingabegestartet = true;
        }

        private void uhrEingabeStop()
        {
            uhreingabe.Enabled = false;
            uhreingabegestartet = false;
        }

        private void TvEingabeStart()
        {
            tveingabe.Stop();
            tveingabe.Start();
            tveingabegestartet = true;
        }

        private void TvEingabeStop()
        {
            tveingabe.Enabled = false;
            tveingabegestartet = false;
        }

        private void RadioEingabeStart()
        {
            radioeingabe.Stop();
            radioeingabe.Start();
            radioeingabegestartet = true;
        }

        private void RadioEingabeStop()
        {
            radioeingabe.Stop();
            radioeingabegestartet = false;
        }

        private void RSenderAnzeigenStart()
        {
            Rsenderzeigen.Stop();
            Rsenderzeigen.Start();
            RZeigeSender = true;
        }

        private String[] RadioS = { "Top 100 Station", "Radio PSR", "ENERGY Sachsen", "MDR Sputnik", "MDR Jump", "SAW", "MDR1 Radio Sachsen Anhalt", "89.0 RTL", "Radio Brocken", "saw 80er", "1LIVE" };
        private String[] RadioA = { "http://87.230.101.49:80", "http://streams.radiopsr.de/psr-live/mp3-128/wwwradiode", "http://radio.nrj.net/sachsen/radiode", "http://c22033-l.i.core.cdn.streamfarm.net/22005mdrsputnik/live/3087mdr_sputnik/live_de_128.mp3", "http://c22033-l.i.core.cdn.streamfarm.net/22004mdrjump/live/3087mdr_jump/live_de_128.mp3", "http://stream.radiosaw.de/saw/mp3-128/radiode/", "http://c22033-l.i.core.cdn.streamfarm.net/22002mdr1sachsenanhalt/live/3087mdr_mdr1sa/live_de_128.mp3", "http://80.237.158.40/890rtl-128.mp3", "http://sites.radiobrocken.de/streams/mp3_128k.m3u", "http://stream.hoerradar.de/saw80er-128", "http://1live.akacast.akamaistream.net/7/706/119434/v1/gnl.akacast.akamaistream.net/1live" };

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private String nullen(int Text, int stellen)
        {
            return nullen(Text.ToString(), stellen);
        }

        private String nullen(String Text, int stellen)
        {
            return Text.PadLeft(stellen, '0');
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Iexplorer.launched)
                Iexplorer.ieClose();
        }

        private int restartwait = 0;

        private bool CheckPoint(POINT P, Label rect)
        {
            if (P.x >= rect.Location.X && P.x <= rect.Location.X + rect.Width && P.y >= rect.Location.Y && P.y <= rect.Location.Y + rect.Height)
            {
                return true;
            }

            return false;
        }

        private bool CheckPoint(POINT P, Rectangle rect)
        {
            if (P.x >= rect.Location.X && P.x <= rect.Location.X + rect.Width && P.y >= rect.Location.Y && P.y <= rect.Location.Y + rect.Height)
            {
                return true;
            }

            return false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (IsDown)
            {
                DownCounter++;
                if (DownCounter >= 10)
                {
                    mausklick = DownPoint;
                }
            }

            if (mausklick != null)
            {
                int w = (int) (Width * 0.125);
                Rectangle D = new Rectangle(Width - label12.Width / 2, label12.Top, label12.Width / 2, label12.Height);
                Rectangle C = new Rectangle(Width - label12.Width, label12.Top, label12.Width/2, label12.Height);

                Rectangle E = new Rectangle(0, Height-label12.Height, (int)(Width * 0.25f), label12.Height);


                if (modus == 0)
                {
                    if (CheckPoint(mausklick, label2))
                    {
                        over = 0;
                        modus = 1;
                    }
                    else
                        if (CheckPoint(mausklick, label3))
                        {
                            if (Mediaplayer.URL != "")
                            {
                                Mediaplayer.Stop();
                            }
                            else
                            {
                                over = 1;
                                modus = 2;
                                // Musik aufrufen
                                if (Iexplorer.iexplore.Visible)
                                {
                                    Iexplorer.ieClose();
                                    Iexplorer.ieStart();
                                    Iexplorer.iexplore.Visible = false;
                                }

                                Starte_Radio(RSenderPos);
                                RSenderAnzeigenStart();
                            }
                        }
                        else
                            if (CheckPoint(mausklick, label4))
                            {
                                over = 2;
                                // Uhr aufrufen
                                uhrover = 0;
                                modus = 3;
                            }
                            else
                                if (CheckPoint(mausklick, label11))
                                {
                                    over = 3;
                                    modus = 4;
                                }
                                else
                                    if (CheckPoint(mausklick, C))
                                    {
                                        int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                                        int max = ushort.MaxValue;
                                        int steps = max / 15;
                                        if (CalcVol < max) CalcVol += steps;
                                        if (CalcVol > max) CalcVol = max;
                                        PC_VolumeControl.VolumeControl.SetVolume(CalcVol);

                                    }
                                    else
                                        if (CheckPoint(mausklick, D))
                                        {
                                            int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                                            int max = ushort.MaxValue;
                                            int steps = max / 15;
                                            if (CalcVol > 0) CalcVol -= steps;
                                            if (CalcVol < 0) CalcVol = 0;
                                            PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
                                        }
                                        else
                                            if (CheckPoint(mausklick, E))
                                            {
                                                if (Iexplorer.iexplore.Visible)
                                                {
                                                    modus = 1;
                                                }
                                                else
                                                    if (Mediaplayer.URL != "")
                                                    {
                                                        modus = 2;
                                                        this.Refresh();
                                                    }
                                            }
                }
                else
                    if (modus == 2)
                    {
                        Rectangle A = new Rectangle(0, label6.Top + label6.Height, (int)(Width * 0.25f), label13.Top - (label6.Top + label6.Height));
                        Rectangle B = new Rectangle((int)((float)(Width - Width * 0.25f)), label6.Top + label6.Height, (int)(Width * 0.25f), label13.Top - (label6.Top + label6.Height));
                        int w2 = (int)(Width * 0.125);
                        Rectangle D2 = new Rectangle(Width - w2, label12.Top, w2, label12.Height);
                        Rectangle C2 = new Rectangle(Width - 2 * w2, label12.Top, w2, label12.Height);
                        Rectangle E2 = new Rectangle(0, Height-label12.Height, (int)(Width * 0.25f), label12.Height);

                        if (CheckPoint(mausklick, B))
                        {
                            RSenderOver++; if (RSenderOver >= RadioA.Length) RSenderOver = 0;
                            RSenderAnzeigenStart();
                            RadioEingabeStop();
                            if (RSenderPos != RSenderOver)
                            {
                                RSenderPos = RSenderOver;
                                RSenderWechsel(RSenderPos);
                            }
                        }
                        else
                            if (CheckPoint(mausklick, A))
                            {
                                RSenderOver--; if (RSenderOver < 0) RSenderOver = RadioA.Length - 1;
                                RSenderAnzeigenStart();
                                RadioEingabeStop();
                                if (RSenderPos != RSenderOver)
                                {
                                    RSenderPos = RSenderOver;
                                    RSenderWechsel(RSenderPos);
                                }
                            }
                            else
                                if (CheckPoint(mausklick, C2))
                                {
                                    int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                                    int max = ushort.MaxValue;
                                    int steps = max / 15;
                                    if (CalcVol < max) CalcVol += steps;
                                    if (CalcVol > max) CalcVol = max;
                                    PC_VolumeControl.VolumeControl.SetVolume(CalcVol);

                                }
                                else
                                    if (CheckPoint(mausklick, D2))
                                    {
                                        int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                                        int max = ushort.MaxValue;
                                        int steps = max / 15;
                                        if (CalcVol > 0) CalcVol -= steps;
                                        if (CalcVol < 0) CalcVol = 0;
                                        PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
                                    }
                        else
                                        if (CheckPoint(mausklick, E2))
                                        {
                                            modus = 0;
                                            TvEingabeStop();
                                            RadioEingabeStop();
                                        }

                    }
                    else
                        if (modus == 3)
                        {
                            Rectangle E3 = new Rectangle(0, Height - label12.Height, (int)(Width * 0.25f), label12.Height);
                            if (CheckPoint(mausklick, label14))
                            {
                                uhrover = 4;
                                second15 = second15 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label15))
                            {uhrover = 5;
                                second30 = second30 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label16)){uhrover = 6;
                            minute1 = minute1 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label17)){uhrover = 7;
                            minute2 = minute2 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label18)){uhrover = 8;
                            minute3 = minute3 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label19)){uhrover = 9;
                            minute5 = minute5 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label20))
                            {
                                uhrover = 19;
                                minute10 = minute10 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label22))
                            {
                                uhrover = 11;
                                minute15 = minute15 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label23))
                            {
                                uhrover = 12;
                                minute20 = minute20 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label24)){uhrover = 13;
                                minute30 = minute30 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label25))
                            {
                                uhrover = 14;
                                minute45 = minute45 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label26))
                            {
                                uhrover = 15;
                                minute60 = minute60 ? false : true;
                            }
                            else
                            if (CheckPoint(mausklick, label10))
                            {

                                if (!uhraktiv)
                                {
                                    Sekunden = ISekunden;
                                    Minuten = IMinuten;
                                    Stunden = IStunden;
                                    uhraktiv = true;
                                    spezialalarm = 1;
                                    Sounds.Alarm.PlaySound(false, 0, 5f, 1f);
                                }
                                else
                                    uhraktiv = false;
                            }
                            else
                                if (CheckPoint(mausklick, E3))
                                {
                                    modus = 0;
                                    TvEingabeStop();
                                    RadioEingabeStop();
                                }

                            Rectangle UpS = new Rectangle(label7.Left, label7.Top, label7.Width, label7.Height);
                            Rectangle UpM = new Rectangle(label8.Left, label8.Top, label8.Width, label8.Height);
                            Rectangle UpSS = new Rectangle(label9.Left, label9.Top, label9.Width, label9.Height);
                            Rectangle DownS = new Rectangle(label7.Left, label7.Top + label7.Height, label7.Width, label7.Height);
                            Rectangle DownM = new Rectangle(label8.Left, label8.Top + label8.Height, label8.Width, label8.Height);
                            Rectangle DownSS = new Rectangle(label9.Left, label9.Top + label9.Height, label9.Width, label9.Height);
                            if (CheckPoint(mausklick, UpS)){IStunden++;uhrover =1;}
                            if (CheckPoint(mausklick, UpM)) {IMinuten++;uhrover =2;}
                            if (CheckPoint(mausklick, UpSS)) { ISekunden++; uhrover = 3; }

                            if (ISekunden > 59) { ISekunden = 0; IMinuten++; }
                            if (IMinuten > 59) { IMinuten = 0; IStunden++; }
                            if (IStunden > 99) { IStunden = 99; }

                            if (CheckPoint(mausklick, DownS)) { IStunden--; uhrover = 1; }
                            if (CheckPoint(mausklick, DownM)) { IMinuten--; uhrover = 2; }
                            if (CheckPoint(mausklick, DownSS)) { ISekunden--; uhrover = 3; }

                            if (ISekunden < 0) { ISekunden = 0; }
                            if (IMinuten < 0) { IMinuten = 0; }
                            if (IStunden < 0) { IStunden = 0; }
                        }

                mausklick = null;
            }

            if (restartwait == 0)
            {
                if (modus == 2 && (Mediaplayer.Mediaplayer.status == "Bereit" || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsLast || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsMediaEnded || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsUndefined || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsStopped || Mediaplayer.Mediaplayer.isOnline == false || Mediaplayer.Mediaplayer.openState == WMPOpenState.wmposUndefined))
                {
                    Mediaplayer.Stop();
                    RSenderWechsel(RSenderPos);
                    restartwait = 50;
                }
            }
            else
                restartwait--;

            for (int i = 0; i < KeyHook.KeyCodes.Count; i++)
            {
                if (alarmaktiv)
                {
                    alarmaktiv = false;
                    uhraktiv = false;
                    Sounds.Alarm.StopSound(0);
                    continue;
                }

                int Taste = -1;
                if (KeyHook.KeyCodes[i] == Keys.D0) Taste = 0;
                if (KeyHook.KeyCodes[i] == Keys.D1) Taste = 1;
                if (KeyHook.KeyCodes[i] == Keys.D2) Taste = 2;
                if (KeyHook.KeyCodes[i] == Keys.D3) Taste = 3;
                if (KeyHook.KeyCodes[i] == Keys.D4) Taste = 4;
                if (KeyHook.KeyCodes[i] == Keys.D5) Taste = 5;
                if (KeyHook.KeyCodes[i] == Keys.D6) Taste = 6;
                if (KeyHook.KeyCodes[i] == Keys.D7) Taste = 7;
                if (KeyHook.KeyCodes[i] == Keys.D8) Taste = 8;
                if (KeyHook.KeyCodes[i] == Keys.D9) Taste = 9;

#region MODUS0
                if (modus == 0)
                {
                    if (KeyHook.KeyCodes[i] == Keys.Up)
                    {
                        if (over == 3) { over = 0; }
                        else
                            over = 3;
                        //timer3_Tick(null, null);
                    }
                    else
                        if (KeyHook.KeyCodes[i] == Keys.Down)
                        {
                            if (over == 3) { over = 0; }
                            else
                                over = 3;
                            //     timer3_Tick(null, null);
                        }
                        else
                            if (KeyHook.KeyCodes[i] == Keys.Right)
                            {
                                over = (over + 1) % 4;
                                //  timer3_Tick(null, null);
                            }
                            else
                                if (KeyHook.KeyCodes[i] == Keys.Left)
                                {
                                    over--; if (over < 0) over = 3;
                                    //   timer3_Tick(null, null);
                                }
                                else
                                    if (KeyHook.KeyCodes[i] == Keys.Space)
                                    {
                                        clicked = false;
                                        if (over == 0 && Iexplorer.iexplore.Visible)
                                        {
                                            Iexplorer.ieClose();
                                            Iexplorer.ieStart();
                                            Iexplorer.iexplore.FullScreen = true;
                                            Iexplorer.iexplore.Visible = false;
                                        }
                                        else
                                            if (over == 1 && Mediaplayer.URL != "")
                                            {
                                                Mediaplayer.Stop();
                                            }
                                            else
                                            {
                                                // modus wechseln
                                                modus = over + 1;

                                                if (modus == 1)
                                                {
                                                    // TV aufrufen
                                                    Mediaplayer.Stop();

                                                    if (!Iexplorer.iexplore.Visible)
                                                    {
                                                        Starte_TV();
                                                    }
                                                    SenderAnzeigenStart();
                                                }
                                                else
                                                    if (modus == 2)
                                                    {
                                                        // Musik aufrufen
                                                        if (Iexplorer.iexplore.Visible)
                                                        {
                                                            Iexplorer.ieClose();
                                                            Iexplorer.ieStart();
                                                            Iexplorer.iexplore.Visible = false;
                                                        }

                                                        Starte_Radio(RSenderPos);
                                                        RSenderAnzeigenStart();
                                                    }
                                                    else
                                                        if (modus == 3)
                                                        {
                                                            // Uhr aufrufen
                                                            uhrover = 0;
                                                        }
                                                        else
                                                            if (modus == 4)
                                                            {
                                                                Starte_Browser();
                                                            }

                                                //  timer3_Tick(null, null);
                                            }
                                    }
                }
#endregion
                else

                    #region MODUS1

                    if (modus == 1)
                    {
                        if (Taste > -1)
                        {
                            if (!tveingabegestartet)
                            {
                                SenderOver = 0;
                            }

                            SenderOver *= 10;
                            SenderOver += Taste;

                            if (SenderOver < 0) SenderOver = Sender.Length - 1;
                            if (SenderOver >= Sender.Length) SenderOver = 0;
                            TvEingabeStart();
                            SenderAnzeigenStart();
                            //  timer3_Tick(null, null);
                        }
                        else
                            if (KeyHook.KeyCodes[i] == Keys.P)
                            {
                                SenderOver++; if (SenderOver >= Sender.Length) SenderOver = 0;
                                SenderAnzeigenStart();
                                SenderPos = SenderOver;
                                SenderWechsel(SenderPos);
                                TvEingabeStop();
                                //   timer3_Tick(null, null);
                            }
                            else
                                if (KeyHook.KeyCodes[i] == Keys.O)
                                {
                                    SenderOver--; if (SenderOver < 0) SenderOver = Sender.Length - 1;
                                    SenderAnzeigenStart();
                                    SenderPos = SenderOver;
                                    SenderWechsel(SenderPos);
                                    TvEingabeStop();
                                    //  timer3_Tick(null, null);
                                }
                                else
                                    if (KeyHook.KeyCodes[i] == Keys.Up)
                                    {
                                        SenderOver++; if (SenderOver >= Sender.Length) SenderOver = 0;
                                        SenderAnzeigenStart();
                                        TvEingabeStop();
                                        // timer3_Tick(null, null);
                                    }
                                    else
                                        if (KeyHook.KeyCodes[i] == Keys.Down)
                                        {
                                            SenderOver--; if (SenderOver < 0) SenderOver = Sender.Length - 1;
                                            SenderAnzeigenStart();
                                            TvEingabeStop();
                                            //  timer3_Tick(null, null);
                                        }
                                        else
                                            if (KeyHook.KeyCodes[i] == Keys.Space)
                                            {
                                                if (SenderPos != SenderOver)
                                                {
                                                    SenderPos = SenderOver;
                                                    SenderWechsel(SenderPos);
                                                }
                                                TvEingabeStop();
                                                SenderAnzeigenStart();
                                                // timer3_Tick(null, null);
                                            }
                    }

                    #endregion MODUS1

                    else

                        #region MODUS2

                        if (modus == 2)
                        {
                            if (Taste > -1)
                            {
                                if (!radioeingabegestartet)
                                {
                                    RSenderOver = 0;
                                }

                                RSenderOver *= 10;
                                RSenderOver += Taste;

                                if (RSenderOver < 0) RSenderOver = RadioA.Length - 1;
                                if (RSenderOver >= RadioA.Length) RSenderOver = 0;
                                RadioEingabeStart();
                                RSenderAnzeigenStart();
                                //  timer3_Tick(null, null);
                            }
                            else
                                if (KeyHook.KeyCodes[i] == Keys.P)
                                {
                                    RSenderOver++; if (RSenderOver >= RadioA.Length) RSenderOver = 0;
                                    RSenderAnzeigenStart();
                                    RSenderPos = RSenderOver;
                                    RSenderWechsel(RSenderPos);
                                    RadioEingabeStop();
                                    //  timer3_Tick(null, null);
                                }
                                else
                                    if (KeyHook.KeyCodes[i] == Keys.O)
                                    {
                                        RSenderOver--; if (RSenderOver < 0) RSenderOver = RadioA.Length - 1;
                                        RSenderAnzeigenStart();
                                        RSenderPos = RSenderOver;
                                        RSenderWechsel(RSenderPos);
                                        RadioEingabeStop();
                                        //   timer3_Tick(null, null);
                                    }
                                    else
                                        if (KeyHook.KeyCodes[i] == Keys.Up)
                                        {
                                            RSenderOver++; if (RSenderOver >= RadioA.Length) RSenderOver = 0;
                                            RSenderAnzeigenStart();
                                            RadioEingabeStop();
                                            //   timer3_Tick(null, null);
                                        }
                                        else
                                            if (KeyHook.KeyCodes[i] == Keys.Down)
                                            {
                                                RSenderOver--; if (RSenderOver < 0) RSenderOver = RadioA.Length - 1;
                                                RSenderAnzeigenStart();
                                                RadioEingabeStop();
                                                //    timer3_Tick(null, null);
                                            }
                                            else
                                                if (KeyHook.KeyCodes[i] == Keys.Space)
                                                {
                                                    if (RSenderPos != RSenderOver)
                                                    {
                                                        RSenderPos = RSenderOver;
                                                        RSenderWechsel(RSenderPos);
                                                    }
                                                    RadioEingabeStop();
                                                    RSenderAnzeigenStart();
                                                    //   timer3_Tick(null, null);
                                                }
                        }

                        #endregion MODUS2

                        else

                            #region MODUS3

                            if (modus == 3)
                            {
                                if (Taste > -1)
                                {
                                    if (!uhreingabegestartet)
                                    {
                                        if (uhrover == 1) IStunden = 0;
                                        if (uhrover == 2) IMinuten = 0;
                                        if (uhrover == 3) ISekunden = 0;
                                    }

                                    if (uhrover == 1) { IStunden *= 10; IStunden += Taste; }
                                    if (uhrover == 2) { IMinuten *= 10; IMinuten += Taste; }
                                    if (uhrover == 3) { ISekunden *= 10; ISekunden += Taste; }

                                    if (ISekunden > 59) { ISekunden = 59; }
                                    if (IMinuten > 59) { IMinuten = 59; }
                                    if (IStunden > 99) { IStunden = 99; }
                                    if (ISekunden < 0) { ISekunden = 0; }
                                    if (IMinuten < 0) { IMinuten = 0; }
                                    if (IStunden < 0) { IStunden = 0; }

                                    uhrEingabeStart();
                                    //  timer3_Tick(null, null);
                                }
                                if (KeyHook.KeyCodes[i] == Keys.Left)
                                {
                                    if (uhrover > 3)
                                    {
                                        uhrover = 0;
                                    }
                                    else
                                    {
                                        uhrover--; if (uhrover < 0) uhrover = 15;
                                    }
                                    uhrEingabeStop();
                                    //  timer3_Tick(null, null);
                                }
                                else
                                    if (KeyHook.KeyCodes[i] == Keys.Right)
                                    {
                                        if (uhrover > 3)
                                        {
                                            uhrover = 0;
                                        }
                                        else
                                        {
                                            uhrover++; if (uhrover >= 16) uhrover = 0;
                                        }
                                        uhrEingabeStop();
                                        //   timer3_Tick(null, null);
                                    }
                                    else
                                        if (KeyHook.KeyCodes[i] == Keys.Up)
                                        {
                                            if (uhrover == 0 || uhrover > 3)
                                            {
                                                uhrover--; if (uhrover < 0) uhrover = 15;
                                                uhrEingabeStop();
                                                // timer3_Tick(null, null);
                                            }

                                            if (uhrover == 1) IStunden++;
                                            if (uhrover == 2) IMinuten++;
                                            if (uhrover == 3) ISekunden++;

                                            if (ISekunden > 59) { ISekunden = 0; IMinuten++; }
                                            if (IMinuten > 59) { IMinuten = 0; IStunden++; }
                                            if (IStunden > 99) { IStunden = 99; }
                                            uhrEingabeStop();
                                            //  timer3_Tick(null, null);
                                        }
                                        else
                                            if (KeyHook.KeyCodes[i] == Keys.Down)
                                            {
                                                if (uhrover == 0 || uhrover > 3)
                                                {
                                                    uhrover++; if (uhrover >= 16) uhrover = 0;
                                                    uhrEingabeStop();
                                                    //   timer3_Tick(null, null);
                                                }

                                                if (uhrover == 1) IStunden--;
                                                if (uhrover == 2) IMinuten--;
                                                if (uhrover == 3) ISekunden--;

                                                if (ISekunden < 0) { ISekunden = 59; IMinuten--; }
                                                if (IMinuten < 0) { IMinuten = 59; IStunden--; }
                                                if (IStunden < 0) { IStunden = 0; }
                                                uhrEingabeStop();
                                                //    timer3_Tick(null, null);
                                            }
                                            else
                                                if (KeyHook.KeyCodes[i] == Keys.Space)
                                                {
                                                    if (uhrover == 4)
                                                        second15 = second15 ? false : true;
                                                    if (uhrover == 5)
                                                        second30 = second30 ? false : true;
                                                    if (uhrover == 6)
                                                        minute1 = minute1 ? false : true;
                                                    if (uhrover == 7)
                                                        minute2 = minute2 ? false : true;
                                                    if (uhrover == 8)
                                                        minute3 = minute3 ? false : true;
                                                    if (uhrover == 9)
                                                        minute5 = minute5 ? false : true;
                                                    if (uhrover == 10)
                                                        minute10 = minute10 ? false : true;
                                                    if (uhrover == 11)
                                                        minute15 = minute15 ? false : true;
                                                    if (uhrover == 12)
                                                        minute20 = minute20 ? false : true;
                                                    if (uhrover == 13)
                                                        minute30 = minute30 ? false : true;
                                                    if (uhrover == 14)
                                                        minute45 = minute45 ? false : true;
                                                    if (uhrover == 15)
                                                        minute60 = minute60 ? false : true;

                                                    if (uhreingabegestartet)
                                                    {
                                                        uhrEingabeStop();
                                                    }
                                                    else
                                                        if (uhrover <= 3)
                                                        {
                                                            if (!uhraktiv)
                                                            {
                                                                Sekunden = ISekunden;
                                                                Minuten = IMinuten;
                                                                Stunden = IStunden;
                                                                uhraktiv = true;
                                                                spezialalarm = 1;
                                                                Sounds.Alarm.PlaySound(false, 0, 5f, 1f);
                                                            }
                                                            else
                                                                uhraktiv = false;
                                                        }
                                                    //  timer3_Tick(null, null);
                                                }
                            }

                            #endregion MODUS3

                            else

                                if (modus == 4)
                                {
                                    if (KeyHook.KeyCodes[i] == Keys.F6)
                                    {
                                        Browser.iexplore.Navigate("www.google.de");
                                    }
                                }

                if (KeyHook.KeyCodes[i] == Keys.VolumeUp)
                {
                    int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                    int max = ushort.MaxValue;
                    int steps = max / 15;
                    if (CalcVol < max) CalcVol += steps;
                    if (CalcVol > max) CalcVol = max;
                    PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
                }
                else
                    if (KeyHook.KeyCodes[i] == Keys.VolumeDown)
                    {
                        int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                        int max = ushort.MaxValue;
                        int steps = max / 15;
                        if (CalcVol > 0) CalcVol -= steps;
                        if (CalcVol < 0) CalcVol = 0;
                        PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
                    }
                    else
                        if (KeyHook.KeyCodes[i] == Keys.Escape)
                        {
                            if (modus == 4)
                            {
                                modus = 0;
                                Browser.iexplore.Visible = false;
                                this.Show();
                                Visible = true;
                                if (Mediaplayer.URL != "") Mediaplayer.Show();
                                this.Focus();
                                focuse();
                            }
                            else
                                if (modus == 0)
                                {
                                    if (Iexplorer.iexplore.Visible)
                                    {
                                        modus = 1;
                                    }
                                    else
                                        if (Mediaplayer.URL != "")
                                        {
                                            modus = 2;
                                            this.Refresh();
                                        }
                                    //    timer3_Tick(null, null);
                                }
                                else
                                {
                                    modus = 0;
                                    TvEingabeStop();
                                    RadioEingabeStop();
                                    //  timer3_Tick(null, null);
                                }
                        }
            }

            KeyHook.KeyCodes.Clear();

            #region Zeichnen
            label2.Text = Iexplorer.iexplore.Visible ? "TV aus" : "TV";
            label3.Text = Mediaplayer.URL != "" ? "Radio aus" : "Radio";
            //  label4.Text = modus == 3 ? "Uhr aus" : "Uhr";

            label2.Font = over == 0 ? new Font(label2.Font, FontStyle.Underline) : new Font(label2.Font, FontStyle.Regular);
            label3.Font = over == 1 ? new Font(label3.Font, FontStyle.Underline) : new Font(label3.Font, FontStyle.Regular);
            label4.Font = over == 2 ? new Font(label4.Font, FontStyle.Underline) : new Font(label4.Font, FontStyle.Regular);
            label11.Font = over == 3 ? new Font(label11.Font, FontStyle.Underline) : new Font(label11.Font, FontStyle.Regular);

            if (uhraktiv)
            {
                // label5.Text = nullen(Stunden, 2) + " " + nullen(Minuten, 2) + " " + nullen(Sekunden, 2);
                // label5.Left = this.Width - label5.Width;

                if (modus == 0)
                {
                    /* label5.Text = ("").PadRight(60, ' ') + nullen(Stunden, 2) + ":" + nullen(Minuten, 2) + ":" + nullen(Sekunden, 2);
                                        label5.Left = this.Width - label5.Width;
                                        label5.Top = 0;*/
                    label5.Text = ("").PadRight(30, ' ') + nullen(Stunden, 2) + ":" + nullen(Minuten, 2) + ":" + nullen(Sekunden, 2) + ("").PadRight(30, ' ');
                    label5.Top = this.Height / 2 - label6.Height / 2 + label6.Height + 150;
                    label5.Left = this.Width / 2 - label5.Width / 2;
                    label5.Show();
                }
                else
                    if (modus == 2)
                    {
                        label5.Text = ("").PadRight(30, ' ') + nullen(Stunden, 2) + ":" + nullen(Minuten, 2) + ":" + nullen(Sekunden, 2) + ("").PadRight(30, ' ');
                        label5.Top = this.Height / 2 - label6.Height / 2 + label6.Height;
                        label5.Left = this.Width / 2 - label5.Width / 2;
                        label5.Show();
                    }
                    else
                        if (modus != 4)
                        {
                            label5.Text = ("").PadRight(30, ' ') + nullen(Stunden, 2) + ":" + nullen(Minuten, 2) + ":" + nullen(Sekunden, 2) + ("").PadRight(30, ' ');
                            label5.Top = this.Height / 2 - label6.Height / 2 + label6.Height;
                            label5.Left = this.Width / 2 - label5.Width / 2;
                            label5.Show();
                        }
                        else
                            label5.Hide();
            }
            else
                label5.Hide();

            if (modus == 1 && ZeigeSender)
            {
                label6.Text = ("").PadRight(30, ' ') + (SenderOver).ToString() + "   " + SenderAnzeige[SenderOver] + ("").PadRight(30, ' ');
                label6.Top = this.Height / 2 - label6.Height / 2;
                label6.Left = this.Width / 2 - label6.Width / 2;
                label6.BringToFront();
                label6.Show();
            }

            if (modus == 2)
            {
                label6.Text = ("").PadRight(30, ' ') + (RSenderOver).ToString() + "   " + RadioS[RSenderOver] + ("").PadRight(30, ' ');
                label6.Top = 0;//this.Height / 4 - label6.Height
                label6.Left = this.Width / 2 - label6.Width / 2;
                label6.BringToFront();
                label6.Show();
                label1.BackColor = Color.MidnightBlue;
                label13.Text = ("").PadLeft(120, ' ') + Mediaplayer.Mediaplayer.status + ("").PadLeft(120, ' ');
                label13.Top = this.Height - label13.Height - label12.Height;
                label13.Left = this.Width / 2 - label13.Width / 2;
                label13.Show();
                label13.Refresh();
                // label5.BackColor = Color.Navy;
            }
            else
            {
                label13.Hide();
                label1.BackColor = Color.Transparent;
                // label5.BackColor = Color.Transparent;
                if (!(modus == 1 && ZeigeSender))
                    label6.Hide();
            }

            {
                int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                int max = 65535;
                int steps = max / 15;
                String temp = "";
                for (int c = max - steps; c > 0; c -= steps)
                {
                    if (CalcVol >= c)
                    {
                        temp = temp + "g";
                    }
                    else
                        temp = temp + "c";
                }
                label12.Text = temp;

                if (CalcVol < steps)
                {
                    label12.Font = new Font("Webdings", 27.75f, FontStyle.Regular);
                    label12.Text = "x";
                }
                else
                {
                    label12.Font = new Font("Webdings", 27.75f, FontStyle.Regular);
                }

                if (CalcVol >= max - steps)
                {
                    label12.Text = label12.Text.Replace('/', '|');
                }

                label12.Top = this.Height - label12.Height;
                label12.Left = this.Width - label12.Width;
                label12.Show();
                label12.Refresh();
            }

            // Hauptmenü
            if (modus == 0)
            {
                int mitte = this.Width / 2;
                label3.Left = mitte - label3.Width / 2;

                int mitte2 = label3.Left / 2;
                label2.Left = mitte2 - label2.Width / 2;

                int mitte3 = label3.Left + label3.Width + (this.Width - (label3.Left + label3.Width)) / 2;
                label4.Left = mitte3 - label4.Width / 2;

                mitte2 = label3.Left / 2;
                label11.Left = mitte2 - label11.Width / 2;
                label11.Top = label2.Top + Height / 4;

                label2.Show();
                label3.Show();
                label4.Show();
                label11.Show();
                label12.Show();

                label2.Refresh();
                label3.Refresh();
                label4.Refresh();
                label11.Refresh();
                label12.Refresh();

                label12.BringToFront();

                /* Iexplorer.iexplore.Visible = false;
                 if (Browser.iexplore!=null)
                 Browser.iexplore.Visible = false;*/
            }
            else
            {
                label2.Hide();
                label3.Hide();
                label4.Hide();
                label11.Hide();
                // label12.Hide();

                label2.Refresh();
                label3.Refresh();
                label4.Refresh();
                label11.Refresh();
            }

            // Stopuhr
            if (modus == 3)
            {
                label10.Font = uhrover == 0 ? new Font(label10.Font, FontStyle.Underline) : new Font(label10.Font, FontStyle.Regular);
                label7.Font = uhrover == 1 ? new Font(label7.Font, FontStyle.Underline) : new Font(label7.Font, FontStyle.Regular);
                label8.Font = uhrover == 2 ? new Font(label8.Font, FontStyle.Underline) : new Font(label8.Font, FontStyle.Regular);
                label9.Font = uhrover == 3 ? new Font(label9.Font, FontStyle.Underline) : new Font(label9.Font, FontStyle.Regular);

                if (uhraktiv)
                {
                    label10.Text = "Deaktivieren";
                }
                else
                    label10.Text = "Aktivieren";

                label7.Text = nullen(IStunden, 2) + "h ";
                label8.Text = nullen(IMinuten, 2) + "m ";
                label9.Text = nullen(ISekunden, 2) + "s ";

                int hoehe = -100;

                label10.Left = this.Width / 2 - label10.Width / 2;
                label10.Top = this.Height / 2 - label10.Height / 2 - 50 + hoehe;

                label8.Left = this.Width / 2 - label8.Width / 2;
                label8.Top = this.Height / 2 - label8.Height / 2 + label10.Height / 2 + hoehe;

                label7.Left = label8.Left - label7.Width;
                label7.Top = label8.Top;

                label9.Left = label8.Left + label8.Width;
                label9.Top = label8.Top;

                label21.Text = "Ansage:";
                label14.Text = (second15 ? "X " : "") + "15sec";
                label15.Text = (second30 ? "X " : "") + "30sec";
                label16.Text = (minute1 ? "X " : "") + " 1min";
                label17.Text = (minute2 ? "X " : "") + " 2min";
                label18.Text = (minute3 ? "X " : "") + " 3min";
                label19.Text = (minute5 ? "X " : "") + " 5min";
                label20.Text = (minute10 ? "X " : "") + "10min";
                label22.Text = (minute15 ? "X " : "") + "15min";
                label23.Text = (minute20 ? "X " : "") + "20min";
                label24.Text = (minute30 ? "X " : "") + "30min";
                label25.Text = (minute45 ? "X " : "") + "45min";
                label26.Text = (minute60 ? "X " : "") + "60min";

                label14.Left = this.Width - label14.Width - 150;
                label15.Left = this.Width - label15.Width - 150;
                label16.Left = this.Width - label16.Width - 150;
                label17.Left = this.Width - label17.Width - 150;
                label18.Left = this.Width - label18.Width - 150;
                label19.Left = this.Width - label19.Width - 150;
                label20.Left = this.Width - label20.Width - 150;
                label22.Left = this.Width - label22.Width - 150;
                label23.Left = this.Width - label23.Width - 150;
                label24.Left = this.Width - label24.Width - 150;
                label25.Left = this.Width - label25.Width - 150;
                label26.Left = this.Width - label26.Width - 150;
                label21.Left = this.Width - label21.Width - 150;

                int hoehemitte = (this.Height - (this.Height - label12.Top)) / 2;

                label21.Top = label14.Top - label21.Height;
                label14.Top = label15.Top - label14.Height;
                label15.Top = label16.Top - label15.Height;
                label16.Top = label17.Top - label16.Height;
                label17.Top = label18.Top - label17.Height;
                label18.Top = label19.Top - label18.Height;
                label19.Top = label20.Top - label19.Height;
                label20.Top = label22.Top - label20.Height;
                label22.Top = label23.Top - label22.Height;
                label23.Top = label24.Top - label23.Height;
                label24.Top = label25.Top - label24.Height;
                label25.Top = label26.Top - label25.Height;
                label26.Top = hoehemitte + (13 * label26.Height) / 2 - label26.Height;

                label14.Font = uhrover == 4 ? new Font(label14.Font, FontStyle.Underline) : new Font(label14.Font, FontStyle.Regular);
                label15.Font = uhrover == 5 ? new Font(label15.Font, FontStyle.Underline) : new Font(label15.Font, FontStyle.Regular);
                label16.Font = uhrover == 6 ? new Font(label16.Font, FontStyle.Underline) : new Font(label16.Font, FontStyle.Regular);
                label17.Font = uhrover == 7 ? new Font(label17.Font, FontStyle.Underline) : new Font(label17.Font, FontStyle.Regular);
                label18.Font = uhrover == 8 ? new Font(label18.Font, FontStyle.Underline) : new Font(label18.Font, FontStyle.Regular);
                label19.Font = uhrover == 9 ? new Font(label19.Font, FontStyle.Underline) : new Font(label19.Font, FontStyle.Regular);
                label20.Font = uhrover == 10 ? new Font(label20.Font, FontStyle.Underline) : new Font(label20.Font, FontStyle.Regular);
                label22.Font = uhrover == 11 ? new Font(label22.Font, FontStyle.Underline) : new Font(label22.Font, FontStyle.Regular);
                label23.Font = uhrover == 12 ? new Font(label23.Font, FontStyle.Underline) : new Font(label23.Font, FontStyle.Regular);
                label24.Font = uhrover == 13 ? new Font(label24.Font, FontStyle.Underline) : new Font(label24.Font, FontStyle.Regular);
                label25.Font = uhrover == 14 ? new Font(label25.Font, FontStyle.Underline) : new Font(label25.Font, FontStyle.Regular);
                label26.Font = uhrover == 15 ? new Font(label26.Font, FontStyle.Underline) : new Font(label26.Font, FontStyle.Regular);

                label7.Show();
                label8.Show();
                label9.Show();
                label10.Show();

                label14.Refresh();
                label15.Refresh();
                label16.Refresh();
                label17.Refresh();
                label18.Refresh();
                label19.Refresh();
                label20.Refresh();
                label21.Refresh();
                label22.Refresh();
                label23.Refresh();
                label24.Refresh();
                label25.Refresh();
                label26.Refresh();

                label14.Show();
                label15.Show();
                label16.Show();
                label17.Show();
                label18.Show();
                label19.Show();
                label20.Show();
                label21.Show();
                label22.Show();
                label23.Show();
                label24.Show();
                label25.Show();
                label26.Show();
            }
            else
            {
                label7.Hide();
                label8.Hide();
                label9.Hide();
                label10.Hide();

                label14.Hide();
                label15.Hide();
                label16.Hide();
                label17.Hide();
                label18.Hide();
                label19.Hide();
                label20.Hide();
                label21.Hide();
                label22.Hide();
                label23.Hide();
                label24.Hide();
                label25.Hide();
                label26.Hide();
            }

            label5.BringToFront();

            label1.Text = nullen(DateTime.Now.Hour, 2) + ":" + nullen(DateTime.Now.Minute, 2) + ":" + nullen(DateTime.Now.Second, 2) + "\n" +
                 nullen(DateTime.Now.Day, 2) + "." + nullen(DateTime.Now.Month, 2) + "." + nullen(DateTime.Now.Year, 3);
            label1.Show();
            label1.BringToFront();

            focuse();
            #endregion
        }

        private void sekunde_Tick(object sender, EventArgs e)
        {
            if (uhraktiv)
            {
                int zeit = Stunden * 3600 + Minuten * 60 + Sekunden;

                if (second15 && zeit == 15)
                {
                    Sounds.second15.PlaySound(0);
                }
                else
                    if (second30 && zeit == 30)
                    {
                        Sounds.second30.PlaySound(0);
                    }
                    else
                        if (minute1 && zeit == 60)
                        {
                            Sounds.minute1.PlaySound(0);
                        }
                        else
                            if (minute2 && zeit == 120)
                            {
                                Sounds.minute2.PlaySound(0);
                            }
                            else
                                if (minute3 && zeit == 180)
                                {
                                    Sounds.minute3.PlaySound(0);
                                }
                                else
                                    if (minute5 && zeit == 300)
                                    {
                                        Sounds.minute5.PlaySound(0);
                                    }
                                    else
                                        if (minute10 && zeit == 600)
                                        {
                                            Sounds.minute10.PlaySoundAny();
                                        }
                                        else
                                            if (minute15 && zeit == 900)
                                            {
                                                Sounds.minute15.PlaySoundAny();
                                            }
                                            else
                                                if (minute20 && zeit == 1200)
                                                {
                                                    Sounds.minute20.PlaySound(0);
                                                }
                                                else
                                                    if (minute30 && zeit == 1800)
                                                    {
                                                        Sounds.minute30.PlaySound(0);
                                                    }
                                                    else
                                                        if (minute45 && zeit == 2700)
                                                        {
                                                            Sounds.minute45.PlaySound(0);
                                                        }
                                                        else
                                                            if (minute60 && zeit == 3600)
                                                            {
                                                                Sounds.minute60.PlaySound(0);
                                                            }

                if (alarmaktiv || Sounds.second15.IsPlaying(0) || Sounds.second30.IsPlaying(0) || Sounds.minute1.IsPlaying(0) || Sounds.minute2.IsPlaying(0) || Sounds.minute5.IsPlaying(0) || Sounds.minute10.IsPlaying(0) || Sounds.minute20.IsPlaying(0))
                {
                    Mediaplayer.Mediaplayer.settings.volume = 0;
                }
                else
                    Mediaplayer.Mediaplayer.settings.volume = this.MediaVolume;

                if (zeit == 0)
                {
                    // Uhr ist abgelaufen
                    if (uhraktiv)
                    {
                        alarmaktiv = true;
                        //Sounds.Alarm.PlaySound(0);
                        Sounds.Alarm.PlaySound(false, 0, 0.75f, 1f);
                        Mediaplayer.Mediaplayer.settings.volume = 0;
                    }
                }
                else
                {
                    Sekunden--;
                    if (Sekunden < 0) { Minuten--; Sekunden = 59; }
                    if (Minuten < 0) { Stunden--; Minuten = 59; }
                }

                if (alarmaktiv == false && spezialalarm > 0)
                {
                    spezialalarm--;
                }
                else
                    if (alarmaktiv == false && spezialalarm <= 0 && Sounds.Alarm.IsPlaying(0))
                    {
                        Sounds.Alarm.StopSound(0);
                    }

                label5.Show();
            }
            else
            {
                Mediaplayer.Mediaplayer.settings.volume = MediaVolume;
                label5.Hide();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Iexplorer.iexplore.ReadyState == SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
            {
                timer1.Enabled = false;
                timer4.Enabled = true;
            }
            else
                timer1.Enabled = true;

            focuse();
        }

        private void senderzeigen_Tick(object sender, EventArgs e)
        {
            ZeigeSender = false;
            SenderOver = SenderPos;
        }

        private void tveingabe_Tick(object sender, EventArgs e)
        {
            tveingabe.Enabled = false;
            tveingabegestartet = false;
            if (SenderPos != SenderOver)
            {
                SenderPos = SenderOver;
                SenderWechsel(SenderPos);
            }
            SenderAnzeigenStart();
            //  timer3_Tick(null, null);
        }

        private void uhreingabe_Tick(object sender, EventArgs e)
        {
            uhreingabe.Enabled = false;
            uhreingabegestartet = false;

            //   timer3_Tick(null, null);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Enabled = false;
            Iexplorer.iexplore.Navigate("javascript:document.body.style.overflow='hidden'; closeAd();void 0");
        }

        private void radioeingabe_Tick(object sender, EventArgs e)
        {
            radioeingabe.Enabled = false;
            radioeingabegestartet = false;
            if (RSenderPos != RSenderOver)
            {
                RSenderPos = RSenderOver;
                RSenderWechsel(RSenderPos);
            }
            RSenderAnzeigenStart();
            //      timer3_Tick(null, null);
        }

        private void Rsenderanzeigen_Tick(object sender, EventArgs e)
        {
            RZeigeSender = false;
            RSenderOver = RSenderPos;
        }
    }
}
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

        private ControlIE Iexplorer = new ControlIE();

        private ControlIE Browser = new ControlIE();

        private const int SW_RESTORE = 9;

        private int over = 0;
        private int modus = 0;
        private bool clicked = false;

        // TV
        private int SenderPos = 0;

        private int SenderOver = 0;
        private bool ZeigeSender = false;
        private bool tveingabegestartet = false;


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
            /*Iexplorer.ieStart();
            Iexplorer.iexplore.Visible = false;*/
            timer2.Enabled = true;
            timer3.Enabled = true;

            timer3_Tick(null, null);
            

           /* Browser.ieNavigate(true, "www.google.de");
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
            Mediaplayer.Mediaplayer.settings.volume = MediaVolume;*/
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
            /*if (Iexplorer != null)
                if (Iexplorer.launched)
                    Iexplorer.ieClose();

            if (Browser != null)
                if (Browser.launched)
                    Browser.ieClose();

            SetSystemCursor(LoadCursor((IntPtr)null, 32651), 32512);*/
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
            ///if (Mediaplayer.URL != "") Mediaplayer.Hide();
        }

        public void Starte_TV()
        {
            Iexplorer.ieNavigate(true, "http://peer-stream.com/api/embedplayer/?id=viva&e=3&width=100%&height=100%");

            Iexplorer.iexplore.Visible = true;
            timer1.Enabled = true;
            focuse();
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

       
        Fenster Bildschirm = null;

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Fenster.Form = this;
            Fenster.GlobalWidth = this.Width;
            Fenster.GlobalHeight = this.Height;

            Fenster Hauptmenü = new Hauptmenü();
            Fenster LautstärkeFenster = new Lautstärke();
            Fenster Stopuhrfenster = new Stopuhr();
            Fenster StopuhrSignal = new StopuhrSignal();
            Fenster RadioFenster = new Radio();
            Fenster VerlassenFenster = new Verlassen();

            Fenster UhrzeitFenster = new Uhrzeit();
            Hauptmenü.UnterfensterHinzufügen(UhrzeitFenster);
            Hauptmenü.UnterfensterHinzufügen(LautstärkeFenster);
            Fenster StopuhrAnzeigeFenster = new StopuhrAnzeige(-1, Lautstärke.label12);
            Hauptmenü.UnterfensterHinzufügen(StopuhrAnzeigeFenster);
            Hauptmenü.UnterfensterHinzufügen(StopuhrSignal);
            Hauptmenü.UnterfensterHinzufügen(VerlassenFenster);

            Fenster UhrzeitFenster2 = new Uhrzeit(Color.MidnightBlue);
            RadioFenster.UnterfensterHinzufügen(UhrzeitFenster2);
            RadioFenster.UnterfensterHinzufügen(LautstärkeFenster);
            Fenster StopuhrAnzeigeFenster2 = new StopuhrAnzeige(+1, Radio.label6);
            RadioFenster.UnterfensterHinzufügen(StopuhrAnzeigeFenster2);
            RadioFenster.UnterfensterHinzufügen(StopuhrSignal);
            RadioFenster.UnterfensterHinzufügen(VerlassenFenster);

            Fenster UhrzeitFenster3 = new Uhrzeit();
            Stopuhrfenster.UnterfensterHinzufügen(UhrzeitFenster3);
            Stopuhrfenster.UnterfensterHinzufügen(LautstärkeFenster);
            Fenster StopuhrAnzeigeFenster3 = new StopuhrAnzeige(-1, Stopuhr.label10);
            Stopuhrfenster.UnterfensterHinzufügen(StopuhrAnzeigeFenster3);
            Stopuhrfenster.UnterfensterHinzufügen(StopuhrSignal);
            Stopuhrfenster.UnterfensterHinzufügen(VerlassenFenster);

            Bildschirm = new Fenster();
            Bildschirm.BildschirmHinzufügen(Hauptmenü);
            Bildschirm.BildschirmHinzufügen(null);
            Bildschirm.BildschirmHinzufügen(RadioFenster);
            Bildschirm.BildschirmHinzufügen(Stopuhrfenster);
            Bildschirm.Wechseln(0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
          /*  if (Iexplorer.launched)
                Iexplorer.ieClose();*/
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (IsDown)
            {
                DownCounter++;
                if (DownCounter >= 10)
                {
                    mausklick = DownPoint;
                    DownCounter = 10;
                }
            }

            if (Bildschirm != null && mausklick != null)
                Bildschirm.Mouse(mausklick, null);

            if (mausklick != null)
            {
             int w = (int) (Width * 0.125);
             ///   Rectangle E = new Rectangle(0, Height-label12.Height, (int)(Width * 0.25f), label12.Height);

                if (modus == 0)
                { /*  
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
                               /// uhrover = 0;
                                modus = 3;
                            }
                            else
                                if (CheckPoint(mausklick, label11))
                                {
                                    over = 3;
                                    modus = 4;
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
                                            }*/
                }

                mausklick = null;
            }

          /**/

            if (Bildschirm!=null)
            KeyHook.KeyCodes = Bildschirm.Keyboard(KeyHook.KeyCodes, null);

            for (int i = 0; i < KeyHook.KeyCodes.Count; i++)
            {
                /*if (alarmaktiv)
                {
                    alarmaktiv = false;
                    uhraktiv = false;
                    Sounds.Alarm.StopSound(0);
                    continue;
                }*/

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

                                if (modus == 4)
                                {
                                    if (KeyHook.KeyCodes[i] == Keys.F6)
                                    {
                                        Browser.iexplore.Navigate("www.google.de");
                                    }
                                }

                        if (KeyHook.KeyCodes[i] == Keys.Escape)
                        {
                            if (modus == 4)
                            {
                                modus = 0;
                                Browser.iexplore.Visible = false;
                                this.Show();
                                Visible = true;
                                ///if (Mediaplayer.URL != "") Mediaplayer.Show();
                                this.Focus();
                                focuse();
                            }
                            else
                                if (modus == 0)
                                {
                                    /*if (Iexplorer.iexplore.Visible)
                                    {
                                        modus = 1;
                                    }
                                    else
                                        if (Mediaplayer.URL != "")
                                        {
                                            modus = 2;
                                            this.Refresh();
                                        }*/
                                }
                                else
                                {
                                    modus = 0;
                                    TvEingabeStop();
                                   /// RadioEingabeStop();
                                }
                        }
            }

            KeyHook.KeyCodes.Clear();

            #region Zeichnen

     
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
// nichts mehr
            }
            else
            {
               // label13.Hide();
                //label1.BackColor = Color.Transparent;
                // label5.BackColor = Color.Transparent;
                if (!(modus == 1 && ZeigeSender))
                    label6.Hide();
            }

            {
               // nichts mehr
               
            }

            // Hauptmenü
            if (modus == 0)
            {
               // nichts mehr

                /* Iexplorer.iexplore.Visible = false;
                 if (Browser.iexplore!=null)
                 Browser.iexplore.Visible = false;*/
            }

            if (Bildschirm!=null)
                Bildschirm.Draw();

            focuse();
            #endregion
        }

        private void sekunde_Tick(object sender, EventArgs e)
        {
            
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
            /*uhreingabe.Enabled = false;
            uhreingabegestartet = false;*/
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Enabled = false;
            Iexplorer.iexplore.Navigate("javascript:document.body.style.overflow='hidden'; closeAd();void 0");
        }

        private void radioeingabe_Tick(object sender, EventArgs e)
        {
          /*  radioeingabe.Enabled = false;
            radioeingabegestartet = false;
            if (RSenderPos != RSenderOver)
            {
                RSenderPos = RSenderOver;
                RSenderWechsel(RSenderPos);
            }
            RSenderAnzeigenStart();*/
           
        }

        private void Rsenderanzeigen_Tick(object sender, EventArgs e)
        {
           /* RZeigeSender = false;
            RSenderOver = RSenderPos;*/
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
        public int x;
        public int y;
    }
}
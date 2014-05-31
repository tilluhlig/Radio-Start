using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class Uhr : Fenster
    {
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

        public void Draw()
        {
            /*if (uhraktiv)
            {
                if (modus == 0)
                {
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
                label5.Hide();*/

            /*label10.Font = uhrover == 0 ? new Font(label10.Font, FontStyle.Underline) : new Font(label10.Font, FontStyle.Regular);
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
            label26.Show();*/
        }

        public void Init()
        {
            Sounds.Load("C:\\Sounds\\", "DE", "F");
        }

        public void Mouse()
        {
          /*  Rectangle E3 = new Rectangle(0, Height - label12.Height, (int)(Width * 0.25f), label12.Height);
            if (CheckPoint(mausklick, label14))
            {
                uhrover = 4;
                second15 = second15 ? false : true;
            }
            else
                if (CheckPoint(mausklick, label15))
                {
                    uhrover = 5;
                    second30 = second30 ? false : true;
                }
                else
                    if (CheckPoint(mausklick, label16))
                    {
                        uhrover = 6;
                        minute1 = minute1 ? false : true;
                    }
                    else
                        if (CheckPoint(mausklick, label17))
                        {
                            uhrover = 7;
                            minute2 = minute2 ? false : true;
                        }
                        else
                            if (CheckPoint(mausklick, label18))
                            {
                                uhrover = 8;
                                minute3 = minute3 ? false : true;
                            }
                            else
                                if (CheckPoint(mausklick, label19))
                                {
                                    uhrover = 9;
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
                                                if (CheckPoint(mausklick, label24))
                                                {
                                                    uhrover = 13;
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
            if (CheckPoint(mausklick, UpS)) { IStunden++; uhrover = 1; }
            if (CheckPoint(mausklick, UpM)) { IMinuten++; uhrover = 2; }
            if (CheckPoint(mausklick, UpSS)) { ISekunden++; uhrover = 3; }

            if (ISekunden > 59) { ISekunden = 0; IMinuten++; }
            if (IMinuten > 59) { IMinuten = 0; IStunden++; }
            if (IStunden > 99) { IStunden = 99; }

            if (CheckPoint(mausklick, DownS)) { IStunden--; uhrover = 1; }
            if (CheckPoint(mausklick, DownM)) { IMinuten--; uhrover = 2; }
            if (CheckPoint(mausklick, DownSS)) { ISekunden--; uhrover = 3; }

            if (ISekunden < 0) { ISekunden = 0; }
            if (IMinuten < 0) { IMinuten = 0; }
            if (IStunden < 0) { IStunden = 0; }*/
        }

        public void Keyboard()
        {
       /*     if (Taste > -1)
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
                            }*/
        }
    }
}

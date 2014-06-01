using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication2
{
    public class Stopuhr : Fenster
    {
        public Stopuhr()
        {
            Init();
        }

        public static bool uhraktiv = false;

        private bool uhreingabegestartet = false;
        private int uhrover = 0;
        private int ISekunden = 0;
        private int IMinuten = 0;
        private int IStunden = 0;
        // private bool letztenansage = false;
        public static bool alarmaktiv = false;
        public static int spezialalarm = 0;

        public static double UhrEnde = 0;

        public static bool[] Zeitansage = { false, false, false, false, false, false, false, false, false, false, false, false };
        public static String[] Ansagetexte = { "15sec", "30sec", " 1min", " 2min", " 3min", " 5min", "10min", "15min", "20min", "30min", "45min", "60min" };
        public static int[] AnsageSekunde = { 15, 30, 60, 120, 180, 300, 600, 900, 1200, 1800, 2700, 3600 };

        Label[] label24 = new Label[12];
        Label label7 = null;
        Label label8 = null;
        Label label9 = null;
        public static Label label10 = null;
        Label label21 = null;
        override public void Init()
        {
            if (Sounds.Alarm == null)
                Sounds.Load("C:\\Sounds\\", "DE", "F");

            if (this.label7 == null)
            {
                this.label7 = new System.Windows.Forms.Label();
                this.label7.AutoSize = true;
                this.label7.BackColor = System.Drawing.Color.Transparent;
                this.label7.Font = new System.Drawing.Font("!PaulMaul", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label7.ForeColor = System.Drawing.Color.Orange;
                this.label7.Location = new System.Drawing.Point(219, 204);
                this.label7.Name = "label7";
                this.label7.Size = new System.Drawing.Size(98, 83);
                this.label7.TabIndex = 6;
                this.label7.Text = "00:";
                this.label7.Visible = false;
                Fenster.Form.Controls.Add(this.label7);
            }

            if (this.label8 == null)
            {
                this.label8 = new System.Windows.Forms.Label();
                this.label8.AutoSize = true;
                this.label8.BackColor = System.Drawing.Color.Transparent;
                this.label8.Font = new System.Drawing.Font("!PaulMaul", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label8.ForeColor = System.Drawing.Color.Orange;
                this.label8.Location = new System.Drawing.Point(330, 204);
                this.label8.Name = "label8";
                this.label8.Size = new System.Drawing.Size(98, 83);
                this.label8.TabIndex = 7;
                this.label8.Text = "00:";
                this.label8.Visible = false;
                Fenster.Form.Controls.Add(this.label8);
            }

            if (this.label9 == null)
            {
                this.label9 = new System.Windows.Forms.Label();
                this.label9.AutoSize = true;
                this.label9.BackColor = System.Drawing.Color.Transparent;
                this.label9.Font = new System.Drawing.Font("!PaulMaul", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label9.ForeColor = System.Drawing.Color.Orange;
                this.label9.Location = new System.Drawing.Point(412, 204);
                this.label9.Name = "label9";
                this.label9.Size = new System.Drawing.Size(91, 83);
                this.label9.TabIndex = 8;
                this.label9.Text = "00";
                this.label9.Visible = false;
                Fenster.Form.Controls.Add(this.label9);
            }

            if (label10 == null)
            {
                label10 = new System.Windows.Forms.Label();
                label10.AutoSize = true;
                label10.BackColor = System.Drawing.Color.Transparent;
                label10.Font = new System.Drawing.Font("!PaulMaul", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label10.ForeColor = System.Drawing.Color.Orange;
                label10.Location = new System.Drawing.Point(241, 44);
                label10.Name = "label10";
                label10.Size = new System.Drawing.Size(299, 100);
                label10.TabIndex = 9;
                label10.Text = "Aktivieren";
                label10.Visible = false;
                Fenster.Form.Controls.Add(label10);
            }

            if (this.label21 == null)
            {
                this.label21 = new System.Windows.Forms.Label();
                this.label21.AutoSize = true;
                this.label21.BackColor = System.Drawing.Color.Transparent;
                this.label21.Font = new System.Drawing.Font("!PaulMaul", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label21.ForeColor = System.Drawing.Color.OrangeRed;
                this.label21.Location = new System.Drawing.Point(661, 125);
                this.label21.Name = "label21";
                this.label21.Size = new System.Drawing.Size(123, 59);
                this.label21.TabIndex = 20;
                this.label21.Text = "20min";
                this.label21.Visible = false;
                Fenster.Form.Controls.Add(this.label21);
            }

            for (int i = 0; i < label24.Length; i++)
            {
                this.label24[i] = new System.Windows.Forms.Label();
                this.label24[i].AutoSize = true;
                this.label24[i].BackColor = System.Drawing.Color.Transparent;
                this.label24[i].Font = new System.Drawing.Font("!PaulMaul", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label24[i].ForeColor = System.Drawing.Color.Orange;
                this.label24[i].Location = new System.Drawing.Point(678, 429);
                this.label24[i].Name = "label24";
                this.label24[i].Size = new System.Drawing.Size(118, 59);
                this.label24[i].TabIndex = 23;
                this.label24[i].Text = "20min";
                this.label24[i].Visible = false;
                Fenster.Form.Controls.Add(this.label24[i]);
            }
        }

        private void uhrEingabeStart()
        {
            ///   uhreingabe.Stop();
            ///   uhreingabe.Start();
            uhreingabegestartet = true;
        }

        private void uhrEingabeStop()
        {
            ///   uhreingabe.Enabled = false;
            uhreingabegestartet = false;
        }

        override public void Show()
        {
            for (int i = 0; i < label24.Length; i++)
            {
                this.label24[i].Show();
                this.label24[i].Refresh();
            }
            label7.Show();
            label8.Show();
            label9.Show();
            label10.Show();
            label21.Show();
            label7.Refresh();
            label8.Refresh();
            label9.Refresh();
            label10.Refresh();
            label21.Refresh();
        }

        override public void Hide()
        {
            for (int i = 0; i < label24.Length; i++)
            {
                this.label24[i].Hide();
                this.label24[i].Refresh();
            }
            label7.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            label21.Hide();
            label7.Refresh();
            label8.Refresh();
            label9.Refresh();
            label10.Refresh();
            label21.Refresh();
        }

        override public void Draw()
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

            for (int i = 0; i < label24.Length; i++)
            {
                label24[i].Text = (Zeitansage[i] ? "X " : "") + Ansagetexte[i];
                label24[i].Left = this.Width - label24[i].Width - 150;
            }
            label21.Left = this.Width - label21.Width - 150;

            int hoehemitte = (this.Height - (this.Height - Lautstärke.label12.Top)) / 2;

            for (int i = label24.Length - 1; i >= 0; i--)
                label24[i].Top = hoehemitte + (13 * label24[0].Height) / 2 - label24[0].Height * (label24.Length - i);

            label21.Top = label24[0].Top - label21.Height;

            for (int i = 0; i < label24.Length; i++)
                label24[i].Font = uhrover == (4 + i) ? new Font(label24[i].Font, FontStyle.Underline) : new Font(label24[i].Font, FontStyle.Regular);
        }

        override public POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            if (mausklick == null) return null;

            for (int i = 0; i < label24.Length; i++)
            {
                if (CheckPoint(mausklick, label24[i]))
                {
                    uhrover = 4 + i;
                    Zeitansage[i] = Zeitansage[i] ? false : true;
                    return null;
                }
            }

            if (CheckPoint(mausklick, label10))
            {

                if (!uhraktiv)
                {
                    UhrEnde = IStunden * 3600 + IMinuten * 60 + ISekunden + (new TimeSpan(DateTime.Now.Ticks)).TotalSeconds;
                    uhraktiv = true;
                    spezialalarm = 1;
                    Sounds.Alarm.PlaySound(false, 0, 5f, 1f);
                }
                else
                    uhraktiv = false;
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
            if (IStunden < 0) { IStunden = 0; }
            return mausklick;
        }

        override public List<Keys> Keyboard(List<Keys> KeyCodes, Fenster Hauptfenster)
        {
            List<Keys> Result = new List<Keys>();
            for (int i = 0; i < KeyCodes.Count; i++)
            {
                int Taste = -1;
                if (KeyCodes[i] == Keys.D0) Taste = 0;
                if (KeyCodes[i] == Keys.D1) Taste = 1;
                if (KeyCodes[i] == Keys.D2) Taste = 2;
                if (KeyCodes[i] == Keys.D3) Taste = 3;
                if (KeyCodes[i] == Keys.D4) Taste = 4;
                if (KeyCodes[i] == Keys.D5) Taste = 5;
                if (KeyCodes[i] == Keys.D6) Taste = 6;
                if (KeyCodes[i] == Keys.D7) Taste = 7;
                if (KeyCodes[i] == Keys.D8) Taste = 8;
                if (KeyCodes[i] == Keys.D9) Taste = 9;

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
                }
                else
                    if (KeyCodes[i] == Keys.Left)
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
                    }
                    else
                        if (KeyCodes[i] == Keys.Right)
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
                        }
                        else
                            if (KeyCodes[i] == Keys.Up)
                            {
                                if (uhrover == 0 || uhrover > 3)
                                {
                                    uhrover--; if (uhrover < 0) uhrover = 15;
                                    uhrEingabeStop();
                                }

                                if (uhrover == 1) IStunden++;
                                if (uhrover == 2) IMinuten++;
                                if (uhrover == 3) ISekunden++;

                                if (ISekunden > 59) { ISekunden = 0; IMinuten++; }
                                if (IMinuten > 59) { IMinuten = 0; IStunden++; }
                                if (IStunden > 99) { IStunden = 99; }
                                uhrEingabeStop();
                            }
                            else
                                if (KeyCodes[i] == Keys.Down)
                                {
                                    if (uhrover == 0 || uhrover > 3)
                                    {
                                        uhrover++; if (uhrover >= 16) uhrover = 0;
                                        uhrEingabeStop();
                                    }

                                    if (uhrover == 1) IStunden--;
                                    if (uhrover == 2) IMinuten--;
                                    if (uhrover == 3) ISekunden--;

                                    if (ISekunden < 0) { ISekunden = 59; IMinuten--; }
                                    if (IMinuten < 0) { IMinuten = 59; IStunden--; }
                                    if (IStunden < 0) { IStunden = 0; }
                                    uhrEingabeStop();
                                }
                                else
                                    if (KeyCodes[i] == Keys.Space)
                                    {
                                        for (int c = 0; c < Zeitansage.Length; c++)
                                        {
                                            if (uhrover == 4 + i)
                                                Zeitansage[c] = Zeitansage[c] ? false : true;
                                        }

                                        if (uhreingabegestartet)
                                        {
                                            uhrEingabeStop();
                                        }
                                        else
                                            if (uhrover <= 3)
                                            {
                                                if (!uhraktiv)
                                                {
                                                    UhrEnde = IStunden * 3600 + IMinuten * 60 + ISekunden + (new TimeSpan(DateTime.Now.Ticks)).TotalSeconds;
                                                    uhraktiv = true;
                                                    spezialalarm = 1;
                                                    Sounds.Alarm.PlaySound(false, 0, 5f, 1f);
                                                }
                                                else
                                                    uhraktiv = false;
                                            }

                                    }
                                    else
                                        Result.Add(KeyCodes[i]);
            }
            return Result;
        }
    }
}

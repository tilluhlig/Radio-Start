using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TL.AutoIE;
using WMPLib;

namespace WindowsFormsApplication2
{
    public class Radio : Fenster
    {
        public static Label label13 = null;
        private blue.Form1 Mediaplayer = null;
        // private int MediaVolume = (int)((float)int.MaxValue * 0.1f);
        private int MediaVolume = 100;

        private int RSenderPos = 0;

        private int RSenderOver = 0;
        private bool RZeigeSender = false;
        private bool radioeingabegestartet = false;
        private int restartwait = 0;

        public void Starte_Radio(int id)
        {
            String zo = RadioA[id];
            Mediaplayer.Starte(RadioA[id]);
            Fenster.Form.focuse();
        }

        public void RSenderWechsel(int id)
        {
            Starte_Radio(id);
        }

        private void RadioEingabeStart()
        {
           /// radioeingabe.Stop();
           /// radioeingabe.Start();
            radioeingabegestartet = true;
        }

        private void RadioEingabeStop()
        {
           /// radioeingabe.Stop();
            radioeingabegestartet = false;
        }

        private void RSenderAnzeigenStart()
        {
           /// Rsenderzeigen.Stop();
           /// Rsenderzeigen.Start();
            RZeigeSender = true;
        }

        private String[] RadioS = { "Top 100 Station", "Radio PSR", "ENERGY Sachsen", "MDR Sputnik", "MDR Jump", "SAW", "MDR1 Radio Sachsen Anhalt", "89.0 RTL", "Radio Brocken", "saw 80er", "1LIVE" };
        private String[] RadioA = { "http://87.230.101.49:80", "http://streams.radiopsr.de/psr-live/mp3-128/wwwradiode", "http://radio.nrj.net/sachsen/radiode", "http://c22033-l.i.core.cdn.streamfarm.net/22005mdrsputnik/live/3087mdr_sputnik/live_de_128.mp3", "http://c22033-l.i.core.cdn.streamfarm.net/22004mdrjump/live/3087mdr_jump/live_de_128.mp3", "http://stream.radiosaw.de/saw/mp3-128/radiode/", "http://c22033-l.i.core.cdn.streamfarm.net/22002mdr1sachsenanhalt/live/3087mdr_mdr1sa/live_de_128.mp3", "http://80.237.158.40/890rtl-128.mp3", "http://sites.radiobrocken.de/streams/mp3_128k.m3u", "http://stream.hoerradar.de/saw80er-128", "http://1live.akacast.akamaistream.net/7/706/119434/v1/gnl.akacast.akamaistream.net/1live" };


        override public void Init()
        {
            label13 = new System.Windows.Forms.Label();
            label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label13.AutoSize = true;
            label13.BackColor = System.Drawing.Color.MidnightBlue;
            label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            label13.Font = new System.Drawing.Font("Gabriola", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label13.ForeColor = System.Drawing.Color.Orange;
            label13.Location = new System.Drawing.Point(548, 411);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(71, 70);
            label13.TabIndex = 12;
            label13.Text = "////";
            label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label13.Visible = false;

            Fenster.Form.Controls.Add(label13);

            Mediaplayer = new blue.Form1();
            Mediaplayer.Mediaplayer.settings.volume = MediaVolume;
        }

        override public void Show()
        {
            label13.Show();
            label13.Refresh();
        }

        override public void Hide()
        {
            label13.Hide();
            label13.Refresh();
        }

        override public void Draw()
        {
            if (restartwait == 0)
            {
                if ((Mediaplayer.Mediaplayer.status == "Bereit" || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsLast || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsMediaEnded || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsUndefined || Mediaplayer.Mediaplayer.playState == WMPPlayState.wmppsStopped || Mediaplayer.Mediaplayer.isOnline == false || Mediaplayer.Mediaplayer.openState == WMPOpenState.wmposUndefined))
                {
                    Mediaplayer.Stop();
                    RSenderWechsel(RSenderPos);
                    restartwait = 50;
                }
            }
            else
                restartwait--;

            /* label6.Text = ("").PadRight(30, ' ') + (RSenderOver).ToString() + "   " + RadioS[RSenderOver] + ("").PadRight(30, ' ');
  label6.Top = 0;//this.Height / 4 - label6.Height
  label6.Left = this.Width / 2 - label6.Width / 2;
  label6.BringToFront();
  label6.Show();
  label1.BackColor = Color.MidnightBlue; */
            label13.Text = ("").PadLeft(120, ' ') + Mediaplayer.Mediaplayer.status + ("").PadLeft(120, ' ');
  label13.Top = this.Height - label13.Height - Lautstärke.label12.Height;
  label13.Left = this.Width / 2 - label13.Width / 2;
  label13.Show();
  label13.Refresh();

        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            return mausklick;
        }

        override public List<Keys> Keyboard(List<Keys> Keys, Fenster Hauptfenster)
        {
            return Keys;
           /* if (Taste > -1)
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
                                }*/
        }
    }
}

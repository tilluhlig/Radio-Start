using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    internal class StopuhrAnzeige : Fenster
    {
        public static Label label5 = null;

        override public void Init()
        {
            if (label5 == null)
            {
                label5 = new System.Windows.Forms.Label();
                label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                label5.AutoSize = true;
                label5.BackColor = System.Drawing.Color.MidnightBlue;
                label5.Font = new System.Drawing.Font("Algerian", 69.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label5.ForeColor = System.Drawing.Color.Yellow;
                label5.Location = new System.Drawing.Point(506, 2);
                label5.Name = "label5";
                label5.Size = new System.Drawing.Size(290, 104);
                label5.TabIndex = 4;
                label5.Text = "00:00";
                label5.Visible = false;
                Fenster.Form.Controls.Add(label5);
            }
        }

        public StopuhrAnzeige()
        {
            Init();
        }

        private Label AbstandsLabel = null;
        private int RichtungsFaktor = 0;

        public StopuhrAnzeige(int _RichtungsFaktor, Label _AbstandsLabel)
        {
            Init();
            RichtungsFaktor = _RichtungsFaktor;
            AbstandsLabel = _AbstandsLabel;
        }

        override public void Show()
        {
            label5.Show();
            label5.Refresh();
        }

        override public void Hide()
        {
            label5.Hide();
            label5.Refresh();
        }

        override public void Draw()
        {
            if (Stopuhr.uhraktiv)
            {
                double Zeit = Stopuhr.UhrEnde - (new TimeSpan(DateTime.Now.Ticks)).TotalSeconds;
                int Stunden = (int)Zeit / 3600; Zeit -= Stunden * 3600;
                int Minuten = (int)Zeit / 60; Zeit -= Minuten * 60;
                int Sekunden = (int)Zeit;

                label5.Text = ("").PadRight(30, ' ') + nullen(Stunden, 2) + ":" + nullen(Minuten, 2) + ":" + nullen(Sekunden, 2) + ("").PadRight(30, ' ');
                if (AbstandsLabel == null)
                {
                    label5.Top = this.Height / 2 - label5.Height / 2;
                }
                else
                {
                    if (RichtungsFaktor == 1)
                    {
                        label5.Top = AbstandsLabel.Top + AbstandsLabel.Height;
                    }
                    else
                        if (RichtungsFaktor == -1)
                        {
                            label5.Top = AbstandsLabel.Top - label5.Height;
                        }
                }

                label5.Left = this.Width / 2 - label5.Width / 2;

                if (Zeit > 0)
                {
                    label5.Show();
                    label5.BringToFront();
                }
                else
                    label5.Hide();
            }
            else
                label5.Hide();
        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            // NICHTS
            return mausklick;
        }

        public override List<Keys> Keyboard(List<Keys> Keys, Fenster Hauptfenster)
        {
            // NICHTS
            return Keys;
        }
    }
}
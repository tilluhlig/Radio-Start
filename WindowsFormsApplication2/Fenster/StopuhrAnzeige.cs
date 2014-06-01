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
    class StopuhrAnzeige : Fenster
    {
        Label label5 = null;
        override public void Init()
        {
            this.label5 = new System.Windows.Forms.Label();
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.MidnightBlue;
            this.label5.Font = new System.Drawing.Font("Algerian", 69.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(506, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(290, 104);
            this.label5.TabIndex = 4;
            this.label5.Text = "00:00";
            this.label5.Visible = false;

            Fenster.Form.Controls.Add(this.label5);
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
                TimeSpan elapsedSpan = new TimeSpan(DateTime.Now.Ticks);
                double Zeit = Stopuhr.UhrEnde - elapsedSpan.TotalSeconds;
                int Stunden = (int)Zeit / 3600; Zeit -= Stunden * 3600;
                int Minuten = (int)Zeit / 60; Zeit -= Minuten * 60;
                int Sekunden = (int)Zeit;

                label5.Text = ("").PadRight(30, ' ') + nullen(Stunden, 2) + ":" + nullen(Minuten, 2) + ":" + nullen(Sekunden, 2) + ("").PadRight(30, ' ');
                label5.Top = this.Height / 2;
                label5.Left = this.Width / 2 - label5.Width / 2;
                label5.Show();

                /*if (modus == 0)
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
                            label5.Hide();*/
            }
            else
                label5.Hide();
        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            return mausklick;
        }

        public override List<Keys> Keyboard(List<Keys> Keys, Fenster Hauptfenster)
        {
            return Keys;
        }
    }
}

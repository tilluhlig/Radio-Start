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
    class Uhrzeit : Fenster
    {
        public static Label label1 = null;
        override public void Init()
        {
            label1 = new System.Windows.Forms.Label();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Algerian", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.Yellow;
            label1.Location = new System.Drawing.Point(0, 2);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(170, 82);
            label1.TabIndex = 0;
            label1.Text = "00:00\r\n00.00.00";
            label1.Visible = false;

            Fenster.Form.Controls.Add(label1);
        }

        override public void Show()
        {
            label1.Show();
            label1.BringToFront();
            label1.Refresh();
        }

        override public void Hide()
        {
            label1.Hide();
            label1.Refresh();
        }

        override public void Draw()
        {
            String []time = DateTime.Now.ToString().Split(' ');
           // TimeSpan elapsedSpan = new TimeSpan(DateTime.Now.Ticks);
           // label1.Text = elapsedSpan.TotalSeconds.ToString();

            label1.Text = time[1]+"\n"+time[0];
            label1.Show();
            label1.BringToFront();
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

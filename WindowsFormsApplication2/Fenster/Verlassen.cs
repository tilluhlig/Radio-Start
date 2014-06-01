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
    class Verlassen : Fenster
    {
        public Verlassen()
        {
            Init();
        }

        public Verlassen(int _WechselZiel)
        {
            WechselZiel = _WechselZiel;
            Init();
        }

        public int WechselZiel = 0;
        public static Label label1 = null;
        override public void Init()
        {
            if (label1 == null)
            {
                label1 = new System.Windows.Forms.Label();
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
        }

        override public void Show()
        {
            // NICHTS
        }

        override public void Hide()
        {
            // NICHTS
        }

        override public void Draw()
        {
            // NICHTS
        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            return mausklick;
        }

        public override List<Keys> Keyboard(List<Keys> KeyCodes, Fenster Hauptfenster)
        {
            List<Keys> Result = new List<Keys>();
            for (int i = 0; i < KeyCodes.Count; i++)
            {
                if (KeyCodes[i] == Keys.Escape)
                {
                    Hauptfenster.Wechseln(WechselZiel);
                }
                else
                    Result.Add(KeyCodes[i]);
            }
            return Result;
        }
    }
}

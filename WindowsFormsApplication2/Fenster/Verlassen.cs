using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    internal class Verlassen : Fenster
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
                label1.Font = new System.Drawing.Font("Algerian", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label1.ForeColor = System.Drawing.Color.Red;// //
                label1.Location = new System.Drawing.Point(0, 2);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(170, 82);
                label1.TabIndex = 0;
                label1.Text = "<<<";
                label1.Visible = false;
                Fenster.Form.Controls.Add(label1);
            }
        }

        override public void Show()
        {
            label1.Show();
            label1.Refresh();
        }

        override public void Hide()
        {
            label1.Hide();
            label1.Refresh();
        }

        override public void Draw()
        {
            label1.Left = 0;
            label1.Top = Height - label1.Height;
        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            if (mausklick == null) return null;

            if (CheckPoint(mausklick, label1))
            {
                Hauptfenster.Wechseln(WechselZiel);
                return null;
            }
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
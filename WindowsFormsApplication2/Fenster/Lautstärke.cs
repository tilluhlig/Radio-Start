using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class Lautstärke : Fenster
    {
        public Lautstärke()
        {
            Init();
        }

        public static Label label12 = null;

        override public void Init()
        {
            if (label12 == null)
            {
                label12 = new System.Windows.Forms.Label();
                label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                label12.AutoSize = true;
                label12.BackColor = System.Drawing.Color.Transparent;
                label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                label12.Font = new System.Drawing.Font("Andy", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label12.ForeColor = System.Drawing.Color.Orange;
                label12.Location = new System.Drawing.Point(394, 426);
                label12.Name = "label12";
                label12.Size = new System.Drawing.Size(80, 44);
                label12.TabIndex = 11;
                label12.Text = "////";
                label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label12.Visible = false;
                Fenster.Form.Controls.Add(label12);
            }
        }

        override public void Show()
        {
            label12.Show();
            label12.Refresh();
        }

        override public void Hide()
        {
            label12.Hide();
            label12.Refresh();
        }

        override public void Draw()
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
        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            Rectangle D = new Rectangle(Width - label12.Width / 2, label12.Top, label12.Width / 2, label12.Height);
            Rectangle C = new Rectangle(Width - label12.Width, label12.Top, label12.Width / 2, label12.Height);
            if (Fenster.CheckPoint(mausklick, C))
            {
                int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                int max = ushort.MaxValue;
                int steps = max / 15;
                if (CalcVol < max) CalcVol += steps;
                if (CalcVol > max) CalcVol = max;
                PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
            }
            else
                if (Fenster.CheckPoint(mausklick, D))
                {
                    int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                    int max = ushort.MaxValue;
                    int steps = max / 15;
                    if (CalcVol > 0) CalcVol -= steps;
                    if (CalcVol < 0) CalcVol = 0;
                    PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
                }
                else
                    return mausklick;

            return null;
        }

        public override List<Keys> Keyboard(List<Keys> KeyCodes, Fenster Hauptfenster)
        {
            List<Keys> Result = new List<Keys>();
            for (int i = 0; i < KeyCodes.Count; i++)
            {
                if (KeyCodes[i] == Keys.VolumeUp)
                {
                    int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                    int max = ushort.MaxValue;
                    int steps = max / 15;
                    if (CalcVol < max) CalcVol += steps;
                    if (CalcVol > max) CalcVol = max;
                    PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
                }
                else
                    if (KeyCodes[i] == Keys.VolumeDown)
                    {
                        int CalcVol = PC_VolumeControl.VolumeControl.GetVolume();
                        int max = ushort.MaxValue;
                        int steps = max / 15;
                        if (CalcVol > 0) CalcVol -= steps;
                        if (CalcVol < 0) CalcVol = 0;
                        PC_VolumeControl.VolumeControl.SetVolume(CalcVol);
                    }
                    else
                        Result.Add(KeyCodes[i]);
            }
            return Result;
        }
    }
}
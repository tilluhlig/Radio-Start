using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class Hauptmenü : Fenster
    {
        public Hauptmenü()
        {
            Init();
        }

        private Label label2 = null;
        private Label label3 = null;
        private Label label4 = null;
        private Label label11 = null;
        private int over = 0;

        override public void Init()
        {
            if (this.label2 == null)
            {
                this.label2 = new System.Windows.Forms.Label();
                this.label2.AutoSize = true;
                this.label2.BackColor = System.Drawing.Color.Transparent;
                this.label2.Font = new System.Drawing.Font("!PaulMaul", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label2.ForeColor = System.Drawing.Color.Orange;
                this.label2.Location = new System.Drawing.Point(54, 222);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(150, 120);
                this.label2.TabIndex = 1;
                this.label2.Text = "TV";
                this.label2.Visible = false;
                Fenster.Form.Controls.Add(this.label2);
            }

            if (this.label3 == null)
            {
                this.label3 = new System.Windows.Forms.Label();
                this.label3.AutoSize = true;
                this.label3.BackColor = System.Drawing.Color.Transparent;
                this.label3.Font = new System.Drawing.Font("!PaulMaul", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label3.ForeColor = System.Drawing.Color.Orange;
                this.label3.Location = new System.Drawing.Point(282, 222);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(204, 120);
                this.label3.TabIndex = 2;
                this.label3.Text = "Radio";
                this.label3.Visible = false;
                Fenster.Form.Controls.Add(this.label3);
            }

            if (this.label4 == null)
            {
                this.label4 = new System.Windows.Forms.Label();
                this.label4.AutoSize = true;
                this.label4.BackColor = System.Drawing.Color.Transparent;
                this.label4.Font = new System.Drawing.Font("!PaulMaul", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label4.ForeColor = System.Drawing.Color.Orange;
                this.label4.Location = new System.Drawing.Point(567, 222);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(155, 120);
                this.label4.TabIndex = 3;
                this.label4.Text = "Uhr";
                this.label4.Visible = false;
                Fenster.Form.Controls.Add(this.label4);
            }

            if (this.label11 == null)
            {
                this.label11 = new System.Windows.Forms.Label();
                this.label11.AutoSize = true;
                this.label11.BackColor = System.Drawing.Color.Transparent;
                this.label11.Font = new System.Drawing.Font("!PaulMaul", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label11.ForeColor = System.Drawing.Color.Orange;
                this.label11.Location = new System.Drawing.Point(54, 368);
                this.label11.Name = "label11";
                this.label11.Size = new System.Drawing.Size(308, 120);
                this.label11.TabIndex = 10;
                this.label11.Text = "Browser";
                this.label11.Visible = false;
                Fenster.Form.Controls.Add(this.label11);
            }
        }

        override public void Show()
        {
            label2.Show();
            label3.Show();
            label4.Show();
            label11.Show();

            label2.BringToFront();
            label3.BringToFront();
            label4.BringToFront();
            label11.BringToFront();

            label2.Refresh();
            label3.Refresh();
            label4.Refresh();
            label11.Refresh();
        }

        override public void Hide()
        {
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label11.Hide();

            label2.Refresh();
            label3.Refresh();
            label4.Refresh();
            label11.Refresh();
        }

        override public void Draw()
        {
            label2.Font = over == 0 ? new Font(label2.Font, FontStyle.Underline) : new Font(label2.Font, FontStyle.Regular);
            label3.Font = over == 1 ? new Font(label3.Font, FontStyle.Underline) : new Font(label3.Font, FontStyle.Regular);
            label4.Font = over == 2 ? new Font(label4.Font, FontStyle.Underline) : new Font(label4.Font, FontStyle.Regular);
            label11.Font = over == 3 ? new Font(label11.Font, FontStyle.Underline) : new Font(label11.Font, FontStyle.Regular);

            label3.Text = Radio.Mediaplayer.URL != "" ? "Radio aus" : "Radio";
            ///label2.Text = Iexplorer.iexplore.Visible ? "TV aus" : "TV";

            int mitte = this.Width / 2;
            label3.Left = Position.X + mitte - label3.Width / 2;

            int mitte2 = label3.Left / 2;
            label2.Left = Position.X + mitte2 - label2.Width / 2;

            int mitte3 = label3.Left + label3.Width + (this.Width - (label3.Left + label3.Width)) / 2;
            label4.Left = Position.X + mitte3 - label4.Width / 2;

            mitte2 = label3.Left / 2;
            label11.Left = Position.X + mitte2 - label11.Width / 2;
            label11.Top = Position.Y + label2.Top + Height / 4;
        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            if (CheckPoint(mausklick, label2))
            {
                over = 0;
                Hauptfenster.Wechseln(1);
            }
            else
                if (CheckPoint(mausklick, label3))
                {
                    if (Radio.Mediaplayer.URL != "")
                    {
                        Radio.Mediaplayer.Stop();
                    }
                    else
                    {
                        over = 1;
                        Hauptfenster.Wechseln(2);
                        // Musik aufrufen
                        /* if (Iexplorer.iexplore.Visible)
                         {
                             Iexplorer.ieClose();
                             Iexplorer.ieStart();
                             Iexplorer.iexplore.Visible = false;
                         }*/
                    }
                }
                else
                    if (CheckPoint(mausklick, label4))
                    {
                        over = 2;
                        // Uhr aufrufen
                        /// uhrover = 0;
                        Hauptfenster.Wechseln(3);
                    }
                    else
                        if (CheckPoint(mausklick, label11))
                        {
                            over = 3;
                            Hauptfenster.Wechseln(4);
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
                if (KeyCodes[i] == Keys.Up)
                {
                    if (over == 3) { over = 0; }
                    else
                        over = 3;
                }
                else
                    if (KeyCodes[i] == Keys.Down)
                    {
                        if (over == 3) { over = 0; }
                        else
                            over = 3;
                    }
                    else
                        if (KeyCodes[i] == Keys.Right)
                        {
                            over = (over + 1) % 4;
                        }
                        else
                            if (KeyCodes[i] == Keys.Left)
                            {
                                over--; if (over < 0) over = 3;
                            }
                            else
                                if (KeyCodes[i] == Keys.Space)
                                {
                                    /*if (over == 0 && Iexplorer.iexplore.Visible)
                                    {
                                        Iexplorer.ieClose();
                                        Iexplorer.ieStart();
                                        Iexplorer.iexplore.FullScreen = true;
                                        Iexplorer.iexplore.Visible = false;
                                    }
                                    else*/
                                    if (over == 1 && Radio.Mediaplayer.URL != "")
                                    {
                                        Radio.Mediaplayer.Stop();
                                    }
                                    else
                                    {
                                        // modus wechseln
                                        Hauptfenster.Wechseln(over + 1);
                                        /* modus = over + 1;

                                         if (modus == 1)
                                         {
                                             // TV aufrufen
                                             Mediaplayer.Stop();

                                             if (!Iexplorer.iexplore.Visible)
                                             {
                                                 Starte_TV();
                                             }
                                             SenderAnzeigenStart();
                                         }
                                         else
                                             if (modus == 2)
                                             {
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
                                             else
                                                 if (modus == 3)
                                                 {
                                                     // Uhr aufrufen
                                                     ///   uhrover = 0;
                                                 }
                                                 else
                                                     if (modus == 4)
                                                     {
                                                         Starte_Browser();
                                                     }*/
                                    }
                                }
                                else
                                    Result.Add(KeyCodes[i]);
            }

            return Result;
        }
    }
}
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
    public class Fenster
    {
        public int Modus = 0;
        public List<Fenster> Bildschirme = new List<Fenster>();
        public List<Fenster> Unterfenster = new List<Fenster>();
        public static Form1 Form;
        public static int GlobalWidth = 0;
        public static int GlobalHeight = 0;

        public void Wechseln(int FensterId)
        {
            if (FensterId < 0 || FensterId >= Bildschirme.Count || Bildschirme[Modus]==null) return;

            if (FensterId != Modus)
            {
                Bildschirme[Modus].Hide();
                for (int i = 0; i < Bildschirme[Modus].Unterfenster.Count; i++)
                    Bildschirme[Modus].Unterfenster[i].Hide();
            }

            Modus = FensterId;
            Bildschirme[Modus].Show();
            for (int i = 0; i < Bildschirme[Modus].Unterfenster.Count; i++)
                Bildschirme[Modus].Unterfenster[i].Show();
        }

        public void BildschirmHinzufügen(Fenster neuesFenster)
        {
            if (neuesFenster != null)
            {
                neuesFenster.Width = Fenster.GlobalWidth;
                neuesFenster.Height = Fenster.GlobalHeight;
            }
            Bildschirme.Add(neuesFenster);
            if (neuesFenster != null)
            {
                neuesFenster.Id = Bildschirme.Count - 1;
                neuesFenster.Init();
            }
        }

        public void UnterfensterHinzufügen(Fenster neuesFenster)
        {
            neuesFenster.Width = Fenster.GlobalWidth;
            neuesFenster.Height = Fenster.GlobalHeight;
            Unterfenster.Add(neuesFenster);
            neuesFenster.Id = Unterfenster.Count - 1;
            neuesFenster.Init();
        }

        public int Id = 0;
        public int Width = 0;
        public int Height = 0;
        public Point Position = Point.Empty;

        public virtual void Init()
        {

        }

        public virtual void Show()
        {

        }

        public virtual void Draw()
        {
            if (Modus < Bildschirme.Count && Modus >= 0)
            {
                Bildschirme[Modus].Draw();
                for (int i = 0; i < Bildschirme[Modus].Unterfenster.Count; i++)
                    Bildschirme[Modus].Unterfenster[i].Draw();
            }
        }

        public virtual List<Keys> Keyboard(List<Keys> Keys, Fenster Hauptfenster)
        {
            if (Modus < Bildschirme.Count && Modus >= 0)
            {
                Keys = Bildschirme[Modus].Keyboard(Keys,this);
                for (int i = 0; i < Bildschirme[Modus].Unterfenster.Count; i++)
                    Keys = Bildschirme[Modus].Unterfenster[i].Keyboard(Keys,this);
            }
            return Keys;
        }

        public virtual POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            if (Modus < Bildschirme.Count && Modus >= 0)
            {
                mausklick = Bildschirme[Modus].Mouse(mausklick, this);
                for (int i = 0; i < Bildschirme[Modus].Unterfenster.Count && mausklick != null; i++)
                    mausklick = Bildschirme[Modus].Unterfenster[i].Mouse(mausklick, this);
            }

            return mausklick;
        }

        public virtual void Hide()
        {

        }

        public static String nullen(int Text, int stellen)
        {
            return nullen(Text.ToString(), stellen);
        }

        public static String nullen(String Text, int stellen)
        {
            return Text.PadLeft(stellen, '0');
        }

        public static bool CheckPoint(POINT P, Label rect)
        {
            if (P.x >= rect.Location.X && P.x <= rect.Location.X + rect.Width && P.y >= rect.Location.Y && P.y <= rect.Location.Y + rect.Height)
            {
                return true;
            }

            return false;
        }

        public static bool CheckPoint(POINT P, Rectangle rect)
        {
            if (P.x >= rect.Location.X && P.x <= rect.Location.X + rect.Width && P.y >= rect.Location.Y && P.y <= rect.Location.Y + rect.Height)
            {
                return true;
            }

            return false;
        }

    }
}

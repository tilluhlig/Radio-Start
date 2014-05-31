using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication2
{
    class Fenster
    {
        public static int Modus = 0;
        public static List<Fenster> Bildschirme = new List<Fenster>();

        public static void Wechseln(int FensterId)
        {
            if (FensterId<0 || FensterId>=Bildschirme.Count) return;

            Modus = FensterId;
            Bildschirme[Modus].Change();
        }

        public static void Hinzufügen(Fenster neuesFenster)
        {
            Bildschirme.Add(neuesFenster);
            neuesFenster.Id = Bildschirme.Count - 1;
            neuesFenster.Init();
        }

        public static void OnDraw()
        {
            Bildschirme[Modus].Draw();
        }

        public static void OnKeyboard()
        {

        }

        public static void OnMouse()
        {

        }

        public int Id = 0;

        public void Init()
        {

        }

        public void Change()
        {

        }

        public void Draw()
        {

        }

        public void Keyboard()
        {

        }

        public void Mouse()
        {

        }
    }
}

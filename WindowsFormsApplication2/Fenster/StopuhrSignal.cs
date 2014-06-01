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
    class StopuhrSignal : Fenster
    {
        public StopuhrSignal()
        {
            Init();
        }

        override public void Init()
        {
            if (Sounds.Alarm==null)
                Sounds.Load("C:\\Sounds\\", "DE", "F");
        }

        override public void Show()
        {
            // NICHTS
        }

        override public void Hide()
        {
            // NICHTS
        }

        public static bool AnsageIsPlaying(){
            for (int i = 0; i < Sounds.Ansage.Length; i++)
            {
                if (Sounds.Ansage[i].IsPlaying(0))
                    return true;
            }
            return false;
        }

        override public void Draw()
        {
            if (Stopuhr.uhraktiv)
            {
                 double Zeit = Stopuhr.UhrEnde - (new TimeSpan(DateTime.Now.Ticks)).TotalSeconds;
                for (int i = 0; i < Stopuhr.AnsageSekunde.Length; i++)
                {
                    if (Zeit <= Stopuhr.AnsageSekunde[i] && Zeit >= Stopuhr.AnsageSekunde[i] - 1 && !Sounds.Ansage[i].IsPlaying(0))
                    {
                        Sounds.Ansage[i].PlaySound(0);
                    }
                }

                if (Stopuhr.alarmaktiv || AnsageIsPlaying())
                {
                    Radio.Mediaplayer.Mediaplayer.settings.volume = 0;
                }
                else
                    Radio.Mediaplayer.Mediaplayer.settings.volume = Radio.MediaVolume;

                if (Zeit <= 0)
                {
                    // Uhr ist abgelaufen
                    if (Stopuhr.uhraktiv && !Sounds.Alarm.IsPlaying(0))
                    {
                        Stopuhr.alarmaktiv = true;
                        Sounds.Alarm.PlaySound(false, 0, 0.75f, 1f);
                        Radio.Mediaplayer.Mediaplayer.settings.volume = 0;
                    }
                }

                if (Stopuhr.alarmaktiv == false && Stopuhr.spezialalarm > 0)
                {
                    Stopuhr.spezialalarm--;
                }
                else
                    if (Stopuhr.alarmaktiv == false && Stopuhr.spezialalarm <= 0 && Sounds.Alarm.IsPlaying(0))
                    {
                        Sounds.Alarm.StopSound(0);
                    }
            }
            else
            {
                Radio.Mediaplayer.Mediaplayer.settings.volume = Radio.MediaVolume;
            }
        }

        public override POINT Mouse(POINT mausklick, Fenster Hauptfenster)
        {
            if (Stopuhr.alarmaktiv)
            {
                Stopuhr.alarmaktiv = false;
                Stopuhr.uhraktiv = false;
                return null;
            }
            return mausklick;
        }

        public override List<Keys> Keyboard(List<Keys> KeyCodes, Fenster Hauptfenster)
        {
            List<Keys> Result = new List<Keys>();
            if (Stopuhr.alarmaktiv)
            {
                Stopuhr.alarmaktiv = false;
                Stopuhr.uhraktiv = false;
                return Result;
            }

            return KeyCodes;
        }
    }
}

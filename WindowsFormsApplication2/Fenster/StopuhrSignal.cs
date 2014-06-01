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
        override public void Init()
        {

        }

        override public void Show()
        {

        }

        override public void Hide()
        {

        }

        override public void Draw()
        {
            /*if (uhraktiv)
            {
                int zeit = Stunden * 3600 + Minuten * 60 + Sekunden;

                if (second15 && zeit == 15)
                {
                    Sounds.second15.PlaySound(0);
                }
                else
                    if (second30 && zeit == 30)
                    {
                        Sounds.second30.PlaySound(0);
                    }
                    else
                        if (minute1 && zeit == 60)
                        {
                            Sounds.minute1.PlaySound(0);
                        }
                        else
                            if (minute2 && zeit == 120)
                            {
                                Sounds.minute2.PlaySound(0);
                            }
                            else
                                if (minute3 && zeit == 180)
                                {
                                    Sounds.minute3.PlaySound(0);
                                }
                                else
                                    if (minute5 && zeit == 300)
                                    {
                                        Sounds.minute5.PlaySound(0);
                                    }
                                    else
                                        if (minute10 && zeit == 600)
                                        {
                                            Sounds.minute10.PlaySoundAny();
                                        }
                                        else
                                            if (minute15 && zeit == 900)
                                            {
                                                Sounds.minute15.PlaySoundAny();
                                            }
                                            else
                                                if (minute20 && zeit == 1200)
                                                {
                                                    Sounds.minute20.PlaySound(0);
                                                }
                                                else
                                                    if (minute30 && zeit == 1800)
                                                    {
                                                        Sounds.minute30.PlaySound(0);
                                                    }
                                                    else
                                                        if (minute45 && zeit == 2700)
                                                        {
                                                            Sounds.minute45.PlaySound(0);
                                                        }
                                                        else
                                                            if (minute60 && zeit == 3600)
                                                            {
                                                                Sounds.minute60.PlaySound(0);
                                                            }

                if (alarmaktiv || Sounds.second15.IsPlaying(0) || Sounds.second30.IsPlaying(0) || Sounds.minute1.IsPlaying(0) || Sounds.minute2.IsPlaying(0) || Sounds.minute5.IsPlaying(0) || Sounds.minute10.IsPlaying(0) || Sounds.minute20.IsPlaying(0))
                {
                    Mediaplayer.Mediaplayer.settings.volume = 0;
                }
                else
                    Mediaplayer.Mediaplayer.settings.volume = this.MediaVolume;

                if (zeit == 0)
                {
                    // Uhr ist abgelaufen
                    if (uhraktiv)
                    {
                        alarmaktiv = true;
                        //Sounds.Alarm.PlaySound(0);
                        Sounds.Alarm.PlaySound(false, 0, 0.75f, 1f);
                        Mediaplayer.Mediaplayer.settings.volume = 0;
                    }
                }
                else
                {
                    Sekunden--;
                    if (Sekunden < 0) { Minuten--; Sekunden = 59; }
                    if (Minuten < 0) { Stunden--; Minuten = 59; }
                }

                if (alarmaktiv == false && spezialalarm > 0)
                {
                    spezialalarm--;
                }
                else
                    if (alarmaktiv == false && spezialalarm <= 0 && Sounds.Alarm.IsPlaying(0))
                    {
                        Sounds.Alarm.StopSound(0);
                    }

                label5.Show();
            }
            else
            {
                Mediaplayer.Mediaplayer.settings.volume = MediaVolume;
                label5.Hide();
            }*/
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

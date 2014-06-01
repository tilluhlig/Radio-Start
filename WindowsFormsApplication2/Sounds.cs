// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-11-2013
// ***********************************************************************
// <copyright file="Sounds.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
//using Microsoft.Xna.Framework.Content;

namespace WindowsFormsApplication2
{
    public class Soundsystem
    {
        private static FMOD.System system = null;
        private static uint ms = 0;

        private static uint lenms = 0;
        private static int channelsplaying = 0;

        private bool first = true;

        private static void Create()
        {
            uint version = 0;
            FMOD.Factory.System_Create(ref system);
            system.getVersion(ref version);
            if (version < FMOD.VERSION.number)
            {
            }

            system.init(32, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
        }

        private FMOD.Sound Sound = null;
        public List<FMOD.Channel> Channel = new List<FMOD.Channel>();
        private List<float> originalfrequenz = new List<float>();
        private float frequenzfaktor = 1.0f;
        private float volume = 1.0f;
        private bool loop = false;

        public Soundsystem(String Datei, int _InitialChannel)
        {
            Init(Datei, 1.0f, 1.0f, false);
            check_channelid(_InitialChannel - 1);
        }

        public Soundsystem(String Datei)
        {
            Init(Datei, 1.0f, 1.0f, false);
        }

        public Soundsystem(String Datei, float _Volume, float _frequenzfaktor, bool _loop, int _InitialChannel)
        {
            Init(Datei, _Volume, _frequenzfaktor, _loop);
            check_channelid(_InitialChannel - 1);
        }

        public Soundsystem(String Datei, float _Volume, float _frequenzfaktor, bool _loop)
        {
            Init(Datei, _Volume, _frequenzfaktor, _loop);
        }

        public void Init(String Datei, float _Volume, float _frequenzfaktor, bool _loop)
        {
            if (!File.Exists(Datei))
            {
                Datei = Datei + "";
            }

            volume = _Volume;
            frequenzfaktor = _frequenzfaktor;
            loop = _loop;

            if (first)
            {
                Create();
                first = false;
            }
            //.CREATESAMPLE
            system.createSound(Datei, FMOD.MODE.SOFTWARE | FMOD.MODE.LOOP_NORMAL, ref Sound);

            Channel.Add(new FMOD.Channel());
            originalfrequenz.Add(-1);
            // frequenzfaktor = _frequenzfaktor;
        }

        private void check_channelid(int _Channel)
        {
            while (Channel.Count <= _Channel)
            {
                Channel.Add(new FMOD.Channel());
                originalfrequenz.Add(-1);
            }
        }

        public void PlaySound(int _Channel)
        {
            PlaySound(loop, _Channel, frequenzfaktor, volume);
        }

        public void PlaySound(bool _loop, int _Channel)
        {
            PlaySound(_loop, _Channel, frequenzfaktor, volume);
        }

        public void PlaySound(bool _loop, int _Channel, float _frequenzfaktor, float _volume)
        {
            check_channelid(_Channel);

            bool playing = false;

            if (Channel[_Channel] != null) Channel[_Channel].isPlaying(ref playing);
            if (!playing)
            {
                FMOD.Channel temp = null;

                system.playSound(FMOD.CHANNELINDEX.FREE, Sound, false, ref temp);
                Channel[_Channel] = temp;

                if (originalfrequenz[_Channel] == -1) { float wert = 0; temp.getFrequency(ref wert); originalfrequenz[_Channel] = wert; }

                Channel[_Channel].setFrequency(originalfrequenz[_Channel] * _frequenzfaktor);
                Channel[_Channel].setVolume(_volume);

                if (_loop)
                {
                    temp.setLoopCount(-1);
                }
                else
                    temp.setLoopCount(0);
            }
        }

        public void PlaySoundAny()
        {
            PlaySoundAny(loop, frequenzfaktor, volume);
        }

        public void PlaySoundAny(bool _loop)
        {
            PlaySoundAny(_loop, frequenzfaktor, volume);
        }

        public void PlaySoundAny(bool _loop, float _volume)
        {
            PlaySoundAny(_loop, frequenzfaktor, volume * _volume);
        }

        public void PlaySoundAny(bool _loop, float _frequenzfaktor, float _volume)
        {
            for (int i = 0; i < Channel.Count; i++)
            {
                bool playing = false;
                if (Channel[i] != null) Channel[i].isPlaying(ref playing);

                if (!playing)
                {
                    FMOD.Channel temp = null;
                    system.playSound(FMOD.CHANNELINDEX.FREE, Sound, false, ref temp);
                    Channel[i] = temp;

                    if (originalfrequenz[i] == -1) { float wert = 0; temp.getFrequency(ref wert); originalfrequenz[i] = wert; }

                    Channel[i].setFrequency(originalfrequenz[i] * _frequenzfaktor);
                    Channel[i].setVolume(_volume);

                    if (_loop)
                    {
                        Channel[i].setLoopCount(-1);
                    }
                    else
                        Channel[i].setLoopCount(0);

                    break;
                }
            }
        }

        public void StopSound(int _Channel)
        {
            check_channelid(_Channel);

            if (Channel[_Channel] != null)
                Channel[_Channel].stop();
        }

        public void PauseSound(int _Channel)
        {
            check_channelid(_Channel);

            if (Channel[_Channel] != null)
                Channel[_Channel].setPaused(true);
        }

        public void ResumeSound(int _Channel)
        {
            check_channelid(_Channel);

            bool paused = true;
            if (Channel[_Channel] != null)
            {
                Channel[_Channel].getPaused(ref paused);

                if (paused)
                    Channel[_Channel].setPaused(false);
            }

            if (Channel[_Channel] == null || !paused)
            {
                PlaySound(_Channel);
            }
        }

        public bool IsPaused(int _Channel)
        {
            check_channelid(_Channel);

            bool paused = false;
            if (Channel[_Channel] != null) Channel[_Channel].getPaused(ref paused);
            return paused;
        }

        public bool IsPlaying(int _Channel)
        {
            check_channelid(_Channel);

            bool playing = false;
            if (Channel[_Channel] != null) Channel[_Channel].isPlaying(ref playing);
            return playing;
        }

        public void SetVolume(int _Channel, float wert)
        {
            check_channelid(_Channel);

            if (Channel[_Channel] != null)
            {
                Channel[_Channel].setVolume(wert);
            }
        }
    }

    /// <summary>
    /// Class Sounds
    /// </summary>
    internal static class Sounds
    {
        public static Soundsystem Alarm;
        public static Soundsystem Sprache;
        public static Soundsystem second15;
        public static Soundsystem second30;
        public static Soundsystem minute1;
        public static Soundsystem minute2;
        public static Soundsystem minute3;
        public static Soundsystem minute5;
        public static Soundsystem minute10;
        public static Soundsystem minute15;
        public static Soundsystem minute20;
        public static Soundsystem minute30;
        public static Soundsystem minute45;
        public static Soundsystem minute60;


        /// <summary>
        /// Loads the specified content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void Load(String Pfad, String Land, String Geschlecht) // lädt alle Sounds
        {
            Alarm = new Soundsystem(Pfad+"alarm.wav", 1f, 1f, true);
            String Bez = Geschlecht + "_" + Land + "_";
            Sprache = new Soundsystem(Pfad + Bez+"0001.wav", 1f, 1f, false);
            second15 = new Soundsystem(Pfad + Bez + "0002.wav", 1f, 1f, false);
            second30 = new Soundsystem(Pfad + Bez + "0003.wav", 1f, 1f, false);
            minute1 = new Soundsystem(Pfad + Bez + "0004.wav", 1f, 1f, false);
            minute2 = new Soundsystem(Pfad + Bez + "0005.wav", 1f, 1f, false);
            minute3 = new Soundsystem(Pfad + Bez + "0006.wav", 1f, 1f, false);
            minute5 = new Soundsystem(Pfad + Bez + "0007.wav", 1f, 1f, false);
            minute10 = new Soundsystem(Pfad + Bez + "0008.wav", 1f, 1f, false);
            minute15 = new Soundsystem(Pfad + Bez + "0009.wav", 1f, 1f, false);
            minute20 = new Soundsystem(Pfad + Bez + "0010.wav", 1f, 1f, false);
            minute30 = new Soundsystem(Pfad + Bez + "0011.wav", 1f, 1f, false);
            minute45 = new Soundsystem(Pfad + Bez + "0012.wav", 1f, 1f, false);
            minute60 = new Soundsystem(Pfad + Bez + "0013.wav", 1f, 1f, false);

        }
    }
}
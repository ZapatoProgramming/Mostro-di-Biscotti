using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class SoundManager
    {
        public static SoundEffect soundEffect, soundeEffectEstrella, soundeEffectBubble, soundeEffectAvion, yomiMonstruo, intro;
        public static SoundEffectInstance CuerdaRota, SonidoEstrella, SonidoBubble, SonidoAvion, yomiMonstruoinst, introInst;
        public static Song song;

        public static void PlaySong()
        {
            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.GameHasControl)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(song);
            }
        }

        public static void StopSong()
        {
            if (MediaPlayer.State == MediaState.Playing && MediaPlayer.GameHasControl)
            {
                MediaPlayer.Stop();
            }
        }

        public static void PlayEffectCuerda()
        {
            CuerdaRota.Play();
        }
        public static void StopEffectCuerda()
        {
            CuerdaRota.Stop();
        }
        public static void PlayEffectEstrella()
        {
            SonidoEstrella.Play();
        }
        public static void StopEffectEstrella()
        {
            SonidoEstrella.Stop();
        }
        public static void PlayEffectBubble()
        {
            SonidoBubble.Play();
        }
        public static void StopEffectBubble()
        {
            SonidoBubble.Stop();
        }
        public static void PlayEffectAvion()
        {
            SonidoAvion.Play();
        }
        public static void StopEffectAvion()
        {
            SonidoAvion.Stop();
        }
        public static void PlayEffectYomi()
        {
            yomiMonstruoinst.Play();
        }
        public static void PlayEffectIntro()
        {
            introInst.Play();
        }

    }


}

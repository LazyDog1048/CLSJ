using System.Collections.Generic;
using data;
using other;
using so;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace audio
{
    public enum SoundType
    {
        Fx,
        Music
    }
    
    public partial class AudioManager : KeepMonoSingleton<AudioManager>
    {
        public  static List<int> VolumeList = new List<int>() { -80,-35,-25,-20,-15,-10,-5,0,2,5,10};

        private static AudioMixer audioMixer;
        private Dictionary<SoundType, AudioThing> audioThings;
        
        private List<BaseAudioSourceClip> baseAudioSourceClips;


        public int audioLevel = 5;
        private bool isPause;
        protected override void Init()
        {
            base.Init();

            audioMixer = AudioSetting.Instance.AudioMixer;
            
            baseAudioSourceClips = new List<BaseAudioSourceClip>();
            AudioGameObj();
            this.DelayExecute(0.1f, () =>
            {
                InitLocalVolume(SoundType.Fx);
                InitLocalVolume(SoundType.Music);
            });
        }

        public AudioThing GetAudioThing(SoundType type)
        {
            return audioThings[type];
        }
        
        public override void BeforeSceneChange()
        {
            ClearAudio();
        }

        private void AudioGameObj()
        {
            audioThings = new Dictionary<SoundType, AudioThing>();
            foreach (var audioThing in AudioSetting.Instance.AudioThings)
            {
                audioThings.Add(audioThing.Type,audioThing);
                audioThings[audioThing.Type].InitAudioSource(transform);
            }
        }

        public void ClearAudio()
        {
            foreach (var audioSource in baseAudioSourceClips)
            {
                audioSource.StopClip();
            }
        }
        public void PauseAudio()
        {
            if(isPause)
                return;
            isPause = true;
            foreach (var audioThing in audioThings.Values)
            {
                if(audioThing.Type == SoundType.Music)
                    continue;
                audioThing.PauseAudio();
            }
        }
        
        public void ResumeAudio()
        {
            if(!isPause)
                return;
            isPause = false;
            foreach (var audioThing in audioThings.Values)
            {
                if(audioThing.Type == SoundType.Music)
                    continue;
                audioThing.ResumeAudio();
            }
        }


        private void InitLocalVolume(SoundType type)
        {
            audioMixer.SetFloat($"{type}_Group",VolumeList[audioLevel]);
        }
        
        public static void UpdateVolumeLevel(SoundType type, int level)
        {
            audioMixer.SetFloat($"{type}_Group",VolumeList[level]);
        }


#region AudioClip

        public void AddMonoAudioSourceClips(BaseAudioSourceClip clip)
        {
            baseAudioSourceClips.Add(clip);
        }
                
        public bool HadMonoAudioSourceClips(BaseAudioSourceClip clip)
        {
            return baseAudioSourceClips.Contains(clip);
        }
        public void RemoveMonoAudioSourceClips(BaseAudioSourceClip clip)
        {
            baseAudioSourceClips.Remove(clip);
        }
#endregion
    }
}

using System;
using System.Collections.Generic;
using audio;
using data;
using Sirenix.OdinInspector;
using so;
using UnityEngine;
using UnityEngine.Audio;

namespace so
{
    [CreateAssetMenu(fileName = "new AudioSetting", menuName = "Data/AudioSetting", order = 10)]
    public class AudioSetting : SingletonSo<AudioSetting>
    {
        public AudioSetting()
        {
            AudioThings = new List<AudioThing>();
            foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
            {
                AudioThing audioThing = new AudioThing(type);
                AudioThings.Add(audioThing);
            }
        }

        public AudioMixer AudioMixer;
        [ShowInInspector]
        public List<AudioThing> AudioThings;
    }
    
}

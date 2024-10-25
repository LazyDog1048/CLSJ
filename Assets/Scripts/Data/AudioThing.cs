using System;
using System.Collections.Generic;
using audio;
using UnityEngine;
using UnityEngine.Audio;

namespace data
{
    [Serializable]
    public class AudioThing
    {
        public SoundType Type;
        [SerializeField]
        private List<float> volumePriorityList;
        public AudioMixerGroup Group;
        
        [HideInInspector]
        public Transform audioSoureTrans;
        public Queue<AudioSource> idleSourceQueue;
        private List<AudioSource> audioSourceList;
        public float GetVolumeByPriority(int priority)
        {
            return volumePriorityList[priority];
        }
        public AudioThing(SoundType type)
        {
            Type = type;            
        }

        public void InitAudioSource(Transform parent)
        {
            idleSourceQueue = new Queue<AudioSource>();
            audioSoureTrans = new GameObject($"{Type}_AudioSource").transform;
            audioSoureTrans.SetParent(parent);
            audioSourceList = new List<AudioSource>();
        }

        public AudioSource PopAudioSource()
        {
            //没有空闲音源
            if (idleSourceQueue.Count == 0)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = $"{Type}_{gameObject.GetHashCode()}";
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.outputAudioMixerGroup = Group;
                gameObject.transform.SetParent(audioSoureTrans);
                audioSourceList.Add(audioSource);
                return audioSource;
            }            
            return idleSourceQueue.Dequeue();
        }

        public void PushAudioSource(AudioSource source)
        {
            idleSourceQueue.Enqueue(source);
        }
        
        public void PauseAudio()
        {
            foreach (var audioSource in audioSourceList)
            {
                audioSource.Pause();
            }
        }
        
        public void ResumeAudio()
        {
            foreach (var audioSource in audioSourceList)
            {
                audioSource.UnPause();
            }
        }
        
    }
    
  
   
    
    [Serializable]
    public class AudioState
    {
        public int level;

        public AudioState(int level)
        {
            this.level = level;
        }
    }
    [Serializable]
    public class AudioSaveData
    {
        public AudioState music;
        public AudioState fx;
        public AudioState total;
        public AudioState this[SoundType type]
        {
            get
            {
                switch (type)
                {
                    case SoundType.Music:
                        return music;
                    case SoundType.Fx:
                        return fx;
                    default:
                        return total;
                }   
            }
        }
        public AudioSaveData()
        {
            music = new AudioState(5);
            fx = new AudioState(5);
            total = new AudioState(5);
        }
    }

}
using System;
using audio;
using DG.Tweening;
using Sirenix.OdinInspector;
using so;
using UnityEngine;
using UnityEngine.Events;

namespace data
{
    [Serializable]
    public class BaseAudioSourceClip
    {
        [ShowInInspector] [ReadOnly] public readonly SoundType SoundType;

        public int priority = 0;
        public bool isLoop;
        // public bool cantBePause = false;
        public bool randomVolume = false;
        public bool randomPitch = false;

        [ShowIf("randomVolume")] public Vector2 volumeRange = new Vector2(0.9f, 1.1f);
        [ShowIf("randomPitch")] public Vector2 pitchRange = new Vector2(0.9f, 1.1f);

        [InlineEditor(InlineEditorModes.SmallPreview)]
        public AudioClip Clip;

        public string Name => Clip.name;

        public AudioThing audioThing => AudioManager.Instance.GetAudioThing(SoundType);

        // public bool isStopPlay => audioSource == null || audioSource.clip == null;
        public virtual bool CantPlay => !Application.isPlaying || Clip == null;
        public bool playing { get; set; }
        protected AudioSource audioSource { get; set; }

        public BaseAudioSourceClip(SoundType type)
        {
            SoundType = type;
        }

        public void RemoveAudioSource()
        {
            audioThing.PushAudioSource(audioSource);
            audioSource = null;
        }

        protected virtual void OnPlay()
        {
            playing = true;            
            audioSource = audioThing.PopAudioSource();
            
            audioSource.clip = Clip;
            audioSource.loop = isLoop;
            
            if(randomVolume)
                audioSource.volume = UnityEngine.Random.Range(volumeRange.x,volumeRange.y) * audioThing.GetVolumeByPriority(priority);
            else
                audioSource.volume = 1 * audioThing.GetVolumeByPriority(priority);
            
            if(randomPitch)
                audioSource.pitch = UnityEngine.Random.Range(pitchRange.x,pitchRange.y);
            else
                audioSource.pitch = 1;
            
            audioSource.Play();
        }

        public virtual void OnComplete()
        {
            if(audioSource == null)
                return;
            audioSource.Stop();
            AudioManager.Instance.RemoveMonoAudioSourceClips(this);
            RemoveAudioSource();
        }
        
        
        public virtual void StopClip()
        {
            playing = false;            
        }
        
        public virtual void PauseClip()
        {
            if(audioSource == null)
                return;
            audioSource.Pause();
        }
        
        public virtual void UnPauseClip()
        {
            if(audioSource == null)
                return;
            audioSource.UnPause();
        }
    }
    

}

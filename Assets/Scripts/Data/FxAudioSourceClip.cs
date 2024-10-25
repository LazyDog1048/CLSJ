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
    public class FxAudioSourceClip:BaseAudioSourceClip
    {
        private float minTime = 0.1f;
        private bool isMinCd;
        public FxAudioSourceClip():base(SoundType.Fx)
        {

        }
        
        public FxAudioSourceClip(AudioClip clip):base(SoundType.Fx)
        {
            isLoop = false;
            Clip = clip;
        }
       
        public virtual void PlayClip()
        {   
            if(CantPlay || isMinCd)
                return;

            isMinCd = true;
            AudioManager.Instance.DelayExecute(minTime, () =>
            {
                isMinCd = false;
            });
            Play();
        }
        
        
        protected virtual void Play()
        {
            OnPlay();
            var tempAudioSource = audioSource;
            AudioManager.Instance.DelayExecute(Clip.length, () =>
            {
                StopClip(tempAudioSource);
            });
        }

        public void StopClip(AudioSource temp)
        {
            temp.Stop();
            OnComplete(temp);
        }
        
        public void OnComplete(AudioSource temp)
        {
            AudioManager.Instance.RemoveMonoAudioSourceClips(this);
            audioThing.PushAudioSource(temp);
        }
    }
}
using System;
using System.Collections;
using game;
using game.manager;
using game.Other;
using UnityEngine;
using UnityEngine.Events;

public static class Mono_Extend
{
    public static void PlayAnim(this Animator animator,string state,UnityAction complete)
    {
        animator.PlayAnim(Animator.StringToHash(state),complete);
    }
    
    public static void PlayAnim(this Animator animator,int animHash)
    {
        animator.PlayAnim(animHash,null);
    }
    
    public static void PlayAnim(this Animator animator,int animHash,UnityAction complete)
    {
        animator.Play(animHash,0,0);
        if(complete == null)
            return;
        MapManager.Instance.FrameEndExecute(() =>
        {
            float currentWaitTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length - StaticValue.FixedTimeScale;
            MapManager.Instance.DelayExecute(currentWaitTime,complete);
        });
    }
    
    public static T Create<T>() where T : Component 
    {
        GameObject gameObject = new GameObject(typeof(T).Name);
        return gameObject.AddComponent<T>();
    }

    public static void WaitFixedExecute(this MonoBehaviour mono,Func<bool> completeCondition,UnityAction complete)
    {
        mono.WaitFixedExecute(completeCondition, complete,null);
    }
    
    
    public static void WaitRealExecute(this MonoBehaviour mono,Func<bool> completeCondition,UnityAction complete,UnityAction Update)
    {
        if(mono.isActiveAndEnabled)
            mono.StartCoroutine(FixedDelay());
        
        IEnumerator FixedDelay()
        {
            do
            {
                Update?.Invoke();
                yield return StaticValue.WaitRealtime002;
            } while (!completeCondition.Invoke());
            
            if(mono.isActiveAndEnabled)
                complete?.Invoke();
        }
    }
    
    public static void WaitFixedExecute(this MonoBehaviour mono,Func<bool> completeCondition,UnityAction complete,UnityAction Update)
    {
        if(mono.isActiveAndEnabled)
            mono.StartCoroutine(FixedDelay());
        
        IEnumerator FixedDelay()
        {
            do
            {
                Update?.Invoke();
                yield return StaticValue.WaitFixedUpdate;
            } while (!completeCondition.Invoke());
            
            if(mono.isActiveAndEnabled)
                complete?.Invoke();
        }
    }

    public static void WaitExecute(this MonoBehaviour mono,Func<bool> completeCondition,UnityAction complete)
    {
        mono.WaitExecute(completeCondition, complete,null);
    }
    
    public static void WaitExecute(this MonoBehaviour mono,Func<bool> completeCondition,UnityAction complete,UnityAction Update)
    {
        if(mono.isActiveAndEnabled)
            mono.StartCoroutine(Delay());
        
        IEnumerator Delay()
        {
            do
            {
                Update?.Invoke();
                yield return StaticValue.WaitEndFrame;
            } while (!completeCondition.Invoke());
            if(mono.isActiveAndEnabled)
                complete?.Invoke();
        }
    }

    #region Delay

    public static void DelayFixedFrameExecute(this Transform transform,UnityAction action)
    {
        transform.gameObject.DelayFixedFrameExecute(action);
    }

    private static void DelayFixedFrameExecute(this GameObject gameObject,UnityAction action)
    {
        if(!gameObject.activeSelf)
            return;
        gameObject.GetComponent<MonoBehaviour>().DelayFixedFrameExecute(action);
    }
    
    public static void DelayFixedFrameExecute(this MonoBehaviour mono, UnityAction action)
    {
        if (mono.isActiveAndEnabled)
            mono.StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return StaticValue.WaitFixedUpdate;
            if(mono.isActiveAndEnabled)
                action?.Invoke();
        }
    }
    
    public static void FrameEndExecute(this MonoBehaviour mono, UnityAction action)
    {
        if (mono.isActiveAndEnabled)
            mono.StartCoroutine(Delay());
    
        IEnumerator Delay()
        {
            yield return StaticValue.WaitEndFrame;
            if(mono.isActiveAndEnabled)
                action?.Invoke();
        }
    }


    public static void DelayExecute(this MonoBehaviour mono, float time, UnityAction action)
    {
        if (mono.isActiveAndEnabled)
            mono.StartCoroutine(Delay());

        IEnumerator Delay()
        {
            float counter = 0;
            do
            {
                counter += StaticValue.FixedTimeScale;
                yield return StaticValue.WaitFixedUpdate;
            } while (counter < time);
            if(mono.isActiveAndEnabled)
                action?.Invoke();
        }
    }
    
    public static void DelayRealTimeExecute(this MonoBehaviour mono, float time, UnityAction action)
    {
        if (mono.isActiveAndEnabled)
            mono.StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSecondsRealtime(time);
            if(mono.isActiveAndEnabled)
                action?.Invoke();
        }
    }
    
    public static void LoopDelayExecute(this MonoBehaviour mono,float interval,int times,UnityAction<int> action)
    {
        mono.LoopDelayExecute(interval,times,()=>false,action);
    }

    public static void LoopDelayExecute(this MonoBehaviour mono,float interval,int times,Func<bool> completeCondition,UnityAction<int> action,UnityAction completeAction = null,UnityAction updateAction = null)
    {
        if (mono.isActiveAndEnabled)
        {
            if(times<=0)
                completeAction?.Invoke();
            else
                mono.StartCoroutine(LoopDelay());
        }
        
        IEnumerator LoopDelay()
        {
            do
            {
                times--;
                float counter = 0;
                
                do
                {
                    counter += StaticValue.FixedTimeScale;
                    if(mono.isActiveAndEnabled)
                        updateAction?.Invoke();
                    yield return StaticValue.WaitFixedUpdate;
                    
                    if(completeCondition?.Invoke() ?? false)
                        break;
                }
                while (counter < interval);
                
                if(mono.isActiveAndEnabled)
                    action?.Invoke(times);
                if(completeCondition?.Invoke() ?? false)
                    break;
            }
            while (times > 0) ;
            if(mono.isActiveAndEnabled)
                completeAction?.Invoke();
        }
    }

    public static void LoopDelayExecute(this MonoBehaviour mono, float interval, UnityAction action)
    {
        mono.LoopDelayExecute(interval,()=>false,action);
    }
    public static void LoopDelayExecute(this MonoBehaviour mono,float interval,Func<bool> completeCondition,UnityAction action,UnityAction completeAction = null,UnityAction updateAction = null)
    {
        if(mono.isActiveAndEnabled)
            mono.StartCoroutine(LoopDelay());

        IEnumerator LoopDelay()
        {
            do
            {
                float counter = 0;
                do
                {
                    counter += Time.fixedDeltaTime;
                    if (mono.isActiveAndEnabled)
                        updateAction?.Invoke();
                    yield return StaticValue.WaitFixedUpdate;
                    if (completeCondition?.Invoke() ?? false)
                        break;
                }
                while (counter < interval);
                
            action?.Invoke();
            if (completeCondition?.Invoke() ?? false)
                break;
            }
            while (mono.isActiveAndEnabled) ;
            if(mono.isActiveAndEnabled)
                completeAction?.Invoke();
        }
    }
    
    public static void WaitJudgeExecute(this MonoBehaviour mono,Func<bool> successCondition,Func<bool> failCondition,UnityAction successComplete,UnityAction failComplete = null)
    {
        if(mono.isActiveAndEnabled)
            mono.StartCoroutine(Delay());
        
        IEnumerator Delay()
        {
            do
            {
                yield return StaticValue.WaitFixedUpdate;
                if (successCondition())
                {
                    successComplete?.Invoke();
                    yield break;
                }
                else if (failCondition())
                {
                    failComplete?.Invoke();
                    yield break;
                }
            }
            while (true) ;
        }
    }
    #endregion
}
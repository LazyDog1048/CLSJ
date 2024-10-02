using UnityEngine;

namespace other
{
    [ExecuteInEditMode]
    public abstract class EditorAction<T> : Mono_Singleton<T> where T : Mono_Singleton<T>
    {
    }
}


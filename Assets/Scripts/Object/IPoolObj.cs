using UnityEngine;

namespace game
{
    public interface IPoolObj
    {
        bool isInPool { get; set; }
        GameObject GetObj();
        void ReleaseObj();
        void OnPopObj();
        void OnPushObj();
        void DestroyObj();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractComponent
{
    protected MonoBehaviour mono;
    public Transform transform => mono.transform;
    public GameObject gomeObject => mono.gameObject;
    public Vector3 position => transform.position;
    public AbstractComponent(MonoBehaviour mono)
    {
        this.mono = mono;
    }
}

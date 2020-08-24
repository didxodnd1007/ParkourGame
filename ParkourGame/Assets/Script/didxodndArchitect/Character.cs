using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void OnEnable()
    {
        
    }

    public virtual void OnAwake() { }

    public virtual void OnStart() { }

    public abstract void Move(float dir = 0);
    public abstract void Jump();
    public abstract void Idle();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MainCharacterState
{
    IDLE, MOVE, RUN, CLIMP, JUMP
}

public class MainCharacter : Character
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpScale;

    //======================================================================================
    MainCharacterState plState;
    private float moveDir;

    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override void OnStart()
    {
        base.OnAwake();
    }

    private void Update()
    {
          
    }



    public override void Move(float dir)
    {
        moveDir = dir;
    }

    public override void Jump()
    {
    }
}

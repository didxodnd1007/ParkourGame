using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command // 추상 클래스 Command
{
    public abstract void PlayCommand(Character obj);
}

public class MoveCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        obj.Move();
    }
}

public class JumpCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        obj.Jump();
    }
}


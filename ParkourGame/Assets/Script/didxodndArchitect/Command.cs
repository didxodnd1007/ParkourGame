using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command // 추상 클래스 Command
{
    public abstract void PlayCommand(Character obj);
}

public class RightMoveCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        obj.Move(1f);
    }
}

public class LeftMoveCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        obj.Move(-1f);
    }
}

public class JumpCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        obj.Jump();
    }
}

public class IdleCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        obj.Idle();
    }
}

<<<<<<< HEAD
=======
public class RightRunCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        Debug.Log("hIRU");
        obj.Move(1f);
    }
}

public class LeftRunCommand : Command
{
    public override void PlayCommand(Character obj)
    {
        Debug.Log("hIRU2");
        obj.Move(-1f);
    }
}
>>>>>>> didxodnd


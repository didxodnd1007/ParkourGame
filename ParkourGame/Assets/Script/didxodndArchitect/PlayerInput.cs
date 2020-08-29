using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Character mainPlayer;

<<<<<<< HEAD
    private Command rightArrow_;
    private Command leftArrow_;
    private Command idleCommand_;
    private Command spaceButton_;
<<<<<<< HEAD
=======
    private Command rightArrow;
    private Command leftArrow;
    private Command idleCommand;
    private Command spaceButton;
>>>>>>> cc43e1a1ab69e05b4d09542d39d7adeaa2bf7f1a
=======
    private Command rightRunArrow_;
    private Command leftRunArrow_;
>>>>>>> didxodnd

    private KeyCode inputKey;


    private void Awake() // 나중에 키 변경할 수 있게 수정
    {
<<<<<<< HEAD
        rightArrow_ = new RightMoveCommand();
        leftArrow_ = new LeftMoveCommand();
        idleCommand_ = new IdleCommand();
        spaceButton_ = new JumpCommand();
<<<<<<< HEAD
=======
        rightArrow = new RightMoveCommand();
        leftArrow = new LeftMoveCommand();
        idleCommand = new IdleCommand();
        spaceButton = new JumpCommand();
>>>>>>> cc43e1a1ab69e05b4d09542d39d7adeaa2bf7f1a
=======
        rightRunArrow_ = new RightRunCommand();
        leftRunArrow_ = new LeftRunCommand();
>>>>>>> didxodnd
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
<<<<<<< HEAD
            rightArrow.PlayCommand(mainPlayer);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            leftArrow.PlayCommand(mainPlayer);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            spaceButton.PlayCommand(mainPlayer);
=======
            if (Input.GetKey(KeyCode.LeftShift))
                rightRunArrow_.PlayCommand(mainPlayer);
            else
                rightArrow_.PlayCommand(mainPlayer);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                leftRunArrow_.PlayCommand(mainPlayer);
            else
                leftArrow_.PlayCommand(mainPlayer);
>>>>>>> didxodnd
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            spaceButton_.PlayCommand(mainPlayer);
        }
        else if (!Input.anyKey)
        {
            idleCommand.PlayCommand(mainPlayer);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Character mainPlayer;

    private Command rightArrow_;
    private Command leftArrow_;
    private Command idleCommand_;
    private Command spaceButton_;
    private Command rightRunArrow_;
    private Command leftRunArrow_;

    private KeyCode inputKey;


    private void Awake() // 나중에 키 변경할 수 있게 수정
    {
        rightArrow_ = new RightMoveCommand();
        leftArrow_ = new LeftMoveCommand();
        idleCommand_ = new IdleCommand();
        spaceButton_ = new JumpCommand();
        rightRunArrow_ = new RightRunCommand();
        leftRunArrow_ = new LeftRunCommand();
    }
    // Update is called once per frame
    void Update()
    {
        if  (Input.GetKey(KeyCode.D))
        {
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
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            spaceButton_.PlayCommand(mainPlayer);
        }
        else // 키 입력이 없을 때
        {
            idleCommand_.PlayCommand(mainPlayer);
        }
    }
}

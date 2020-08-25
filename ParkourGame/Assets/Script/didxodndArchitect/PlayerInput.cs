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

    private KeyCode inputKey;


    private void Awake() // 나중에 키 변경할 수 있게 수정
    {
        rightArrow_ = new RightMoveCommand();
        leftArrow_ = new LeftMoveCommand();
        idleCommand_ = new IdleCommand();
        spaceButton_ = new JumpCommand();
    }
    // Update is called once per frame
    void Update()
    {
        if  (Input.GetKey(KeyCode.D))
        {
            rightArrow_.PlayCommand(mainPlayer);
        }
        else if (Input.GetKey(KeyCode.A))
        {
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

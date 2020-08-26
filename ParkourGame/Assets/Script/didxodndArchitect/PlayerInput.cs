using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Character mainPlayer;

    private Command rightArrow;
    private Command leftArrow;
    private Command idleCommand;
    private Command spaceButton;

    private KeyCode inputKey;


    private void Awake() // 나중에 키 변경할 수 있게 수정
    {
        rightArrow = new RightMoveCommand();
        leftArrow = new LeftMoveCommand();
        idleCommand = new IdleCommand();
        spaceButton = new JumpCommand();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rightArrow.PlayCommand(mainPlayer);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            leftArrow.PlayCommand(mainPlayer);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            spaceButton.PlayCommand(mainPlayer);
        }
        else // 키 입력이 없을 때
        {
            idleCommand.PlayCommand(mainPlayer);
        }
    }
}

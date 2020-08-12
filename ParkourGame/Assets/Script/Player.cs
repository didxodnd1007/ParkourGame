using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PLAYER_STATE
{
    IDLE, MOVE, RUN, CLIMP
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator playerAni;

    static public Player S; // SingleTon


    private PLAYER_STATE plState;
    Vector3 mvPos;
    int prevMvPos = 0;

    private void Awake()
    {
        plState = PLAYER_STATE.IDLE;
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        AnimatorChecking();

        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Turn") && playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            playerAni.SetBool("Turn", false);
        }
    }

    public void MovePlayer()
    {
        mvPos = Vector3.zero;

        // 방향 지정 y는 z로 대체
        if (Input.GetKey(KeyCode.D))
        {
            plState = PLAYER_STATE.MOVE;
            mvPos.x = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            plState = PLAYER_STATE.MOVE;
            mvPos.x = 1;
        }
        else
        {
            plState = PLAYER_STATE.IDLE;
            prevMvPos = 0;
            return; // 입력해야하는 키가 아니면 빠져나감
        }

        if  (prevMvPos == 0 || prevMvPos == (int)mvPos.x)
        {
            prevMvPos = (int)mvPos.x;
        }
        else // 방향전환
        {
            prevMvPos = (int)mvPos.x;
            playerAni.SetBool("Turn", true);
        }

        this.transform.Translate(mvPos * speed * Time.deltaTime);
        this.transform.localScale = new Vector3((mvPos.x * -1), 1, 1);
        Debug.Log("Move");

    }



    public void AnimatorChecking()
    {
        switch (plState)
        {
            case PLAYER_STATE.MOVE:
                playerAni.SetBool("Move", true);
                break;
            case PLAYER_STATE.IDLE:
                playerAni.SetBool("Move", false);
                break;
            default:
                break;
        }
    }

    public void TurnFalse()
    {
        playerAni.SetBool("Turn", false);
    }
}

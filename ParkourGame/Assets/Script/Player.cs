using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PLAYER_STATE
{
    IDLE, MOVE, RUN, CLIMP, JUMP
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator playerAni;
    [Header("Pivot")]
    [SerializeField]
    private Transform playerTop;
    [SerializeField]
    private Transform playerFoot;


    static public Player S; // SingleTon


    private PLAYER_STATE plState;
    Vector3 mvPos;
    int prevMvPos = 0;
    private float turnSpeed;
    private float mvSpeed;

    private Ray2D ray2D;
    private RaycastHit2D hit2D;

    private void Awake()
    {
        plState = PLAYER_STATE.IDLE;
    }


    // Update is called once per frame
    void Update()
    {
        KeyInput();
        AnimatorChecking();

        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Turn") && playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            playerAni.SetBool("Turn", false);
            mvSpeed = speed;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void KeyInput()
    {
        if (Input.GetKey(KeyCode.D)) // 앞으로
        {
            plState = PLAYER_STATE.MOVE;
            Moving(-1f);
        }
        else if (Input.GetKey(KeyCode.A)) // 뒤로
        {
            plState = PLAYER_STATE.MOVE;
            Moving(1f);
        }
        else // 가만히
        {
            plState = PLAYER_STATE.IDLE;
            prevMvPos = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 점프
        {
            if (plState != PLAYER_STATE.JUMP)
            {
                int layerMask = 1 << LayerMask.NameToLayer("Player"); // 플레이어를 제외한 나머지 레이어 마스크
                layerMask = ~layerMask;

                ray2D.origin = this.transform.position;
                //Debug.Log(playerFoot.position);
                Debug.Log(ray2D.origin);

                Debug.DrawRay(ray2D.origin, Vector2.down, Color.red, 30f);

                hit2D = Physics2D.Raycast(ray2D.origin, Vector2.down,1f,layerMask);
                Debug.Log(hit2D.collider.gameObject);

                if (hit2D) // 아래를 검사했을 때 땅이면 점프
                {
                    Debug.Log("Hiru");
                    if (hit2D.collider.tag == "Ground")
                    {
                        Jumping();
                        plState = PLAYER_STATE.JUMP;
                    }
                }
            }
        }
    }

    public void Moving(float mvX)
    {
        mvPos = Vector3.zero;
        mvPos.x = mvX;

        if(prevMvPos == 0 || prevMvPos == (int)mvPos.x)
        {
            prevMvPos = (int)mvPos.x;
        }
         else // 방향전환
        {
            prevMvPos = (int)mvPos.x;
            playerAni.SetBool("Turn", true);
            mvSpeed = speed / 2f;
        }

        this.transform.Translate(mvPos * speed * Time.deltaTime);
        this.transform.localScale = new Vector3((mvPos.x * -1), 1, 1);
    }

    public void Jumping()
    {
        Debug.Log("Jumping!!");
    }

    public void Climbing()
    {

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

        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Turn") && playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            playerAni.SetBool("Turn", false);
            mvSpeed = speed;
        }
    }
}

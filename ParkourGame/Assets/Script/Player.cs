using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    private float jumpScale;
    [SerializeField]
    private Animator playerAni;
    [Header("Pivot")]
    [SerializeField]
    private Transform playerTop;
    [SerializeField]
    private Transform playerFoot;
    [Range(0, 1)]
    [SerializeField]
    private float climpAniSec;
    [SerializeField]
    private Vector2 climpMovePivot;
    [SerializeField]
    private Sprite climpStartSprite;
    [SerializeField]
    private Sprite idleSprite;
    [SerializeField]
    private Sprite jumpSprite;
    [SerializeField]
    private Sprite jumpSpriteStay;


    static public Player S; // SingleTon


    private PLAYER_STATE plState;
    Vector3 mvPos;
    int prevMvPos = 0;
    private float turnSpeed;
    private float mvSpeed;
    private bool jumpingChk = false;
    private float currentSpeed;

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
    }

    private void FixedUpdate()
    {
        if (plState == PLAYER_STATE.MOVE)
        {
            if (currentSpeed <= 1)
            {
                currentSpeed += 0.05f;
            }
        }
        else if (plState == PLAYER_STATE.IDLE)
        {
            currentSpeed = 0;
        }
    }

    public void KeyInput()
    {
        if (plState != PLAYER_STATE.CLIMP)
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
                if (jumpingChk == false && plState != PLAYER_STATE.JUMP)
                {
                    plState = PLAYER_STATE.IDLE;
                    prevMvPos = 0;
                }

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

                    Debug.DrawRay(ray2D.origin, Vector2.down, Color.red, 2f);

                    hit2D = Physics2D.Raycast(ray2D.origin, Vector2.down, 1f, layerMask);
                    Debug.Log(hit2D.collider.gameObject);

                    if (hit2D) // 아래를 검사했을 때 땅이면 점프
                    {
                        if (hit2D.collider.tag == "Ground")
                        {
                            StartCoroutine(Jumping());
                            plState = PLAYER_STATE.JUMP;
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.W)) // 올라가기(이동중이면 불가<점프중 가능>)
            {
                if (plState != PLAYER_STATE.MOVE)
                {
                    Ray2D topRay = new Ray2D();
                    RaycastHit2D topRayHit;
                    int layerMask = 1 << LayerMask.NameToLayer("Wall"); // 벽를 제외한 나머지 레이어 마스크
                    ray2D.origin = this.transform.position;
                    topRay.origin = playerTop.transform.position;

                    Debug.DrawRay(ray2D.origin, new Vector2(1, -0.5f), Color.blue, 2f);
                    Debug.DrawRay(topRay.origin, Vector2.right, Color.blue, 2f);

                    hit2D = Physics2D.Raycast(ray2D.origin, new Vector2(1, -0.5f), 1f, layerMask);
                    //Debug.Log(hit2D.collider.gameObject);

                    topRayHit = Physics2D.Raycast(topRay.origin, Vector2.right, 1f, layerMask);
                    //Debug.Log(topRayHit.collider.gameObject);

                    if (hit2D.collider != null && topRayHit.collider == null)
                    {
                        if (hit2D.collider.tag == "Ground")
                            StartCoroutine(Climbing());
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

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

        this.transform.Translate(mvPos * speed * Time.deltaTime * currentSpeed);
        this.transform.localScale = new Vector3((mvPos.x * -1), 1, 1);
    }

    public IEnumerator Jumping()
    {
        jumpingChk = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*jumpScale, ForceMode2D.Impulse);
        jumpingChk = false;
        yield return null;
    }

    public IEnumerator Climbing()
    {
        plState = PLAYER_STATE.CLIMP;
        this.GetComponent<Rigidbody2D>().gravityScale = 0; // 오르는 중에는 중력의 영향을 받지 않음
        Vector2 nowPos = (Vector2)this.transform.position;
        Vector2 endPos = (Vector2)this.transform.position + climpMovePivot;

        float endXpos = endPos.x;
        float endYpos = endPos.y;

        this.GetComponent<SpriteRenderer>().sprite = climpStartSprite;
        yield return new WaitForSeconds(0.2f);

        MainCameraScript.SingleTon.MainFocus = null;
        while(true)
        {
            nowPos = Vector2.Lerp(nowPos, endPos, 0.5f);
            if (Vector2.Distance(nowPos, endPos) <= 0.03f) // 차이가 0.1f정도 차이날 때 빠져나가고 코루틴 정지
            {
                this.transform.position = endPos;
                break;
            }

            this.transform.position = nowPos;
            yield return new WaitForSeconds(0.2f);
        }

        this.GetComponent<Rigidbody2D>().gravityScale = 1f;
        plState = PLAYER_STATE.IDLE; // 플레이어를 아무 행동을 안하는 상태로 변경
        this.GetComponent<SpriteRenderer>().sprite = idleSprite;
        MainCameraScript.SingleTon.MainFocus = this.gameObject;
        yield return null;
    }



    public void AnimatorChecking()
    {
        switch (plState)
        {
            case PLAYER_STATE.MOVE:
                playerAni.SetBool("Move", true);
                break;
            case PLAYER_STATE.IDLE:
                this.GetComponent<Animator>().enabled = true;
                playerAni.SetBool("Move", false);
                this.GetComponent<SpriteRenderer>().sprite = idleSprite;
                this.GetComponent<SpriteRenderer>().flipX = false;
                break;
            case PLAYER_STATE.CLIMP:
                this.transform.localScale = new Vector2(-1, 1);
                playerAni.SetBool("Climp", true);
                break;
            case PLAYER_STATE.JUMP:
                Debug.Log("SpriteChange");
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<SpriteRenderer>().sprite = jumpSprite;
                this.GetComponent<SpriteRenderer>().flipX = true;
                break;
            default:
                break;
        }

        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Turn") && playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            playerAni.SetBool("Turn", false);
            mvSpeed = speed;
        }

        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Climp") && playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            playerAni.SetBool("Climp", false);
        }
    }
<<<<<<< HEAD
    void ShowDebug(Text text)
    {
        MainDebug.instance.Addstruct("speed",speed);   
        MainDebug.instance.Addstruct("jumpScale", jumpScale);
        for (int i=0; i < MainDebug.instance.value_name.Count; i++)
        {            
            text.text = MainDebug.instance.value_name[i] + " :"+MainDebug.instance.value[i]+"\n";
        }
       // text.text = "";
    }   
   
=======

    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            //방향 Sign -> 음수 면 - 1 양수거나 0이면 1 반환
            float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hiru");
        if (collision.gameObject.tag == "Ground")
            StartCoroutine(downJumping());
    }

    private IEnumerator downJumping()
    {
        plState = PLAYER_STATE.JUMP;
        yield return new WaitForSeconds(0.2f);
        plState = PLAYER_STATE.IDLE;
    }
>>>>>>> ed0cbfc1727b2d8a67817b7080c48db622d0758d
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MainCharacterState
{
    IDLE, MOVE, RUN, CLIMP, JUMP, TURN
}

public class MainCharacter : Character
{

    [SerializeField]
    private float speed; // 플레이어 기본 스피드
    [SerializeField]
    private float jumpScale; // 점프 파워
    [SerializeField]
    private float jumpDelay;
    [SerializeField]
    private Animator characterAni;
    [SerializeField]
    private float runSpeedMagnification;

    [Header("PlayerSprite")]
    [SerializeField]
    private Sprite jumpSprite;
    [SerializeField]
    private Sprite idleSprite;

    //======================================================================================
    MainCharacterState plState; // 플레이어 현재 상태 FSM 적용 예정
    private float moveDir = 0; // 이동 방향
    private int prevMoveDir; // 이전 이동 방향
    private float currentSpeed = 0; // 가속도
    private float mvSpeed;
    private float timeFlow = 0f;
    private bool runEvent = false;
    private bool jumpPowerChk;

    private Ray2D ray2D;
    private RaycastHit2D hit2D;
    private GameObject touchObj;

    public override void OnAwake()
    {
        base.OnAwake();
        plState = MainCharacterState.IDLE;
        mvSpeed = speed;
    }

    public override void OnStart()
    {
        base.OnAwake();
    }

    private void Update()
    {
        switch (plState)
        {
            case MainCharacterState.IDLE:
                Idle();
                break;
            case MainCharacterState.MOVE:
                Moving(); // 물리 이동
                break;
            case MainCharacterState.JUMP:
                timeFlow += Time.deltaTime;
                JumpUpdate(); // 최종 검사
                break;
            case MainCharacterState.RUN:
                RunUpdate();
                break;
        }
        AnimatorChecking(); // 애니메이션 검사 (테스트)
    }

    public override void Run(float dir)
    {
        if (plState == MainCharacterState.JUMP)
            return;
        else
        {
            moveDir = dir;
            prevMoveDir = 0;
            characterAni.SetBool("Run", true);
            if (plState == MainCharacterState.MOVE)
                characterAni.SetBool("Move", false);
            plState = MainCharacterState.RUN;

        }
    }

    public void RunUpdate()
    {
        if (currentSpeed <= 1) // !!!가속도 변수(인스펙터에서 제어 예정)
        {
            currentSpeed += 0.02f;
        }

        this.transform.Translate(new Vector2(moveDir, 0) * (mvSpeed * runSpeedMagnification) * Time.deltaTime * currentSpeed);
        this.transform.localScale = new Vector3(moveDir, 1, 1);
    }

    public void RunExit()
    {
        characterAni.SetBool("RunStop", true);
        mvSpeed = speed;
        if (characterAni.GetCurrentAnimatorStateInfo(0).IsName("RunStop") && characterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            characterAni.SetBool("RunStop", false);
            characterAni.SetBool("Run", false);
            plState = MainCharacterState.IDLE;
        }
    }

    public override void Move(float dir) // 이동코드 인풋
    {
        if (plState == MainCharacterState.IDLE || plState == MainCharacterState.MOVE)
        {
            moveDir = dir;
            plState = MainCharacterState.MOVE;
            characterAni.SetBool("Move", true); // Move 애니메이션 재생
        }
        else if (plState == MainCharacterState.JUMP) // 플레이어가 점프중일 때 (이동은 가능)
        {
            moveDir = dir;
            Moving();
        }
        else if (plState == MainCharacterState.RUN)
        {
            moveDir = dir;
            plState = MainCharacterState.MOVE;
            characterAni.SetBool("Move", true); // Move 애니메이션 재생
            characterAni.SetBool("Run", false); // Move 애니메이션 재생
        }
    }

    private void Moving() // 실제 물리 코드 (달리기 포함)
    {
        if (prevMoveDir == 0 || prevMoveDir == (int)moveDir)
        {
            prevMoveDir = (int)moveDir;
        }
        else // 방향전환
        {
            prevMoveDir = (int)moveDir;
            CharacterTurnStart(); // 캐릭터 턴 시작
        }

        if (currentSpeed <= 1) // !!!가속도 변수(인스펙터에서 제어 예정)
        {
            currentSpeed += 0.02f;
        }

        this.transform.Translate(new Vector2(moveDir, 0) * mvSpeed * Time.deltaTime * currentSpeed);
        this.transform.localScale = new Vector3(moveDir, 1, 1);
    }

    public override void Jump()
    {
        if (plState != MainCharacterState.JUMP)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            this.GetComponent<Animation>().enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = jumpSprite;
=======
>>>>>>> cc43e1a1ab69e05b4d09542d39d7adeaa2bf7f1a
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpScale, ForceMode2D.Impulse);
=======
            characterAni.SetBool("Jump", true);
>>>>>>> didxodnd
            plState = MainCharacterState.JUMP;
        }
    }

    public void JumpUpdate()
    {
        if (jumpDelay <= timeFlow && jumpPowerChk == false) // 점프 딜레이가 끝나면 점프하게
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpScale, ForceMode2D.Impulse);
            jumpPowerChk = true;
        }
        else
        {
            int layerMask = 1 << LayerMask.NameToLayer("Player"); // 플레이어를 제외한 나머지 레이어 마스크
            layerMask = ~layerMask;
            ray2D.origin = this.transform.position;
            //Debug.Log(playerFoot.position);

            Debug.DrawRay(ray2D.origin, Vector2.down, Color.red, 1f);

            hit2D = Physics2D.Raycast(ray2D.origin, Vector2.down, 1f, layerMask);
            Debug.Log(hit2D);

            if (hit2D && timeFlow >= jumpDelay + 0.5f)
            {
                if (hit2D.collider.tag == "Ground")
                {
                    characterAni.SetBool("Jump", true);
                    if (characterAni.GetCurrentAnimatorStateInfo(0).IsName("Jump") && characterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    {
                        timeFlow = 0;
                        jumpPowerChk = false;
                        plState = MainCharacterState.IDLE;
                    }
                }
            }
        }

        // 플레이어가 땅에 닿았을 경우 IDLE 상태로 돌아감
    }

    public override void Idle()
    {
<<<<<<< HEAD
       switch(plState)
        {
            case MainCharacterState.JUMP:
                return;
            case MainCharacterState.RUN:
                RunExit();
                return;
        }
=======
        if (plState == MainCharacterState.JUMP)
            return;
>>>>>>> cc43e1a1ab69e05b4d09542d39d7adeaa2bf7f1a

        // 플레이어 움직임이 멈추고 가만히 있을 때 초기화 되야하는 변수
        plState = MainCharacterState.IDLE;
        characterAni.SetBool("Move", false); // 움직임 코드 빠져나감
        characterAni.SetBool("Run", false);
        currentSpeed = 0; // 가속도 스피드 초기화
        prevMoveDir = 0; // 이전 이동 초기화
        moveDir = 0;
    }

    private void CharacterTurnStart()
    {
        characterAni.SetBool("Turn", true);
        mvSpeed = speed / 2f;
        currentSpeed = 0; // 가속도 리셋
    }

    private void AnimatorChecking()
    {
        if (characterAni.GetCurrentAnimatorStateInfo(0).IsName("Turn") && characterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            characterAni.SetBool("Turn", false);
            mvSpeed = speed;
        }

<<<<<<< HEAD
=======
        if (characterAni.GetCurrentAnimatorStateInfo(0).IsName("Jump") && characterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            characterAni.SetBool("Jump", false);
            mvSpeed = speed;
        }

>>>>>>> didxodnd
    }

    public override void ShowDebug()
    {
        MainDebug.instance.Addstruct<float>(0, "speed", speed);
        MainDebug.instance.Addstruct<float>(1, "jumpScale", jumpScale);
        MainDebug.instance.Addstruct<Vector2>(2, "position", transform.position);
        MainDebug.instance.Addstruct<Sprite>(3, "Sprite", gameObject.GetComponent<SpriteRenderer>().sprite);
        MainDebug.instance.Addstruct<MainCharacterState>(3, "MainCharacterState plState", plState);
        Debug.Log(MainDebug.instance.value_name.Length);
        /* for (int i = 0; i < MainDebug.instance.value_name.Length; i++)
         {
             MainDebug.instance.DebugText[i].text = MainDebug.instance.value_name[i] + " :" + MainDebug.instance.bool_value[i] + MainDebug.instance.float_value[i] + MainDebug.instance.string_value[i];
         }*/

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
        
=======

>>>>>>> cc43e1a1ab69e05b4d09542d39d7adeaa2bf7f1a
    }


}


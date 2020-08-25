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
    private Animator characterAni;

    //======================================================================================
    MainCharacterState plState; // 플레이어 현재 상태 FSM 적용 예정
    private float moveDir = 0; // 이동 방향
    private int prevMoveDir; // 이전 이동 방향
    private float currentSpeed = 0; // 가속도
    private float mvSpeed;

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
        switch(plState)
        {
            case MainCharacterState.IDLE:
                Idle();
                break;
            case MainCharacterState.MOVE:
                Moving(); // 물리 이동
                break;
            case MainCharacterState.JUMP:
                Jump();
                break;
        }
        AnimatorChecking(); // 애니메이션 검사 (테스트)
    }



    public override void Move(float dir)
    {
        if (plState == MainCharacterState.IDLE || plState == MainCharacterState.MOVE)
        {
            moveDir = dir;
            plState = MainCharacterState.MOVE;
            characterAni.SetBool("Move", true); // Move 애니메이션 재생
        }
    }

    private void Moving() // 실제 물리 코드
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

        this.transform.Translate(new Vector2(moveDir,0) * mvSpeed * Time.deltaTime * currentSpeed);
        this.transform.localScale = new Vector3(moveDir, 1, 1);
    }

    public override void Jump()
    {

    }

    public override void Idle()
    {
        // 플레이어 움직임이 멈추고 가만히 있을 때 초기화 되야하는 변수
        plState = MainCharacterState.IDLE;
        moveDir = 0;
        characterAni.SetBool("Move", false); // 움직임 코드 빠져나감
        currentSpeed = 0; // 가속도 스피드 초기화
        prevMoveDir = 0; // 이전 이동 초기화
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
    }
}

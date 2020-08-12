using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{

    private bool camFocusEventReference = false;
    public bool CamFocusEventReference
    {
        get { return camFocusEventReference; }
    }

    IEnumerator CamFocusEventSuccess()
    {
        camFocusEventReference = true;
        yield return null;
        camFocusEventReference = false;
        yield return null;
    }

    private GameObject mainFocus = null;
    public GameObject MainFocus
    {
        set
        {
            mainFocus = value;
            focus = mainFocus.transform;
        }
    }
    public Transform focus = null;
    private List<Transform> focusList = new List<Transform>(); // 포커스가 여러번 이동해야하는 카메라 이벤트

    private static MainCameraScript S;
    public static MainCameraScript SingleTon
    {
        get
        {
            return S;
        }
    }


    public Transform Focus // 단일 포커스 변경
    {
        set
        {
            STATE_MOVE = false;
            focus = value;
            StartCoroutine(FocusChange());
        }
    }

    public List<Transform> FocusList // 리스트 포커스 변경
    {
        set
        {
            STATE_MOVE = false;
            foreach(Transform g in value)
            {
                focusList.Add(g);
            }
            StartCoroutine(FocusChangeList());
        }
    }

    [SerializeField]
    private Vector3 focusFallPos = Vector3.zero;

    public Vector3 FocusFallPosition
    {
        set
        {
            focusFallPos = value;
            if (focus != null)
                this.transform.position = focus.transform.position + focusFallPos;
        }
    }

    private bool STATE_MOVE = true;

    private bool CAM_ACTION = false;
    public bool CamAction // 카메라 액션중인가?
    {
        get
        {
            return CAM_ACTION;
        }
    }

    private bool mainFoCng = true;
    public bool MainFocusChangeBool
    {
        set
        {
            mainFoCng = value;
        }
    }

    private bool FOCUS_CHANGE = true;
    private float focusChangeDelay = 1.0f;


    // Start is called before the first frame update
    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    private void FixedUpdate()
    {
        if (STATE_MOVE == true) // 포커스체인지 이동중이면 이동안함
        {
            Moving();
        }
    }

    public IEnumerator FocusMoving() // 포커스가 변하면 갑자기 이동하는 것이 아닌 천천히 이동
    {
        if (focus != null) // 포커스가 있어야 이동함
        {
            Vector3 endPos;
            Vector3 nowPos;
            float timeDelay = 0.3f;

            endPos = focus.position + focusFallPos;
            nowPos = this.transform.position;
            FOCUS_CHANGE = false;
            STATE_MOVE = false;
            while (true) // 이동이 끝나면 빠져나가는 루트를 만들어야함
            {
                nowPos = Vector3.Lerp(nowPos, endPos, 0.2f);
                if ((Vector3.Distance(nowPos, endPos) < 0.01f))
                {
                    this.transform.position = endPos;
                    break;
                }
                this.transform.position = nowPos;
                yield return new WaitForSeconds(timeDelay);
            }
        }

        StartCoroutine(CamFocusEventSuccess()); // 카메라 이동 끝나면 끝난 함수 실행
        FOCUS_CHANGE = true;
        STATE_MOVE = true;
        yield return null;
    }

    public IEnumerator FocusChange()
    {
        CAM_ACTION = true;
        StartCoroutine(FocusMoving());
        while (true)
        {
            if (FOCUS_CHANGE == true)
            {
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(focusChangeDelay);

        ChangeMainFocus();

        CAM_ACTION = false;
        yield return null;
    }

    public IEnumerator FocusChangeList() // 포커스 오브젝트가 리스트로 들어오면
    {
        CAM_ACTION = true;
        foreach (Transform g in focusList)
        {
            while (true)
            {
                if (FOCUS_CHANGE == true)
                {
                    focus = g;
                    StartCoroutine(FocusMoving());
                    break;
                }
                yield return null;
            }
            yield return new WaitForSeconds(focusChangeDelay);
        }
        focusList.Clear();

        ChangeMainFocus(); // 다시 메인으로 돌아온다(플레이어)

        CAM_ACTION = false;
        yield return null;
    }

    public void ChangeMainFocus()
    {
        focus = mainFocus.transform;
        StartCoroutine(FocusMoving());
    }

    void Moving()
    {
        if (focus != null) // 포커스가 있어야 이동함
        {
            this.transform.position = focus.transform.position + focusFallPos;
        }
    }
}

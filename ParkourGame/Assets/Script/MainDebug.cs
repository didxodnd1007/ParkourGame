using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainDebug : MonoBehaviour
{
    public GameObject target;
    public Text[] DebugText;
    public GameObject debugWindow;
    public string[] value_name;
    // public ArrayList value = new ArrayList();   
    public static MainDebug instance;
    [SerializeField] bool debug_On;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        DebugText = debugWindow.GetComponentsInChildren<Text>();
       
        value_name = new string[DebugText.Length];
}
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (debug_On)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0);

            if (Input.GetMouseButtonDown(0))
            {

                if (hit.transform.gameObject.GetComponent<debug>() != null)
                {
                    target = hit.transform.gameObject;
                    for (int i = 0; i < DebugText.Length; i++)
                    {
                        DebugText[i].text = "";
                    }
                }
            }

            target.SendMessage("ShowDebug", DebugText, SendMessageOptions.DontRequireReceiver);
        }
    }
   
    public void Addstruct<T>(int Nu0mber, string name, T _value)
    {
        var val = _value;
        DebugText[Nu0mber].text = name + " : " + val;
        // value_name[Nu0mber] = name;
        //float_value[Nu0mber] = _value;
    }

}
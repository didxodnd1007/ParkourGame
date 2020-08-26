using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainDebug : MonoBehaviour
{
    public GameObject target;
    public Text[] DebugText;
    public GameObject debugWindow;
<<<<<<< HEAD
    public string[] value_name ;
    public ArrayList value = new ArrayList();
   public static MainDebug instance;
    // Start is called before the first frame update
   
=======
    public string[] value_name;
    // public ArrayList value = new ArrayList();   
    public static MainDebug instance;
    [SerializeField] bool debug_On;
    // Start is called before the first frame update

>>>>>>> c031bce551902c3520bd80cb06a506787d7d8997
    private void Awake()
    {
        instance = this;
        DebugText = debugWindow.GetComponentsInChildren<Text>();
<<<<<<< HEAD
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero,0);
        
        if (Input.GetMouseButtonDown(0))
        {

            if (hit.transform.gameObject != null)
            {
                target = hit.transform.gameObject;
                for (int i = 0; i < DebugText.Length; i++)
                {
                    DebugText[i].text = "";
                }
            }
        }
        target.SendMessage("ShowDebug",DebugText,SendMessageOptions.DontRequireReceiver);
       
    }
   public void Addstruct(int Nu0mber,string name, float _value)
    {

        value_name[Nu0mber] = name;
        value.Insert(Nu0mber, _value);
        
        value.RemoveRange(Nu0mber + 1, DebugText.Length);
    }


    public void Addstruct(int Nu0mber,string name, bool _value)
    {

        value_name[Nu0mber] = name;
        value.Insert(Nu0mber, _value);
        
        value.RemoveRange(Nu0mber + 1, DebugText.Length);
    }
    public void Addstruct(int Nu0mber, string name, string _value)
    {

        value_name[Nu0mber] = name;
        value.Insert(Nu0mber,_value);
       
        value.RemoveRange(Nu0mber + 1, DebugText.Length);
    }

}
=======
       
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
>>>>>>> c031bce551902c3520bd80cb06a506787d7d8997

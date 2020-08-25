using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainDebug : MonoBehaviour
{
    public GameObject target;
    public Text[] DebugText;
    public GameObject debugWindow;
    public string[] value_name ;
    public ArrayList value = new ArrayList();
   public static MainDebug instance;
    // Start is called before the first frame update
   
    private void Awake()
    {
        instance = this;
        DebugText = debugWindow.GetComponentsInChildren<Text>();
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

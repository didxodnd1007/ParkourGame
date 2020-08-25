using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainDebug : MonoBehaviour
{
    public GameObject target;
    public Text DebugText;
    public List<string> value_name = new List<string>();
    public ArrayList value = new ArrayList();
   public static MainDebug instance;
    // Start is called before the first frame update
   
    private void Awake()
    {
        instance = this;
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
        Debug.Log(hit.transform.gameObject.name);
        if (Input.GetMouseButtonDown(0))
        {
           
            target = hit.transform.gameObject;
            DebugText.text = "";
        }
        target.SendMessage("ShowDebug",DebugText,SendMessageOptions.DontRequireReceiver);
       
    }
   public void Addstruct(string name, float _value)
    {
        value_name.Add(name);       
        value.Add(_value);

    }
    public void Addstruct(string name, bool _value)
    {
        value_name.Add(name);
        value.Add(_value);
    }
    public void Addstruct(string name, string _value)
    {
        value_name.Add(name);
        value.Add(_value);
    }
}

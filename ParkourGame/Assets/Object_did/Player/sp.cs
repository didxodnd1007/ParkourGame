using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp : MonoBehaviour
{

    public Sprite r1;
    public Sprite r2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("r1");
            this.GetComponent<SpriteRenderer>().sprite = r1;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("r2");
            this.GetComponent<SpriteRenderer>().sprite = r2;
        }
    }
}

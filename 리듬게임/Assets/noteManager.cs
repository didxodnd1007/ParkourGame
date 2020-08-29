using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteManager : MonoBehaviour
{
    public int bpm;
    double currentTime = 0d;
    public Transform tfnoteAppear;
    public GameObject gonote;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(gonote, tfnoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            currentTime -= 60d / bpm;
        }
    }
}

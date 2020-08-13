using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enamy : MonoBehaviour
{
    public float angle;
    public GameObject eye;
    public GameObject Player;
    public LayerMask layer;
    public Transform a, b;
    public Transform target;
    public Transform Previoustarget;
    public float z ;
    public float speed;
    public bool Playeron;
    public bool set;
    // Start is called before the first frame update
    void Start()
    {
        target = b;
        Previoustarget = a;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Player.transform.position - eye.transform.position;
        if (Vector3.Distance(Player.transform.position, transform.position) <= 10)
        {
            //Debug.Log("거리안에 들어옴");
            // Debug.Log(Vector3.Angle(dir, eye.transform.right));
            if (Vector3.Angle(dir, eye.transform.right) <= angle * 0.5f)
            {
                //Debug.Log("시야각안에 들어옴");
                RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, dir, 10, layer);
                Debug.DrawRay(eye.transform.position, dir, Color.red);
                if (hit.transform.gameObject.tag == "Player")
                {
                    Playeron = true;
                    set = false;
                    Previoustarget = target;
                    target = null;

                    target = Player.transform;
                }


            }

        }
        else
        {
            Playeron = false;
        }
       

        transform.position = Vector2.MoveTowards(transform.position,new Vector2(target.position.x,transform.position.y),speed*Time.deltaTime);        
        if (Playeron==false)
        {
            if (set == false)
            {
                set = true;
                target = b;
                Previoustarget = a;
            }
            if (transform.position == new Vector3(b.position.x, transform.position.y))
            {
                target = a;
                Previoustarget = b;
                //eye.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (transform.position == new Vector3(a.position.x, transform.position.y))
            {
                target = b;
                Previoustarget = a;
                //eye.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        EnamyRotation(target);
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 dir = Quaternion.Euler(0, 0, -angle*0.5f) * eye.transform.right;
    
    
        Handles.color = new Color(1,1,1,0.2f);
        Handles.DrawSolidArc(eye.transform.position, Vector3.forward, dir, angle, 10);
    }
    void EnamyRotation(Transform target)
    {
        Vector2 Position = new Vector2(target.position.x, target.position.y);
        Vector2 direction = new Vector2(transform.position.x, transform.position.y) - Position;
        float Rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
      
        z = Rotation;
        if (z < 180&& z > 0)
        {
            eye.transform.rotation = Quaternion.Euler(0, 180, 0);
            //Debug.Log(z);
        }
        if (z > -180&& z < 0)
        {
            eye.transform.rotation = Quaternion.Euler(0, 0, 0);
            //Debug.Log(z);
        }
    }
}

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
    public Vector3 a, b;
    public Vector3 target;
    public Vector3 Previoustarget;
    public float z ;
    public float speed;
    public bool Playeron;
    public bool set;
    bool move;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        move = true;
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
                   

                    target = Player.transform.position;
                }


            }

        }
        else
        {
            Playeron = false;
        }

        if (move)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), speed * Time.deltaTime);
        }       
        if (Playeron==false)
        {
            if (set == false)
            {
                set = true;
                target = b;
                Previoustarget = a;
            }
            if (transform.position == new Vector3(b.x, transform.position.y))
            {
                move = false;
                anim.SetBool("Turn", true);
                target = a;
                Previoustarget = b;
                //eye.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (transform.position == new Vector3(a.x, transform.position.y))
            {
                move = false;
                anim.SetBool("Turn", true);
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
    void EnamyRotation(Vector3 target)
    {
        Vector2 Position = new Vector2(target.x, target.y);
        Vector2 direction = new Vector2(transform.position.x, transform.position.y) - Position;
        float Rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
      
        z = Rotation;
        if (z < 180&& z > 0)
        {
            eye.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Turn R") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
              
            }
            //Debug.Log(z);
        }
        if (z > -180&& z < 0)
        {
            eye.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Turn R") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            //Debug.Log(z);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Turn R") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            move = true;
            anim.SetBool("Turn", false);
        }
    }
}

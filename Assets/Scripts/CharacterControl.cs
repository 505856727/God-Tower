using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float sqrtspeed;
    private Animator anim;
    private Rigidbody2D rigid;
    private Database testdatabase;
    // Start is called before the first frame update

    private void Awake()
    {
        GameObject go = Resources.Load("Database") as GameObject;
        testdatabase = go.GetComponent<Database>();
        print(testdatabase.test);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sqrtspeed = Mathf.Sqrt(speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CharacterMove();
    }

    void CharacterMove()
    {
        
        if(Input.GetKey(KeyCode.D))
        {
            if(Input.GetKey(KeyCode.W))
            {
                AnimRight();
                //transform.Translate(sqrtspeed * Time.deltaTime, sqrtspeed * Time.deltaTime, 0);
                rigid.velocity = new Vector3(sqrtspeed, sqrtspeed, 0);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                AnimRight();
                //transform.Translate(sqrtspeed * Time.deltaTime, -sqrtspeed * Time.deltaTime, 0);
                rigid.velocity = new Vector3(sqrtspeed, -sqrtspeed, 0);
            }
            else
            {
                AnimRight();
                //transform.Translate(speed * Time.deltaTime, 0, 0);
                rigid.velocity = new Vector3(speed, 0, 0);
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W))
            {
                AnimLeft();
                //transform.Translate(-sqrtspeed * Time.deltaTime, sqrtspeed * Time.deltaTime, 0);
                rigid.velocity = new Vector3(-sqrtspeed, sqrtspeed, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                AnimLeft();
                //transform.Translate(-sqrtspeed * Time.deltaTime, -sqrtspeed * Time.deltaTime, 0);
                rigid.velocity = new Vector3(-sqrtspeed, -sqrtspeed, 0);
            }
            else
            {
                AnimLeft();
                //transform.Translate(-speed * Time.deltaTime, 0, 0);
                rigid.velocity = new Vector3(-speed, 0, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                AnimUp();
                //transform.Translate(0, speed*Time.deltaTime, 0);
                rigid.velocity = new Vector3(0, speed, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                AnimDown();
                //transform.Translate(0, -speed * Time.deltaTime, 0);
                rigid.velocity = new Vector3(0, -speed, 0);
            }
            else
            {
                AnimStand();
                rigid.velocity = Vector3.zero;
            }
        }

    }

    void AnimUp()
    {
        anim.Play("up");
    }

    void AnimDown()
    {
        anim.Play("down");
    }

    void AnimLeft()
    {
        anim.Play("left");
    }

    void AnimRight()
    {
        anim.Play("right");
    }

    void AnimStand()
    {
        anim.SetTrigger("stand");
    }  
}

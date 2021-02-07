using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScript : MonoBehaviour
{
    bool released = false;
    bool active = false;
    bool hooked = false;
    // Start is called before the first frame update
    private Transform pivotPoint;
    private Transform arm;
    private Transform hand;
    public GameObject Dog = null;

    Rigidbody2D rb;
    void Start()
    {
        //pivotPoint = GameObject.Find("RotatePoint").transform;
        arm = GameObject.Find("Crane Hand 2.1").transform;
        hand = GameObject.Find("Hand").transform;

        rb = Dog.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - Dog.transform.position).magnitude < 10)
        {
            active = true;
            faceMouse();
        }

        /*if (released == false && (hand.position - Dog.transform.position).magnitude < 1)
        {
            hooked = true;
            //Debug.Log(hooked);
            //Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            //cam.transform.parent = transform;
        }
        else
        {
            //Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            //cam.transform.parent = Dog.transform;
        }*/

        if (Dog.GetComponent<CircleCollider2D>().IsTouching(transform.GetComponent<BoxCollider2D>()))
        {
            //if (!released)
            {
                rb.velocity -= rb.velocity * 0.05f;
                var delta = hand.position - Dog.transform.position;

                //Aproach 
                if (delta.magnitude < 1.5f && !released)
                {
                    Dog.transform.position = hand.transform.position;
                    rb.velocity = new Vector2(0, 0);
                }
                else
                {
                    var vel = delta.normalized * 500 * Time.deltaTime;
                    if (!released)
                    {
                        rb.velocity += new Vector2(vel.x, vel.y);
                    }
                    else
                    {
                        rb.velocity += rb.velocity * Time.deltaTime;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && active)
        {
            released = true;
        }
    }

    void faceMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(
            arm.position.x - mousePos.x,
            arm.position.y - mousePos.y);

        arm.up = direction;

        Vector2 dist = new Vector2(
            arm.position.x - mousePos.x,
            arm.position.y - mousePos.y);

        Vector2 distFromArm = new Vector2(
            arm.position.x - hand.position.x,
            arm.position.y - hand.position.y);

        float diff = dist.magnitude - distFromArm.magnitude;

        if (dist.magnitude < distFromArm.magnitude) //mouse is closer to pivot point than arm
        {   
            if (arm.gameObject.transform.localScale.y > 0.2f)
            {
                arm.gameObject.transform.localScale += new Vector3(0, diff, 0);
            }
        }
        else if (dist.magnitude > distFromArm.magnitude)  //arm is closer to pivot point than mouse
        {
            if (dist.magnitude < 5 && diff > 0.2)
            {
                arm.gameObject.transform.localScale += new Vector3(0, 0.05f, 0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScript : MonoBehaviour
{
    bool released = false;
    bool active = false;
    bool hooked = false;

    private Transform arm;
    private Transform hand;
    public GameObject Dog;
    public GameObject cam;

    inputProsessor ip;

    Rigidbody2D rb;
    AudioSource audio;
    void Start()
    {
        arm = GameObject.Find("Crane Hand 2.1").transform;
        hand = GameObject.Find("Hand").transform;
        cam = GameObject.Find("Main Camera");

        ip = GameObject.Find("input_dashboard").GetComponent<inputProsessor>();
        rb = Dog.GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }
    IEnumerator release()
    {
        released = true;
        cam.transform.parent = Dog.transform;
        cam.transform.localPosition = new Vector3(0, 0, -10);
        yield return new WaitForSeconds(2.0f);
        released = false;
        audio.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - Dog.transform.position).magnitude < 10)
        {
            active = true;
            ip.setCraneMode();
            faceJoyStick();
            //faceMouse();
        }


        if (Dog.GetComponent<CircleCollider2D>().IsTouching(transform.GetComponent<BoxCollider2D>()))
        {
            rb.velocity -= rb.velocity * 0.05f;
            var delta = hand.position - Dog.transform.position;

            //Aproach 
            if (delta.magnitude < 1.5f && !released)
            {
                audio.Play(0);

                Dog.transform.position = hand.transform.position;
                rb.velocity = new Vector2(0, 0);

                cam.transform.parent = transform;
                cam.transform.localPosition = new Vector3(0, 0, -10);
            }
            else
            {
                var vel = delta.normalized * 200 * Time.deltaTime;
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

        if (ip.RotteryE_held && active)
        {
            StartCoroutine(release());
        }
    }

    

    float curX = 0.5f;
    float curY = 0.5f;
    void faceJoyStick()
    {
        var x = ip.x_joystick;
        var y = ip.y_joystick;

        var dir = new Vector2(y, x);
        var mag = Mathf.Clamp(dir.magnitude, 0.2f, 1.0f);
        arm.gameObject.transform.localScale = new Vector3(1, mag, 1);

        arm.up = dir;



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

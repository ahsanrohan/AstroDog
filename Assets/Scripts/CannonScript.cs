using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool shot = false;
    bool active = false;
    public float power;
    private Transform shootPosition;
    private Transform basePosition;
    private GameObject Dog;

    public float rotateClockwise_limit = -0.5f;
    public float rotateCounterClockwise_limit = 0.5f;

    public static bool debugKeyboard = true;

    inputProsessor ip;
    Rigidbody2D rb;

    AudioSource audio;

    void Start()
    {
        Dog = GameObject.Find("bubble");
        rb = Dog.GetComponent<Rigidbody2D>();

        //GetComponent<BoxCollider2D>().i

        ip = GameObject.Find("input_dashboard").GetComponent<inputProsessor>();

        basePosition = transform.Find("CannonBase").transform;
        shootPosition = transform.Find("CannonBase/Cannon/CannonHead").transform;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = ip.x_joystick;
        var y = ip.y_joystick;

        if (debugKeyboard)
        {
            debugUpdate();
        }

        if (Dog.GetComponent<CircleCollider2D>().IsTouching(transform.GetComponent<BoxCollider2D>()))
        {
            ip.setCannonMode();
            //Soft Capture
            if (!shot)
            {
                rb.velocity -= rb.velocity * 0.1f;
                var delta = shootPosition.position - Dog.transform.position;

                //Snap Mode
                if (delta.magnitude < 0.2)
                {
                    Dog.transform.position = shootPosition.transform.position;
                    rb.velocity = new Vector2(0, 0);

                    //Only Shoot If It Is within snap
                    if (ip.RotteryE_held)
                        StartCoroutine(shoot());
                }
                else
                {
                    var vel = delta.normalized * 100 * Time.deltaTime;
                    rb.velocity += new Vector2(vel.x, vel.y);
                }
            }


            //ROATE
            var b = basePosition.parent.rotation;
            if (Mathf.Abs(ip.x_joystick) > 0.1f)
            {
                if (
                    !((basePosition.parent.rotation.z < rotateClockwise_limit)          && ip.x_joystick < 0) &&
                    !((basePosition.parent.rotation.z > rotateCounterClockwise_limit)   && ip.x_joystick > 0)
                   )
                    basePosition.parent.Rotate(Vector3.forward, -ip.x_joystick, Space.Self);
                
            }

            //POWER
            //power = (ip.getRottery_counter()/32.0f)*20;

            
                

        }
    }

    private void debugUpdate()
    {
        if (Dog.GetComponent<CircleCollider2D>().IsTouching(transform.GetComponent<BoxCollider2D>()))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                StartCoroutine(shoot());
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (basePosition.parent.rotation.z < rotateCounterClockwise_limit)
                {
                    basePosition.parent.Rotate(Vector3.forward, 15, Space.Self);
                }
                //Debug.Log(basePosition.rotation);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (basePosition.parent.rotation.z > rotateClockwise_limit)
                {
                    basePosition.parent.Rotate(Vector3.forward, -15, Space.Self);
                }
                //Debug.Log(basePosition.rotation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dog = collision.collider.gameObject;
        shot = false;

    }
    IEnumerator shoot()
    {
        // GameObject dog = Instantiate(Dog, shootPosition.position, Quaternion.identity);

        GameObject cannon = GameObject.Find("Cannon");
        Animator manimator = cannon.GetComponent<Animator>();
        manimator.SetTrigger("Shoot");

        yield return new WaitForSeconds(0.35f);

        //  Debug.Log(GetComponent<float>.power);
        //Debug.Log(shootPosition.forward * power);
        //Vector2 forceVec = new Vector2(power, power);
        Vector2 shootDir = new Vector2(shootPosition.position.x - basePosition.position.x, shootPosition.position.y - basePosition.position.y);
        rb.velocity = (power * shootDir);
        shot = true;
        audio.Play(0);

        yield return new WaitForSeconds(1);

        shot = false;
    }
}

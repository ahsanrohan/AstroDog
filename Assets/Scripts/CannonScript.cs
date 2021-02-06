using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool shot = false;
    public float power;
    private Transform shootPosition;
    private Transform basePosition;
    public GameObject Dog = null;

    public float rotateClockwise = -0.5f;
    public float rotateCounterClockwise = 0.5f;

    public static bool debugKeyboard = true;

    inputProsessor ip;
    void Start()
    {
        ip = GameObject.Find("input_dashboard").GetComponent<inputProsessor>();
        basePosition = transform.Find("CannonBase").transform;
        shootPosition = transform.Find("CannonBase/Cannon/CannonHead").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(debugKeyboard)
        {
            debugUpdate();
            return;
        }



    }

    private void debugUpdate()
    {
        if (Dog.GetComponent<CircleCollider2D>().IsTouching(transform.GetComponent<BoxCollider2D>()))
        {
            if (!shot)
            {
                Dog.transform.position += (shootPosition.position - Dog.transform.position);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                shoot();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (basePosition.parent.rotation.z < rotateCounterClockwise)
                {
                    basePosition.parent.Rotate(Vector3.forward, 15, Space.Self);
                }
                Debug.Log(basePosition.rotation);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (basePosition.parent.rotation.z > rotateClockwise)
                {
                    basePosition.parent.Rotate(Vector3.forward, -15, Space.Self);
                }
                Debug.Log(basePosition.rotation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dog = collision.collider.gameObject;
        shot = false;

    }
    private void shoot()
    {
        // GameObject dog = Instantiate(Dog, shootPosition.position, Quaternion.identity);

        Rigidbody2D rb = Dog.GetComponent<Rigidbody2D>();
        GameObject cannon = GameObject.Find("Cannon");
        Animator manimator = cannon.GetComponent<Animator>();
        manimator.SetTrigger("Shoot");
        //  Debug.Log(GetComponent<float>.power);
        //Debug.Log(shootPosition.forward * power);
        //Vector2 forceVec = new Vector2(power, power);
        Vector2 shootDir = new Vector2(shootPosition.position.x - basePosition.position.x, shootPosition.position.y - basePosition.position.y);
        rb.velocity = (power * shootDir);
        shot = true;
    }
}

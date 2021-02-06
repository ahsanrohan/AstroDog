using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool shot = false;
    [SerializeField] static float power;
    private Transform shootPosition;
    private Transform basePosition;
    public GameObject Dog = null;

    [SerializeField] float rotateClockwise = -0.5f;
    [SerializeField] float rotateCounterClockwise = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        basePosition = transform.Find("CannonBase").transform;
        shootPosition = transform.Find("CannonBase/Cannon/CannonHead").transform;    
        if (!shot)
        {
            Dog.transform.position = shootPosition.position;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            shoot();
        }
        //float moveHorizontal = Input.GetAxis("Horizontal");
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
        //Debug.Log(shootPosition.parent.position);
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
        rb.velocity = (power*shootDir);
        shot = true;
    }
}

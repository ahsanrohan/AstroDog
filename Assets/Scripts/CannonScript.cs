using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool shot = false;
    [SerializeField] float power = 10;
    private Transform shootPosition;
    private Transform basePosition;
    public GameObject Dog = null;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        basePosition = GameObject.Find("CannonBase").transform;
        shootPosition = GameObject.Find("CannonHead").transform;    
        if (!shot)
        {
            Dog.transform.position = shootPosition.position;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            shoot();
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.A))
        {
            basePosition.parent.Rotate(Vector3.forward, 15, Space.Self);
            Debug.Log(basePosition.rotation);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            basePosition.parent.Rotate(Vector3.forward, -15, Space.Self);
            Debug.Log(basePosition.rotation);
        }
        //Debug.Log(shootPosition.parent.position);
    }

    private void shoot()
    {
        // GameObject dog = Instantiate(Dog, shootPosition.position, Quaternion.identity);

        GameObject dog = GameObject.Find("bubble");
        Rigidbody2D rb = dog.GetComponent<Rigidbody2D>();
        //Debug.Log(power);
        //Debug.Log(shootPosition.forward * power);
        //Vector2 forceVec = new Vector2(power, power);
        Vector2 shootDir = new Vector2(shootPosition.position.x - basePosition.position.x, shootPosition.position.y - basePosition.position.y);
        rb.velocity = (power*shootDir);
        shot = true;
    }
}

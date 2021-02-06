using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float power = 10;
    private Transform shootPosition;
    private Transform basePosition;
    public GameObject Dog = null;
    void Start()
    {
        basePosition = gameObject.transform.Find("CannonBase");
        shootPosition = basePosition.transform.Find("CannonHead");    
    }

    // Update is called once per frame
    void Update()
    {
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

        //Debug.Log(shootPosition.parent.position);
    }

    private void shoot()
    {
        GameObject dog = Instantiate(Dog, shootPosition.position, Quaternion.identity);
        Rigidbody2D rb = dog.GetComponent<Rigidbody2D>();
        //Debug.Log(power);
        //Debug.Log(shootPosition.forward * power);
        //Vector2 forceVec = new Vector2(power, power);
        rb.AddForce(new Vector2(0, power));
    }
}

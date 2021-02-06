using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float power = 10;
    private Transform shootPosition;
    public GameObject Dog = null;
    void Start()
    {
        shootPosition = gameObject.transform.Find("CannonHead");    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            shoot();
        }
        //Debug.Log(shootPosition.parent.position);
    }

    private void shoot()
    {
        GameObject dog = Instantiate(Dog, shootPosition.position, Quaternion.identity);
        Rigidbody2D rb = dog.GetComponent<Rigidbody2D>();
        //Debug.Log(power);
        //Debug.Log(shootPosition.forward);
        rb.velocity = power * shootPosition.forward;
    }
}

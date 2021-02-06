using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float spd = 10;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHoriz, moveVert);

        rb.AddForce(movement * spd);
    }
}

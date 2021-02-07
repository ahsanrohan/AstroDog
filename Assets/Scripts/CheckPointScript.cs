using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    inputProsessor ip;
    Rigidbody2D rb;
    GameObject bubble;

    public int numberOfCheckPoints;
    public int progress;

    public bool attachedToObject = false;
    public GameObject[] c;
    void Start()
    {
        bubble = GameObject.Find("bubble");
        rb = bubble.GetComponent<Rigidbody2D>();
        ip = GameObject.Find("input_dashboard").GetComponent<inputProsessor>();

        c = new GameObject[numberOfCheckPoints];

        for (int i = 0; i < numberOfCheckPoints; i++)
        {
            c[i] = GameObject.Find("c" + i.ToString());
            Debug.Log("C" + i.ToString());
        }

        progress = -1;
    }

    

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].GetComponent<BoxCollider2D>().IsTouching(bubble.GetComponent<CircleCollider2D>()))
            {
                if(progress < i)
                {
                    progress = i;
                }
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public int bound = 30;
    public float step = 1.0f;
    private int current = 0;
    private bool goingUp = true;
    [SerializeField] bool vertical = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(current > bound)
        {
            goingUp = false;
        }
        if(current < 0)
        {
            goingUp = true;
        }
        if (goingUp)
        {
            if (vertical)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + step * Time.deltaTime * 2);
                current++;
            }
            else
            {
                transform.position = new Vector2(transform.position.x + step * Time.deltaTime, transform.position.y);
                current++;
            }
        }
        else
        {
            if (vertical)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - step * Time.deltaTime * 2);
                current--;
            }
            else
            {
                transform.position = new Vector2(transform.position.x - step * Time.deltaTime, transform.position.y);
                current--;
            }
        }
    }
}

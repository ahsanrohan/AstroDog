using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform pivotPoint;
    private Transform arm;
    private Transform hand;
    void Start()
    {
        pivotPoint = GameObject.Find("RotatePoint").transform;
        arm = GameObject.Find("crane hand 2").transform;
        hand = GameObject.Find("Hand").transform;
    }

    // Update is called once per frame
    void Update()
    {
        faceMouse();
    }

    void faceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(
            mousePos.x - hand.position.x,
            mousePos.y - hand.position.y);

        float angle = Vector2.Angle(mousePos, hand.position);
        Debug.Log(angle);

        if (Vector2.Angle(mousePos, hand.position) > 10)
        {
            arm.RotateAround(pivotPoint.position, arm.forward, 1);
        }

        if (direction.magnitude > 1)
        {
            //arm.RotateAround(pivotPoint.position, arm.forward, 1);
        }
        
        //Debug.Log(arm);
        //arm.up = direction;
    }
}

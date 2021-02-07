using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;

    public CheckPointScript cps;

    [SerializeField] private float zoomLerpSpeed;
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;

        cps = GameObject.Find("CheckPoint").GetComponent<CheckPointScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float zoomData = 0;
        var a = transform.parent.GetComponent<Rigidbody2D>();
        if (a != null)
        {
            zoomData = transform.parent.GetComponent<Rigidbody2D>().velocity.magnitude;

        }
            


        float tempPower = 60;
        if (cps.props[cps.progress].tag == "Cannon")
        {
            tempPower = (cps.props[cps.progress].GetComponent<CannonScript>().power * 6) + 1;
        }
        zoomFactor = ((zoomData / tempPower) * 6) - 2;
        targetZoom += zoomFactor;

        targetZoom = Mathf.Clamp(targetZoom, 7.5f, 15f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}

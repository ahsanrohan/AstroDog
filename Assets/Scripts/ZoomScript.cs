using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;

    [SerializeField] private float zoomLerpSpeed;
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float zoomData = transform.parent.GetComponent<Rigidbody2D>().velocity.magnitude;
        zoomFactor = ((zoomData / 30) * 6) - 2;
        targetZoom += zoomFactor;

        targetZoom = Mathf.Clamp(targetZoom, 7.5f, 15f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;

    [SerializeField] private float zoomLerpSpeed = 10;
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float zoomData = transform.parent.GetComponent<Rigidbody2D>().velocity.magnitude;
        zoomFactor = map(zoomData, 0, -3, 100, 3);
        Debug.Log("zoomfactor" + zoomFactor);
        targetZoom += zoomFactor;

        targetZoom = Mathf.Clamp(targetZoom, 7.5f, 15f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        Debug.Log("targetzoom" + targetZoom);
    }

    public float map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}

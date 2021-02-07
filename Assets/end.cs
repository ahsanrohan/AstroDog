using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float duration = 10f;
    private float timePassed;
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > duration)
        {
            Application.Quit();
        }
    }
}

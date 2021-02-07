using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterTime : MonoBehaviour
{
    [SerializeField] private float delay = 34f;
    [SerializeField] private string scene;

    private float timePassed;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > delay)
        {
            SceneManager.LoadScene(scene);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartBoyGirl : MonoBehaviour
{

    private int whom = 0;
    [SerializeField]
    private string boy;
    [SerializeField]
    private string girl;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            whom = 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            whom = 2;
        }

        if(whom == 1)
        {
            SceneManager.LoadScene(boy);

        }
        else if(whom == 2)
        {
            SceneManager.LoadScene(girl);

        }
    }
}

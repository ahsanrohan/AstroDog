using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogChooser : MonoBehaviour
{
    // Start is called before the first frame update
    int chooser = 1;
    GameObject boy;
    GameObject girl;
    void Start()
    {
        boy = GameObject.Find("BoyDog");
        girl = GameObject.Find("GirlDog");

        if (chooser == 1)
        {
            boy.SetActive(true);
            girl.SetActive(false);
        }
        if (chooser == 2)
        {
            boy.SetActive(false);
            girl.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

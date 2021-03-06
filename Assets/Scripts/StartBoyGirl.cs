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
    inputProsessor ip;

    float timer = 0;
    AudioSource audio;
    void Start()
    {
        ip = GameObject.Find("input_dashboard").GetComponent<inputProsessor>();
        
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        ip.setGenderMode();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            whom = 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            whom = 2;
        }


        whom = ip.gender;

        if(whom == 1)
        {
            if (timer == 0)
            {
                audio.Play(0);
            }
            timer += Time.deltaTime;
            //WaitForSeconds(2.0f);
            if (timer >= 2.5)
            {
                ip.close();
                SceneManager.LoadScene(boy);
            }
        }
        else if(whom == 2)
        {
            if (timer == 0)
            {
                audio.Play(0);
            }
            timer += Time.deltaTime;
            //yield return new WaitForSeconds(2.0f);
            if (timer >= 2.5)
            {
                ip.close();
                SceneManager.LoadScene(girl);
                
            }

        }
    }
}

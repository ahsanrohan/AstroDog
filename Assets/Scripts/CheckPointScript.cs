using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    inputProsessor ip;
    Rigidbody2D rb;
    GameObject bubble;

    public int numberOfCheckPoints;
    public int progress;

    public bool attachedToObject = false;
    public GameObject[] checkPoints;
    public GameObject[] props;
    [SerializeField] string sceneToLoad;

    AudioSource audio;
    float timer;
    void Start()
    {
        bubble = GameObject.Find("bubble");
        rb = bubble.GetComponent<Rigidbody2D>();
        ip = GameObject.Find("input_dashboard").GetComponent<inputProsessor>();

        checkPoints = new GameObject[numberOfCheckPoints];

        for (int i = 0; i < numberOfCheckPoints; i++)
        {
            checkPoints[i] = GameObject.Find("c" + i.ToString());
        }

        progress = 0;
        timer = 0;
        audio = GetComponent<AudioSource>();
    }

    

    // Update is called once per frame
    void Update()
    {
        //PROGRESS UPDATE
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (checkPoints[i].GetComponent<BoxCollider2D>().IsTouching(bubble.GetComponent<CircleCollider2D>()))
            {
                if(progress < i+1)
                {
                    progress = i+1;
                }
            }
        }


        //RESET TEMP
        //TODO SWICH OVER TO BUTTON
        if (ip.joystrick_held)
        {
            rb.velocity = Vector3.zero;
            bubble.transform.position = props[progress].transform.position;
        }


        //Update Arrow
        if(progress < checkPoints.Length)
        {
            Vector3 direction = checkPoints[progress].transform.position - bubble.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
          
            ip.angle = (byte)((180 - angle) / 45);

        } else
        {
            if (timer == 0)
            {
                audio.Play(0);
            }
            timer += Time.deltaTime;
            if (timer >= 1.5)
            {
                ip.close();
                SceneManager.LoadScene(sceneToLoad);
            }
            //END EACHED
        }
    }
}

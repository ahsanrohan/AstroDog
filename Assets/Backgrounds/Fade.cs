using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fader;
    // Start is called before the first frame update
    void Start()
    {
        fader.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        fader.CrossFadeAlpha(1, 1, false);
        fader.CrossFadeAlpha(0, 1, false);
    }
}

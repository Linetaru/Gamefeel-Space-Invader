using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubliminalPicture : MonoBehaviour
{
    public bool isDisplay = false;
    public float displayTimae = 0.5f;
    private float time;
    [SerializeField] private Image sublimImage;

    // Start is called before the first frame update
    void Start()
    {
        sublimImage.enabled = false;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Image is display");
            isDisplay = true;
        }
        */
        if (GameManager.instance.Feature2SublimImage)
        {
            if (isDisplay)
            {
                sublimImage.enabled = true;
                time += Time.deltaTime;
                if (time >= displayTimae)
                {
                    isDisplay = false;
                    time = 0f;
                }
            }
            else
            {
                sublimImage.enabled = false;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource heartSound;
    public float timeToDecrease = 5;
    public bool canPlaySound;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetHeartSoundPitch()
    {
        canPlaySound = true;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlaySound)
        {
            if (!heartSound.isPlaying)
            heartSound.Play();
        //timeToDecrease = distance;
        
            if (heartSound.pitch > 0)
            {
                heartSound.pitch -= Time.deltaTime * (1 / timeToDecrease);
            }
            else
            {
                heartSound.Stop();
                heartSound.pitch = 1;
                canPlaySound = false;
            }
        }
    }
}

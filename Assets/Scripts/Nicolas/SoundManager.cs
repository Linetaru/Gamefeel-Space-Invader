using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource heartSound;
    public float timeToDecrease = 5;
    private bool canPlaySound;

    public AudioSource crySound1;
    public AudioSource crySound2;
    public AudioSource crySound3;

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

    public void PlayCrySound()
    {
        int random = Random.Range(1, 4);
        switch(random)
        {
            case 1:
                if (!crySound1.isPlaying && !crySound2.isPlaying && !crySound3.isPlaying)
                    crySound1.Play();
                break;
            case 2:
                if (!crySound1.isPlaying && !crySound2.isPlaying && !crySound3.isPlaying)
                    crySound2.Play();
                break;
            case 3:
                if (!crySound1.isPlaying && !crySound2.isPlaying && !crySound3.isPlaying)
                    crySound3.Play();
                break;
        }
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

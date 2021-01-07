using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource heartSound;
    public float startingPitch = 4;
    public float timeToDecrease = 5;
    private GameObject bullet;

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

    public void SetHeartSoundPitch(float distance, GameObject bulletObject = null)
    {
        bullet = bulletObject;
        if (!heartSound.isPlaying)
            heartSound.Play();
        //timeToDecrease = distance;
        if (heartSound.pitch > 0)
        {
            heartSound.pitch -= Time.deltaTime * (1 / distance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet == null)
        {
            heartSound.Stop();
            heartSound.pitch = 1;
        }
    }
}

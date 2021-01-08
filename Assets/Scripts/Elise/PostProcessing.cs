using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{

    public PostProcessVolume volume;
    public ColorGrading colorSatur;
    public Grain grain;

    public GameObject packEnnemy;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out colorSatur);
        volume.profile.TryGetSettings(out grain);

        colorSatur.saturation.value = 0;
        grain.intensity.value = 0;
    }


    void Update()
    {

    }

}

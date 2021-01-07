using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{ 

    // Desired duration of the shake effect
    public float shakeDurationMax = 2.0f;
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = Camera.main.transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            Camera.main.transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            Camera.main.transform.localPosition = initialPosition;
        }
    }
    public void TriggerShake()
    {
        shakeDuration = shakeDurationMax;
    }
}

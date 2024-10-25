using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D light2D;
    [SerializeField] public float minIntensity = 0f; // Minimum intensity of the light
    [SerializeField] public float maxIntensity = 2f; // Maximum intensity of the light
    [SerializeField] public float flickerSpeed = 10f; // Speed of flickering

    private void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Update()
    {
        // Calculate a new intensity value based on a sine wave
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1));
        light2D.intensity = intensity;
    }
}
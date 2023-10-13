using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{

    public AudioSource engineAudio;
    public AudioSource tiresAudio;
    public AudioSource crashAudio;

    Driver driver;

    float enginePitch = 0.5f;
    float tiresPitch = 1.0f;

    private void Awake()
    {
        driver = GetComponentInParent<Driver>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateTiresSFX();
    }

    void UpdateEngineSFX()
    {
        float velmag = driver.GetVelocityMagnitude();

        float engineVolume = velmag * 0.05f;

        engineVolume = Mathf.Clamp(engineVolume, 0.2f, 1.0f);

        engineAudio.volume = Mathf.Lerp(engineAudio.volume, engineVolume, Time.deltaTime * 10);

        enginePitch = velmag * 0.2f;
        enginePitch = Mathf.Clamp(enginePitch, 0.8f, 2.5f);
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, enginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTiresSFX()
    {
        if (driver.isSkidding(out float lateralVel, out bool isBraking, out bool isHandBraking))
        {
            if (isBraking || isHandBraking)
            {
                tiresAudio.volume = Mathf.Lerp(tiresAudio.volume, 1.0f, Time.deltaTime * 10);
                tiresPitch = Mathf.Lerp(tiresPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tiresAudio.volume = Mathf.Abs(lateralVel) * 0.05f;
                tiresPitch = Mathf.Abs(lateralVel) * 0.1f;
            }
        }
        else tiresAudio.volume = Mathf.Lerp(tiresAudio.volume, 0, Time.deltaTime * 10);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float relativeVel = collision.relativeVelocity.magnitude;
        float volume = relativeVel * 0.1f;

        crashAudio.pitch = Random.Range(0.95f, 1.05f);
        crashAudio.volume = volume;

        if (!crashAudio.isPlaying)
            crashAudio.Play();

    }
}

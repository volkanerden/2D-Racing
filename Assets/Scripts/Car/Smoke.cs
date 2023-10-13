using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{

    float emmisionRate = 0;

    Driver driver;
    ParticleSystem smoke;
    ParticleSystem.EmissionModule emmisionMod;

    private void Awake()
    {
        driver = GetComponentInParent<Driver>();
        smoke = GetComponent<ParticleSystem>();
        emmisionMod = smoke.emission;
        emmisionMod.rateOverTime = 0;   
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        emmisionRate = Mathf.Lerp(emmisionRate, 0, Time.deltaTime * 5);
        emmisionMod.rateOverTime = emmisionRate;

        if (driver.isSkidding(out float lateralVel, out bool isBreaking, out bool isHandbraking))
        {
            if (isBreaking || isHandbraking) emmisionRate = 30;
            else emmisionRate = Mathf.Abs(lateralVel) * 2;
        }
        
    }
}

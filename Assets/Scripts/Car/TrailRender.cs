using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRender : MonoBehaviour
{
    Driver driver;
    TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        driver = GetComponentInParent<Driver>();
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()

    {

        if (driver.isSkidding(out float lateralVel, out bool isBraking, out bool isHandBraking))
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}
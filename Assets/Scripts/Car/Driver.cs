using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using Fusion;

public class Driver : MonoBehaviour
{
    [Header("CarSettings")]
    public float speed = 10.0f;
    public float turn = 3.5f;
    public float drift = 1.0f;
    public float maxSpeed = 30.0f;
    public float brakeTorque = 50.0f;

    float grassSpeed = 6.0f;
    float currentSpeed;

    //local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velvsup = 0;
    bool offRoad = false;
    bool handBrake = false;
    bool jumping = false;
    
    //Components
    Rigidbody2D carRb;
    Collider2D carCollider; 
    public SpriteRenderer carSprite;
    public SpriteRenderer shadowSprite;

    public AnimationCurve jumpCurve;
    public ParticleSystem landParticle;


    private void Awake()
    {
        carRb = GetComponent<Rigidbody2D>();
        carCollider = GetComponentInChildren<Collider2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        Debug.Log(offRoad);
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillSideForces();
        ApplySteering();
    }

    void ApplyEngineForce()
    {

        if (jumping && accelerationInput < 0)
            accelerationInput = 0;

        if (handBrake && !jumping)
        {
            carRb.drag = Mathf.Lerp(carRb.drag, 2.0f, Time.fixedDeltaTime * 3);
            return;
        }

        velvsup = Vector2.Dot(transform.up, carRb.velocity);

        //limit top speed
        if (velvsup > maxSpeed && accelerationInput > 0)
        {
            return;
        }
        if (velvsup < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }
        if (carRb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0 && !jumping)
        {
            return;
        }

        //if there's no acceleration input, slow down over time
        if (accelerationInput == 0)
        {
            carRb.drag = Mathf.Lerp(carRb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRb.drag = 0;
        }
        //calculate speed(normal vs offroad)
        currentSpeed = offRoad ? grassSpeed : speed;


        //engine force
        Vector2 engineForce = transform.up * accelerationInput * currentSpeed;

        //apply force to the object
        carRb.AddForce(engineForce, ForceMode2D.Force);
        // Reduce drag after drifting
        if (Mathf.Abs(GetLateralVel()) < 0.1f)
        {
            carRb.drag = Mathf.Lerp(carRb.drag, 0.0f, Time.fixedDeltaTime * 3);
        }
    }

    void ApplySteering()
    {
        //Limit steering when slow
        float minSpeedForSteering = carRb.velocity.magnitude / 8;
        minSpeedForSteering = Mathf.Clamp01(minSpeedForSteering);
        //update rotation based on input
        rotationAngle -= steeringInput * turn * minSpeedForSteering;
        //apply steering by rotating the car
        carRb.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void KillSideForces()
    {
        Vector2 forwardVel = transform.up * Vector2.Dot(carRb.velocity, transform.up);
        Vector2 rightVel = transform.right * Vector2.Dot(carRb.velocity, transform.right);

        if (handBrake)
        {
            carRb.velocity = forwardVel + rightVel * drift * 2.12f;
            carRb.AddForce(-transform.right * GetLateralVel() * brakeTorque, ForceMode2D.Force);
        }
        else
        {
            carRb.velocity = forwardVel + rightVel * drift;
        }
        
    }


    float GetLateralVel()
    {
        return Vector2.Dot(transform.right, carRb.velocity);
    }

    public bool isSkidding(out float lateralVel, out bool isBraking, out bool isHandBraking)
    {
        lateralVel = GetLateralVel();
        isBraking = false;
        isHandBraking = false;

        if (jumping)
            return false;

        if(accelerationInput < 0 && velvsup > 0)
        {
            isBraking = true;
            return true;
        }

        if (handBrake)
        {
            isHandBraking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVel()) > 3.0f)
            return true;

        return false;
    }

    public void SetHandBrake(bool active)
    {
        handBrake = active;
    }

    public float GetVelocityMagnitude()
    {
        return carRb.velocity.magnitude;
    }

    public void Jump(float jumpHeight, float jumpPush)
    {
        if (!jumping)
            StartCoroutine(JumpCo(jumpHeight, jumpPush));
    }

    private IEnumerator JumpCo(float jumpHeight, float jumpPush)
    {
        jumping = true;
        float jumpStart = Time.time;
        float jumpDuration = carRb.velocity.magnitude * 0.07f;
        jumpHeight = jumpHeight * carRb.velocity.magnitude * 0.05f;
        jumpHeight = Mathf.Clamp(jumpHeight, 0.0f, 1.0f);


        carCollider.enabled = false;


        carRb.AddForce(carRb.velocity.normalized * jumpPush * 10, ForceMode2D.Impulse);

        Vector3 carScale = Vector3.one * 18 / 100;

        while (jumping)
        {
            float jumpPercentage = (Time.time - jumpStart) / jumpDuration;
            jumpPercentage = Mathf.Clamp01(jumpPercentage);

            carSprite.transform.localScale = carScale + carScale * jumpCurve.Evaluate(jumpPercentage) * jumpHeight;

            shadowSprite.transform.localScale = Vector3.one;
            shadowSprite.transform.localPosition = new Vector3(1, -1, 0.0f) * 10 * jumpCurve.Evaluate(jumpPercentage) * jumpHeight;

            if (jumpPercentage == 1.0f)
                break;

            yield return null;
        }

        carSprite.transform.localScale = carScale;

        shadowSprite.transform.localPosition = Vector3.zero;
        shadowSprite.transform.localScale = Vector3.one;

        carCollider.enabled = true;

        if (jumpHeight < 0.2f)
        {
            landParticle.Play();
        }

        jumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Road"))
            offRoad = false;
        else offRoad = true;
        
        if (collision.CompareTag("Jump"))
        {
            JumpData jumpData = collision.GetComponent<JumpData>();
            Jump(jumpData.jumpHeight, jumpData.jumpForce);
        }
    }
}
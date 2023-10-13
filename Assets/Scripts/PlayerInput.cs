using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int playerNumber = 1;
    public bool isTouchInput = false;

    Vector2 inputVector = Vector2.zero;

    //Components
    Driver driver;

    void Awake()
    {
        driver = GetComponent<Driver>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouchInput)
        {

        }

        else
        {
            inputVector = Vector2.zero;

            switch(playerNumber)
            {
                case 1:
                    //Steering inputs
                    inputVector.x = Input.GetAxis("Horizontal_P1");
                    inputVector.y = Input.GetAxis("Vertical_P1");

                    //Handbrake input
                    if (Input.GetButtonDown("Jump_P1"))
                        driver.SetHandBrake(true);
                    if (Input.GetButtonUp("Jump_P1"))
                        driver.SetHandBrake(false);
                    break;
                case 2:
                    //Steering inputs
                    inputVector.x = Input.GetAxis("Horizontal_P2");
                    inputVector.y = Input.GetAxis("Vertical_P2");

                    //Handbrake input
                    if (Input.GetButtonDown("Jump_P2"))
                        driver.SetHandBrake(true);
                    if (Input.GetButtonUp("Jump_P2"))
                        driver.SetHandBrake(false);
                    break;
                case 3:
                    //Steering inputs
                    inputVector.x = Input.GetAxis("Horizontal_P3");
                    inputVector.y = Input.GetAxis("Vertical_P3");

                    //Handbrake input
                    if (Input.GetButtonDown("Jump_P3"))
                        driver.SetHandBrake(true);
                    if (Input.GetButtonUp("Jump_P3"))
                        driver.SetHandBrake(false);
                    break;
                case 4:
                    //Steering inputs
                    inputVector.x = Input.GetAxis("Horizontal_P4");
                    inputVector.y = Input.GetAxis("Vertical_P4");

                    //Handbrake input
                    if (Input.GetButtonDown("Jump_P4"))
                        driver.SetHandBrake(true);
                    if (Input.GetButtonUp("Jump_P4"))
                        driver.SetHandBrake(false);
              
                    break;
            }
        }

        driver.SetInputVector(inputVector);

        //Jump input

        //if (Input.GetButtonDown("Fire1"))
        //  driver.Jump(0.2f, 0.0f);
    }

    public void SetInput(Vector2 newInput)
    {
        inputVector = newInput;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    PlayerInput input;

    Vector2 inputVector = Vector2.zero;

    private void Awake()
    {

        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();
        foreach (PlayerInput playerInput in playerInputs)
        {
            if (playerInput.isTouchInput)
            {
                input = playerInput;
                break;
            }
        }
    }

    void Start()
    {
        
    }

    public void OnGas()
    {
        inputVector.y = 1.0f;
        input.SetInput(inputVector);
    }

    public void OnBrake()
    {
        inputVector.y = -1.0f;
        input.SetInput(inputVector);
    }

    public void HorizontalRelease()
    {
        inputVector.y = 0.0f;
        input.SetInput(inputVector);
    }

    public void OnLeftArrow()
    {
        inputVector.x = -1.0f;
        input.SetInput(inputVector);
    }

    public void OnRightArrow()
    {
        inputVector.x = 1.0f;
        input.SetInput(inputVector);
    }

    public void VerticalRelease()
    {
        inputVector.x = 0.0f;
        input.SetInput(inputVector);
    }
}

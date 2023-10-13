using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    public Image carImage;
    Animator animator = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupCar(CarData carData)
    {
        carImage.sprite = carData.CarSelectSprite;
    }

    public void StartCarAnimation(bool isAppearingOnRightSide)
    {
        if (isAppearingOnRightSide)
            animator.Play("CarSelectAppearFromRight");
        else animator.Play("CarSelectAppearFromLeft");
    }

    public void StartCarExitAnim(bool isExitingOnRightSide)
    {
        if (isExitingOnRightSide)
            animator.Play("CarSelectDisappearToRight");
        else animator.Play("CarSelectDisappearToLeft");
    }

    public void ExitAnimCompleted()
    {
        Destroy(gameObject);
    }
}
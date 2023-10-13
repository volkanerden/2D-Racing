using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Data", menuName = "Car Data", order = 10)]

public class CarData : ScriptableObject
{
    [SerializeField] private int carUniqueID = 0;

    [SerializeField] private Sprite carSelectSprite;

    [SerializeField] private GameObject carPrefab;

    public int  CarUniqueID
    {
        get { return carUniqueID; }
    }

    public Sprite CarSelectSprite
    {
        get { return carSelectSprite; }
    }

    public GameObject CarPrefab
    {
        get { return carPrefab; }
    }
}

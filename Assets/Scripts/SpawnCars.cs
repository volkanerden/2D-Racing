using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");

        CarData[] carDatas = Resources.LoadAll<CarData>("CarData/");

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i].transform;

            int playerSelectedCarID = PlayerPrefs.GetInt($"P({i + 1}SelectedCarID");

            foreach (CarData cardata in carDatas)
            {
                if (cardata.CarUniqueID == playerSelectedCarID)
                {
                    GameObject playerCar = Instantiate(cardata.CarPrefab, spawnPoint.position, spawnPoint.rotation);

                    playerCar.GetComponent<PlayerInput>().playerNumber = i + 1;
                    break;
                }
            }
        }
    }
}

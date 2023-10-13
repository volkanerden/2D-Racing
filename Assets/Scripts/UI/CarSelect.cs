using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour
{

    public GameObject carPrefab;
    public Transform spawnPos;

    bool isChangingCar = false;

    CarData[] carDatas;

    int selectedCarIndex = 0;

    CarUI carui = null;

    void Start()
    {
        carDatas = Resources.LoadAll<CarData>("CarData/");

        StartCoroutine(SpawnCarCO(true));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ToPreviousCar();
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ToNextCar();
        }
        if(Input.GetKey(KeyCode.Return))
        {
            OnSelectedCar();
        }

    }

    public void ToPreviousCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex--;

        if (selectedCarIndex < 0)
            selectedCarIndex = carDatas.Length - 1;

        StartCoroutine(SpawnCarCO(true));
    }

    public void ToNextCar()
    {
        if (isChangingCar)
            return;

        selectedCarIndex++;

        if (selectedCarIndex > carDatas.Length - 1)
            selectedCarIndex = 0;

        StartCoroutine(SpawnCarCO(false));
    }

    public void OnSelectedCar()
    {
        PlayerPrefs.SetInt("P1SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        PlayerPrefs.SetInt("P2SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);

        PlayerPrefs.Save();

        SceneManager.LoadScene("SpawnCar");

    }

    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        isChangingCar = true;

        if (carui != null)
            carui.StartCarExitAnim(!isCarAppearingOnRightSide);

        GameObject instantiatedCar = Instantiate(carPrefab, spawnPos);

        carui = instantiatedCar.GetComponent<CarUI>();
        carui.SetupCar(carDatas[selectedCarIndex]);
        carui.StartCarAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.4f);
        isChangingCar = false;
    }
}
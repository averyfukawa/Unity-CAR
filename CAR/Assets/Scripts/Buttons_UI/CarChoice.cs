using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChoice : MonoBehaviour
{
    public GameObject[] carBody;
    
    private int _carImport;

    public void Start()
    {
        if (carBody.Length == 0) { Debug.Log("Please attach the customized car bodies to the Car Choice Manager!"); return; }
        
        if (carBody[_carImport].activeInHierarchy == true) { carBody[_carImport].SetActive(false); } 
        // Static variables and everything was working just fine, but we never turned off the previous CarImport!
        _carImport = GlobalCar.CarType;
        carBody[_carImport].SetActive(true);
    }
}
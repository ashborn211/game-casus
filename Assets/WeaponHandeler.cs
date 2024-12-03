using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class WeaponHandeler : MonoBehaviour
{
    [System.Serializable]
    public struct WeaponModel {//change because vars only need to be get after being set
        public GameObject Model;
        public int Id ;
    }

    [SerializeField]
    public WeaponModel[] weaponModels;
    public GameObject spawnedWeapon;

    float update = 0;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SetWeapon(int id)
    {
        foreach(Transform child in transform){
            GameObject.Destroy(child.gameObject);
        }
        WeaponModel weaponModel = Array.Find(weaponModels , m => m.Id == id);//add check
        spawnedWeapon = Instantiate(weaponModel.Model);
        spawnedWeapon.transform.SetParent(this.transform);
        spawnedWeapon.transform.localPosition = new Vector3(0, 0, 0);
    }
}

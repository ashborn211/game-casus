using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class WeaponHandeler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] weapons;
    public GameObject spawnedWeapon;

    float update = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWeapon(int index)
    {
        foreach(Transform child in transform){
            GameObject.Destroy(child.gameObject);
        }
        spawnedWeapon = Instantiate(weapons[index]);
        spawnedWeapon.transform.SetParent(this.transform);
        spawnedWeapon.transform.localPosition = new Vector3(0, 0, 0);
    }
}

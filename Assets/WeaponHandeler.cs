using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandeler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] weapons;
    public GameObject spawnedWeapon;
    void Start()
    {
        SetWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWeapon(int index)
    {
        //destroy all child objects
        spawnedWeapon = Instantiate(weapons[index]);
        spawnedWeapon.transform.SetParent(this.transform);
        spawnedWeapon.transform.localPosition = new Vector3(0, 0, 0);
    }
}

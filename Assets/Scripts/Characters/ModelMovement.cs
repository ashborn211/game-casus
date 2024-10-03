using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public movementPlayer movementPlayer;
    private Transform mc;

    private float lastDirection = 0f;

    void Start()
    {
        mc = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        lastDirection = movementPlayer.lastDirection;
        mc.localRotation = Quaternion.Euler(0, lastDirection*-1, 0);
    }
}
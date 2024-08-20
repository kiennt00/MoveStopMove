using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 60f;
    [SerializeField] Transform tf;

    void Update()
    {
        tf.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}

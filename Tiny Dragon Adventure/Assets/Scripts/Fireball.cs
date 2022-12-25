using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 5;
    
    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player") BlowUp();
    }

    private void BlowUp()
    {
        // добавить эффект взрыва
        Destroy(gameObject);
    }

    private void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

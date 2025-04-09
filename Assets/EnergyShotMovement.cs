using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotMovement : DinoBehaviour
{
    [SerializeField] protected float speed;

    protected void Update()
    {
        this.transform.parent.Translate(Vector3.right * speed * Time.deltaTime);
    }
}

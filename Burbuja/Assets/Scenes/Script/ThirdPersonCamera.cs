using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Vector3 offset;
    private Transform target;
    [Range (0,1)]public float lerpValue; //como de rapido quiero que pase desde una posicion a otra
    public float sensibilidad;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    

    void LateUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up) * offset;

        transform.LookAt(target);// esto hace que mira al jugador

    }

}

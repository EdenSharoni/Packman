﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class AboveCameraScript : MonoBehaviour
{

    [Range(0.01f, 1.0f)]
    public float smoothFactor = .5f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    void Start()
    {
        target = GameObject.Find("Pacman3D").GetComponent<Transform>();
        offsetPosition = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (transform.name.Equals("BackCamera"))
            Refresh();
        else
        {
            Vector3 newPosition = target.position + offsetPosition;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        }
    }

    public void Refresh()
    {
        // compute position
        if (offsetPositionSpace == Space.Self)        
            transform.position = target.TransformPoint(offsetPosition);
        
        else        
            transform.position = target.position + offsetPosition;        

        // compute rotation
        if (lookAt)        
            transform.LookAt(target);
        
        else        
            transform.rotation = target.rotation;        
    }
}

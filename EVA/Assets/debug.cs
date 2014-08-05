/// <summary>
/// This file is part of the EVA simulation. 
/// Author : Thomas Schweizer
/// Date   : May 2014
/// </summary>

using UnityEngine;
using System.Collections;

/// <summary>
/// Debug class for the organisms. This show gizmos on the terrain in unity.
/// </summary>
public class debug : MonoBehaviour {
    void OnDrawGizmos()
    {
//        Gizmos.color = Color.red;
//        Vector3 position = transform.TransformDirection(Vector3.forward) * 5;
//        Gizmos.DrawRay(transform.position, position);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}

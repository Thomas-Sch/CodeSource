using UnityEngine;
using System.Collections;

public class debug : MonoBehaviour {
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, position);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}

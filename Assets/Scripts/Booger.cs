using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booger : MonoBehaviour {

    MeshDeformer hyde;
    Dictionary<string, Collision> cols;

    private void Start()
    {
        cols = new Dictionary<string, Collision>();
        hyde = GetComponent<MeshDeformer>();
        GetComponent<Rigidbody>().AddForce(Vector3.one * 10, ForceMode.VelocityChange);
    }

    private void Update()
    { 
            foreach (Collision collision in cols.Values)
            {
                foreach (ContactPoint point in collision.contacts)
                {
                    hyde.AddDeformingForce(point.point, 5);
                }
            }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        cols.Add(collision.gameObject.name, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        cols.Remove(collision.gameObject.name);
    }
}

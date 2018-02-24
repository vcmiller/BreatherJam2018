using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booger : MonoBehaviour {

    MeshDeformer hyde;
    Dictionary<string, Collision> cols;
    public GameObject explosion;

    public float health = 1000;

    private void Start()
    {
        cols = new Dictionary<string, Collision>();
        hyde = GetComponent<MeshDeformer>();
        //GetComponent<Rigidbody>().AddForce(Vector3.one * 10, ForceMode.VelocityChange);
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
        float f = collision.relativeVelocity.magnitude;
        if (f > 10 && collision.collider.GetComponent<Finger>()) {
            health -= f;
            if (health < 0) {
                Destroy(gameObject);
                Destroy(Instantiate(explosion, transform.position, transform.rotation), 4.0f);
            }
        }

        cols[collision.gameObject.name] = collision;
    }

    private void OnCollisionExit(Collision collision)
    {
        cols.Remove(collision.gameObject.name);
    }
}

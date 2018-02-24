using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SBR;

public class Finger : MonoBehaviour {
    public float speed;
    public MeshFilter meshObject;
    public int complexity = 16;
    public float radius = 2;
    public Transform plugObj;

    private List<Vector3> vertices;
    private List<int> indices;
    private List<int> colIndices;
    private List<Vector2> uvs;

    private MeshCollider col;
    private Mesh mesh;
    private Mesh colMesh;
    private Vector3 lastPos;

    private List<Vector3> plugPoints;

    public float offset = 1;
    public int back = 4;
    private bool first;

    private int cur = 0;

    private CooldownTimer recallTimer;

	// Use this for initialization
	void Start () {
        mesh = new Mesh();
        colMesh = new Mesh();
        meshObject.sharedMesh = mesh;
        col = meshObject.gameObject.AddComponent<MeshCollider>();
        col.convex = false;
        col.sharedMesh = colMesh;

        vertices = new List<Vector3>();
        indices = new List<int>();
        uvs = new List<Vector2>();
        colIndices = new List<int>();

        plugPoints = new List<Vector3>();

        lastPos = transform.position;
        first = true;

        recallTimer = new CooldownTimer(0.5f * offset / speed);
	}

    void Back() {
        if (plugPoints.Count > 1) {
            indices.RemoveRange(indices.Count - complexity * 4, complexity * 4);
            colIndices.RemoveRange(colIndices.Count - complexity * 4, complexity * 4);

            vertices.RemoveRange(vertices.Count - complexity, complexity);
            uvs.RemoveRange(uvs.Count - complexity, complexity);

            cur--;

            plugPoints.RemoveAt(plugPoints.Count - 1);
            plugObj.GetComponent<Collider>().enabled = plugPoints.Count > back;
            if (plugPoints.Count > back) {
                plugObj.transform.position = plugPoints[plugPoints.Count - back - 1];
            }

            UpdateSubtracted();

            Vector3 newPos = plugPoints[plugPoints.Count - 1];
            transform.rotation = Quaternion.LookRotation(Vector3.down, transform.position - newPos);

            transform.position = newPos;
            lastPos = transform.position;
        }
        
    }

    void UpdateMesh() {
        mesh.SetVertices(vertices);
        mesh.SetUVs(0, uvs);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0, true);
        mesh.RecalculateNormals();

        colMesh.SetVertices(vertices);
        colMesh.SetIndices(colIndices.ToArray(), MeshTopology.Quads, 0, true);
        colMesh.RecalculateNormals();

        col.sharedMesh = colMesh;
    }

    void UpdateSubtracted() {
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0, true);
        mesh.SetVertices(vertices);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();

        colMesh.SetIndices(colIndices.ToArray(), MeshTopology.Quads, 0, true);
        colMesh.SetVertices(vertices);
        colMesh.RecalculateNormals();

        col.sharedMesh = colMesh;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 v = Vector3.zero;

        if (Input.GetButton("Jump") && recallTimer.Use()) {
            Back();
        } else {
            v += Input.GetAxis("Vertical") * Vector3.forward * speed;
            v += Input.GetAxis("Horizontal") * Vector3.right * speed;
        }

        GetComponent<Rigidbody>().velocity = v;

        if (transform.position != lastPos) {
            transform.rotation = Quaternion.LookRotation(Vector3.down, transform.position - lastPos);
        }

        float f = Vector3.Distance(transform.position, lastPos);

        var toff = (offset * cur) + f;

        Material m = meshObject.GetComponent<MeshRenderer>().material;
       m.mainTextureOffset = new Vector2(0, -toff * m.mainTextureScale.y);

        if (f > offset) {
            plugPoints.Add(transform.position);

            float per = 2.0f * Mathf.PI / complexity;

            for (int i = 0; i < complexity; i++) {
                float a1 = i * per;
                float s1 = Mathf.Sin(a1);
                float c1 = Mathf.Cos(a1);

                Vector3 v1 = (transform.right * s1 + transform.forward * c1) * radius;
                
                vertices.Add(v1 + transform.position);

                uvs.Add(new Vector2(i * 1.0f / complexity, cur));

                if (i > 0 && !first) {
                    indices.Add(vertices.Count - 1);
                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - complexity - 2);
                    indices.Add(vertices.Count - complexity - 1);
                }
            }

            if (!first) {
                indices.Add(vertices.Count - 1 - (complexity - 1));
                indices.Add(vertices.Count - 1);
                indices.Add(vertices.Count - 1 - complexity);
                indices.Add(vertices.Count - 1 - (complexity - 1) - complexity);
            }

            if (indices.Count >= complexity * back * 4) {
                for (int i = indices.Count - complexity * back * 4; i < indices.Count - complexity * (back - 1) * 4; i++) {
                    colIndices.Add(indices[i]);
                }
            }

            first = false;
            cur++;

            UpdateMesh();

            lastPos = transform.position;

            plugObj.GetComponent<Collider>().enabled = plugPoints.Count > back;
            if (plugPoints.Count > back) {
                plugObj.transform.position = plugPoints[plugPoints.Count - back - 1];
            }
        }

        
    }

}

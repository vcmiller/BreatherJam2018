using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour {

	public float springForce = 20f;
	public float damping = 5f;
    public float maxInflate = 3;

    Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;

    float uniformScale { get { return transform.localScale.x; } }

	void Start () {
		deformingMesh = GetComponent<MeshFilter>().sharedMesh;
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++) {
			displacedVertices[i] = originalVertices[i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];
	}

	void Update () {
		for (int i = 0; i < displacedVertices.Length; i++) {
			UpdateVertex(i);
		}
		deformingMesh.vertices = displacedVertices;
		deformingMesh.RecalculateNormals();

        GetComponent<MeshCollider>().sharedMesh = deformingMesh;
	}

	void UpdateVertex (int i) {
		Vector3 velocity = vertexVelocities[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];


        displacement *= uniformScale;
        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxInflate);

		vertexVelocities[i] = velocity;
		displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
	}

	public void AddDeformingForce (Vector3 point, float force) {
		point = transform.InverseTransformPoint(point);
		for (int i = 0; i < displacedVertices.Length; i++) {
			AddForceToVertex(i, point, force);
		}
	}

	void AddForceToVertex (int i, Vector3 point, float force) {
		Vector3 pointToVertex = displacedVertices[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;
	}
}
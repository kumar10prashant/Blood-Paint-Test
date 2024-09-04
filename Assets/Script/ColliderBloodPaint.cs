using UnityEditor.PackageManager;
using UnityEngine;

public class ColliderBloodPaint : MonoBehaviour
{
    public ParticleSystem particleSystem;
    Mesh mesh;
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        Invoke("Destroy", 5f);

        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
        int collisionCount = particleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < collisionCount; i++)
        {
            // Get the world position of the collision
            Vector3 collisionPoint = collisionEvents[i].intersection;

            // Convert the world position to local space
            Vector3 localPos = other.transform.InverseTransformPoint(collisionPoint);

            // Get the mesh and its UVs
            try
            {
             mesh = other.GetComponent<MeshFilter>().mesh;

            }
            catch
            {
                 mesh = other.GetComponent<SkinnedMeshRenderer>().sharedMesh;

            }
            Vector2 uv = GetUVFromLocalPosition(localPos, mesh);

            if (uv != Vector2.zero)
            {
                if (other.transform.gameObject.TryGetComponent<PaintBlood>(out PaintBlood paintblood))
                {
                    Debug.Log(uv);
                    paintblood.PaintParticle(uv);
                    return;
                }
            }
        }
    }

    private Vector2 GetUVFromLocalPosition(Vector3 localPos, Mesh mesh)
    {
        // Access the mesh data
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            // Check if the point is inside the triangle
            if (IsPointInTriangle(localPos, v0, v1, v2))
            {
                // Barycentric coordinates to interpolate the UVs
                Vector2 uv0 = uvs[triangles[i]];
                Vector2 uv1 = uvs[triangles[i + 1]];
                Vector2 uv2 = uvs[triangles[i + 2]];

                return BarycentricInterpolate(localPos, v0, v1, v2, uv0, uv1, uv2);
            }
        }

        return Vector2.zero;
    }

    private bool IsPointInTriangle(Vector3 p, Vector3 v0, Vector3 v1, Vector3 v2)
    {
        Vector3 d0 = v1 - v0;
        Vector3 d1 = v2 - v0;
        Vector3 d2 = p - v0;

        float dot00 = Vector3.Dot(d0, d0);
        float dot01 = Vector3.Dot(d0, d1);
        float dot02 = Vector3.Dot(d0, d2);
        float dot11 = Vector3.Dot(d1, d1);
        float dot12 = Vector3.Dot(d1, d2);

        float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        return (u >= 0) && (v >= 0) && (u + v <= 1);
    }

    private Vector2 BarycentricInterpolate(Vector3 p, Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2)
    {
        Vector3 d0 = v1 - v0;
        Vector3 d1 = v2 - v0;
        Vector3 d2 = p - v0;

        float dot00 = Vector3.Dot(d0, d0);
        float dot01 = Vector3.Dot(d0, d1);
        float dot02 = Vector3.Dot(d0, d2);
        float dot11 = Vector3.Dot(d1, d1);
        float dot12 = Vector3.Dot(d1, d2);

        float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        return uv0 + u * (uv1 - uv0) + v * (uv2 - uv0);
    }
}


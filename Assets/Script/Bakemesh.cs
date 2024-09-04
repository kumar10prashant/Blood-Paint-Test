using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bakemesh : MonoBehaviour
{
    public MeshCollider mehscollider;
    public SkinnedMeshRenderer skinnedMeshRenderer;
   
    public void UpdateMeshCollider()
    {
        Mesh bakedMesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(bakedMesh);

        mehscollider.sharedMesh = null;

        // Assign the new baked mesh
        mehscollider.sharedMesh = bakedMesh;
    }
}

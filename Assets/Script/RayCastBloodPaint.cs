using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RayCastBloodPaint : MonoBehaviour
{
    public Bakemesh[] bakeMesh;
    public PaintBlood[] blood;
    public float Timer = 0;
    public Transform cam;
    private void Update()
    {
        Timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && Timer > 0.3f)
        {
            Timer = 0;
           // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(cam.position,cam.forward,out RaycastHit hitinfo))
            {
                if (hitinfo.collider.transform.tag != "Player") return;
                if(hitinfo.transform.gameObject.TryGetComponent<PaintBlood>(out PaintBlood paintblood))
                {
                    Debug.Log(hitinfo.textureCoord);
                    paintblood.Paint(hitinfo);
                    return;
                }
                
                blood = (hitinfo.transform.gameObject.GetComponentsInChildren<PaintBlood>());
                if(blood.Length != 0)
                {

                    if (hitinfo.transform.gameObject.TryGetComponent<CapsuleCollider>(out CapsuleCollider capsuleCollider))
                    {

                    capsuleCollider.enabled = false;
                    }
                    bakeMesh = hitinfo.transform.GetComponentsInChildren<Bakemesh>();
                   
                    for(int i = 0; i < bakeMesh.Length; i++)
                    {
                        bakeMesh[i].UpdateMeshCollider();
                    }
                    if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfos))
                    {

                       
                        hitInfos.transform.TryGetComponent<PaintBlood>(out PaintBlood g);
                        g.Paint(hitInfos);

                    }
                    capsuleCollider.enabled = true;
                }
            }
        }
    }

}

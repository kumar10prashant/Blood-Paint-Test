using Unity.VisualScripting;
using UnityEngine;

public class PaintBlood : MonoBehaviour
{
    public GameObject ActiveObject;
    public Camera cam;
    public RenderTexture renderTexture;
    public Texture2D brushTexture;
   
    public float brushRadius = 50.0f; // Brush radius in pixels
    public Renderer render;
    public CapsuleCollider capsuleCollider;
    public GameObject ParticleSystem;
    public float Timer;
    public Texture2D EraseTex;
    private void Awake()
    {
        render = GetComponent<Renderer>();

        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(1024, 1024, 0);
            renderTexture.Create();
        }

      
        render.material.SetTexture("_Mask", renderTexture);
        

        
      
    }
    private void OnMouseDown()
    {
        
    }

    
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            
           /* Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit Check))
            {
               
            }
            
                   

             
            
            RaycastHit[] hits = Physics.RaycastAll(ray);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject != this.gameObject) return;
                    
                   
                    
             
                   Paint(hit.textureCoord);
                    
                }

            }*/
            

        }
    }



     public void Paint(RaycastHit hit)
    {
        Vector2 uv = hit.textureCoord;
        Vector3 HitPos = hit.point;
        if(ParticleSystem != null)
        {

        Instantiate(ParticleSystem,HitPos,Quaternion.LookRotation(hit.normal));
        }
        Debug.Log(transform.gameObject.name);
        RenderTexture.active = renderTexture;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);


        
        // Calculate the size of the brush based on the brush radius
        float scaledBrushWidth = brushTexture.width  * (brushRadius / brushTexture.width);
        float scaledBrushHeight = brushTexture.height * (brushRadius / brushTexture.height);

        Vector2 pixelUV = new Vector2(uv.x * renderTexture.width, (1 - uv.y) * renderTexture.height);
        Graphics.DrawTexture(new Rect(pixelUV.x - scaledBrushWidth / 2, pixelUV.y - scaledBrushHeight / 2, scaledBrushWidth, scaledBrushHeight), brushTexture);

        GL.PopMatrix();
        RenderTexture.active = null;
    }

    public void PaintParticle(Vector2 uv)
    {
        Debug.Log(transform.gameObject.name);
        RenderTexture.active = renderTexture;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);



        // Calculate the size of the brush based on the brush radius
        float scaledBrushWidth = brushTexture.width * (brushRadius / brushTexture.width);
        float scaledBrushHeight = brushTexture.height * (brushRadius / brushTexture.height);

        Vector2 pixelUV = new Vector2(uv.x * renderTexture.width, (1 - uv.y) * renderTexture.height);
        Graphics.DrawTexture(new Rect(pixelUV.x - scaledBrushWidth / 2, pixelUV.y - scaledBrushHeight / 2, scaledBrushWidth, scaledBrushHeight), brushTexture);

        GL.PopMatrix();
        RenderTexture.active = null;
    }
}

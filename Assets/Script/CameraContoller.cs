using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float mouseX,mouseY;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
         mouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity*Time.deltaTime;
         mouseY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity*Time.deltaTime;


        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        playerBody.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
   Rigidbody Rigidbody;
    [SerializeField] float Speed;
    float hor, ver;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MyInput();
    }
    public void MyInput()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
    }


    public void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        Vector3 Movement = transform.forward * ver + transform.right * hor;
        Rigidbody.AddForce(Movement.normalized*10*Speed,ForceMode.Force);
    }

}

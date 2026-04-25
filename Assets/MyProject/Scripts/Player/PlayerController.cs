using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public Joystick joystick;
    public Animator animator;
    bool isMobile;
    private void Start()
    {
        animator = GetComponent<Animator>();
        isMobile = Application.isMobilePlatform;

        joystick.gameObject.SetActive(isMobile);

        if (!isMobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void Update()
    {
        HandlePlatform();
       
    }

    void HandlePlatform()
    {
        float h =  0f;
        float v =  0f; 
        if (isMobile)
        {
             h = joystick.Horizontal;
             v = joystick.Vertical;
        }
        else
        { 
             h = Input.GetAxis("Horizontal");
             v = Input.GetAxis("Vertical");
        }

        Vector3 move = new Vector3(h, 0, v);

        if (move.magnitude > 0.1f)
        {
            move.Normalize();

            // MOVE no mundo (não relativo ao player)
            transform.position += move * speed * Time.deltaTime;
            animator.SetBool("Run", true);

            // ROTACIONA para onde está indo
            Quaternion targetRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}
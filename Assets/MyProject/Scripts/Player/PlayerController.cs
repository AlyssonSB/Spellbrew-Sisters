using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public Joystick? joystick;
    public Animator? animator;
    bool isMobile;

    [Header("Modo de controle")]
    public bool firstPerson = false;
    public Transform? cameraTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isMobile = Application.isMobilePlatform;

        joystick?.gameObject.SetActive(isMobile);

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

        if (firstPerson && cameraTransform != null)
        {
            // Direção baseada na câmera
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // remove inclinação vertical (pra não voar)
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            move = (forward * v + right * h);
        }
        else
        {
            // Direção padrão (mundo)
            move = new Vector3(h, 0, v);
        }

        if (move.magnitude > 0.1f)
        {
            move.Normalize();

            transform.position += move * speed * Time.deltaTime;
            animator?.SetBool("Run", true);

            // Só rotaciona no modo não-first person
            if (!firstPerson)
            {
                Quaternion targetRot = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
            }
        }
        else
        {
            animator?.SetBool("Run", false);
        }
    }
}

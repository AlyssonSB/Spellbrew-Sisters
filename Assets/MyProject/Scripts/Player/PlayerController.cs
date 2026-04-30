using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public Joystick joystick;
    public RectTransform joystickArea;
    //public Animator? animator;
    bool isMobile;
    public float lookSpeed = 2f;
    private float rotationY;

    [Header("Modo de controle")]
    public bool firstPerson = false;
    public Transform? cameraTransform;
    private int cameraFingerId = -1;
    private bool cameraFingerActive = false;


    private void Start()
    {
        //animator = GetComponent<Animator>(); 
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
        HandleTouchLook();
    }
    bool IsTouchInsideArea(Touch touch)
    {

        if (RectTransformUtility.RectangleContainsScreenPoint(
            joystickArea,
            touch.position,
            null))
        {
            return false;
        }

        return true;
    }
    void HandleTouchLook()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                continue;
            if (!cameraFingerActive)
            {
                if (touch.phase == TouchPhase.Began &&
                    IsTouchInsideArea(touch))
                {
                    cameraFingerId = touch.fingerId;
                    cameraFingerActive = true;
                }
            }
            if (touch.phase == TouchPhase.Ended ||
                touch.phase == TouchPhase.Canceled)
            {
                cameraFingerActive = false;
                cameraFingerId = -1;
            }
        }
    }


    void HandlePlatform()
    {
        float h = 0f;
        float v = 0f;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

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
            //animator?.SetBool("Run", true);

        }
        else
        {
            //animator?.SetBool("Run", false);
        }

        if (firstPerson)
        {
            Quaternion targetRot = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
        else
        {
            Quaternion targetRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
    }
}

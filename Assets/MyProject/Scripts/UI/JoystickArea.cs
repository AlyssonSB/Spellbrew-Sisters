using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool IsTouchingJoystick = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsTouchingJoystick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsTouchingJoystick = false;
    }
}
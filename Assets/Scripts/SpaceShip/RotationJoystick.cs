using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Camera shipCam;
    public TouchThruster TouchThruster;

    public float deadzone = 0.1f;
    private Vector2 inputVector;

    public RectTransform LookStick;
    public RectTransform LookRing;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        LookStick.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(LookRing, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / LookRing.sizeDelta.x);
            pos.y = (pos.y / LookRing.sizeDelta.y);

            inputVector = pos;
            inputVector = (pos.magnitude > 1) ? pos.normalized : pos;

            LookStick.anchoredPosition = new Vector2(inputVector.x * LookStick.sizeDelta.x, inputVector.y * LookStick.sizeDelta.y);
        }
    }

    void FixedUpdate()
    {
        if(inputVector != Vector2.zero)
        {
            Vector2 rotationVector = new Vector2(-inputVector.y, inputVector.x);

            bool xDead = false;
            bool yDead = false;
            if(rotationVector.x < deadzone && rotationVector.x > -deadzone)
            {
                rotationVector.x = 0;
                xDead = true;
            }

            if (rotationVector.y < deadzone && rotationVector.y > -deadzone)
            {
                rotationVector.y = 0;
                yDead = true;
            }

            if (!xDead || !yDead)
            {
                TouchThruster.LookThruster(rotationVector);
            }
        }
    }
}
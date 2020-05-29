using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public PlaneOrbitController shipController;

    public float deadzone = 0.01f;
    private Vector2 inputVector;

    public RectTransform MoveStick;
    public RectTransform MoveRing;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        MoveStick.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(MoveRing, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / MoveRing.sizeDelta.x);
            pos.y = (pos.y / MoveRing.sizeDelta.y);

            inputVector = pos;
            inputVector = (pos.magnitude > 1) ? pos.normalized : pos;

            MoveStick.anchoredPosition = new Vector2(inputVector.x * MoveStick.sizeDelta.x, inputVector.y * MoveStick.sizeDelta.y);
        }
    }

    void FixedUpdate()
    {
        if (inputVector != Vector2.zero)
        {
            bool xDead = false;
            bool yDead = false;
            if (inputVector.x < deadzone && inputVector.x > -deadzone)
            {
                inputVector.x = 0;
                xDead = true;
            }

            if (inputVector.y < deadzone && inputVector.y > -deadzone)
            {
                inputVector.y = 0;
                yDead = true;
            }

            if (!xDead || !yDead)
            {
                shipController.VectorMoveDirection(inputVector);
            }
        }
    }
}
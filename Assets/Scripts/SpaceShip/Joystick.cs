using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public UITweener joysticAnimator;
    public UITweener nextAnimator;

    public float deadzone = 0.01f;
    public Vector2 outputVector;
    private Vector2 inputVector;

    public RectTransform MoveStick;
    public RectTransform MoveRing;

    private bool tutoAnim = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);

        if (joysticAnimator != null)
        {
            if (joysticAnimator.gameObject.activeSelf)
            {
                if (tutoAnim)
                {
                    joysticAnimator.Disabe();

                    if (nextAnimator != null) nextAnimator.gameObject.SetActive(true);

                    tutoAnim = false;
                }
            }
        }
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

            MoveStick.anchoredPosition = new Vector2(inputVector.x * MoveRing.sizeDelta.x, inputVector.y * MoveRing.sizeDelta.y) * 0.5f;
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
                outputVector = inputVector;
            }
        }
        else
        {
            outputVector = Vector2.zero;
        }
    }
}
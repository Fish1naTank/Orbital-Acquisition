using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIAnimationType
{
    Move,
    Rotate,
    Scale,
    Fade
}

public class UITweener : MonoBehaviour
{
    public GameObject objectToAnimate;

    public UIAnimationType animationType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;

    public bool loop;
    public bool pingpong;

    public bool startOffset;
    public Vector3 from;
    public Vector3 to;

    public bool showOnEnable;
    public bool showOnDisable;
    public bool destroyOnCompleate = false;

    private LTDescr tweenObject;

    private bool direction = true;
    private bool disabling = false;

    public void OnEnable()
    {
        if(showOnEnable)
        {
            Show();
        }
    }

    public void OnDisable()
    {
        if (showOnDisable)
        {
            SwapDirection();
            Show();
            SwapDirection();
        }
    }

    public void Show()
    {
        HandleTween();
    }

    public void Disabe()
    {
        if (disabling || !gameObject.activeSelf) return;
        disabling = true;

        if (!pingpong && !loop)
        {
            SwapDirection();
            HandleTween();
        }
        else if(pingpong)
        {
            if ((tweenObject.loopCount ^ 1) == tweenObject.loopCount + 1)
            {
                tweenObject.loopCount = 1;
            }
            else
            {
                tweenObject.loopCount = 2;
            }
        }
        else if(loop)
        {
            tweenObject.loopCount = 1;
        }

        tweenObject.setOnComplete(() =>
        {
            if (!pingpong && !loop)
            {
                SwapDirection();
            }
            LeanTween.cancel(objectToAnimate);
            gameObject.SetActive(false);
            disabling = false;

            if(destroyOnCompleate)
            {
                Destroy(gameObject);
            }
        });
    }

    public void SwapDirection()
    {
        var tmp = from;
        from = to;
        to = tmp;

        direction = !direction;
    }

    private void HandleTween()
    {
        if(objectToAnimate == null)
        {
            objectToAnimate = gameObject;
        }

        switch (animationType)
        {
            case UIAnimationType.Move:
                Move();
                break;
            case UIAnimationType.Rotate:
                Rotate();
                break;
            case UIAnimationType.Scale:
                Scale();
                break;
            case UIAnimationType.Fade:
                Fade();
                break;
        }

        tweenObject.setDelay(delay);
        tweenObject.setEase(easeType);

        if (loop) tweenObject.loopCount = int.MaxValue; //tweenObject.setLoopType()

        if (pingpong) tweenObject.setLoopPingPong();
    }

    private void Move()
    {
        RectTransform rect = objectToAnimate.GetComponent<RectTransform>();

        if(startOffset)
        {
            rect.anchoredPosition = from;
        }

        tweenObject = LeanTween.move(rect, to, duration);
    }

    private void Rotate()
    {
        RectTransform rect = objectToAnimate.GetComponent<RectTransform>();

        if (startOffset)
        {
            rect.localRotation = Quaternion.Euler(from);
        }

        tweenObject = LeanTween.rotate(rect, to, duration);
    }

    private void Scale()
    {
        RectTransform rect = objectToAnimate.GetComponent<RectTransform>();

        if (startOffset)
        {
            rect.localScale = from;
        }

        tweenObject = LeanTween.scale(rect, to, duration);
    }

    private void Fade()
    {
        CanvasGroup canvasGroup = objectToAnimate.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = objectToAnimate.AddComponent<CanvasGroup>();
        }

        if(startOffset)
        {
            canvasGroup.alpha = from.x;
        }

        tweenObject = LeanTween.alphaCanvas(canvasGroup, to.x, duration);
    }
}

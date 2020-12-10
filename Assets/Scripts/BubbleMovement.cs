using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BubbleMovement : MonoBehaviour
{
    bool up = true, left = true;

    private void Start()
    {
        moveBubbleY();
        moveBubbleX();
    }

    void moveBubbleY()
    {
        if (up)
        {
            transform.DOMoveY(transform.position.y + 5f, 10f).SetEase(Ease.InOutSine).OnComplete(() => moveBubbleY());
            up = false;
        }
        else
        {
            transform.DOMoveY(transform.position.y - 5f, 10f).SetEase(Ease.InOutSine).OnComplete(() => moveBubbleY());
            up = true;
        }
    }

    void moveBubbleX()
    {
        if (left)
        {
            transform.DOMoveX(transform.position.x + 1f, 4f).SetEase(Ease.InOutSine).OnComplete(() => moveBubbleX());
            left = false;
        }
        else
        {
            transform.DOMoveX(transform.position.x - 1f, 4f).SetEase(Ease.InOutSine).OnComplete(() => moveBubbleX());
            left = true;
        }
    }
}

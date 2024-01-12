using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeAnimation : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public float AnimationTime;
    float currentTime;
    bool isReversed;
    [HideInInspector] public bool IsFinished;

    // Start is called before the first frame update
    void Start()
    {
        StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        CanvasGroup.alpha = currentTime / (AnimationTime / 2);

        if (currentTime >= AnimationTime / 2)
            isReversed = true;
        if (isReversed)
            currentTime -= Time.deltaTime;
        else
            currentTime += Time.deltaTime;

        if (CanvasGroup.alpha == 0 && isReversed)
        {
            IsFinished = true;
        }
    }

    public void StartAnimation()
    {
        isReversed = false;
        IsFinished = false;
        currentTime = 0;
        CanvasGroup.alpha = 0;
    }
}

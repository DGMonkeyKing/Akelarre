using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGrabber))]
public class HideShowCardGrabber : AbstractInteractable
{
    [SerializeField]
    private AnimationCurve hideShowCurve;
    [SerializeField]
    private float animationTime;

    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private float timer = 0f;
    private float deltaTime = 0f;
    private float animationTimeTarget = 0f;

    private Vector3 defaultPosition = Vector3.zero;

    public override void OnClick(){}

    public override void OnExit()
    {
        StopCoroutine(HideShowCoroutine());
        animationTimeTarget = animationTime;
        //-0.65 Objetivo

        initialPosition = defaultPosition;
        finalPosition = defaultPosition + (Vector3.up * hideShowCurve.Evaluate(1));

        deltaTime = Time.deltaTime;
        StartCoroutine(HideShowCoroutine());
    }

    public override void OnOver()
    {
        StopCoroutine(HideShowCoroutine());
        animationTimeTarget = 0f;
        //0 Objetivo
        
        initialPosition = defaultPosition + (Vector3.up * hideShowCurve.Evaluate(0));
        finalPosition = defaultPosition;

        deltaTime = -Time.deltaTime;
        StartCoroutine(HideShowCoroutine());
    }

    IEnumerator HideShowCoroutine()
    {        
        while(timer >= 0f && timer <= animationTime)
        {
            float ratio = timer/animationTime;
            float positionOffset = hideShowCurve.Evaluate(ratio);
            transform.localPosition = initialPosition + (Vector3.up * positionOffset);
            yield return 0;

            timer += deltaTime;
        }

        transform.localPosition = finalPosition;
        timer = animationTimeTarget;

        yield return 0;
    }
}

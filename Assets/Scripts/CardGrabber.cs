using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script se va a encargar de gestionar todas las cartas que tiene el palyer.
public class CardGrabber : MonoBehaviour
{
    private List<Card> cards;
    
    private Vector3 defaultCardPosition;

    [Serializable]
    public class CardAnimation
    {
        public AnimationCurve LerpCurve;
        public float MovementTime = 0.2f;
    }

    [Header("Order in Hand")]
    [SerializeField]
    private AnimationCurve orderLerpCurve;
    [SerializeField]
    private float orderMovementTime = 0.3f;

    [Header("Draw Card")]
    [SerializeField]
    private CardAnimation drawAnimation;

    [Header("Play Card")]
    [SerializeField]
    private AnimationCurve playLerpCurve;
    [SerializeField]
    private float playMovementTime = 0.3f;
    
    [Header("On Over Card")]
    [SerializeField]
    private CardAnimation overAnimation;

    [Header("On Exit Card")]
    [SerializeField]
    private CardAnimation exitAnimation;
    
    private Vector3 initialMovementPoint;
    private Vector3 finalMovementPoint;
    private float movementTime = 0.2f;
    private float acc_movementTime = 0f;
    private bool moveCard = false;

    void Awake() 
    {
        cards = new List<Card>();    
    }

    public void DrawCard(Card card)
    {
        cards.Add(card);

        card.onClickCard += ClickCardAnimation;
        card.onOverCard += OverCardAnimation;
        card.onExitCard += ExitCardAnimation;

        DrawCardAnimation(card);
    }

    public void PlayCard(Card card)
    {
        cards.Remove(card);

        card.onClickCard -= ClickCardAnimation;
        card.onOverCard -= OverCardAnimation;
        card.onExitCard -= ExitCardAnimation;
    }

    void FixedUpdate() 
    {
        if(moveCard)
        {
            transform.localPosition = Vector3.Lerp(initialMovementPoint, finalMovementPoint, acc_movementTime / movementTime);
            acc_movementTime += Time.fixedDeltaTime;

            if(acc_movementTime >= movementTime)
            {
                FinishMovement();
            }
        }
    }

    private void FinishMovement()
    {
        transform.localPosition = finalMovementPoint;
        acc_movementTime = 0f;
        moveCard = false;
    }

    public void DrawCardAnimation(Card card)
    {
        SetUpAnimation(card, drawAnimation);
        card.CurrentAnimation.Animation = StartCoroutine(CardAnimationCoroutine(card, drawAnimation));
    }

    public void OverCardAnimation(Card card)
    {
        SetUpAnimation(card, overAnimation);
        card.CurrentAnimation.Animation = StartCoroutine(CardAnimationCoroutine(card, overAnimation));
    }

    public void ClickCardAnimation(Card card)
    {
        Debug.Log("CLICK CARD ANIMATION");
    }

    public void ExitCardAnimation(Card card)
    {
        SetUpAnimation(card, exitAnimation);
        card.CurrentAnimation.Animation = StartCoroutine(CardAnimationCoroutine(card, exitAnimation));
    }

    private void SetUpAnimation(Card card, CardAnimation _animation)
    {
        if(card.CurrentAnimation.Animation != null) StopCoroutine(card.CurrentAnimation.Animation);

        // Si la animacion no es null, significa que está ejecutando una y que FinalPosition tiene un valor válido
        Vector3 initialPosition = (card.CurrentAnimation.Animation != null) ? card.CurrentAnimation.FinalPosition : card.GetLocalPosition();
        Vector3 finalPosition = initialPosition + (Vector3.up * _animation.LerpCurve.Evaluate(1));

        card.CurrentAnimation.InitialPosition = initialPosition;
        card.CurrentAnimation.FinalPosition = finalPosition;
    }

    IEnumerator CardAnimationCoroutine(Card card, CardAnimation animation)
    {
        float timer = 0f;

        while(timer < animation.MovementTime)
        {
            float lerpRatio = timer/animation.MovementTime;
            float positionOffset = animation.LerpCurve.Evaluate(lerpRatio);
            card.SetLocalPosition(card.CurrentAnimation.InitialPosition + (Vector3.up * positionOffset));
            yield return 0;

            timer += Time.deltaTime;
        }

        card.SetLocalPosition(card.CurrentAnimation.FinalPosition);

        card.CurrentAnimation.Animation = null;

        yield return 0;
    }

    public void MoveAnimation(bool isDraw)
    {
        //Ya hay un movimiento en proceso
        if(moveCard) FinishMovement();

        Vector3 leftOrRight = (isDraw) ? Vector3.right : Vector3.left; 

        initialMovementPoint = transform.localPosition;
        finalMovementPoint = transform.localPosition + (leftOrRight * 0.35f);

        transform.localPosition = initialMovementPoint;

        movementTime = orderMovementTime;
        moveCard = true;
    }

    public void PlayAnimation()
    {
        //Ya hay un movimiento en proceso
        if(moveCard) FinishMovement();

        initialMovementPoint = transform.localPosition;
        finalMovementPoint = transform.localPosition + (Vector3.up * 5f);

        transform.localPosition = initialMovementPoint;
        
        movementTime = playMovementTime;
        moveCard = true;
    }
}

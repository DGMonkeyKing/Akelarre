using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Posición de la carta baja: -1.5. Posición entera: 1
public class Card : MonoBehaviour
{
    public Cards CardData;
    
    [SerializeField]
    public SpriteRenderer cardImageRenderer;
    [SerializeField]
    public TMP_Text cardTextRenderer;

#region CARD_ANIMATION

    [Header("Order in Hand")]
    [SerializeField]
    private float orderMovementTime = 0.2f;

    [Header("Draw Card")]
    [SerializeField]
    private float drawMovementTime = 0.3f;

    [Header("Play Card")]
    [SerializeField]
    private float playMovementTime = 0.3f;
    
    private Vector3 initialMovementPoint;
    private Vector3 finalMovementPoint;
    private float movementTime = 0.2f;
    private float acc_movementTime = 0f;
    private bool moveCard = false;

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

    public void DrawAnimation()
    {
        //Ya hay un movimiento en proceso
        if(moveCard) FinishMovement();

        initialMovementPoint = transform.localPosition - (Vector3.up * 3f);
        finalMovementPoint = transform.localPosition;

        movementTime = drawMovementTime;
        moveCard = true;
    }

    public void MoveAnimation(bool isDraw)
    {
        //Ya hay un movimiento en proceso
        if(moveCard) FinishMovement();

        Vector3 leftOrRight = (isDraw) ? Vector3.right : Vector3.left; 

        initialMovementPoint = transform.localPosition;
        finalMovementPoint = transform.localPosition + (leftOrRight * 0.35f);

        movementTime = orderMovementTime;
        moveCard = true;
    }

    public void PlayAnimation()
    {
        //Ya hay un movimiento en proceso
        if(moveCard) FinishMovement();

        initialMovementPoint = transform.localPosition;
        finalMovementPoint = transform.localPosition + (Vector3.up * 3f);

        movementTime = playMovementTime;
        moveCard = true;
    }
#endregion

    // Start is called before the first frame update
    void Start()
    {
        cardImageRenderer.sprite = CardData.Image;
        cardTextRenderer.text = CardData.Description;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        cardImageRenderer.sprite = CardData.Image;
        cardTextRenderer.text = CardData.Description;
#endif
    }

    public void CardEffect(Player player, List<Player> players)
    {

    }
}

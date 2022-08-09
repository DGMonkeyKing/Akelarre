using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;

// Posición de la carta baja: -1.5. Posición entera: 1
public class Card : AbstractInteractable
{
    public delegate void OnInteractWithCard(Card card);
    public event OnInteractWithCard onOverCard;
    public event OnInteractWithCard onExitCard;
    public event OnInteractWithCard onClickCard;

    public class CardAnimation
    {
        public Vector3 InitialPosition;
        public Vector3 FinalPosition;
        public Coroutine Animation;
    }

    public Cards CardData;
    
    [SerializeField]
    public SpriteRenderer cardImageRenderer;
    [SerializeField]
    public TMP_Text cardTextRenderer;

    
    [SerializeField]
    private Material overMaterial;
    [SerializeField]
    private Material notOverMaterial;

    private SpriteRenderer _spriteRenderer;
    private new Collider _collider;

    private bool lastOver;
    private bool isOver;

    public CardAnimation CurrentAnimation;

    void Awake()
    {
        CurrentAnimation = new CardAnimation();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider>();
    }

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

    public override void OnOver()
    {
        _spriteRenderer.material = overMaterial;
        onOverCard(this);
    }

    public override void OnExit()
    {
        _spriteRenderer.material = notOverMaterial;
        onExitCard(this);
    }

    public override void OnClick()
    {
        onClickCard(this);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public Vector3 GetLocalPosition()
    {
        return transform.localPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private ObjectPool OP;

    private int numberOfCard = 0;
    private List<Card> cards;
    [SerializeField]
    private GameObject cardPrefab;

    void Awake()
    {
        numberOfCard = PlayerPrefs.GetInt(GameData.NUMBER_OF_CARDS, 120);
        cards = new List<Card>();
        OP = new ObjectPool(cardPrefab, 8, gameObject);

        for(int i = 0; i < numberOfCard; i++)
        {
            GameObject go = OP.GetObject();
            cards.Add(go.GetComponent<Card>());
            go.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

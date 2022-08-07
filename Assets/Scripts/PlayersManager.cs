using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    private const float initialAngle = 4.71238898038f; //270º en radianes
    public float radiusOfAkelarre = 3f;

    [SerializeField] private GameObject playerPrefab;

    [Header("Previsualization and Gizmo")]
    [SerializeField]
    private int NoP = 6;

    void OnDrawGizmos()
    {
        //Radio igual al tamaño del circulo, en este caso 2.5
        List<Vector3> points = new List<Vector3>();
        List<float> angles = new List<float>();

        angles.Add(initialAngle);
            
        for(int i = 1; i < NoP; i++)
        {
            angles.Add(angles[i-1] + (2f * Mathf.PI / NoP));
        }

        points.Add(new Vector3(radiusOfAkelarre * Mathf.Cos(initialAngle), 0, radiusOfAkelarre * Mathf.Sin(initialAngle)));

        for(int i = 0; i < NoP; i++)
        {
            points.Add(new Vector3(radiusOfAkelarre * Mathf.Cos(angles[i]), 0, radiusOfAkelarre * Mathf.Sin(angles[i])));
        }

        Gizmos.color = new Color(0f,0f,1f,1f);

        for(int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawSphere(points[i], 0.5f);
        }

        Gizmos.color = new Color(1f,0f,0f,0.1f);
        Gizmos.DrawSphere(Vector3.zero, radiusOfAkelarre);
    }


    private ObjectPool OP;
    private List<Player> players;
    private int numberOfPlayers;

    void Awake()
    {
        numberOfPlayers = PlayerPrefs.GetInt(GameData.NUMBER_OF_PLAYERS, 4);
        players = new List<Player>();
        OP = new ObjectPool(playerPrefab, 8, gameObject);

        for(int i = 0; i < numberOfPlayers; i++)
        {
            GameObject go = OP.GetObject();
            players.Add(go.GetComponent<Player>());
            go.SetActive(true);
        }

        //Recolocar los jugadores que vamos a usar.
        List<Vector3> points = GetPostionOfPlayersInAkelarre();
        OrderPlayersInTheAkelarre(points);
    }

    private List<Vector3> GetPostionOfPlayersInAkelarre()
    {
        //Radio igual al tamaño del circulo, en este caso 2.5
        List<Vector3> points = new List<Vector3>();
        List<float> angles = new List<float>();

        angles.Add(initialAngle);
            
        for(int i = 1; i < numberOfPlayers; i++)
        {
            angles.Add(angles[i-1] + (2f * Mathf.PI / numberOfPlayers));
        }

        points.Add(new Vector3(radiusOfAkelarre * Mathf.Cos(initialAngle), 0, radiusOfAkelarre * Mathf.Sin(initialAngle)));

        for(int i = 1; i < numberOfPlayers; i++)
        {
            points.Add(new Vector3(radiusOfAkelarre * Mathf.Cos(angles[i]), 0, radiusOfAkelarre * Mathf.Sin(angles[i])));
        }

        return points;
    }

    private void OrderPlayersInTheAkelarre(List<Vector3> points)
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            players[i].transform.localPosition = points[i];
        }
    }

    void OnEnable()
    {
        //Suscribirse a los eventos de los Player para saber cuando hacen algo.
        foreach(Player p in players)
        {
            p.onCardPlayed += OnCardPlayed;
        }
    }

    void OnDisable()
    {
        //Desuscribirse de los eventos.
        foreach(Player p in players)
        {
            p.onCardPlayed -= OnCardPlayed;
        }
    }

    void OnCardPlayed(Player player, Card card)
    {
        //Ejecutamos el efecto de la carta
        card.CardEffect(player, players);
    }
}

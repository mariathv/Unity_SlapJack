using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static global;

public class GlobalGameController : MonoBehaviour
{
    public int numberOfPlayers = 4;
    public int cardsPerPlayer = 5;
    public GameObject cardPrefab;
    public Dictionary<(Suit, Rank), Sprite> cardImages;

    private List<Player> players;
    private Deck deck;
    public double timer;
    public int turnOf;

    GlobalAssets gameAssetObjects;
    void Start()
    {
        GameObject gameControllerObject = GameObject.Find("GameController");
        gameAssetObjects = gameControllerObject.GetComponent<GlobalAssets>();
        Debug.Log("START");

        timer = 0;
        turnOf = 0;
        
    // Load card images
        LoadCardImages();

        // Initialize the deck and shuffle it
        deck = new Deck(cardImages);
        deck.Shuffle();

        // Initialize players
        players = new List<Player>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players.Add(new Player());
        }

        // Deal cards to players
        DealCards();

        // Display each player's hand
        DisplayHands();

        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Player " + (i + 1) + " hand:\n" + players[i].ToString());
        }
    }

    private void Update()
    {
        timer = timer + 0.1;
    }

    void LoadCardImages()
    {
        cardImages = new Dictionary<(Suit, Rank), Sprite>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 1; i <= 13; i++) // Assuming 13 ranks per suit
            {
                Rank rank = (Rank)i;
                string imageName = $"{suit} {i}";
                Sprite image = Resources.Load<Sprite>($"Sprites/{imageName}");
                cardImages[(suit, rank)] = image;
            }
        }
    }
    
    public void throwTopCard()
    {
        Debug.Log("Player " + turnOf + " throwing card");
        Card topCard = players[turnOf].hand[0];
        players[turnOf].hand.RemoveAt(0);
        if (turnOf == 0) {
            gameAssetObjects.P1Hand.text = "" + players[turnOf].hand.Count +"";
        }
        else
        {
            gameAssetObjects.P2Hand.text = "" + players[turnOf].hand.Count + "";
        }
        turnOf = (turnOf + 1) % numberOfPlayers;
        Vector3 Position;
        Position = new Vector3(-0.03f, 0.04f, 0);

        GameObject mainC = gameAssetObjects.mainCard;
        SpriteRenderer spriteRenderer = mainC.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = topCard.image;
        
        
    }
    void DisplayHands()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            for (int j = 0; j < player.hand.Count; j++)
            {
                Card card = player.hand[j];
                Vector3 position;
                if (i == 0)
                    position = new Vector3(-1.46f, -3.72f, 0); // Adjust positioning as needed
                else
                    position = new Vector3(1.46f, 3.72f, 0); // Adjust positioning as needed

                GameObject cardObject = Instantiate(cardPrefab, position, Quaternion.identity);
                cardObject.GetComponent<SpriteRenderer>().sprite = card.image;
            }
        }
    }
    void DealCards()
    {
        for (int i = 0; i < cardsPerPlayer; i++)
        {
            foreach (Player player in players)
            {
                List<Card> dealtCards = deck.Deal(1);
                player.ReceiveCards(dealtCards);
            }
        }
    }

    
}


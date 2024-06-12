using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global : MonoBehaviour
{
    
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum Rank
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    [Serializable]
    public class Card
    {
        public Suit suit;
        public Rank rank;
        public Sprite image; // Reference to the card image

        public Card(Suit suit, Rank rank, Sprite image)
        {
            this.suit = suit;
            this.rank = rank;
            this.image = image;
        }

        public override string ToString()
        {
            return rank + " of " + suit;
        }
    }
    public class Deck
    {
        private List<Card> cards;

        public Deck(Dictionary<(Suit, Rank), Sprite> cardImages)
        {
            cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Sprite image = cardImages[(suit, rank)];
                    cards.Add(new Card(suit, rank, image));
                }
            }
        }

        public void Shuffle()
        {
            System.Random rng = new System.Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public List<Card> Deal(int numberOfCards)
        {
            List<Card> dealtCards = new List<Card>();
            for (int i = 0; i < numberOfCards; i++)
            {
                if (cards.Count > 0)
                {
                    Card card = cards[0];
                    dealtCards.Add(card);
                    cards.RemoveAt(0);
                }
            }
            return dealtCards;
        }
    }


    public class Player
    {
        public List<Card> hand;

        public Player()
        {
            hand = new List<Card>();
        }

        public void ReceiveCards(List<Card> cards)
        {
            hand.AddRange(cards);
        }

        public void throwCard()
        {
            
        }

        public override string ToString()
        {
            string handString = "";
            foreach (Card card in hand)
            {

                handString += card.ToString() + "\n";
            }
            return handString;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

// Carson Moon

// Card struct for storing our card attributes.
struct Card{
    char face;
    char suit;

    public Card(char face, char suit){
        this.face = face;
        this.suit = suit;
    }

    public char GetFace(){
        return face;
    }

    public char GetSuit(){
        return suit;
    }
}

public class Task3 : MonoBehaviour
{
    // Runtime
    bool hasWon = false;

    [Header("Deck")]
    List<Card> deck = new List<Card>();

    [Header("Hand")]
    List<Card> hand = new List<Card>();

    
    void Start(){
        // Create deck.
        deck.Add(new Card('A', '♠'));
        deck.Add(new Card('A', '♣'));
        deck.Add(new Card('A', '♥'));
        deck.Add(new Card('A', '♦'));
        deck.Add(new Card('K', '♠'));
        deck.Add(new Card('K', '♣'));
        deck.Add(new Card('K', '♥'));
        deck.Add(new Card('K', '♦'));
        deck.Add(new Card('Q', '♠'));
        deck.Add(new Card('Q', '♣'));
        deck.Add(new Card('Q', '♥'));
        deck.Add(new Card('Q', '♦'));
        deck.Add(new Card('J', '♠'));
        deck.Add(new Card('J', '♣'));
        deck.Add(new Card('J', '♥'));
        deck.Add(new Card('J', '♦'));

        // Draw 4 cards in the beginning.
        for(int i=0; i<4; i++){
            int index = Random.Range(0, deck.Count);
            hand.Add(deck[index]);
            deck.Remove(deck[index]);
        }

        // Check our initial hand.
        if(WinCheck()){
            hasWon = true;
            PrintOutHand(true);
        }else{
            PrintOutHand(false);
        }

        // Start loop of discards until the player wins or runs out of cards.
        while(!hasWon && deck.Count > 0){
            if(DiscardAndDraw()){
                hasWon = true;
            }
        }

        // If we break out of the loop, we have either won or run out of cards.
        if(!hasWon){
            Debug.LogFormat("You have lost! Final hand: {0}{1}, {2}{3}, {4}{5}, {6}{7}.",
                hand[0].GetFace(), hand[0].GetSuit(), hand[1].GetFace(), hand[1].GetSuit(), 
                hand[2].GetFace(), hand[2].GetSuit(), hand[3].GetFace(), hand[3].GetSuit());
        }
    }

// Discards a random card and draws a random card.
    private bool DiscardAndDraw(){
        // Get random card from deck.
        int index = Random.Range(0, deck.Count);
        Card drawnCard = deck[index];
        deck.Remove(deck[index]);

        // Discard and draw random card from hand.
        int discardIndex = Random.Range(0, 3);
        Card discardedCard = hand[discardIndex];
        hand[discardIndex] = drawnCard;

        if(WinCheck()){
            PrintOutHand(discardedCard, drawnCard, true);
            return true;
        }else{
            PrintOutHand(discardedCard, drawnCard, false);
            return false;
        }
    }

// Runs through our hand and determines if we have won or not.
    private bool WinCheck(){
        int[] winCheckArr = {0, 0, 0, 0};

        for(int i=0; i<4; i++){
            switch(hand[i].GetSuit()){
                case '♠':
                    winCheckArr[0]++;
                    if(winCheckArr[0] >= 3)
                        return true;
                    break;
                case '♣':
                    winCheckArr[1]++;
                    if(winCheckArr[1] >= 3)
                        return true;
                    break;
                case '♥':
                    winCheckArr[2]++;
                    if(winCheckArr[2] >= 3)
                        return true;
                    break;
                case '♦':
                    winCheckArr[3]++;
                    if(winCheckArr[3] >= 3)
                        return true;
                    break;
            }
        }

        return false;
    }

// Prints out our hand depending on the situation.
#region Print Out Hand
    private void PrintOutHand(Card discard, Card drawn, bool won){
        if(won){
            Debug.LogFormat("I discard {0}{1} and draw {2}{3}. My hand is: {4}{5}, {6}{7}, {8}{9}, {10}{11}. The game is WON.",
                discard.GetFace(), discard.GetSuit(), drawn.GetFace(), drawn.GetSuit(), hand[0].GetFace(), hand[0].GetSuit(),
                hand[1].GetFace(), hand[1].GetSuit(), hand[2].GetFace(), hand[2].GetSuit(), hand[3].GetFace(), hand[3].GetSuit());
        }else{
            Debug.LogFormat("I discard {0}{1} and draw {2}{3}. My hand is: {4}{5}, {6}{7}, {8}{9}, {10}{11}. This is not a winning hand.",
                discard.GetFace(), discard.GetSuit(), drawn.GetFace(), drawn.GetSuit(), hand[0].GetFace(), hand[0].GetSuit(),
                hand[1].GetFace(), hand[1].GetSuit(), hand[2].GetFace(), hand[2].GetSuit(), hand[3].GetFace(), hand[3].GetSuit());
        }
    }

    private void PrintOutHand(bool won){
        if(won){
            Debug.LogFormat("I made the initial deck and draw. My hand is: {0}{1}, {2}{3}, {4}{5}, {6}{7}. The game is WON.",
                hand[0].GetFace(), hand[0].GetSuit(), hand[1].GetFace(), hand[1].GetSuit(), hand[2].GetFace(), hand[2].GetSuit(),
                hand[3].GetFace(), hand[3].GetSuit());
        }else{
            Debug.LogFormat("I made the initial deck and draw. My hand is: {0}{1}, {2}{3}, {4}{5}, {6}{7}. This is not a winning hand.",
                hand[0].GetFace(), hand[0].GetSuit(), hand[1].GetFace(), hand[1].GetSuit(), hand[2].GetFace(), hand[2].GetSuit(),
                hand[3].GetFace(), hand[3].GetSuit());
        }
    }
#endregion
}

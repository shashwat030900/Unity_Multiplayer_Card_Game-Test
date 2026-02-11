using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance; 
    public List<CardData> allCards;     

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public CardData GetCardById(int id)
    {
        foreach (CardData card in allCards)
        {
            if (card.id == id)
            {
                return card;
            }
        }
        Debug.LogError($"Card with ID {id} not found!");
        return null;
    }
}   
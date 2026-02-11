using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject cardPrefab;   
    public Transform handContainer; 

    

    public void AddCard(int id)
    {
     
        CardData data = CardDatabase.Instance.GetCardById(id);
        
        if (data != null)
        {
            
            GameObject newCard = Instantiate(cardPrefab, handContainer);
            
            
            CardDisplay display = newCard.GetComponent<CardDisplay>();
            display.Initialize(data);
        }
    }

    public Transform playedCardArea; 

    public void PlaySelectedCards()
    {
        
        for (int i = handContainer.childCount - 1; i >= 0; i--)
        {
            Transform card = handContainer.GetChild(i);
            CardInteraction interaction = card.GetComponent<CardInteraction>();

            
            if (interaction != null && interaction.isSelected)
            {
                card.SetParent(playedCardArea);
                
               
                card.GetComponent<UnityEngine.UI.Button>().interactable = false;
                
                
                card.GetComponent<CardDisplay>().SetFaceDown(true);

                if (interaction != null && interaction.isSelected)
            {
                
                int id = card.GetComponent<CardDisplay>().cardData.id;
                
                if (MatchManager.Instance.localPlayerNet != null)
                {
                    MatchManager.Instance.localPlayerNet.SendPlayCard(id);
                }
            }
            }
        }
    }

    
}
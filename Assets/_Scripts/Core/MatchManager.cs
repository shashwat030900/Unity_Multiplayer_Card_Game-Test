using UnityEngine;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public static MatchManager Instance; 

    [Header("Game State")]
    public int currentTurn = 1;
    public int maxMana = 1;
    public int currentMana = 1;

    [Header("UI")]
    public TMP_Text manaText;

    [Header("Turn State")]
    public int playersReady = 0; 

    [Header("Network")]
    public NetworkPlayer localPlayerNet; 
    public Transform opponentArea;       
    public GameObject cardPrefab;        

    
    public void OnOpponentPlayedCard(int cardId)
    {
       
        GameObject enemyCard = Instantiate(cardPrefab, opponentArea);
        
        
        CardData data = CardDatabase.Instance.GetCardById(cardId);
        
        
        CardDisplay display = enemyCard.GetComponent<CardDisplay>();
        display.Initialize(data); 
        
        
        display.SetFaceDown(true);
        
       
        Destroy(enemyCard.GetComponent<UnityEngine.UI.Button>());
    }    void Awake()
    {
        Instance = this;
    }

    
    
    public bool TrySpendMana(int cost)
    {
        if (currentMana >= cost)
        {
            currentMana -= cost;
            UpdateManaUI();
            return true; 
        }
        else
        {
            Debug.Log("Not enough Mana!");
            return false; 
        }
    }

    
    public void RefundMana(int cost)
    {
        currentMana += cost;
        UpdateManaUI();
    }

    void UpdateManaUI()
    {
        manaText.text = $"Mana: {currentMana}/{maxMana}";
    }

    public PlayerHand playerHand; 

    public void OnEndTurnClicked()
    {
       
        playerHand.PlaySelectedCards();

        
        int randomCardId = Random.Range(1, 3); 
        playerHand.AddCard(randomCardId);

       
        currentTurn++;
        maxMana++; 
        if (maxMana > 6) maxMana = 6;
        
        currentMana = maxMana;
        UpdateManaUI();
        
        Debug.Log($"Turn {currentTurn} Started. Drew card {randomCardId}.");

        if (localPlayerNet != null)
        {
            localPlayerNet.SendEndTurn();
        }
        
        Debug.Log("End Turn Clicked. Waiting for opponent...");
    }

    public void StartGame()
    {
        Debug.Log("Game Starting!");
        
       
        currentTurn = 1;
        maxMana = 1;
        currentMana = 1;
        UpdateManaUI();

       
        playerHand.AddCard(1); 
        playerHand.AddCard(2);
        playerHand.AddCard(1);
    }

    public void StartRevealSequence()
    {
        Debug.Log("Both players ready! Revealing cards...");

        
        foreach (Transform card in playerHand.playedCardArea)
        {
            card.GetComponent<CardDisplay>().SetFaceDown(false);
        }

       
        foreach (Transform card in opponentArea)
        {
            card.GetComponent<CardDisplay>().SetFaceDown(false); 
        }

        
    }

}
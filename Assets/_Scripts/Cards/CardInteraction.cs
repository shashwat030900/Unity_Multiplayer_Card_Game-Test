using UnityEngine;
using UnityEngine.UI; 
public class CardInteraction : MonoBehaviour
{
    public bool isSelected = false;
    private Image cardImage;
    private Color originalColor;
    private Button myButton; 

    void Awake()
    {
        cardImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        originalColor = cardImage.color;
    }

    void Start()
    {
        
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnCardClicked);
        }
    }

    
    public void OnCardClicked()
    {
      
        int cost = GetComponent<CardDisplay>().cardData.cost;

        if (isSelected)
        {
           
            MatchManager.Instance.RefundMana(cost);
            
           
            isSelected = false;
            cardImage.color = originalColor;
        }
        else
        {
            
            bool success = MatchManager.Instance.TrySpendMana(cost);
            
            if (success)
            {
                
                isSelected = true;
                cardImage.color = Color.green;
            }
        }
    }

    
}
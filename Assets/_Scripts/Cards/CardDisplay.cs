using UnityEngine;
using TMPro;       
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardData cardData; 

    [Header("UI References")]
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text powerText;
        public void Initialize(CardData data)
    {
        cardData = data;
        nameText.text = cardData.name;
        costText.text = cardData.cost.ToString();
        powerText.text = cardData.power.ToString();
    }

    public void SetFaceDown(bool isFaceDown)
    {
        
        
        if (isFaceDown)
        {
            nameText.gameObject.SetActive(false);
            costText.gameObject.SetActive(false);
            powerText.gameObject.SetActive(false);
            GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.2f, 0.2f); 
        }
        else
        {
            nameText.gameObject.SetActive(true);
            costText.gameObject.SetActive(true);
            powerText.gameObject.SetActive(true);
            GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }
}
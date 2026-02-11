using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ConnectionUI : MonoBehaviour
{
    public Button hostButton;
    public Button joinButton;
    public GameObject connectionPanel; 

    void Start()
    {
        
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            HideUI();
        });

        joinButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            HideUI();
        });
    }

    void HideUI()
    {
        connectionPanel.SetActive(false);
    }
}
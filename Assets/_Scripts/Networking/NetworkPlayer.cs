using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    
    [System.Serializable]
    public class NetworkMessage
    {
        public string action;   
        public int cardId;      
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            
            MatchManager.Instance.StartGame();
            
            
            MatchManager.Instance.localPlayerNet = this;
        }
    }

    
    public void SendPlayCard(int id)
    {
        if (IsOwner)
        {
           
            NetworkMessage msg = new NetworkMessage();
            msg.action = "playCard";
            msg.cardId = id;

            string jsonString = JsonUtility.ToJson(msg);

            
            SubmitPlayRequestServerRpc(jsonString);
        }
    }

    
    [ServerRpc]
    void SubmitPlayRequestServerRpc(string json)
    {
        
        ReceivePlayClientRpc(json);
    }

    
    [ClientRpc]
    void ReceivePlayClientRpc(string json)
    {
        
        if (IsOwner) return;

        
        NetworkMessage msg = JsonUtility.FromJson<NetworkMessage>(json);

        if (msg.action == "playCard")
        {
            Debug.Log("Opponent played a card!");
           
            MatchManager.Instance.OnOpponentPlayedCard(msg.cardId);
        }
    }

    public void SendEndTurn()
    {
        if (IsOwner)
        {
            SubmitEndTurnServerRpc();
        }
    }

    [ServerRpc]
    void SubmitEndTurnServerRpc()
    {
        
        MatchManager.Instance.playersReady++;

        Debug.Log($"Server: {MatchManager.Instance.playersReady} players ready.");

        
        if (MatchManager.Instance.playersReady == 2)
        {
            
            MatchManager.Instance.playersReady = 0;
            
            
            TriggerRevealClientRpc();
        }
    }

    [ClientRpc]
    void TriggerRevealClientRpc()
    {
        
        MatchManager.Instance.StartRevealSequence();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour
{
    [Header("Debug Info")]
    public Text debugText;
    public bool showDebugInfo = true;
    
    void Start()
    {
        CheckSetup();
    }
    
    void Update()
    {
        if (showDebugInfo)
        {
            UpdateDebugInfo();
        }
    }
    
    void CheckSetup()
    {
        Debug.Log("=== SETUP CHECK ===");
        
        // Check EnergyManager
        if (EnergyManager.Instance == null)
        {
            Debug.LogError("❌ EnergyManager not found! Create a GameObject with EnergyManager script.");
        }
        else
        {
            Debug.Log($"✅ EnergyManager found. Energy: {EnergyManager.Instance.currentEnergy}/{EnergyManager.Instance.maxEnergy}");
        }
        
        // Check GameManager
        if (GameManager.Instance == null)
        {
            Debug.LogError("❌ GameManager not found! Create a GameObject with GameManager script.");
        }
        else
        {
            Debug.Log($"✅ GameManager found. Temples: {GameManager.Instance.totalTemples}");
        }
        
        // Check Temples
        Temple[] temples = FindObjectsByType<Temple>(FindObjectsSortMode.None);
        Debug.Log($"Found {temples.Length} temples in scene");
        
        foreach (Temple temple in temples)
        {
            if (temple.GetComponent<Collider>() == null)
            {
                Debug.LogError($"❌ Temple '{temple.name}' missing Collider component!");
            }
            else if (!temple.GetComponent<Collider>().isTrigger)
            {
                Debug.LogError($"❌ Temple '{temple.name}' Collider is not set to 'Is Trigger'!");
            }
            else
            {
                Debug.Log($"✅ Temple '{temple.name}' has trigger collider");
            }
        }
        
        // Check Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("❌ No GameObject with 'Player' tag found!");
        }
        else
        {
            Debug.Log($"✅ Player found: {player.name}");
            if (player.GetComponent<Collider>() == null)
            {
                Debug.LogError($"❌ Player '{player.name}' missing Collider component!");
            }
        }
        
        // Check BridgeSequencer
        BridgeSequencer bridgeSequencer = FindFirstObjectByType<BridgeSequencer>();
        if (bridgeSequencer == null)
        {
            Debug.LogError("❌ BridgeSequencer not found!");
        }
        else
        {
            Debug.Log($"✅ BridgeSequencer found with {bridgeSequencer.GetTotalSegments()} segments");
        }
        
        Debug.Log("=== SETUP CHECK COMPLETE ===");
    }
    
    void UpdateDebugInfo()
    {
        if (debugText == null) return;
        
        string info = "DEBUG INFO:\n";
        
        // Energy info
        if (EnergyManager.Instance != null)
        {
            info += $"Energy: {EnergyManager.Instance.currentEnergy}/{EnergyManager.Instance.maxEnergy}\n";
        }
        else
        {
            info += "EnergyManager: NOT FOUND\n";
        }
        
        // Temple info
        if (GameManager.Instance != null)
        {
            info += $"Temples: {GameManager.Instance.rechargedTemples}/{GameManager.Instance.totalTemples}\n";
        }
        else
        {
            info += "GameManager: NOT FOUND\n";
        }
        
        // Player info
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            info += $"Player: {player.name}\n";
        }
        else
        {
            info += "Player: NOT FOUND\n";
        }
        
        // Bridge info
        BridgeSequencer bridgeSequencer = FindFirstObjectByType<BridgeSequencer>();
        if (bridgeSequencer != null)
        {
            info += $"Bridge: {bridgeSequencer.GetCurrentSegmentIndex()}/{bridgeSequencer.GetTotalSegments()}\n";
            info += $"Constructing: {bridgeSequencer.IsConstructing()}\n";
        }
        else
        {
            info += "BridgeSequencer: NOT FOUND\n";
        }
        
        debugText.text = info;
    }
    
    [ContextMenu("Run Setup Check")]
    public void RunSetupCheck()
    {
        CheckSetup();
    }
    
    [ContextMenu("Add Energy")]
    public void AddEnergy()
    {
        if (EnergyManager.Instance != null)
        {
            EnergyManager.Instance.AddEnergy(50);
        }
    }
    
    [ContextMenu("Force Bridge Construction")]
    public void ForceBridgeConstruction()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ForceBridgeConstruction();
        }
    }
}

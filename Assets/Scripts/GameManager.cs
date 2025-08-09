using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Temple Management")]
    public List<Temple> temples = new List<Temple>();
    public int totalTemples = 0;
    public int rechargedTemples = 0;
    
    [Header("Bridge Sequencer")]
    public BridgeSequencer bridgeSequencer;
    
    [Header("UI")]
    public UnityEngine.UI.Text progressText;
    public UnityEngine.UI.Slider progressSlider;
    
    private bool allTemplesRecharged = false;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        // Find all temples in the scene if not manually assigned
        if (temples.Count == 0)
        {
            Temple[] foundTemples = FindObjectsByType<Temple>(FindObjectsSortMode.None);
            temples.AddRange(foundTemples);
        }
        
        totalTemples = temples.Count;
        rechargedTemples = 0;
        
        Debug.Log($"GameManager initialized with {totalTemples} temples");
        UpdateUI();
    }
    
    public void OnTempleRecharged(Temple temple)
    {
        if (allTemplesRecharged) return;
        
        rechargedTemples++;
        Debug.Log($"Temple recharged! {rechargedTemples}/{totalTemples} temples charged");
        
        UpdateUI();
        
        // Check if all temples are recharged
        if (rechargedTemples >= totalTemples)
        {
            AllTemplesRecharged();
        }
    }
    
    private void AllTemplesRecharged()
    {
        allTemplesRecharged = true;
        Debug.Log("All temples recharged! Starting bridge construction...");
        
        // Call BridgeSequencer to start building the bridge
        if (bridgeSequencer != null)
        {
            bridgeSequencer.StartBridgeConstruction();
        }
        else
        {
            Debug.LogError("BridgeSequencer not assigned to GameManager!");
        }
    }
    
    private void UpdateUI()
    {
        if (progressText != null)
        {
            progressText.text = $"Temples Recharged: {rechargedTemples}/{totalTemples}";
        }
        
        if (progressSlider != null)
        {
            progressSlider.value = (float)rechargedTemples / totalTemples;
        }
    }
    
    public float GetRechargeProgress()
    {
        if (totalTemples == 0) return 0f;
        return (float)rechargedTemples / totalTemples;
    }
    
    public bool AreAllTemplesRecharged()
    {
        return allTemplesRecharged;
    }
    
    // Debug method to manually trigger bridge construction
    [ContextMenu("Force Bridge Construction")]
    public void ForceBridgeConstruction()
    {
        if (!allTemplesRecharged)
        {
            allTemplesRecharged = true;
            rechargedTemples = totalTemples;
        }
        
        if (bridgeSequencer != null)
        {
            bridgeSequencer.StartBridgeConstruction();
        }
    }
    
    // Debug method to reset all temples
    [ContextMenu("Reset All Temples")]
    public void ResetAllTemples()
    {
        allTemplesRecharged = false;
        rechargedTemples = 0;
        
        foreach (Temple temple in temples)
        {
            if (temple != null)
            {
                temple.ResetTemple();
            }
        }
        
        UpdateUI();
    }
}

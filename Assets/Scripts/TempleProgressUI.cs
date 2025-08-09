using UnityEngine;
using UnityEngine.UI;

public class TempleProgressUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text progressText;
    public Slider progressSlider;
    public Text statusText;
    public GameObject bridgeStatusPanel;
    public Text bridgeStatusText;
    
    [Header("Messages")]
    public string progressMessage = "Temples Recharged: {0}/{1}";
    public string allTemplesMessage = "All temples recharged!";
    public string bridgeAppearingMessage = "Bridge is appearing...";
    public string bridgeReadyMessage = "Bridge is ready!";
    
    private TempleManager templeManager;
    
    void Start()
    {
        templeManager = TempleManager.Instance;
        
        if (templeManager == null)
        {
            Debug.LogError("TempleManager not found!");
            return;
        }
        
        UpdateUI();
    }
    
    void Update()
    {
        if (templeManager != null)
        {
            UpdateUI();
        }
    }
    
    void UpdateUI()
    {
        // Update progress
        if (progressText != null)
        {
            progressText.text = string.Format(progressMessage, 
                templeManager.rechargedTemples, 
                templeManager.totalTemples);
        }
        
        if (progressSlider != null)
        {
            progressSlider.value = templeManager.GetRechargeProgress();
        }
        
        // Update status
        if (statusText != null)
        {
            if (templeManager.AreAllTemplesRecharged())
            {
                if (templeManager.HasBridgeAppeared())
                {
                    statusText.text = bridgeReadyMessage;
                }
                else
                {
                    statusText.text = bridgeAppearingMessage;
                }
            }
            else
            {
                statusText.text = "";
            }
        }
        
        // Update bridge status panel
        if (bridgeStatusPanel != null)
        {
            bridgeStatusPanel.SetActive(templeManager.AreAllTemplesRecharged());
        }
        
        if (bridgeStatusText != null)
        {
            if (templeManager.HasBridgeAppeared())
            {
                bridgeStatusText.text = bridgeReadyMessage;
            }
            else if (templeManager.AreAllTemplesRecharged())
            {
                bridgeStatusText.text = bridgeAppearingMessage;
            }
        }
    }
    
    // Public method to force UI update
    public void RefreshUI()
    {
        UpdateUI();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TempleInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 3f;
    public KeyCode interactKey = KeyCode.E;
    public LayerMask playerLayer = 1;
    
    [Header("UI Elements")]
    public GameObject interactionPrompt;
    public Text promptText;
    public string promptMessage = "Press E to Recharge Temple";
    public string noEnergyMessage = "Not enough energy!";
    
    [Header("Visual Feedback")]
    public Material highlightMaterial;
    public Material originalMaterial;
    
    private Temple temple;
    private Renderer templeRenderer;
    private bool playerInRange = false;
    private bool isInteracting = false;
    
    void Start()
    {
        temple = GetComponent<Temple>();
        templeRenderer = GetComponent<Renderer>();
        
        if (templeRenderer != null && originalMaterial == null)
        {
            originalMaterial = templeRenderer.material;
        }
        
        // Hide prompt initially
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }
    
    void Update()
    {
        CheckPlayerInRange();
        
        if (playerInRange && !isInteracting)
        {
            HandleInteraction();
        }
    }
    
    void CheckPlayerInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange, playerLayer);
        bool wasInRange = playerInRange;
        playerInRange = colliders.Length > 0;
        
        // Handle entering/leaving range
        if (playerInRange && !wasInRange)
        {
            OnPlayerEnterRange();
        }
        else if (!playerInRange && wasInRange)
        {
            OnPlayerLeaveRange();
        }
    }
    
    void OnPlayerEnterRange()
    {
        if (temple.CanRecharge())
        {
            ShowInteractionPrompt();
            HighlightTemple(true);
        }
        else
        {
            ShowNoEnergyPrompt();
        }
    }
    
    void OnPlayerLeaveRange()
    {
        HideInteractionPrompt();
        HighlightTemple(false);
    }
    
    void HandleInteraction()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (temple.CanRecharge())
            {
                StartTempleRecharge();
            }
            else
            {
                ShowNoEnergyMessage();
            }
        }
    }
    
    void StartTempleRecharge()
    {
        isInteracting = true;
        HideInteractionPrompt();
        
        // Start the recharge process
        temple.StartRecharge();
        
        // Disable interaction during recharge
        StartCoroutine(RechargeCooldown());
    }
    
    private System.Collections.IEnumerator RechargeCooldown()
    {
        yield return new WaitForSeconds(temple.rechargeTime + 0.5f);
        isInteracting = false;
    }
    
    void ShowInteractionPrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(true);
            if (promptText != null)
            {
                promptText.text = promptMessage;
            }
        }
    }
    
    void ShowNoEnergyPrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(true);
            if (promptText != null)
            {
                promptText.text = noEnergyMessage;
            }
        }
    }
    
    void HideInteractionPrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }
    
    void ShowNoEnergyMessage()
    {
        // Flash the prompt with no energy message
        ShowNoEnergyPrompt();
        StartCoroutine(FlashPrompt());
    }
    
    private System.Collections.IEnumerator FlashPrompt()
    {
        yield return new WaitForSeconds(1f);
        if (playerInRange && temple.CanRecharge())
        {
            ShowInteractionPrompt();
        }
        else
        {
            HideInteractionPrompt();
        }
    }
    
    void HighlightTemple(bool highlight)
    {
        if (templeRenderer != null && highlightMaterial != null)
        {
            templeRenderer.material = highlight ? highlightMaterial : originalMaterial;
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw interaction range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
    
    // Public method to check if player can interact
    public bool CanInteract()
    {
        return playerInRange && temple.CanRecharge() && !isInteracting;
    }
    
    // Public method to get current temple
    public Temple GetTemple()
    {
        return temple;
    }
}

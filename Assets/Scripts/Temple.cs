using UnityEngine;
using System.Collections;

public class Temple : MonoBehaviour
{
    [Header("Temple Settings")]
    public int energyRequired = 10;
    public float rechargeTime = 2f;
    
    [Header("Visual Feedback")]
    public Material unchargedMaterial;
    public Material chargedMaterial;
    public Light templeLight;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip rechargeSound;
    public AudioClip completeSound;
    
    private Renderer templeRenderer;
    private bool isRecharged = false;
    private bool isRecharging = false;
    private bool playerInRange = false;
    
    void Start()
    {
        templeRenderer = GetComponent<Renderer>();
        
        // Set initial material
        if (templeRenderer != null && unchargedMaterial != null)
        {
            templeRenderer.material = unchargedMaterial;
        }
        
        // Disable light initially
        if (templeLight != null)
        {
            templeLight.enabled = false;
        }
    }
    
    void Update()
    {
        // Check for E key press when player is in range
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isRecharged && !isRecharging && EnergyManager.Instance != null && EnergyManager.Instance.HasEnergy(energyRequired))
            {
                StartRecharge();
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    
    public void StartRecharge()
    {
        if (isRecharged || isRecharging) return;
        
        // Spend energy
        if (EnergyManager.Instance == null)
        {
            Debug.LogError("EnergyManager not found!");
            return;
        }
        
        if (!EnergyManager.Instance.SpendEnergy(energyRequired))
        {
            return;
        }
        
        isRecharging = true;
        
        // Play recharge sound
        if (audioSource != null && rechargeSound != null)
        {
            audioSource.PlayOneShot(rechargeSound);
        }
        
        // Start recharge animation
        StartCoroutine(RechargeCoroutine());
    }
    
    private System.Collections.IEnumerator RechargeCoroutine()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < rechargeTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / rechargeTime;
            
            // Smooth material transition
            if (templeRenderer != null && unchargedMaterial != null && chargedMaterial != null)
            {
                // Create a temporary material for smooth transition
                if (templeRenderer.material.name.Contains("(Instance)"))
                {
                    // Use the existing instance
                    Color unchargedColor = unchargedMaterial.color;
                    Color chargedColor = chargedMaterial.color;
                    templeRenderer.material.color = Color.Lerp(unchargedColor, chargedColor, progress);
                }
                else
                {
                    // Create new material instance for transition
                    templeRenderer.material = new Material(unchargedMaterial);
                    Color unchargedColor = unchargedMaterial.color;
                    Color chargedColor = chargedMaterial.color;
                    templeRenderer.material.color = Color.Lerp(unchargedColor, chargedColor, progress);
                }
            }
            
            yield return null;
        }
        
        // Complete recharge
        isRecharged = true;
        isRecharging = false;
        
        // Set final charged material
        if (templeRenderer != null && chargedMaterial != null)
        {
            templeRenderer.material = chargedMaterial;
        }
        
        // Enable light
        if (templeLight != null)
        {
            templeLight.enabled = true;
        }
        
        // Play completion sound
        if (audioSource != null && completeSound != null)
        {
            audioSource.PlayOneShot(completeSound);
        }
        
        // Notify GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTempleRecharged(this);
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }
        
        Debug.Log($"Temple {gameObject.name} has been recharged!");
    }
    
    public bool IsRecharged()
    {
        return isRecharged;
    }
    
    public bool IsRecharging()
    {
        return isRecharging;
    }
    
    public bool CanRecharge()
    {
        return !isRecharged && !isRecharging && EnergyManager.Instance != null && EnergyManager.Instance.HasEnergy(energyRequired);
    }
    
    public void ResetTemple()
    {
        isRecharged = false;
        isRecharging = false;
        playerInRange = false;
        
        // Reset material
        if (templeRenderer != null && unchargedMaterial != null)
        {
            templeRenderer.material = unchargedMaterial;
        }
        
        // Disable light
        if (templeLight != null)
        {
            templeLight.enabled = false;
        }
        
        // Stop any ongoing coroutines
        StopAllCoroutines();
        
        Debug.Log($"Temple {gameObject.name} has been reset");
    }
}

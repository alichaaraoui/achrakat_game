using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int energyAmount = 25;
    public float rotationSpeed = 50f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;
    
    [Header("Visual Effects")]
    public GameObject pickupModel;
    public ParticleSystem pickupEffect;
    public Light pickupLight;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pickupSound;
    
    private Vector3 startPosition;
    private bool isCollected = false;
    
    void Start()
    {
        startPosition = transform.position;
        
        if (pickupLight != null)
        {
            pickupLight.intensity = 0.5f;
        }
    }
    
    void Update()
    {
        if (!isCollected)
        {
            // Rotate the pickup
            if (pickupModel != null)
            {
                pickupModel.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
            
            // Bob up and down
            float bob = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = startPosition + Vector3.up * bob;
            
            // Pulse light
            if (pickupLight != null)
            {
                pickupLight.intensity = 0.5f + Mathf.Sin(Time.time * 2f) * 0.3f;
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        
        if (other.CompareTag("Player"))
        {
            CollectPickup();
        }
    }
    
    void CollectPickup()
    {
        isCollected = true;
        
        // Add energy to player
        EnergyManager.Instance.AddEnergy(energyAmount);
        
        // Play pickup effect
        if (pickupEffect != null)
        {
            pickupEffect.Play();
        }
        
        // Play pickup sound
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        
        // Disable light
        if (pickupLight != null)
        {
            pickupLight.enabled = false;
        }
        
        // Hide the pickup
        if (pickupModel != null)
        {
            pickupModel.SetActive(false);
        }
        
        // Destroy after a short delay to allow effects to play
        Destroy(gameObject, 2f);
        
        Debug.Log($"Collected {energyAmount} energy! Total: {EnergyManager.Instance.currentEnergy}");
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw pickup range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}

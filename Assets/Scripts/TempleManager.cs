using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TempleManager : MonoBehaviour
{
    public static TempleManager Instance;
    
    [Header("Temple Management")]
    public List<Temple> temples = new List<Temple>();
    public int totalTemples = 0;
    public int rechargedTemples = 0;
    
    [Header("Bridge Settings")]
    public GameObject bridgeObject;
    public float bridgeAppearTime = 3f;
    public Material bridgeMaterial;
    public ParticleSystem bridgeAppearEffect;
    
    [Header("Audio")]
    public AudioSource bridgeAudioSource;
    public AudioClip bridgeAppearSound;
    
    [Header("Events")]
    public UnityEvent onAllTemplesRecharged;
    public UnityEvent onBridgeAppeared;
    
    private bool allTemplesRecharged = false;
    private bool bridgeAppeared = false;
    
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
        
        // Hide bridge initially
        if (bridgeObject != null)
        {
            bridgeObject.SetActive(false);
        }
        
        Debug.Log($"TempleManager initialized with {totalTemples} temples");
    }
    
    public void OnTempleRecharged(Temple temple)
    {
        if (allTemplesRecharged) return;
        
        rechargedTemples++;
        Debug.Log($"Temple recharged! {rechargedTemples}/{totalTemples} temples charged");
        
        // Check if all temples are recharged
        if (rechargedTemples >= totalTemples)
        {
            AllTemplesRecharged();
        }
    }
    
    private void AllTemplesRecharged()
    {
        allTemplesRecharged = true;
        Debug.Log("All temples recharged! Bridge will appear...");
        
        onAllTemplesRecharged?.Invoke();
        
        // Start bridge appearance sequence
        StartCoroutine(AppearBridge());
    }
    
    private System.Collections.IEnumerator AppearBridge()
    {
        if (bridgeObject == null)
        {
            Debug.LogWarning("No bridge object assigned!");
            yield break;
        }
        
        // Play bridge appear effect
        if (bridgeAppearEffect != null)
        {
            bridgeAppearEffect.Play();
        }
        
        // Play bridge appear sound
        if (bridgeAudioSource != null && bridgeAppearSound != null)
        {
            bridgeAudioSource.PlayOneShot(bridgeAppearSound);
        }
        
        // Activate bridge
        bridgeObject.SetActive(true);
        
        // Animate bridge appearance
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = bridgeObject.transform.localScale;
        bridgeObject.transform.localScale = startScale;
        
        float elapsedTime = 0f;
        while (elapsedTime < bridgeAppearTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / bridgeAppearTime;
            
            // Smooth scale animation
            float smoothProgress = Mathf.SmoothStep(0f, 1f, progress);
            bridgeObject.transform.localScale = Vector3.Lerp(startScale, endScale, smoothProgress);
            
            // Animate material if available
            if (bridgeMaterial != null)
            {
                float alpha = Mathf.Lerp(0f, 1f, smoothProgress);
                Color color = bridgeMaterial.color;
                color.a = alpha;
                bridgeMaterial.color = color;
            }
            
            yield return null;
        }
        
        // Ensure final state
        bridgeObject.transform.localScale = endScale;
        if (bridgeMaterial != null)
        {
            Color color = bridgeMaterial.color;
            color.a = 1f;
            bridgeMaterial.color = color;
        }
        
        bridgeAppeared = true;
        onBridgeAppeared?.Invoke();
        
        Debug.Log("Bridge has appeared!");
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
    
    public bool HasBridgeAppeared()
    {
        return bridgeAppeared;
    }
    
    // Debug method to manually trigger bridge appearance
    [ContextMenu("Force Bridge Appearance")]
    public void ForceBridgeAppearance()
    {
        if (!allTemplesRecharged)
        {
            allTemplesRecharged = true;
            rechargedTemples = totalTemples;
        }
        StartCoroutine(AppearBridge());
    }
}

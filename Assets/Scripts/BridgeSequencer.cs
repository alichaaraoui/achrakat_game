using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BridgeSequencer : MonoBehaviour
{
    [Header("Bridge Segments")]
    public List<GameObject> bridgeSegments = new List<GameObject>();
    public float delayBetweenSegments = 0.5f;
    
    [Header("Animation Settings")]
    public float popAnimationDuration = 0.3f;
    public float maxScale = 1.2f;
    public AnimationCurve popCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Effects")]
    public ParticleSystem segmentAppearEffect;
    public AudioSource audioSource;
    public AudioClip segmentAppearSound;
    
    [Header("Debug")]
    public bool autoFindSegments = true;
    
    private bool isConstructing = false;
    private int currentSegmentIndex = 0;
    
    void Start()
    {
        // Find all child segments if auto-find is enabled
        if (autoFindSegments && bridgeSegments.Count == 0)
        {
            FindBridgeSegments();
        }
        
        // Disable all segments initially
        DisableAllSegments();
        
        Debug.Log($"BridgeSequencer initialized with {bridgeSegments.Count} segments");
    }
    
    void FindBridgeSegments()
    {
        bridgeSegments.Clear();
        
        // Find all direct children that are bridge segments
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.name.ToLower().Contains("segment") || 
                child.gameObject.name.ToLower().Contains("tile") ||
                child.gameObject.name.ToLower().Contains("bridge"))
            {
                bridgeSegments.Add(child.gameObject);
            }
        }
        
        // If no segments found with specific names, add all children
        if (bridgeSegments.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                bridgeSegments.Add(transform.GetChild(i).gameObject);
            }
        }
    }
    
    void DisableAllSegments()
    {
        foreach (GameObject segment in bridgeSegments)
        {
            if (segment != null)
            {
                segment.SetActive(false);
            }
        }
    }
    
    public void StartBridgeConstruction()
    {
        if (isConstructing)
        {
            Debug.LogWarning("Bridge construction already in progress!");
            return;
        }
        
        if (bridgeSegments.Count == 0)
        {
            Debug.LogError("No bridge segments found!");
            return;
        }
        
        isConstructing = true;
        currentSegmentIndex = 0;
        
        Debug.Log("Starting bridge construction...");
        
        // Start the construction sequence
        StartCoroutine(ConstructBridge());
    }
    
    private System.Collections.IEnumerator ConstructBridge()
    {
        for (int i = 0; i < bridgeSegments.Count; i++)
        {
            currentSegmentIndex = i;
            
            // Enable the segment
            if (bridgeSegments[i] != null)
            {
                bridgeSegments[i].SetActive(true);
                
                // Start pop animation
                StartCoroutine(PopAnimation(bridgeSegments[i]));
                
                // Play effects
                PlaySegmentEffects();
                
                Debug.Log($"Bridge segment {i + 1}/{bridgeSegments.Count} appeared");
            }
            
            // Wait before next segment
            if (i < bridgeSegments.Count - 1)
            {
                yield return new WaitForSeconds(delayBetweenSegments);
            }
        }
        
        // Construction complete
        isConstructing = false;
        Debug.Log("Bridge construction complete!");
    }
    
    private System.Collections.IEnumerator PopAnimation(GameObject segment)
    {
        Vector3 originalScale = segment.transform.localScale;
        Vector3 targetScale = originalScale * maxScale;
        
        float elapsedTime = 0f;
        
        // Scale up
        while (elapsedTime < popAnimationDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (popAnimationDuration * 0.5f);
            float curveValue = popCurve.Evaluate(progress);
            
            segment.transform.localScale = Vector3.Lerp(originalScale, targetScale, curveValue);
            yield return null;
        }
        
        // Scale back down
        elapsedTime = 0f;
        while (elapsedTime < popAnimationDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (popAnimationDuration * 0.5f);
            float curveValue = popCurve.Evaluate(progress);
            
            segment.transform.localScale = Vector3.Lerp(targetScale, originalScale, curveValue);
            yield return null;
        }
        
        // Ensure final scale
        segment.transform.localScale = originalScale;
    }
    
    void PlaySegmentEffects()
    {
        // Play particle effect
        if (segmentAppearEffect != null)
        {
            segmentAppearEffect.Play();
        }
        
        // Play sound effect
        if (audioSource != null && segmentAppearSound != null)
        {
            audioSource.PlayOneShot(segmentAppearSound);
        }
    }
    
    public void ResetBridge()
    {
        StopAllCoroutines();
        isConstructing = false;
        currentSegmentIndex = 0;
        DisableAllSegments();
        Debug.Log("Bridge reset");
    }
    
    public bool IsConstructing()
    {
        return isConstructing;
    }
    
    public int GetCurrentSegmentIndex()
    {
        return currentSegmentIndex;
    }
    
    public int GetTotalSegments()
    {
        return bridgeSegments.Count;
    }
    
    // Debug method to test bridge construction
    [ContextMenu("Test Bridge Construction")]
    public void TestBridgeConstruction()
    {
        StartBridgeConstruction();
    }
    
    // Debug method to reset bridge
    [ContextMenu("Reset Bridge")]
    public void DebugResetBridge()
    {
        ResetBridge();
    }
}

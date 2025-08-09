using UnityEngine;
using UnityEngine.Events;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;
    
    [Header("Energy Settings")]
    public int currentEnergy = 0;
    public int maxEnergy = 100;
    
    [Header("UI")]
    public UnityEngine.UI.Text energyText;
    public UnityEngine.UI.Slider energySlider;
    
    [Header("Events")]
    public UnityEvent onEnergyChanged;
    public UnityEvent onEnergyDepleted;
    public UnityEvent onEnergyAdded;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        UpdateUI();
    }
    
    public void AddEnergy(int amount)
    {
        int oldEnergy = currentEnergy;
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        
        if (currentEnergy != oldEnergy)
        {
            onEnergyAdded?.Invoke();
            onEnergyChanged?.Invoke();
            UpdateUI();
        }
    }
    
    public bool SpendEnergy(int amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            onEnergyChanged?.Invoke();
            
            if (currentEnergy <= 0)
            {
                onEnergyDepleted?.Invoke();
            }
            
            UpdateUI();
            return true;
        }
        return false;
    }
    
    public bool HasEnergy(int amount)
    {
        return currentEnergy >= amount;
    }
    
    public float GetEnergyPercentage()
    {
        return (float)currentEnergy / maxEnergy;
    }
    
    private void UpdateUI()
    {
        if (energyText != null)
        {
            energyText.text = $"Energy: {currentEnergy}/{maxEnergy}";
        }
        
        if (energySlider != null)
        {
            energySlider.value = GetEnergyPercentage();
        }
    }
    
    // Debug method to add energy
    [ContextMenu("Add 50 Energy")]
    public void AddEnergyDebug()
    {
        AddEnergy(50);
    }
    
    // Debug method to set energy to max
    [ContextMenu("Set Energy to Max")]
    public void SetEnergyToMax()
    {
        currentEnergy = maxEnergy;
        UpdateUI();
    }
}

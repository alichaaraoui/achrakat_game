using UnityEngine;

public class MouseFix : MonoBehaviour
{
    [Header("Mouse Settings")]
    public bool lockCursor = true;
    public bool showCursor = false;
    
    void Start()
    {
        // Fix cursor state
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = showCursor;
    }
    
    void Update()
    {
        // Press Escape to toggle cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
        // Press Tab to force unlock cursor
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    [ContextMenu("Unlock Cursor")]
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    [ContextMenu("Lock Cursor")]
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

using UnityEngine;

public class VeilRechargeTrigger : MonoBehaviour
{
    public int rechargeAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        var controller = other.GetComponent<StarterAssets.ThirdPersonController>();
        if (controller != null)
        {
            controller.RechargeVeil(rechargeAmount);
            // Optional: visual/audio feedback
            Destroy(gameObject); // or disable instead
        }
    }
}

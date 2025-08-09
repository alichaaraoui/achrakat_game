using UnityEngine;

public class EnergyTrigger : MonoBehaviour
{
    public int energyCost = 1;
    public Animator animator;
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated) return;

        if (other.CompareTag("Player") && EnergyManager.Instance.SpendEnergy(energyCost))
        {
            isActivated = true;

            if (animator != null)
            {
                animator.SetTrigger("Activate");
            }
            else
            {
                StartCoroutine(BuildBridge());
            }
        }
    }

    private System.Collections.IEnumerator BuildBridge()
    {
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = transform.localScale;
        transform.localScale = startScale;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
    }
}

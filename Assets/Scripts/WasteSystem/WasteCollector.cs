
using UnityEngine;
using System.Collections;

public class WasteCollector : MonoBehaviour
{
    [Header("Collection Animation")]
    public float animationDuration = 0.3f;
    public float riseHeight = 0.5f;

    [Header("Collection Sound")]
    public AudioClip collectSound;

    private bool isCollected = false;
    private WasteScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<WasteScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected)
            return;

        CharacterController player =
            other.GetComponent<CharacterController>();

        if (player != null)
        {
            isCollected = true;

            if (scoreManager != null)
            {
                scoreManager.CollectWaste();
            }

            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    collectSound,
                    transform.position
                );
            }

            StartCoroutine(CollectAnimation());
        }
    }

    private IEnumerator CollectAnimation()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 originalPosition = transform.position;

        float elapsed = 0f;

        Collider wasteCollider = GetComponent<Collider>();

        if (wasteCollider != null)
            wasteCollider.enabled = false;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / Mathf.Max(animationDuration, 0.01f)
            );

            transform.localScale =
                Vector3.Lerp(originalScale, Vector3.zero, t);

            transform.position =
                Vector3.Lerp(
                    originalPosition,
                    originalPosition + Vector3.up * riseHeight,
                    t
                );

            yield return null;
        }

        gameObject.SetActive(false);
    }
}

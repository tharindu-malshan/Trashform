
using UnityEngine;
using TMPro;

public class WasteScoreManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text wasteScoreText;
    public GameObject levelCompletePanel;

    [Header("Waste Settings")]
    public Transform wasteParent;

    private int totalWaste;
    private int collectedWaste;

    void Start()
    {
        totalWaste = 0;
        collectedWaste = 0;

        // Count all waste objects automatically
        foreach (Transform waste in wasteParent)
        {
            totalWaste++;
        }

        UpdateScoreUI();

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
    }

    public void CollectWaste()
    {
        collectedWaste++;

        UpdateScoreUI();

        if (collectedWaste >= totalWaste && totalWaste > 0)
        {
            LevelCompleted();
        }
    }

    void UpdateScoreUI()
    {
        if (wasteScoreText != null)
        {
            wasteScoreText.text =
                "Waste Collected: " +
                collectedWaste + "/" + totalWaste;
        }
    }

    void LevelCompleted()
    {
        Debug.Log("Level 1 Completed!");

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }
    }
}

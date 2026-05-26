using UnityEngine;
using TMPro;
using Unity.Netcode;

public class PlayerHealthUI : NetworkBehaviour
{
    [SerializeField] private TMP_Text healthText;

    private NetworkPlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<NetworkPlayerHealth>();

        if (!IsOwner)
        {
            healthText.gameObject.SetActive(false);
            return;
        }

        UpdateHealthText(
            playerHealth.CurrentHealth.Value,
            playerHealth.CurrentHealth.Value
        );

        playerHealth.CurrentHealth.OnValueChanged += UpdateHealthText;
    }

    public override void OnNetworkDespawn()
    {
        if (playerHealth != null)
        {
            playerHealth.CurrentHealth.OnValueChanged -= UpdateHealthText;
        }
    }

    private void UpdateHealthText(int oldHealth, int newHealth)
    {
        healthText.text = "Health: " + newHealth;
    }
}
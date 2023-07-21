using TMPro;
using UnityEngine;

public class UpdateCash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        PlayerInformation playerInformation = PlayerInformation.Instance;

        textMeshProUGUI.text = playerInformation.CurrentMoney.ToString();
    }
}

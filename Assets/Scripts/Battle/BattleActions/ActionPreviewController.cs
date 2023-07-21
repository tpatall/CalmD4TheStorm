using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPreviewController : MonoBehaviour {

    public static ActionPreviewController Instance;

    public GameObject previewObject;
    public Text previewText;

    private void Start() {
        Instance = this;
    }

    public void ShowPreview(int[] numbersRolled, int modifier) {
        previewObject.SetActive(true);

        string text = "";

        for(int i = 0; i < numbersRolled.Length; i++) {
            text += numbersRolled[i];

            if(i != numbersRolled.Length - 1) {
                text += " + ";
            }
        }
        if(modifier != 0) {
            text += " + " + modifier * numbersRolled.Length;
        }

        previewText.text = text;
    }

    public void HidePreview() {
        previewObject.SetActive(false);
    }
}

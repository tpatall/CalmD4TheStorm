using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicator : MonoBehaviour {
    IEnumerator MoveAndDisappear(int damage) {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = damage.ToString();

        float timer = 0;

        float x = Random.Range(0f, 2f);
        float y = Random.Range(0f, 2f);

        while(timer < 1) {
            timer += Time.deltaTime;
            transform.position += new Vector3(x * Time.deltaTime, y * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}

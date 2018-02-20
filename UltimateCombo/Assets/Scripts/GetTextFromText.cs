using UnityEngine;
using UnityEngine.UI;

public class GetTextFromText : MonoBehaviour {
    public Text textSource;

	void Start () {
        GetComponent<Text>().text = textSource.text;
	}
}

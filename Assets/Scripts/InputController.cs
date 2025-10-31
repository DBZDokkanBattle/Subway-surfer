using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI playerName;
    public Button submitButton;

    private string[] randomNames =
    {
        "Jeff", "Blop", "Ronaldo", "Chris", "Anglea", "Arthur",
        "Katara", "Luffy", "Zoro", "Usopp"
    };

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Username"))
        {
            PlayerPrefs.SetString("Username", randomNames[Random.Range(0, randomNames.Length)]);
        }

        playerName.text = PlayerPrefs.GetString("Username");

        submitButton.onClick.AddListener(() => SaveName());
    }

    private void SaveName()
    {
        if (inputField.text != "" && inputField.text.Length <= 17)
        {
            PlayerPrefs.SetString("Username", inputField.text);
            playerName.text = PlayerPrefs.GetString("Username");
            inputField.text = "";
        }
        else
        {
            inputField.text = "Error";
        }
    }
}

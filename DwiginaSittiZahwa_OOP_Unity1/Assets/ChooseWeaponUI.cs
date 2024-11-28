using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponUI : MonoBehaviour
{
    [SerializeField] private Text gameTitleText;

    void Start()
    {
        // Set game title
        gameTitleText.text = "Guardian of The Galaxy";
    }
}
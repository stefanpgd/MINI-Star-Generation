using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContent;
    [SerializeField] private GameObject gameContent;
    [SerializeField] private TMP_InputField seedInput;
    [SerializeField] private Button confirmButton;

    private StarSystemManager starSystemManager;

    private void Start()
    {
        starSystemManager = StarSystemManager.Instance;
        mainMenuContent.SetActive(true);
        gameContent.SetActive(false);

        confirmButton.onClick.AddListener(ConfirmSeed);
    }

    private void OnDestroy()
    {
        confirmButton.onClick.AddListener(ConfirmSeed);
    }

    private void ConfirmSeed()
    {
        if(seedInput.text != "")
        {
            starSystemManager.CreateSystem(seedInput.text);
            mainMenuContent.SetActive(false);
            gameContent.SetActive(true);
        }
    }
}

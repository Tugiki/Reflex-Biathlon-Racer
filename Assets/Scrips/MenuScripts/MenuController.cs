using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject NoSaveGameDialogue = null;

    public void NewGameDialogueYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogueYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            NoSaveGameDialogue.SetActive(true); 
            Debug.Log("No saved game found.");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }



}

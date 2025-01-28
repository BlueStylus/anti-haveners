using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas menu1;
    [SerializeField] private Canvas menu2;
    [SerializeField] private Canvas menu3;

    [SerializeField] private bool showornot;

    private void Start()
    {
        GetComponent<Canvas>().enabled = showornot;
    }

    public void ResetButton()
    {
        PauseManager.Instance.Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
        //this only functions in build tho, not editor
        Application.Quit();
    }

    public void SettingsButton()
    {
        // close first menu and open second menu
        menu1.enabled = false;
        menu2.enabled = true;
    }

    public void LevelMenu()
    {
        // close first menu and open second menu
        menu1.enabled = false;
        menu3.enabled = true;
    }

    public void ReturntoMainButton()
    {
        SceneManager.LoadScene(0);
        Destroy(GameObject.Find("TimeTracker"));
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene(3);
    }
}

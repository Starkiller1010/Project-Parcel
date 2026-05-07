using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;
    public Button newGameButton;
    public Button loadGameButton;
    public Button exitGameButton;
    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        newGameButton = ui.Q<Button>("NewGame");
        newGameButton.clicked += OnNewGameClicked;

        loadGameButton = ui.Q<Button>("LoadGame");
        loadGameButton.clicked += OnLoadGameClicked;

        exitGameButton = ui.Q<Button>("ExitGame");
        exitGameButton.clicked += OnExitGameClicked;
    }

    private void OnNewGameClicked()
    {
        Debug.Log("Clicked on New Game Button");
        // SceneManager.LoadScene("WorkroomScene");
        StartCoroutine(LoadAsyncScene());   
        Game.MAKE_GAME_STATE();
    }

    private void OnLoadGameClicked()
    {
        Debug.Log("Clicked on Load Game Button");
        SceneManager.LoadScene("WorkroomScene");
        string save = FileManager.GetSavedGameStates(FileManager.debugDirectory)[0];
        GameState state = Game.GET_GAME_STATE();
        FileManager.LoadGameState(save);
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WorkroomScene");
        string loadingMessage = "Loading";
        while (!asyncLoad.isDone)
        {
            Debug.Log(loadingMessage);
            loadingMessage += ".";
            yield return null;
        }
    }
    
    private void OnExitGameClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}

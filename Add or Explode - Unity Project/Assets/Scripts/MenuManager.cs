using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //This script is always attached to a canvas
    public GameObject[] menuObjects;
    public GameObject joyStick;
    public GameObject instructions;
    public GameObject startingFocusObject;
    public GameObject eventSystem;

    bool paused = false;
    float selectLength;
    PlayerInput playerinput;
    GameObject focusObject;
    static MenuManager instance;

    public void OpenWebPage(string url) => Application.OpenURL(url);
    private void OnEnable() => playerinput.Enable();
    private void OnDisable() => playerinput.Disable();

    private void Awake()
    {
        playerinput = new PlayerInput();

        //Don't reset the canvas object with each reload. If the user is paused, this would change the menu
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(focusObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL
            Destroy(joyStick);
        #endif

        //Set variables
        selectLength = ((AudioClip)Resources.Load("Audio/select")).length;
        Time.timeScale = 1;
        focusObject = startingFocusObject;
        Focus(focusObject);

        playerinput.All.Pause.performed += _ => TogglePause();
        playerinput.All.Fullscreen.performed += _ => Screen.fullScreen = !Screen.fullScreen;

        if (instructions != null)
        {
#if UNITY_STANDALONE || UNITY_WEBGL
            string text = "Use WASD, the arrow keys, or an external joystick to move.";
#elif UNITY_ANDROID
            string text = "Use the joystick to move.";
        #endif

            instructions.GetComponent<TextMeshProUGUI>().text =
            $"{text} Collect squares to increase your score and avoid bullets. See how high you can go!";
        }
    }

    public void ChangeScene(string scene)
    {
        SFXPlayer.Play("select");
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
        DestroyImmediate(eventSystem);
        Destroy(gameObject);
    }

    #region
    public void TogglePause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        SFXPlayer.Play("select");

        //Keep track of where they left off
        focusObject = EventSystem.current.currentSelectedGameObject ?? startingFocusObject;
        if (paused) { Focus(focusObject); }

        foreach (GameObject thing in menuObjects) { thing.SetActive(paused); }
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(selectLength);
        SFXPlayer.TogglePause();
    }
    #endregion

    public void Focus(GameObject target)
    {
        Button component = focusObject.GetComponent<Button>();
        component.Select();
        component.OnSelect(null);
    }

    public void QuitGame()
    {
        SFXPlayer.Play("select");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public Animator logoAnimator;
    public TransitionTool transitionTool;
    // Start is called before the first frame update
    void Start()
    {
        QuestionGameController.QuestionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        logoAnimator.SetTrigger("play");
        transitionTool.sceneName = sceneName;
        transitionTool.LoadScene();
        // SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}

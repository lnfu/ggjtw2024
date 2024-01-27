using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;
using UnityEngine.SceneManagement;

public class TransitionTool : MonoBehaviour
{
    public TransitionSettings transition;
    public float startDelay;
    public string sceneName;
    public bool loadInSameScene = false;
    
    public void LoadScene()
    {
        if (loadInSameScene)
        {
            TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex, transition, startDelay);
            return;
        }
        TransitionManager.Instance().Transition(sceneName, transition, startDelay);
    }
}

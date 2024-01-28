using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGameController : MonoBehaviour
{
    public static int QuestionIndex = 0;
    public GameObject nextBtn;
    public GameObject backToMenuBtn;
    public TransitionTool transitionTool;
    private QuestionManager _questionManager;
    // Start is called before the first frame update
    void Awake()
    {
        _questionManager = FindObjectOfType<QuestionManager>();
        _questionManager.Correct += Correct;
        _questionManager.Wrong += Wrong;
        _questionManager.End += End;
        _questionManager.SpecifyQuestion(QuestionIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Correct()
    {
        //play sound
        // nextBtn.SetActive(true);
    }

    public void Wrong()
    {
        //play sound
        // nextBtn.SetActive(true);
    }

    public void End()
    {
        //play sound
        nextBtn.SetActive(false);
        backToMenuBtn.SetActive(true);
    }

    public void NextQuestion()
    {
        QuestionIndex++;
        StartCoroutine(WaitAndTriggerTransition());
    }

    public void BackToMenu()
    {
        transitionTool.loadInSameScene = false;
        transitionTool.sceneName = "MainMenu";
        TriggerTransition();
    }

    public void TriggerTransition()
    {
        transitionTool.LoadScene();
    }

    IEnumerator WaitAndTriggerTransition()
    {
        TriggerTransition();
        yield return new WaitForSeconds(0.5f);
        //next question
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGameController : MonoBehaviour
{
    public static int QuestionIndex = 0;
    public GameObject nextBtn;
    public TransitionTool transitionTool;
    private QuestionManager _questionManager;
    // Start is called before the first frame update
    void Start()
    {
        _questionManager = FindObjectOfType<QuestionManager>();
        _questionManager.Correct += Correct;
        _questionManager.Wrong += Wrong;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Correct()
    {
        //play sound
        nextBtn.SetActive(true);
    }

    public void Wrong()
    {
        //play sound
        nextBtn.SetActive(true);
    }

    public void NextQuestion()
    {
        StartCoroutine(WaitAndTriggerTransition());
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

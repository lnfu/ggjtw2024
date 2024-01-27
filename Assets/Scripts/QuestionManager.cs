using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
	public event Action Correct;
	public event Action Wrong;

	public QuestionPool QPool;

	public int Number;
	public Image Photo;

	public GameObject UserAnswer;
	public GameObject CorrectAnswer;
		
	private Question _currentQuestion;

    void Start()
    {
		Number = 0;

		_currentQuestion = QPool.Questions[Number];
		Photo.sprite = _currentQuestion.QuestionSprite;
    }

	private void ShowAnswer()
	{
		UserAnswer.SetActive(false);
		CorrectAnswer.SetActive(true);
	}

	public void Submit()
	{
		string answerText = _currentQuestion.AnswerText;
		CorrectAnswer.GetComponent<TextMeshProUGUI>().text = answerText;

		ShowAnswer();

		if (UserAnswer.GetComponent<TMP_InputField>().text == answerText)
		{
			Debug.Log("Correct!");
			if (Correct != null)
				Correct();
		}
		else
		{
			Debug.Log("Wrong!");
			if (Wrong != null)
				Wrong();
		}
	}
}

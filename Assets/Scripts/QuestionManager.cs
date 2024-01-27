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
	public bool answered;
	public Image Photo;

	public GameObject UserAnswer;
	public GameObject CorrectAnswer;
		
	private Question _currentQuestion;

	private void Judge()
	{
		if (UserAnswer.GetComponent<TMP_InputField>().text == _currentQuestion.AnswerText)
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

	public void NextQuestion()
	{
		Number++;
		answered = false;
		CheckEndGame();
	}

	public void SpecifyQuestion(int index)
	{
		Number = index;
		answered = false;
		CheckEndGame();
	}

	void CheckEndGame()
	{
		if (Number == QPool.Questions.Length)
		{
			Debug.Log("題目沒了");
		}
	}

    void Start()
    {
		Number = 0;
		answered = false;
    }

	void Update()
	{
		_currentQuestion = QPool.Questions[Number];

		if (answered)
		{
			CorrectAnswer.GetComponent<TextMeshProUGUI>().text = _currentQuestion.AnswerText;
			Photo.sprite = _currentQuestion.AnswerSprite;

			UserAnswer.SetActive(false);
			CorrectAnswer.SetActive(true);
		}
		else
		{
			Photo.sprite = _currentQuestion.QuestionSprite;

			UserAnswer.SetActive(true);
			CorrectAnswer.SetActive(false);
		}
	}

	public void Answer()
	{
		answered = true;

		Judge();
	}

}

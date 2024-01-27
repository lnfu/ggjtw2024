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
	public event Action End;

	public QuestionPool QPool;

	public int Number;
	public bool answered;
	public Image Photo;

	public GameObject UserAnswer;
	public GameObject CorrectAnswer;
		
	private Question _currentQuestion;
	private AudioSource _audioSource;

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
	}

	public void SpecifyQuestion(int index)
	{
		Number = index;
		answered = false;
	}

	private void CheckEndGame()
	{
		if (Number == QPool.Questions.Length - 1)
		{
			Debug.Log("題目沒了");
			if (End != null)
				End();
		}
	}

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();

		Number = 0;
		answered = false;
	}

	private void Update()
	{
		_currentQuestion = QPool.Questions[Number];

		if (answered)
		{
			CorrectAnswer.GetComponent<TextMeshProUGUI>().text = _currentQuestion.AnswerText;
			if (_currentQuestion.AnswerText != null)
				Photo.sprite = _currentQuestion.AnswerSprite;

			UserAnswer.SetActive(false);
			CorrectAnswer.SetActive(true);
		}
		else
		{
			if (_currentQuestion.QuestionSprite != null)
				Photo.sprite = _currentQuestion.QuestionSprite;

			UserAnswer.SetActive(true);
			CorrectAnswer.SetActive(false);
		}
	}

	public void Answer()
	{
		answered = true;

		PlayAnswerAudio();

		Judge();

		CheckEndGame();
	}

	private void PlayAnswerAudio()
	{
		if (_currentQuestion.Audio != null)
		{
			_audioSource.clip = _currentQuestion.Audio;
			_audioSource.Play();
		}
	}

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question : MonoBehaviour
{
	public event Action Correct;
	public event Action Wrong;

	public TextAsset QuestionFile;
	public TextAsset AnswerFile;
	public int Number;
	public Image QuestionImage;

	public GameObject UserAnswer;
	public GameObject CorrectAnswer;

	[SerializeField]
	private Sprite[] _questions;
	[SerializeField]
	private string[] _answers;

	void OnValidate()
	{
		if (QuestionFile == null || AnswerFile == null)
		{
			Debug.LogError("File not exist!");
			return;
		}
		_answers = AnswerFile.text.Split(new []{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

		var questionPaths = QuestionFile.text.Split(new []{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

		_questions = new Sprite[questionPaths.Length];
		for (int i = 0; i < _questions.Length; ++i)
		{
			_questions[i] = Resources.Load<Sprite>(questionPaths[i]);
		}
	}

    void Start()
    {
		Number = 0;
    }

    void Update()
    {
		QuestionImage.sprite = _questions[Number];
    }

	private void ShowAnswer()
	{
		UserAnswer.SetActive(false);
		CorrectAnswer.SetActive(true);
	}

	public void Submit()
	{
		CorrectAnswer.GetComponent<TextMeshProUGUI>().text = _answers[Number];
		ShowAnswer();

		if (UserAnswer.GetComponent<TMP_InputField>().text == _answers[Number])
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

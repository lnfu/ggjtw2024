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
	public TextMeshProUGUI Description;

	public GameObject UserAnswer;
	public GameObject CorrectAnswer;

	private Question _currentQuestion;
	private AudioSource _soundSource;
	private AudioSource _bgmSource;
	public float bgmVolume = 1;

	public AudioClip openSound;
	public AudioClip openSound2;
	public AudioClip endSound;
	public List<AudioClip> wrongSounds;

	void Awake()
	{
		_soundSource = gameObject.AddComponent<AudioSource>();
		_bgmSource = gameObject.AddComponent<AudioSource>();
		//_bgmSource 音量設定
		_bgmSource.volume = bgmVolume;
	}
	private void Judge()
	{
		_bgmSource.PlayOneShot(endSound);

		if (UserAnswer.GetComponent<TMP_InputField>().text == _currentQuestion.AnswerText)
		{
			Debug.Log("Correct!");
			if (_currentQuestion.Audio != null)
				_soundSource.PlayOneShot(_currentQuestion.Audio);
			if (Correct != null)
				Correct();
		}
		else
		{
			Debug.Log("Wrong!");
			//如果 WrongSounds 有東西，就播放 WrongSounds 裡的音效
			if (wrongSounds.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, wrongSounds.Count);
				_soundSource.PlayOneShot(wrongSounds[index]);
			}
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
		// Number = 0;
		answered = false;
		_bgmSource.PlayOneShot(openSound);
		_soundSource.PlayOneShot(openSound2);
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

			if (!string.IsNullOrEmpty(_currentQuestion.Desciption))
				Description.text = _currentQuestion.Desciption;

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
			_soundSource.clip = _currentQuestion.Audio;
			_soundSource.Play();
		}
	}

}

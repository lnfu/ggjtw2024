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
	public Image Photo;
	public TextMeshProUGUI Description;

	public GameObject UserAnswer;
	public GameObject CorrectAnswer;
	public GameObject Comment;

	private Question _currentQuestion;
	private AudioSource _soundSource;
	private AudioSource _bgmSource;
	private bool _answered;
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

	public void SpecifyQuestion(int index)
	{
		Number = index % QPool.Questions.Length;
		_answered = false;
		CheckEndGame();
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
		_answered = false;
		_bgmSource.PlayOneShot(openSound);
		_soundSource.PlayOneShot(openSound2);
	}

	private void Update()
	{
		_currentQuestion = QPool.Questions[Number];

		if (_answered)
		{
			CorrectAnswer.GetComponent<TextMeshProUGUI>().text = _currentQuestion.AnswerText;
			Comment.GetComponent<TextMeshProUGUI>().text = _currentQuestion.Comment;
			if (_currentQuestion.AnswerText != null)
			{
				Photo.sprite = _currentQuestion.AnswerSprite;
				adjustPhotoSize();
			}
			Comment.SetActive(true);
			UserAnswer.SetActive(false);
			CorrectAnswer.SetActive(true);
		}
		else
		{
			if (_currentQuestion.QuestionSprite != null)
			{
				Photo.sprite = _currentQuestion.QuestionSprite;
				adjustPhotoSize();
			}

			if (!string.IsNullOrEmpty(_currentQuestion.Desciption))
				Description.text = _currentQuestion.Desciption;
			Comment.SetActive(false);
			UserAnswer.SetActive(true);
			CorrectAnswer.SetActive(false);
		}
	}

	private void adjustPhotoSize()
	{
		Photo.SetNativeSize();
		//如果Photo的寬度大於480,就把Photo的大小等比例縮小
		if (Photo.rectTransform.sizeDelta.x > 480)
		{
			float ratio = 480 / Photo.rectTransform.sizeDelta.x;
			Photo.rectTransform.sizeDelta *= ratio;
		}
		//如果Photo的高度大於700,就把Photo的大小等比例縮小
		if (Photo.rectTransform.sizeDelta.y > 700)
		{
			float ratio = 700 / Photo.rectTransform.sizeDelta.y;
			Photo.rectTransform.sizeDelta *= ratio;
		}
	}

	public void Answer()
	{
		_answered = true;

		PlayAnswerAudio();

		Judge();

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

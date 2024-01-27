using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
	public string Desciption;
	public Sprite QuestionSprite;
	public Sprite AnswerSprite;
	public string AnswerText;
	public AudioClip Audio;
}


[CreateAssetMenu(menuName="QuestionPool", order=1)]
public class QuestionPool : ScriptableObject
{
	public Question[] Questions;
}

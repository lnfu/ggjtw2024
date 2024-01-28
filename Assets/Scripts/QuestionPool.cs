using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
	Medium,
	Hard,
};

[System.Serializable]
public class Question
{
	public string Desciption;
	public Sprite QuestionSprite;
	public Sprite AnswerSprite;
	public string AnswerText;
	public AudioClip Audio;
	public string Comment;
	public Difficulty QuestionDifficulty;
}


[CreateAssetMenu(menuName="QuestionPool", order=1)]
public class QuestionPool : ScriptableObject
{
	public Question[] Questions;
}

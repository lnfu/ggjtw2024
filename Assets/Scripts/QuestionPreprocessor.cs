using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPreprocessor : MonoBehaviour
{
	public QuestionPool QPool;

	public class QuestionComparer : IComparer<Question>
	{
	    public int Compare(Question x, Question y)
	    {
	        if (x == null || y == null)
	            throw new ArgumentNullException("Cannot compare null objects.");
	
	        return x.QuestionDifficulty.CompareTo(y.QuestionDifficulty);
	    }
	}

    void Awake()
    {
		Array.Sort(QPool.Questions, new QuestionComparer());
    }

}

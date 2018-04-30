using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Questions : MonoBehaviour {

    public TextAsset questionsText;

    private List<string[]> questionsList;
    private System.Random rnd;
    private int correctIndex;
    private GameObject[] allBoxes;

    //Game Object references
    public GameObject questionsPanel, questionBox, answerBox1, answerBox2, answerBox3, answerBox4, correctPanel, incorrectPanel;

	// Use this for initialization
	void Start () {

        //initialize references to game objects --not needed, done in inspector
        //questionBox = GameObject.Find("QuestionBox");
        //answerBox1 = GameObject.Find("Answer1");
        //answerBox2 = GameObject.Find("Answer2");

        /*
        rnd = new System.Random();
        allBoxes = new GameObject[] { questionBox, answerBox1, answerBox2, answerBox3, answerBox4 };

        //initialize the Questions Array
        foreach (string line in questionsText.text.Split('\n'))
        {
            questionsList.Add(line.Split(';'));
        }
        */
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void askQuestion()
    {
        rnd = new System.Random();
        allBoxes = new GameObject[] { questionBox, answerBox1, answerBox2, answerBox3, answerBox4 };
        questionsList = new List<string[]>();

        //initialize the Questions Array
        foreach (string line in questionsText.text.Split('\n'))
        {
            questionsList.Add(line.Split(';'));
        }

        //chooses a random index to randomize question selection
        int questionIndex = rnd.Next(questionsList.Count - 1);

        //The first index of this array is the question, the second is the correct answer. All other index are wrong answers.
        string[] questionArray = questionsList[questionIndex];

        //Randomize where the correct answer is.
        correctIndex = rnd.Next(1, questionArray.Length - 1);
        //move the correct answer into its new index
        string temp = questionArray[1];
        questionArray[1] = questionArray[correctIndex];
        questionArray[correctIndex] = temp;

        //Ensures that only the necessary QuestionPanel children are active and set text fields.
        for (int i=0; i<questionArray.Length; i++)
        {
            allBoxes[i].SetActive(true);
            allBoxes[i].GetComponentInChildren<Text>().text = questionArray[i];
        }

        questionsPanel.SetActive(true);
    }

    public void handleQuestion(int selectedAnswer)
    {
        //Question was answered correctly
        if(selectedAnswer == correctIndex)
        {
            correctPanel.SetActive(true);
        }
        else //question was answered incorrectly
        {
            incorrectPanel.SetActive(true);
        }
    }

}

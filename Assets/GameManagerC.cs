using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MathQuizGamec : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public GameObject quizPanel;
    public TMP_Text questionText;
    public TMP_Text optionAText;
    public TMP_Text optionBText;
    public TMP_Text optionCText;

    private int correctAnswer;
    private Vector3 posA, posB, posC; // Posisi untuk opsi A, B, C

    void Start()
    {
        // Simpan posisi original sebagai posisi opsi
        posA = object1.transform.position; // Posisi A
        posB = object2.transform.position; // Posisi B
        posC = object3.transform.position; // Posisi C

        GenerateNewQuestion();
    }

    void GenerateNewQuestion()
    {
        // Generate soal matematika
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);

        string question = "";
        correctAnswer = 0;

        switch (Random.Range(0, 3))
        {
            case 0:
                question = $"{num1} + {num2} = ?";
                correctAnswer = num1 + num2;
                break;
            case 1:
                question = $"{num1} - {num2} = ?";
                correctAnswer = num1 - num2;
                break;
            case 2:
                question = $"{num1} × {num2} = ?";
                correctAnswer = num1 * num2;
                break;
        }

        // Generate jawaban salah
        int wrongAnswer1 = correctAnswer + Random.Range(1, 4);
        int wrongAnswer2 = correctAnswer - Random.Range(1, 4);

        // Pastikan jawaban salah tidak sama dengan benar
        while (wrongAnswer1 == correctAnswer) wrongAnswer1++;
        while (wrongAnswer2 == correctAnswer) wrongAnswer2--;

        // Tampilkan soal
        questionText.text = question;

        // Selalu letakkan jawaban benar di option A
        optionAText.text = $"A) {wrongAnswer1}";
        optionBText.text = $"B) {wrongAnswer2}";
        
        optionCText.text = $"C) {correctAnswer}";

        // Posisi object tetap tidak berubah
        object1.transform.position = posC;
        object2.transform.position = posB;
        object3.transform.position = posA;

        if (StartGameController.IsMainMenu) return;
        quizPanel.SetActive(true);
    }
}
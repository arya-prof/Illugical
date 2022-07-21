using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public static QuestionUI Instance { get; private set; }

    private TextMeshProUGUI textMeshPro;
    private Button yesBtn;
    private Button noBtn;

    private void Awake()
    {
        Instance = this;

        textMeshPro = transform.Find("QText").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();

        gameObject.SetActive(false);
    }
    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);

        textMeshPro.text = questionText;
        yesBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            yesAction();
        });
        noBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            noAction();
        });
    }
}

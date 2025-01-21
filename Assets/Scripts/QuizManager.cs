using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizLayoutCreator : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public Question[] questions;

    private int currentQuestionIndex = 0;

    private GameObject canvas;
    private TextMeshProUGUI questionText;
    private Button[] answerButtons;
    private TextMeshProUGUI resultText;

    public Sprite backgroundImageSprite; // Background image sprite

    void Start()
    {
        CreateCanvas();
        CreateQuizLayout();
        DisplayQuestion();
    }

    void CreateCanvas()
    {
        canvas = new GameObject("QuizCanvas");
        Canvas canvasComp = canvas.AddComponent<Canvas>();
        canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.AddComponent<GraphicRaycaster>();

        // Background
        GameObject background = new GameObject("Background");
        background.transform.SetParent(canvas.transform, false);
        Image bgImage = background.AddComponent<Image>();
        if (backgroundImageSprite != null)
        {
            bgImage.sprite = backgroundImageSprite;
            bgImage.type = Image.Type.Sliced;
        }
        else
        {
            bgImage.color = new Color(0.9f, 0.9f, 0.9f); // Default light gray
        }
        RectTransform bgRect = background.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;
    }

    void CreateQuizLayout()
    {
        // Question Text
        GameObject questionObj = new GameObject("QuestionText");
        questionObj.transform.SetParent(canvas.transform, false);
        questionText = questionObj.AddComponent<TextMeshProUGUI>();
        RectTransform questionRect = questionObj.GetComponent<RectTransform>();
        questionRect.anchorMin = new Vector2(0.33f, 0.7f); // Start closer to the center
        questionRect.anchorMax = new Vector2(0.73f, 0.85f); // End closer to the center
        questionRect.offsetMin = Vector2.zero;
        questionRect.offsetMax = Vector2.zero;
        questionText.fontSize = 36;
        questionText.alignment = TextAlignmentOptions.Center;
        questionText.text = "Question will appear here";
        questionText.color = Color.black; // Set text color to black

        // Answer Buttons
        answerButtons = new Button[4];
        for (int i = 0; i < 4; i++)
        {
            GameObject buttonObj = new GameObject($"AnswerButton{i + 1}");
            buttonObj.transform.SetParent(canvas.transform, false);

            // Button
            Button button = buttonObj.AddComponent<Button>();
            answerButtons[i] = button;
            Image buttonImage = buttonObj.AddComponent<Image>();
            buttonImage.color = new Color(0, 0, 0, 0); // Fully transparent background

            // Button Text
            GameObject buttonTextObj = new GameObject("Text");
            buttonTextObj.transform.SetParent(buttonObj.transform, false);
            TextMeshProUGUI buttonText = buttonTextObj.AddComponent<TextMeshProUGUI>();
            buttonText.text = $"Answer {i + 1}";
            buttonText.fontSize = 24;
            buttonText.alignment = TextAlignmentOptions.Center;
            buttonText.color = Color.black; // Set text color to black

            // Position Buttons
            RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
            buttonRect.anchorMin = new Vector2(0.33f, 0.6f - (i * 0.13f));
            buttonRect.anchorMax = new Vector2(0.73f, 0.7f - (i * 0.13f));
            buttonRect.offsetMin = Vector2.zero;
            buttonRect.offsetMax = Vector2.zero;

            // Position Button Text
            RectTransform buttonTextRect = buttonTextObj.GetComponent<RectTransform>();
            buttonTextRect.anchorMin = Vector2.zero;
            buttonTextRect.anchorMax = Vector2.one;
            buttonTextRect.offsetMin = Vector2.zero;
            buttonTextRect.offsetMax = Vector2.zero;

            // Add Click Event
            int index = i;
            button.onClick.AddListener(() => CheckAnswer(index));
        }

        // Result Text
        GameObject resultObj = new GameObject("ResultText");
        resultObj.transform.SetParent(canvas.transform, false);
        resultText = resultObj.AddComponent<TextMeshProUGUI>();
        RectTransform resultRect = resultObj.GetComponent<RectTransform>();
        resultRect.anchorMin = new Vector2(0.33f, 0.1f);
        resultRect.anchorMax = new Vector2(0.73f, 0.2f);
        resultRect.offsetMin = Vector2.zero;
        resultRect.offsetMax = Vector2.zero;
        resultText.fontSize = 28;
        resultText.alignment = TextAlignmentOptions.Center;
        resultText.text = "";
        resultText.color = Color.black; // Set text color to black
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            resultText.text = "Quiz Complete!";
            questionText.text = "";
            foreach (Button btn in answerButtons)
            {
                btn.gameObject.SetActive(false);
            }
            return;
        }

        // Set Question Text
        Question currentQuestion = questions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        // Set Answer Buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(i < currentQuestion.answers.Length);
            if (i < currentQuestion.answers.Length)
            {
                TextMeshProUGUI btnText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                btnText.text = currentQuestion.answers[i];
            }
        }

        resultText.text = "";
    }

    void CheckAnswer(int index)
    {
        Question currentQuestion = questions[currentQuestionIndex];
        if (index == currentQuestion.correctAnswerIndex)
        {
            resultText.text = "Correct!";
        }
        else
        {
            resultText.text = "Wrong!";
        }

        currentQuestionIndex++;
        Invoke(nameof(DisplayQuestion), 1.5f); // Wait 1.5 seconds before displaying the next question
    }
}

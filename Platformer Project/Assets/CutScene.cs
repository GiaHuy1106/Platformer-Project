using UnityEngine;
using UnityEngine.UI; // Nếu bạn dùng UI Text
using System.Collections;

public class CutScene : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialoguePanel;   // Panel hiển thị hội thoại
    public Text dialogueText;          // Text hiển thị câu nói
    public string[] dialogueLines;     // Các câu thoại của boss
    public float textSpeed = 0.05f;    // Tốc độ gõ chữ

    private int index = 0;
    private bool isCutsceneActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCutsceneActive)
        {
            isCutsceneActive = true;
            other.GetComponent<playerMove>().enabled = false; // Khoá điều khiển player
            dialoguePanel.SetActive(true);
            StartCoroutine(ShowLine());
        }
    }

    IEnumerator ShowLine()
    {
        dialogueText.text = "";
        foreach (char letter in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            StopAllCoroutines();
            StartCoroutine(ShowLine());
        }
        else
        {
            // Kết thúc cutscene
            dialoguePanel.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerMove>().enabled = true; // Mở lại điều khiển player
        }
    }
}

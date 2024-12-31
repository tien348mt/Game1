using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject shop;
    public GameObject contBtn;
    public GameObject endButton; // Nút mới xuất hiện ở đoạn cuối
    public Text text;
    public string[] dialogueMember;
    private int index;
    private bool playerIsClose;
    private bool isTyping;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerIsClose)
        {
            if (dialogue.activeInHierarchy && !isTyping)
            {
                zeroText();
            }
            else if (!isTyping)
            {
                dialogue.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }

    public void zeroText()
    {
        text.text = "";
        index = 0;

        if (dialogue != null)
            dialogue.SetActive(false);

        if (contBtn != null)
            contBtn.SetActive(false);

        if (endButton != null)
            endButton.SetActive(false); // Ẩn nút mới khi reset
    }

    IEnumerator Typing()
    {
        isTyping = true;
        contBtn.SetActive(false);

        foreach (char letter in dialogueMember[index].ToCharArray())
        {
            if (text == null) yield break;
            text.text += letter;
            yield return new WaitForSeconds(0.006f);
        }

        isTyping = false;
        contBtn.SetActive(true);
        if (index == dialogueMember.Length - 1)
        {
            endButton.SetActive(true);
        }
    }

    public void NextLine()
    {
        contBtn.SetActive(false);

        if (index < dialogueMember.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            // Đoạn cuối cùng của hội thoại
            zeroText();
        }
    }

    public void EndDialogue()
    {
        zeroText();
    }

    public void Shop()
    {
        EndDialogue();
        shop.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            StopAllCoroutines(); // Dừng tất cả các coroutine
            zeroText();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int score;
    private int oldScore;
    private bool expand = false;
    private bool shrink = false;
    private TMP_Text txt;
    private float initialTxtSize = 47;

    void Start()
    {
        txt = this.gameObject.GetComponent<TMP_Text>();
        oldScore = score;
        this.gameObject.GetComponent<TMP_Text>().text = "SCORE: " + score;
    }
    void Update()
    {
        if(oldScore != score)
            StartCoroutine(AddScore());

        if(expand)
            txt.fontSize += 0.1f;
        else if(txt.fontSize > initialTxtSize)
            txt.fontSize -= 0.1f;
        

        Debug.Log(txt.fontSize);

    }

    private IEnumerator AddScore()
    {
        expand = true;
        txt.color = Color.green;
        yield return new WaitForSeconds(0.05f);
        expand = false;
        txt.color = Color.white;
        


        txt.text = "SCORE: " + score;
        oldScore = score;
    }
}

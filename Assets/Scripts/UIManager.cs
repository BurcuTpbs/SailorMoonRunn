using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField] Text scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }


}//class

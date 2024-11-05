using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //싱글턴 객체(인스턴스)
    public static ScoreManager Instance = null;
    public Text currentScoreUI;
    public Text bestScoreUI;
    int currentScore;
    int bestScore;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        //처음에 최고점수를 불러와 보여준다.
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreUI.text = "TOP : " + bestScore;
    }

    public int Score{
        get{
            return currentScore;
        }
        set{
            currentScore = value;
            currentScoreUI.text = "SCORE : " + currentScore;
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                bestScoreUI.text = "TOP : " + bestScore;
                PlayerPrefs.SetInt("BestScore", bestScore);
            }
        }
    }
    // public void SetScore(int value){
    //     currentScore = value;
    //     currentScoreUI.text = "SCORE : " + currentScore;
    //     if( currentScore > bestScore){
    //         bestScore = currentScore;
    //         bestScoreUI.text = "TOP : " + bestScore;
    //         PlayerPrefs.SetInt("BestScore", bestScore);
    //     }
    // }
    // public int GetScore(){
    //     return currentScore;
    // }


    // 프레임마다 Update할 필요는 없고, 에너미 충돌이벤트에서 값을 업데이트하면 된다.
}

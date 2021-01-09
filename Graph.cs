using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Graph : MonoBehaviour
{
    
    public Image Red_bar;
    public Text Red_text;

    public Image Blue_bar;
    public Text Blue_text;

    public Image Green_bar;
    public Text Green_text;

    public Image Orange_bar;
    public Text Orange_text;

    public Text NumberOfGames;
    public int[] winsCOunter = {0,0,0,0}; //red,blue,green,orange
    public int totalGames = 0;

    public Text TimeOut_Text;
    public Text TimeOutPercentage_Text;
    public int timeOuts=0;

    public Text turn_text;
    public Text averageTurns_perGame;

    public Dictionary<int,int> AllGamesDuration = new Dictionary<int,int>();
    public void UpdateGraph(int winerNumber,bool timedOut) 
    {
        totalGames++;
        winsCOunter[winerNumber]++;
        updateBars();

        Red_text.text = winsCOunter[0].ToString();
        Blue_text.text = winsCOunter[1].ToString();
        Green_text.text = winsCOunter[2].ToString();
        Orange_text.text = winsCOunter[3].ToString();

        NumberOfGames.text = totalGames.ToString();

        if (timedOut)
        {
            timeOuts = timeOuts + 1;
        }
        TimeOut_Text.text = timeOuts.ToString();
        TimeOutPercentage_Text.text = "%" + (((float)timeOuts / (float)totalGames)*100).ToString("F2");
    }
    public void updateNumberOfTurnsPerRound(int turnsThatTookThisGame) 
    {
        if (AllGamesDuration.ContainsKey(turnsThatTookThisGame)) 
        {
            AllGamesDuration[turnsThatTookThisGame]++;
        }else
        AllGamesDuration.Add(turnsThatTookThisGame,1);

        float totalNumberOfTurns = 0;
        foreach (var item in AllGamesDuration)
        {
            totalNumberOfTurns = totalNumberOfTurns + (item.Key * item.Value);
        }
        averageTurns_perGame.text = ((float)totalNumberOfTurns / (float)totalGames).ToString("F2");
    }
    public void updateturn(int turnNumber) 
    {
        turn_text.text = turnNumber.ToString();
    }
    public void updateBars() 
    {
        Red_bar.fillAmount = (float)winsCOunter[0]/ (float)totalGames;
        Blue_bar.fillAmount = (float)winsCOunter[1] / (float)totalGames;
        Green_bar.fillAmount = (float)winsCOunter[2] / (float)totalGames;
        Orange_bar.fillAmount = (float)winsCOunter[3] / (float)totalGames;
    }

}

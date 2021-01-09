using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour , IComparable
{
    public enum PColor
    {
        RED, BLUE, GREEN, ORANGE, GRAY
    }
    public PColor pColor = PColor.GRAY;

    public PlayerBoard board;
    public int money=300;
    public Sprite image;
    public Vector3 ofset;
    public int MINmovement = 1;
    public int MAXmovement = 6;
    public string MatchStatus = "Playing";

    public int currentPositionIndex = -1;

    public int myPlayOrder =-1;

    public GameObject playerPawn;
    public virtual void PlayHisTurn()
    {
        if(MatchStatus!= "Playing") 
        {
            return;
        }
        int newIndex = GetPlayerNewPosition();//roll dice
        if (newIndex< currentPositionIndex) //player compleated a lap
        {
            ReciveMoney(100);
        }
        Vector3 newPosition = MANAGER.BoardManager.instance.Tiles[newIndex].transform.position;//coordinate to move
        playerPawn.transform.position = newPosition + ofset;//move
        currentPositionIndex = newIndex;//update Index
        if (MANAGER.BoardManager.instance.Tiles[currentPositionIndex].donoPropriedade == this)
        {
            //Eu Sou o Dono Dessa Propriedade nao posso fazer nada
        }
        else
        {
            if (MANAGER.BoardManager.instance.Tiles[currentPositionIndex].donoPropriedade == null)
            {
                RealizarComportamento();//do behaviour
            }
            else
            {
                //pagar para o dono
                //Player dono = MANAGER.BoardManager.instance.Tiles[currentPositionIndex].donoPropriedade;
                MANAGER.BoardManager.instance.PagarAluguel(this, currentPositionIndex);
            }
        }
        updateBoard();//Update board

    }
    public virtual void RealizarComportamento() 
    {
    }
    public Sprite getPlayerImage() 
    {
        return image;
    }
    public void ReciveMoney(int newMoney) 
    {
        if(money + newMoney < 0) 
        {
            Debug.LogError("You cant Recive Negative amount of money");
        } 
        money = money + newMoney;
    }
    public void ResetStatusAndPlace()
    {
        MatchStatus = "Playing";
        currentPositionIndex = -1;
        money = 300;
        updateBoard();


    }
    public bool PayMoneytoBank(int newMoney)
    {
        if (money - newMoney > money)
        {
            Debug.LogError("You cant Recive money paying");
        }
        if (money - newMoney < 0)
        {
            return false;
        }
        money = money - newMoney;
        return true;
    }
    public bool PayToOtherPlayer(int value, Player otherPlayer) 
    {
        if (money - value < 0) 
        {
            otherPlayer.ReciveMoney(money);
            money = money - value;
            return false;
        }
        money = money - value;
        otherPlayer.ReciveMoney(value);
        return true;
    }
    public void OBJLeaveGame() 
    {
        playerPawn.SetActive(false);
    }


    public int rollMovementDice()
    {
        int numeroAleatorio = UnityEngine.Random.Range(MINmovement, MAXmovement+1);
        return numeroAleatorio;

    }
    public  int GetPlayerNewPosition()
    {
        int result = (currentPositionIndex + rollMovementDice()) % 20;
        return result;
    }
    public void updateBoard() 
    {
        board.money.text = money.ToString();
        board.placement.text = MatchStatus;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        Player otherPlayer = obj as Player;
        if (otherPlayer != null) 
        {
            if (this.money != otherPlayer.money) 
            {
                return this.money.CompareTo(otherPlayer.money);
            }
            return this.myPlayOrder.CompareTo(otherPlayer.myPlayOrder);
        }
        else
            throw new ArgumentException("Object is not a Player");
    }
}

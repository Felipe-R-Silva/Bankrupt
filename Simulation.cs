using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Simulation : MonoBehaviour
{

    public int NumberOfSimulations = 300;
    public int currentSimulation;
    public int turnsCounter = 0;
    public int MaxTurnsToEndGame = 500;
    [SerializeField]
    public Dictionary<int, string[]> matches= new Dictionary<int, string[]>(); //<match number,(first place-x , second place-y , third place-z)>

    Dictionary<Player, int> PlayerPlayOrder = new Dictionary<Player, int>();
    private void Start()
    {
        StartCoroutine(Simulacao300Partidas(0f));
    }
    IEnumerator Simulacao300Partidas(float speed)
    {
        for (currentSimulation = 1; currentSimulation < NumberOfSimulations+1; currentSimulation++)
        {
            PlayerPlayOrder = GetRandomOrderOfPlayers();
            List<Player> PlayerOrder= new List<Player>();
            foreach (var item in PlayerPlayOrder)
            {
                PlayerOrder.Insert(item.Value, item.Key);
            }
            while (!MANAGER.BoardManager.instance.GameOver)
            {
                for (int i = 0; i < PlayerOrder.Count; i++)
                {
                    PlayerOrder[i].PlayHisTurn();
                    yield return new WaitForSeconds(speed);
                }
                turnsCounter++;
                MANAGER.BoardManager.instance.graphUI.GetComponent<Graph>().updateturn(turnsCounter);
                if (turnsCounter >= MaxTurnsToEndGame)
                {
                    MANAGER.BoardManager.instance.GameOver = true;
                }
            }
            if (turnsCounter >= MaxTurnsToEndGame) 
            {
                MANAGER.BoardManager.instance.match = Desempate();
            }
            ResetAllPlayersAndBoard(currentSimulation);
        }
    }
    public string[] Desempate() 
    {
        List<Player> allPlayers = new List<Player>();
        allPlayers.Add(MANAGER.BoardManager.instance.Red);
        allPlayers.Add(MANAGER.BoardManager.instance.Green);
        allPlayers.Add(MANAGER.BoardManager.instance.Blue);
        allPlayers.Add(MANAGER.BoardManager.instance.Orange);
        allPlayers.Sort();
        string[] finalResult = { "", "", "", "" };
        Debug.Log("("+ allPlayers.Count);
        for (int i = 0; i < allPlayers.Count; i++)
        {
            finalResult[i] = allPlayers[i].pColor.ToString();
        }

        //update player placement

        //milésima rodada com a vitória do jogador com mais coins GANHA
        //Caso mais jogadores terminem com a mesma quantidade de coins, o critério de desempate é a ordem de turno dos jogadores nesta partida, sendo a prioridade do último a jogar para o primeiro.
        return finalResult;
    }
    private Dictionary<Player,int> GetRandomOrderOfPlayers() 
    {

        // menor numero jogou antes
        Dictionary< Player,int> PlayerOder = new Dictionary<Player, int>();
        List<Player> players = new List <Player>();
        players.Add(MANAGER.BoardManager.instance.Red);
        players.Add(MANAGER.BoardManager.instance.Green);
        players.Add(MANAGER.BoardManager.instance.Blue);
        players.Add(MANAGER.BoardManager.instance.Orange);

        while (players.Count != 0 ) 
        {
            int randomIndexToRemove = Random.Range(0, players.Count);
            players[randomIndexToRemove].myPlayOrder = PlayerOder.Count;// colocar no player
            PlayerOder.Add(players[randomIndexToRemove], PlayerOder.Count);
            players.RemoveAt(randomIndexToRemove);
        }
        return PlayerOder;
    }
    private void ResetAllPlayersAndBoard(int currentMatchNumber)
    {
        Debug.Log("I am reseting status");
        //STORE GAME RESULT "match" string
        int winnerNumber = -1;
        if (MANAGER.BoardManager.instance.match[3] == "RED") 
        {
            winnerNumber = 0;
        }
        if (MANAGER.BoardManager.instance.match[3] == "BLUE")
        {
            winnerNumber = 1;
        }
        if (MANAGER.BoardManager.instance.match[3] == "GREEN")
        {
            winnerNumber = 2;
        }
        if (MANAGER.BoardManager.instance.match[3] == "ORANGE")
        {
            winnerNumber = 3;
        }
        bool timedOut = false;
        if(turnsCounter == MaxTurnsToEndGame) 
        { 
            timedOut = true; 
        } else { timedOut = false; }
        Debug.Log(timedOut);
        MANAGER.BoardManager.instance.graphUI.GetComponent<Graph>().UpdateGraph(winnerNumber, timedOut);
        MANAGER.BoardManager.instance.graphUI.GetComponent<Graph>().updateNumberOfTurnsPerRound(turnsCounter);
        Debug.Log("(" + MANAGER.BoardManager.instance.match[0] + "," + MANAGER.BoardManager.instance.match[1] + "," + MANAGER.BoardManager.instance.match[2] + "," + MANAGER.BoardManager.instance.match[3] + ")");
        matches.Add(currentMatchNumber, MANAGER.BoardManager.instance.match);
        //Clear "match" string
        cleanStringArray(MANAGER.BoardManager.instance.match);
        //RESET PLAYERS
        MANAGER.BoardManager.instance.Red.ResetStatusAndPlace();
        MANAGER.BoardManager.instance.Blue.ResetStatusAndPlace();
        MANAGER.BoardManager.instance.Green.ResetStatusAndPlace();
        MANAGER.BoardManager.instance.Orange.ResetStatusAndPlace();
        //RESET BOARD *can be simplified* 
        MANAGER.BoardManager.instance.DesapropriarPlayerDeTodasAsAreas(MANAGER.BoardManager.instance.Red);
        MANAGER.BoardManager.instance.DesapropriarPlayerDeTodasAsAreas(MANAGER.BoardManager.instance.Blue);
        MANAGER.BoardManager.instance.DesapropriarPlayerDeTodasAsAreas(MANAGER.BoardManager.instance.Green);
        MANAGER.BoardManager.instance.DesapropriarPlayerDeTodasAsAreas(MANAGER.BoardManager.instance.Orange);
        //UPDATE PLAYERBOARD
        MANAGER.BoardManager.instance.Red.updateBoard();
        MANAGER.BoardManager.instance.Blue.updateBoard();
        MANAGER.BoardManager.instance.Green.updateBoard();
        MANAGER.BoardManager.instance.Orange.updateBoard();
        //SET GAME TO NOT OVER
        MANAGER.BoardManager.instance.GameOver = false;
        turnsCounter = 0;
    }
    private void cleanStringArray(string[]target) 
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i] = "";
        }
    }
}


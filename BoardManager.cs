using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace MANAGER
{
    public class BoardManager : MonoBehaviour
    {
        public bool GameOver = false;
        public string[] match = {"","","",""};
        public Sprite ImagemOfFeeProperty;
        public static BoardManager instance; //singleton
        public GameObject board;
        public GameObject graphUI;
        public List<Tile_Data> Tiles;
        public podium podiumplace;

        public Player Red;
        public Player Green;
        public Player Blue;
        public Player Orange;
        //textConversion
        string path = "Assets/Resources/gameConfig.txt";
        private void Awake()
        {
            if (instance == null) 
            {
                instance = this;
            }
            else 
            {
                Destroy(this.gameObject);
            }
        }
        void Start()
        {
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);
            for (int i = 0; i < Tiles.Count; i++)
            {
                string firstLine = reader.ReadLine();//splits every line of text file
                string[] subs = firstLine.Split(' ');// splits every word
                if (firstLine == null) 
                {
                    break;
                }
                Tiles[i].change_Custo(int.Parse(subs[0]));
                Tiles[i].change_Aluguel(int.Parse(subs[subs.Length - 1]));
            }
            reader.Close();
            // Write all the text file on the board
        }

        public void ChangeTileOwner(Player NewOwner,int indexOfTile) 
        {
            Tiles[indexOfTile].change_Imagem(NewOwner.image);
            Tiles[indexOfTile].set_Dono_Propriedade(NewOwner);
        }
        public void BuyProperty(Player buyer, int indexProperty) 
        {
            if (buyer.PayMoneytoBank(Tiles[indexProperty].get_Custo()))
            {
                ChangeTileOwner(buyer, indexProperty);
            }
            else
            {
              //  Debug.Log(buyer.pColor + " could not buy property[" + Tiles[indexProperty] + "]");
            }
        }
        public void PagarAluguel(Player playerDevendo,int indexProperty) 
        {
            bool PlayerCOnseguiuPagar = playerDevendo.PayToOtherPlayer(Tiles[indexProperty].get_Aluguel(), Tiles[indexProperty].get_Dono_Propriedade());
            if (!PlayerCOnseguiuPagar) 
            {
                RemoverPlayerDoJogo(playerDevendo);
            }


        }
        public void RemoverPlayerDoJogo(Player LoserPlayer) 
        {
            //remover Propriedades
            foreach (Tile_Data tile in Tiles)
            {
                if (tile.compare_Image_e_Igual(LoserPlayer.image)) 
                {
                    tile.desapripriarArea();
                }
            }
            //remove player peca do jogador do tabuleiro
            MovePlayerToPodium(LoserPlayer);
            //Contabilizar LOSE
        }

        public void DesapropriarPlayerDeTodasAsAreas(Player TargetPlayer)
        {
            //remover Propriedades
            foreach (Tile_Data tile in Tiles)
            {
                if (tile.compare_Image_e_Igual(TargetPlayer.image))
                {
                    tile.desapripriarArea();
                }
            }
        }

        private void MovePlayerToPodium(Player playerThatReachedEnd) 
        {
            if (match[0] == "") //forth place
            {
                match[0]=playerThatReachedEnd.pColor.ToString();
                playerThatReachedEnd.MatchStatus = "4th";
                playerThatReachedEnd.transform.position = podiumplace.Forth.position;
            }
            else if (match[1] == "") //third place
            {
                match[1] = playerThatReachedEnd.pColor.ToString();
                playerThatReachedEnd.MatchStatus = "3th";
                playerThatReachedEnd.transform.position = podiumplace.Third.position;
            }
            else if (match[2] == "") //second place
            {
                match[2] = playerThatReachedEnd.pColor.ToString();
                playerThatReachedEnd.MatchStatus = "2th";
                playerThatReachedEnd.transform.position = podiumplace.Second.position;

                Player winer = FindWiner();
                match[3] = winer.pColor.ToString();
                winer.MatchStatus = "1st";
                winer.updateBoard();
                winer.transform.position = podiumplace.First.position;
                //Game Over
                GameOver = true;
            }
        }
        public Player FindWiner() 
        {
            if(Red.MatchStatus == "Playing") 
            {
                return Red;
            }
            if (Blue.MatchStatus == "Playing")
            {
                return Blue;
            }
            if (Green.MatchStatus == "Playing")
            {
                return Green;
            }
                return Orange;
        }
    }
}

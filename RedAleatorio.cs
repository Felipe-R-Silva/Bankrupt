using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAleatorio : Player
{
    RedAleatorio()
    {
        pColor = PColor.RED;
        ofset = new Vector3(-0.3f, 0, -0.3f);
    }
    public override void RealizarComportamento()
    {
        //O jogador (RED)   4 é aleatório;Compra a propriedade de maneira aleatória, com probabilidade de 50 %.
        bool vouComprar = false;
        int numeroAleatorio = Random.Range(0, 2);
        if (numeroAleatorio == 1) 
        {
            vouComprar = true;
        }
        if (vouComprar)
        {
            MANAGER.BoardManager.instance.BuyProperty(this, currentPositionIndex);
        }
    }
}

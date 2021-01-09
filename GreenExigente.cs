using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenExigente : Player
{
    GreenExigente()
    {
        pColor = PColor.GREEN;
        ofset = new Vector3(0.3f, 0, 0.3f);
    }

    public override void RealizarComportamento()
    {

        //O jogador (GREEN) 2 é exigente; Compra qualquer propriedade, desde que o aluguel dela seja maior do que 50 coins.
        if (MANAGER.BoardManager.instance.Tiles[currentPositionIndex].get_Custo() > 50) 
        {
            MANAGER.BoardManager.instance.BuyProperty(this, currentPositionIndex);
        }
    }
}

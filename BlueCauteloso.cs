using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCauteloso : Player
{
    BlueCauteloso()
    {
        pColor = PColor.BLUE;
        ofset = new Vector3(0.3f, 0, -0.3f);
    }
    public override void RealizarComportamento()
    {
        //O jogador (BLUE)  3 é cauteloso;Compra qualquer propriedade, desde que ao final da compra ele possua uma reserva maior ou igual a 80 coins.
        if (money - MANAGER.BoardManager.instance.Tiles[currentPositionIndex].get_Custo()>= 80)
        {
            MANAGER.BoardManager.instance.BuyProperty(this, currentPositionIndex);
        }
    }
}

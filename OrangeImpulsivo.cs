using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeImpulsivo : Player
{
    OrangeImpulsivo() 
    {
        pColor = PColor.ORANGE;
        ofset = new Vector3(-0.3f,0,0.3f);
    }

    public override void RealizarComportamento()
    {
        //O jogador (ORANGE)1 é impulsivo; Compra qualquer propriedade sobre a qual ele parar. 
        MANAGER.BoardManager.instance.BuyProperty(this,currentPositionIndex);
    }
}

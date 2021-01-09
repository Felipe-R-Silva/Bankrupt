using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile_Data : MonoBehaviour
{
    int custo;
    public Text text_custo;

    public Image ImagemDono;
    public Player donoPropriedade;
    int aluguel;
    public Text text_aluguel;

    // Update is called once per frame
    public void change_Custo(int newCusto) 
    {
        custo = newCusto;
        text_custo.text = custo.ToString();
    }
    public void change_Aluguel(int newaluguel)
    {
        aluguel = newaluguel;
        text_aluguel.text = aluguel.ToString();
    }
    public Player get_Dono_Propriedade() 
    {
        return donoPropriedade;
    }
    public void set_Dono_Propriedade(Player novoDono)
    {
        donoPropriedade = novoDono;
    }
    public void change_Imagem(Sprite ImagemNova)
    {
        ImagemDono.sprite = ImagemNova;
    }
    public void desapripriarArea() 
    {
        ImagemDono.sprite = MANAGER.BoardManager.instance.ImagemOfFeeProperty;
        donoPropriedade = null;
    }
    public bool compare_Image_e_Igual(Sprite ImagemComparar) 
    {
        if (ImagemComparar == ImagemDono.sprite)
        {
            return true;
        }
        else return false;
    }
    public int get_Custo() 
    {
        return custo;
    }
    public int get_Aluguel()
    {
        return aluguel;
    }

}

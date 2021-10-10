using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
megcsinaljuk pygameben DE DE csak ha elobb c++ ban (meg brainfckban is egybol), opengl nélkül
Done:E

jo ohm guys amig Boti meghackeli a githubot es megprobalja feltolteni a cuccot osszeszedhetnenk h mi kell meg a gamebe

Todo: hi:)
- Enpassant
- Checkmate system
°O°
*/
public class GameController : MonoBehaviour
{
    public int turns = 0;
    public List<GameObject> Whites = new List<GameObject>();
    public List<GameObject> Blacks = new List<GameObject>();
    public GameObject Board;
    public GameObject Pieces;
    private GameObject SS;
    public GameObject IG;
    public int BD;
    public int WD;
    void Awake()
    {
        PlayerChange();
        SS = GameObject.Find("Start Screen");
    }

    public void PlayerChange()
    {
        turns += 1;
        GetPieces();
        switch (turns % 2)
        {
            case 1:
                foreach (GameObject piece in Blacks)
                {
                    piece.GetComponent<BoxCollider2D>().enabled = false;
                }
                foreach (GameObject piece in Whites)
                {
                    piece.GetComponent<BoxCollider2D>().enabled = true;
                }
                break;
            case 0:
                foreach (GameObject piece in Whites)
                    {
                        piece.GetComponent<BoxCollider2D>().enabled = false;
                    }
                foreach (GameObject piece in Blacks)
                {
                    piece.GetComponent<BoxCollider2D>().enabled = true;
                }
                break;
        }
    }


    public void GetPieces()
    {
        Blacks.Clear();
        Whites.Clear();
        foreach (GameObject OB in GameObject.FindGameObjectsWithTag("Black"))
        {
            Blacks.Add(OB);
        }
        foreach (GameObject OB in GameObject.FindGameObjectsWithTag("White"))
        {
            Whites.Add(OB);
        }
    }
    public void TP()
    {
        Board.SetActive(true);
        Pieces.SetActive(true);
        SS.SetActive(false);
        IG.SetActive(true);
    }
    public void OP()
    {
        Board.SetActive(true);
        Pieces.SetActive(true);
        SS.SetActive(false);
        IG.SetActive(true);
    }
    public void NewDestroyed(Sprite S)
    {
        GameObject Img = new GameObject(S.name);
        Image image = Img.AddComponent<Image>();
        RectTransform RT = Img.GetComponent<RectTransform>();
        RT.SetParent(IG.transform);
        RT.sizeDelta = new Vector2(62.5f, 62.5f);
        RT.localScale = new Vector2(1, 1);
        if (System.Convert.ToInt32(S.name) <= 5)
        {
            if (WD <= 7)
            {
                RT.localPosition += new Vector3(-368.75f, -218.75f + (62.5f * (WD % 8)), 0);
            }
            else
            {
                RT.localPosition += new Vector3(-306.25f, -218.75f + (62.5f * (WD % 8)), 0);
            }

            WD++;
        }
        else
        {
            if (WD <= 7)
            {
                RT.localPosition += new Vector3(368.75f, -218.75f + (62.5f * (BD % 8)), 0);
            }
            else
            {
                RT.localPosition += new Vector3(306.25f, -218.75f + (62.5f * (BD % 8)), 0);
            }
            BD++;
        }
        image.sprite = S;
    }
}
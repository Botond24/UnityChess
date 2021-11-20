using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Todo: enpassant
Todo: Check fix
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
    void Start()
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
    public void NewDestroyed(Sprite s)
    {
        var img = new GameObject(s.name);
        var image = img.AddComponent<Image>();
        var rt = img.GetComponent<RectTransform>();
        rt.SetParent(IG.transform);
        rt.sizeDelta = new Vector2(62.5f, 62.5f);
        rt.localScale = new Vector2(1, 1);
        if (System.Convert.ToInt32(s.name) <= 5)
        {
            if (WD <= 7)
            {
                rt.localPosition += new Vector3(-368.75f, -218.75f + (62.5f * (WD % 8)), 0);
            }
            else
            {
                rt.localPosition += new Vector3(-306.25f, -218.75f + (62.5f * (WD % 8)), 0);
            }

            WD++;
        }
        else
        {
            if (WD <= 7)
            {
                rt.localPosition += new Vector3(368.75f, -218.75f + (62.5f * (BD % 8)), 0);
            }
            else
            {
                rt.localPosition += new Vector3(306.25f, -218.75f + (62.5f * (BD % 8)), 0);
            }
            BD++;
        }
        image.sprite = s;
    }
}
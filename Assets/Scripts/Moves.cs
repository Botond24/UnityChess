using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    private List<string> names = new List<string>() { "TQ", "TB", "TN", "TR" }; //t for temporary
    private Camera Cam;
    private string a; 
    private List<Transform> BPs = new List<Transform>();
    public Dictionary<GameObject, List<int>> everyMove = new Dictionary<GameObject, List<int>>();
    public List<int> Moved = new List<int>();
    public List<Transform> possibleMoves = new List<Transform>();
    private List<int> BlackLocations = new List<int>();
    private List<int> WhiteLocations = new List<int>();
    private List<Transform> BlackTakes = new List<Transform>();
    private List<Transform> WhiteTakes = new List<Transform>();
    void Start() {
        a = gameObject.name;
        BPs = GameObject.Find("Board").GetComponent<GenerateBoard>().BPs;
        Cam = Camera.main;
        everyMove = GameObject.Find("Pieces").GetComponent<GeneratePieces>().allMoves;
        Moved = everyMove[gameObject];
        Moved.Add(BPs.IndexOf(GetClosest(BPs)));
        GenerateMoves();
        GetTakes();
    }
    void OnMouseDown()
    {
        GetLocations();                         //   -7, +1, +9
        possibleMoves.Clear();                  //   -8,  0, +8
        GenerateMoves();                        //   -9, -1, +7
        foreach (Transform BP in possibleMoves)
        {
            BP.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    void OnMouseDrag() {
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(2.1f,2.1f);
        transform.position = new Vector3(
            Cam.ScreenToWorldPoint(Input.mousePosition).x,
            Cam.ScreenToWorldPoint(Input.mousePosition).y,
            -1.1f
        );
    }
    void OnMouseUp()
    {
        foreach (var BP in possibleMoves)
        {
            BP.GetComponent<SpriteRenderer>().color = Color.white;
        }
        transform.position = GetClosest(possibleMoves).position - new Vector3(0,0,1);
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(2,2);
        if (BPs.IndexOf(GetClosest(BPs)) != Moved[Moved.Count - 1])
        {
            Moved.Add(BPs.IndexOf(GetClosest(BPs)));
            foreach (KeyValuePair<GameObject, List<int>> kvp in everyMove)
            {
                if (kvp.Value[kvp.Value.Count - 1] == Moved[Moved.Count - 1] && kvp.Key != gameObject)
                {
                    GameObject.Find("Pieces").GetComponent<GeneratePieces>().Keys.Remove(kvp.Key);
                    everyMove.Remove(kvp.Key);
                    GameObject.Find("Canvas").GetComponent<GameController>().NewDestroyed(kvp.Key.GetComponent<SpriteRenderer>().sprite);
                    Destroy(kvp.Key);
                    break;
                }
            }
            GameObject.Find("Canvas").GetComponent<GameController>().PlayerChange();
        }
    }
    public Transform GetClosest(List<Transform> BP)
    {
        Transform T = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform tr in BP)
        {
            float dist = Vector3.Distance(tr.position, currentPos);
            if (dist < minDist)
            {
                T = tr;
                minDist = dist;
            }
        }
        return T;
    }
    void GetLocations()
    {
        BlackLocations.Clear();
        WhiteLocations.Clear();
        foreach (GameObject OB in GameObject.FindGameObjectsWithTag("Black"))
        {
            BlackLocations.Add(BPs.IndexOf(OB.GetComponent<Moves>().GetClosest(BPs)));
        }
        foreach (GameObject OB in GameObject.FindGameObjectsWithTag("White"))
        {
            WhiteLocations.Add(BPs.IndexOf(OB.GetComponent<Moves>().GetClosest(BPs)));
        }
    }
    void GenerateMoves()
    {
        int i = BPs.IndexOf(GetClosest(BPs));
        if (a.Contains("K"))       
        {
            switch (i % 8)
            {
                case 0:
                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+1]);
                        possibleMoves.Add(BPs[i+8]);
                        possibleMoves.Add(BPs[i+9]);
                    } else if (56 <= i && i <= 63){
                        possibleMoves.Add(BPs[i+1]);
                        possibleMoves.Add(BPs[i-8]);
                        possibleMoves.Add(BPs[i-7]);
                    } else {
                        possibleMoves.Add(BPs[i+1]);
                        possibleMoves.Add(BPs[i+8]);
                        possibleMoves.Add(BPs[i-8]);
                        possibleMoves.Add(BPs[i-7]);
                        possibleMoves.Add(BPs[i+9]);
                    }
                    break;
                case 7:
                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+8]);
                        possibleMoves.Add(BPs[i-1]);
                        possibleMoves.Add(BPs[i+7]);
                    } else if (56 <= i && i <= 63){
                        possibleMoves.Add(BPs[i-8]);
                        possibleMoves.Add(BPs[i-9]);
                        possibleMoves.Add(BPs[i-1]); 
                    } else {
                        possibleMoves.Add(BPs[i+8]);
                        possibleMoves.Add(BPs[i-8]);
                        possibleMoves.Add(BPs[i-9]);
                        possibleMoves.Add(BPs[i-1]);
                        possibleMoves.Add(BPs[i+7]);
                    }
                    
                    break;
                default:
                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+1]);
                        possibleMoves.Add(BPs[i+9]);
                        possibleMoves.Add(BPs[i+8]);
                        possibleMoves.Add(BPs[i-1]);
                        possibleMoves.Add(BPs[i+7]);
                    } else if (56 <= i && i <= 63){
                        possibleMoves.Add(BPs[i-7]);
                        possibleMoves.Add(BPs[i+1]);
                        possibleMoves.Add(BPs[i-8]);
                        possibleMoves.Add(BPs[i-9]);
                        possibleMoves.Add(BPs[i-1]);
                    } else {
                        possibleMoves.Add(BPs[i-7]);
                        possibleMoves.Add(BPs[i+1]);
                        possibleMoves.Add(BPs[i+9]);
                        possibleMoves.Add(BPs[i-8]);
                        possibleMoves.Add(BPs[i+8]);
                        possibleMoves.Add(BPs[i-9]);
                        possibleMoves.Add(BPs[i-1]);
                        possibleMoves.Add(BPs[i+7]);
                    }
                    break;
            }
            if (a.Contains("B"))
            {
                foreach (Transform tr in WhiteTakes)
                {
                    possibleMoves.Remove(tr);
                }

            }else
            {
                foreach (Transform tr in BlackTakes)
                {
                    possibleMoves.Remove(tr);
                }
            }

        }
        else if(a == "BB" || a == "WB")
        {
            int j = i;
            j = i;
            while (j % 8 != 7 && j < 56)
            {
                j += 9;
                if ((a == "BB" && BlackLocations.Contains(j)) || (a == "WB" && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 0 && j < 56)
            {
                j += 7;
                if ((a == "BB" && BlackLocations.Contains(j)) || (a == "WB" && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 0 && j > 7)
            {
                j -= 9;
                if ((a == "BB" && BlackLocations.Contains(j)) || (a == "WB" && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 7 && j > 7)
            {
                j -= 7;
                if ((a == "BB" && BlackLocations.Contains(j)) || (a == "WB" && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
        }
        else if(a.Contains("N"))
        {
            switch (i % 8)
            {
                case 0:
                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                    } else if (8 <= i && i <= 15){
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                    } else if (56 <= i && i <= 63)
                    {
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i-15]);
                    } else if (48 <= i && i <= 55)
                    {
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i-15]);
                    } else {
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                        possibleMoves.Add(BPs[i-15]);
                    }
                    break;
                case 1:
                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                        possibleMoves.Add(BPs[i+15]);
                    } else if (8 <= i && i <= 15){
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                        possibleMoves.Add(BPs[i+15]);
                    } else if (56 <= i && i <= 63)
                    {
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-17]);
                    } else if (48 <= i && i <= 55)
                    {
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-17]);
                    } else {
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i+15]);
                    }
                    break;
                case 6:
                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i+15]);
                        possibleMoves.Add(BPs[i+17]);
                    } else if (8 <= i && i <= 15){
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i+15]);
                        possibleMoves.Add(BPs[i+17]);
                    } else if (56 <= i && i <= 63)
                    {
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-17]);
                    } else if (48 <= i && i <= 55)
                    {
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i+6]);
                    } else {
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i+15]);
                        possibleMoves.Add(BPs[i+17]);
                    }
                    break;
                case 7:
                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i+15]);
                    } else if (8 <= i && i <= 15){
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i+15]);
                    } else if (56 <= i && i <= 63)
                    {
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-17]);
                    } else if (48 <= i && i <= 55)
                    {
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i+6]);
                    } else {
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i+15]);
                    }
                    break;
                default:

                    if (0 <= i && i <= 7)
                    {
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                        possibleMoves.Add(BPs[i+15]);
                        possibleMoves.Add(BPs[i+6]);
                    } else if (8 <= i && i <= 15){
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                        possibleMoves.Add(BPs[i+15]);
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-6]);
                    } else if (56 <= i && i <= 63)
                    {
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-6]);
                    } else if (48 <= i && i <= 55)
                    {
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-6]);
                        possibleMoves.Add(BPs[i+10]);
                    } else {
                        possibleMoves.Add(BPs[i+10]);
                        possibleMoves.Add(BPs[i+17]);
                        possibleMoves.Add(BPs[i+15]);
                        possibleMoves.Add(BPs[i+6]);
                        possibleMoves.Add(BPs[i-10]);
                        possibleMoves.Add(BPs[i-17]);
                        possibleMoves.Add(BPs[i-15]);
                        possibleMoves.Add(BPs[i-6]);
                    }
                    break;
                
            }
        }
        else if(a.Contains("P"))
        {
            //double moves
            if (i % 8 == 1 && a.Contains("W") && !BlackLocations.Contains(i+1) && !WhiteLocations.Contains(i+1))
            {
                possibleMoves.Add(BPs[i+1]);
                if (!BlackLocations.Contains(i+2) && !WhiteLocations.Contains(i+2))
                {
                    possibleMoves.Add(BPs[i+2]);
                }
                
            }
            else if (i % 8 == 6 && a.Contains("B") && !BlackLocations.Contains(i-1) && !WhiteLocations.Contains(i-1))
            {
                possibleMoves.Add(BPs[i-1]);
                if (!BlackLocations.Contains(i-2) && !WhiteLocations.Contains(i-2))
                {
                    possibleMoves.Add(BPs[i-2]);
                }
            }

            //simlpe moves
            if(i % 8 != 1 && a.Contains("W") && i % 8 != 7 && !BlackLocations.Contains(i+1) && !WhiteLocations.Contains(i+1))
            {
                possibleMoves.Add(BPs[i+1]);
        
            }
            if(i % 8 != 6 && a.Contains("B") && i % 8 != 0 && !BlackLocations.Contains(i-1) && !WhiteLocations.Contains(i-1))
            {
                possibleMoves.Add(BPs[i-1]);        
            }

            //taking enemy pieces
            if (BlackLocations.Contains(i - 7) && a.Contains("W")) //enemy left 4 white
            {
                possibleMoves.Add(BPs[i - 7]);
            }
            if (BlackLocations.Contains(i + 9) && a.Contains("W")) //enemy right 4 white
            {
                possibleMoves.Add(BPs[i + 9]);
            }
            if (WhiteLocations.Contains(i - 9) && a.Contains("B")) //enemy left 4 black
            {
                possibleMoves.Add(BPs[i - 9]);
            }
            if (WhiteLocations.Contains(i + 7) && a.Contains("B")) //enemy right 4 black
            {
                possibleMoves.Add(BPs[i + 7]);
            }

            //in
            if (i % 8 == 7 && a.Contains("W"))
            {
                /*
                Sprites appear;
                Select with onclick;
                save and replace character;
                */

                


                for (int j = 1; j < 5; j++)
                {
                    var P = new GameObject();
                    var SR = P.AddComponent<SpriteRenderer>();
                    P.transform.parent = transform;
                    P.name = names[j];
                    SR.sprite = Resources.LoadAll<Sprite>("cp")[j + 6];
                    SR.drawMode = SpriteDrawMode.Sliced;
                    SR.size = new Vector2(4, 4);
                    Vector3 show = new Vector3(j, 4, -1);
                    P.transform.position = show;
                    P.AddComponent<BoxCollider2D>();
                }
            }
            else if (i % 8 == 0 && a.Contains("B"))
            {
                
            }
            
        }
        else if(a.Contains("R"))
        {
            int j = i;
            while (j % 8 != 7)
            {
                j += 1;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j > 7)
            {
                j -= 8;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 0)
            {
                j -= 1;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j < 56)
            {
                j += 8;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
        }
        else
        {
            int j = i;
            while (j % 8 != 7 && j < 56)
            {
                j += 9;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 0 && j < 56)
            {
                j += 7;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 0 && j > 7)
            {
                j -= 9;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 7 && j > 7)
            {
                j -= 7;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 7)
            {
                j += 1;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j > 7)
            {
                j -= 8;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j % 8 != 0)
            {
                j -= 1;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
            j = i;
            while (j < 56)
            {
                j += 8;
                if ((a.Contains("B") && BlackLocations.Contains(j)) || (a.Contains("W") && WhiteLocations.Contains(j)))
                {
                    break;
                }
                possibleMoves.Add(BPs[j]);
                if (BlackLocations.Contains(j) || WhiteLocations.Contains(j))
                {
                    break;
                }
            }
        }
        goto Remove;
        Remove:
            foreach (Transform m in possibleMoves)
            {
                if (!a.Contains("P") && !a.Contains("R") && (a != "BB" && a != "WB") && !a.Contains("Q"))
                {
                    if (BlackLocations.Contains(BPs.IndexOf(m)) && a.Contains("B"))
                    {
                        possibleMoves.Remove(m);
                        goto Remove;
                    }
                    else if (WhiteLocations.Contains(BPs.IndexOf(m)) && a.Contains("W"))
                    {
                        possibleMoves.Remove(m);
                        goto Remove;
                    }
                }

            }
        possibleMoves.Add(BPs[i]);
    }
    void GetTakes() 
    {
        BlackTakes.Clear();
        WhiteTakes.Clear();
        foreach (KeyValuePair<GameObject,List<int>> kvp in everyMove)
        {
            int i = BPs.IndexOf(kvp.Key.GetComponent<Moves>().GetClosest(BPs));
            if (kvp.Key.name == "BP")
            {
                if (i >= 8)
                {
                    BlackTakes.Add(BPs[i - 9]);
                }
                if (i <=55)
                {
                    BlackTakes.Add(BPs[i + 7]);
                }
            }
            else if (kvp.Key.name == "WP")
            {
                if (i >= 8)
                {
                    BlackTakes.Add(BPs[i - 9]);
                }
                if (i <= 55)
                {
                    BlackTakes.Add(BPs[i + 7]);
                }
            }
        }
        foreach (GameObject OB in GameObject.FindGameObjectsWithTag("Black"))
        {
            if (!OB.name.Contains("P"))
            {
                BlackTakes.AddRange(OB.GetComponent<Moves>().possibleMoves);
            }
        }
        foreach (GameObject OB in GameObject.FindGameObjectsWithTag("White"))
        {
            if (!OB.name.Contains("P"))
            {
                WhiteTakes.AddRange(OB.GetComponent<Moves>().possibleMoves);
            }
        
        }
    }
}
                    //   -14, -6, +2, +10, +18
                    //   -15, -7, +1,  +9, +17
                    //   -16, -8,  0,  +8, +16
                    //   -17, -9, -1,  +7, +15 
                    //   -18, -10, -2, +6, +14
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GeneratePieces : MonoBehaviour
{
    // Start is called before the first frame update
    private List<int> p = new List<int>(){4,3,2,1,0,2,3,4};
    private List<string> n = new List<string>(){"BK","BQ","BB","BN","BR","BP","WK","WQ","WB","WN","WR","WP"};
    public Dictionary<GameObject, List<int>> allMoves = new Dictionary<GameObject, List<int>>();
    public List<GameObject> Temp = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (j > 5 || j < 2)
                {
                    var P = new GameObject();
                    var SR = P.AddComponent<SpriteRenderer>();
                    P.transform.parent = transform;
                    switch (j)
                    {
                        case 0:
                            P.name = n[p[i]+6];
                            SR.sprite = Resources.LoadAll<Sprite>("cp")[p[i]+6];
                            break;
                        
                        case 1:
                            P.name = n[11];
                            SR.sprite = Resources.LoadAll<Sprite>("cp")[11];
                            break;
                        
                        case 6:
                            P.name = n[5];
                            SR.sprite = Resources.LoadAll<Sprite>("cp")[5];
                            break;
                        
                        case 7:
                            P.name = n[p[i]];
                            SR.sprite = Resources.LoadAll<Sprite>("cp")[p[i]];
                            break;
                    }
                    SR.drawMode = SpriteDrawMode.Sliced;
                    SR.size = new Vector2(2,2);
                    //SR.sortingLayerName = "Pieces";
                    Vector3 spawn = transform.position + new Vector3(
                        i*SR.size.x,
                        j*SR.size.y,
                        -1
                    );
                    P.transform.position = spawn;
                    P.AddComponent<BoxCollider2D>();
                    
                    P.AddComponent<Moves>();
                    allMoves.Add(P, new List<int>());
                    if (P.name.Contains("W"))
                    {
                        P.tag = "White";
                    }else
                    {
                        P.tag = "Black";
                    }
                }
            }
        }
    }
    Transform GetClosest(List<Transform> BP, Transform Pt)
    {
        Transform T = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = Pt.position;
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

    public void Promote(GameObject P)
    {
        foreach (var T in Temp)
            {
                T.GetComponent<Button>().onClick.AddListener();
                Instantiate(T, transform.parent.GetChild(4).GetChild(2));
            }
    }
}

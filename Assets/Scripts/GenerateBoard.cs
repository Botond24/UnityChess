using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    private List<string> names = new List<string>(){"A1","A2","A3","A4","A5","A6","A7","A8","B1","B2","B3","B4","B5","B6","B7","B8","C1","C2","C3","C4","C5","C6","C7","C8","D1","D2","D3","D4","D5","D6","D7","D8","E1","E2","E3","E4","E5","E6","E7","E8","F1","F2","F3","F4","F5","F6","F7","F8","G1","G2","G3","G4","G5","G6","G7","G8","H1","H2","H3","H4","H5","H6","H7","H8"};
    private List<int> s = new List<int>(){0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0};
    public List<Transform> BPs = new List<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var BP = new GameObject(names[i*8+j]);
                var SR = BP.AddComponent<SpriteRenderer>();
                SR.sprite = Resources.LoadAll<Sprite> ("cb")[s[i*8+j]];
                BP.transform.parent = transform;
                SR.drawMode = SpriteDrawMode.Sliced;
                SR.size = new Vector2(2,2);
                Vector2 spawn = (Vector2)transform.position + new Vector2(
                    i*SR.size.x,
                    j*SR.size.y
                );
                BP.transform.localScale = new Vector2(1,1);
                BP.transform.position = spawn;
                BPs.Add(BP.transform);
            }
        }
    }
}

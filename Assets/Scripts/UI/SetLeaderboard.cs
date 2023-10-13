using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLeaderboard : MonoBehaviour
{

    public Text rankText;
    public Text IDText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetRankText(string newRank)
    {
        rankText.text = newRank;
    }
    public void SetIDText(string newID)
    {
        IDText.text = newID;
    }
}

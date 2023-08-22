﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text score, drops;
    void Awake()
    {
        score.text = 0 + "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Global.guiState == GUISTATE.game)
        {
            if(Global.seed)
            {
                score.text = Global.score + "";
                drops.text = Global.orb + "";
            }
            else
            {
                score.text = 0 + "";
                drops.text = 0 + "";
            }
        }
    }
}

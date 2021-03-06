﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public float tickDelay = 1f;

    public DecisionMaker decisionMaker;

    void Start()
    {
        InvokeRepeating("TickBrain", tickDelay, tickDelay);
    }

    void TickBrain()
    {
        decisionMaker.MakeDecision();
    }
}

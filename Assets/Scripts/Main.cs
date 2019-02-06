﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MetrixSDK;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public Text eventText = null;

	void Start() 
	{
        // Use your MetricX key for initialization
        Metrix.Initialize ("hqsogeujhbrwube");
		Metrix.EnableLogging(true);
    }

	public void SendEvent()
	{
		Metrix.NewEvent("New Event");
		eventText.text = "Event Sent";
	}
}

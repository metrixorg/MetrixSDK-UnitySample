using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MetrixSDK;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public Text eventText = null;

	void Start() 
	{
        MetrixConfig metrixConfig = new MetrixConfig("hqsogeujhbrwube");
        metrixConfig.SetAppSecret(4, 232, 45455, 5555554, 556665);
		metrixConfig.SetStore("caffee");
        metrixConfig.SetFirebaseAppId("1:730143097783:android:227c981a44d0492eaa9e32");
        Metrix.OnCreate(metrixConfig);
        Dictionary<string, string> dict = new Dictionary<string, string>()
                                            {
                                                {"1","One"},
                                                {"2", "Two"},
                                                {"3","Three"}
                                            };
        Metrix.AddUserAttributes(dict);

    }

	public void SendEvent()
	{
		Metrix.NewEvent("New Event");
		Metrix.NewRevenue("buy",13.2,1,"44");
		eventText.text = "Event Sent";
	}
}

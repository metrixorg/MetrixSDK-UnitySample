using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class MetrixDeeplinks : MonoBehaviour {

	#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void MetrixDeeplinks_init(string gameObject = null, string deeplinkMethod = null);
	#endif

	// Use this for initialization
	void Start () {
		#if UNITY_IOS
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			MetrixDeeplinks_init(gameObject.name);
		}
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onDeeplink(string deeplink) {
		Debug.Log("onDeeplink " + deeplink);
	}


}

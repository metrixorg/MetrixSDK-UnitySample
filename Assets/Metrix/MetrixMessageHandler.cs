using System;
using MetrixSDK;
using UnityEngine;

public class MetrixMessageHandler : MonoBehaviour {

	public void OnDeferredDeeplink (String uri) {
		Metrix.OnDeferredDeeplink (uri);
	}
	public void OnSessionIdListener (String sessionId) {
		Metrix.OnSessionIdListener (sessionId);
	}

	public void OnReceiveUserIdListener (String userId) {
		Metrix.OnReceiveUserIdListener (userId);
	}



}
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif


namespace MetrixSDK
{

    public class Metrix
    {

#if UNITY_ANDROID && !UNITY_EDITOR
		private static AndroidJavaClass metrix;
#endif


#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        private static extern void _MXInitialize(string appkey);
        [DllImport ("__Internal")]
        private static extern void _MXEvent(string slug);
        [DllImport ("__Internal")]
        private static extern void _MXTrackRevenue(string slug,double value, string currency, string orderId);
#endif
        private static GameObject metrixManager = null;
        private static Action<string> deferredDeeplinkDelegate = null;
        private static Action<string> sessionIdDelegate = null;
        private static Action<string> userIdDelegate = null;

        public static void Initialize(string apiKey)
        {
            if (metrixManager == null)
            {
                metrixManager = new GameObject("MetrixManager");
                UnityEngine.Object.DontDestroyOnLoad(metrixManager);
                metrixManager.AddComponent<MetrixMessageHandler>();
            }

#if UNITY_ANDROID && !UNITY_EDITOR
			setJavaObject();
			metrix.CallStatic("initialize", apiKey);
            
#elif UNITY_IOS && !UNITY_EDITOR
            _MXInitialize(apiKey);
#endif
        }

        private static void setJavaObject()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			metrix = new AndroidJavaClass("ir.metrix.sdk.MetrixUnity");
#endif
        }

        public static void EnableLocationListening()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			metrix.CallStatic("enableLocationListening");
#endif
        }

        public static void OnCreate(MetrixConfig metrixConfig)
        {
            if (metrixManager == null)
            {
                metrixManager = new GameObject("MetrixManager");
                UnityEngine.Object.DontDestroyOnLoad(metrixManager);
                metrixManager.AddComponent<MetrixMessageHandler>();
            }
            if (metrixConfig.deferredDeeplinkDelegate != null)
            {
                deferredDeeplinkDelegate = metrixConfig.deferredDeeplinkDelegate;
            }
            if (metrixConfig.sessionIdDelegate != null)
            {
                sessionIdDelegate = metrixConfig.sessionIdDelegate;
            }
            if (metrixConfig.userIdDelegate != null)
            {
                userIdDelegate = metrixConfig.userIdDelegate;
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            setJavaObject();
            metrix.CallStatic("onCreate", ConvertDictionaryToMap(metrixConfig.settings));
    
#elif UNITY_IOS && !UNITY_EDITOR
            _MXInitialize(metrixConfig.settings["appId"]);
#endif
        }


        public static void DisableLocationListening()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("disableLocationListening");
#endif
        }

        public static void SetDeferredDeeplinkResponseListener(Action<string> callback)
        {
            deferredDeeplinkDelegate = callback;
        }
        public static void SetEventUploadThreshold(int eventUploadThreshold)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setEventUploadThreshold", eventUploadThreshold);
#endif
        }

        public static void SetEventUploadMaxBatchSize(int eventUploadMaxBatchSize)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setEventUploadMaxBatchSize", eventUploadMaxBatchSize);
#endif
        }

        public static void SetEventMaxCount(int eventMaxCount)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setEventMaxCount", eventMaxCount);
#endif
        }
        public static long GetSessionNum()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return metrix.CallStatic<Int64>("getSessionNum");
#else
            // todo: fix for other platforms !
            return 0;
#endif
        }

        public static void NewEvent(string eventName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			metrix.CallStatic("newEvent", eventName);
#elif UNITY_IOS && !UNITY_EDITOR
            _MXEvent(eventName);
#endif
        }

        public static void NewEvent(string eventName,
                                    Dictionary<string, string> customAttributes,
                                    Dictionary<string, object> customMetrics)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("newEvent", eventName, ConvertDictionaryToMap(customAttributes), ConvertDictionaryToMap(customMetrics));
#endif
        }
        public static void NewRevenue(string slug, double revenue)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("newRevenueSimple", slug, revenue);
#elif UNITY_IOS && !UNITY_EDITOR
            _MXTrackRevenue(slug, revenue, "IRR", null);
#endif
        }


        public static void NewRevenue(string slug, double revenue, int currency)
        {

            string cr = null;
            if (currency == 0)
            {
                cr = "IRR";
            }
            else if (currency == 1)
            {
                cr = "USD";
            }
            else if (currency == 2)
            {
                cr = "EUR";
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("newRevenueCurrency", slug, revenue, cr);
#elif UNITY_IOS && !UNITY_EDITOR
            _MXTrackRevenue(slug, revenue, cr, null);
#endif

        }

        public static void NewRevenue(string slug, double revenue, string orderId)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("newRevenueOrderId", slug, revenue, orderId);
#elif UNITY_IOS && !UNITY_EDITOR
            _MXTrackRevenue(slug, revenue, "IRR", orderId);
#endif

        }
        public static void AppWillOpenUrl(string deeplink)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("appWillOpenUrl", deeplink);
#endif

        }
        public static void NewRevenue(string slug, double revenue, int currency, string orderId)
        {

            string cr = null;
            if (currency == 0)
            {
                cr = "IRR";
            }
            else if (currency == 1)
            {
                cr = "USD";
            }
            else if (currency == 2)
            {
                cr = "EUR";
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("newRevenueFull", slug, revenue, cr, orderId);
#elif UNITY_IOS && !UNITY_EDITOR
            _MXTrackRevenue(slug, revenue, cr, orderId);
#endif


        }

        public static void AddUserAttributes(Dictionary<string, string> userAttrs)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("addUserAttributes", ConvertDictionaryToMap(userAttrs));
#endif
        }

        public static void AddUserMetrics(Dictionary<string, object> userMets)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("addUserMetrics", ConvertDictionaryToMap(userMets));
#endif
        }

        public static void ScreenDisplayed(string screenName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("screenDisplayed", screenName);
#endif
        }
        public static void OnDeferredDeeplink(String uri)
        {
            if (deferredDeeplinkDelegate != null)
            {
                deferredDeeplinkDelegate(uri);
            }
        }

        public static void OnSessionIdListener(String sessionId)
        {
            if (sessionIdDelegate != null)
            {
                sessionIdDelegate(sessionId);
            }
        }
        public static void OnReceiveUserIdListener(String userId)
        {
            if (userIdDelegate != null)
            {
                userIdDelegate(userId);
            }
        }

        public static bool IsScreenFlowsAutoFill()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return metrix.CallStatic<Boolean>("isScreenFlowsAutoFill");
#else
            return false;
#endif
        }

        public static void ShouldLaunchDeferredDeeplink(bool launch)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("shouldLaunchDeferredDeeplink", launch);
#endif
        }

        public static void SetScreenFlowsAutoFill(bool screenFlowsAutoSend)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setScreenFlowsAutoFill", screenFlowsAutoSend);
#endif
        }

        public static void SetMetrixApiKey(string apiKey)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setMetrixApiKey", apiKey);
#endif
        }

        public static void SetAppSecret(string secretId, string info1, string info2, string info3, string info4)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setAppSecret", secretId, info1, info2, info3, info4);
#endif
        }

        public static void SetDefaultTracker(string trackerToken)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setDefaultTracker", trackerToken);
#endif
        }

        private static AndroidJavaObject ConvertDictionaryToMap(IDictionary<string, string> parameters)
        {


            AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr putMethod = AndroidJNIHelper.GetMethodID(
                javaMap.GetRawClass(), "put",
                    "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (KeyValuePair<string, string> kvp in parameters)
            {

                using (AndroidJavaObject k = new AndroidJavaObject(
                    "java.lang.String", kvp.Key))
                {
                    using (AndroidJavaObject v = new AndroidJavaObject(
                        "java.lang.String", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                                putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }

        private static AndroidJavaObject ConvertDictionaryToMap(IDictionary<string, object> parameters)
        {
            AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap");
            IntPtr putMethod = AndroidJNIHelper.GetMethodID(
         javaMap.GetRawClass(), "put",
             "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (KeyValuePair<string, object> kvp in parameters)
            {

                using (AndroidJavaObject k = new AndroidJavaObject(
                    "java.lang.String", kvp.Key))
                {
                    string type = "java.lang.String";
                    if (kvp.Value.GetType() == typeof(int))
                        type = "java.lang.Integer";
                    else if (kvp.Value.GetType() == typeof(double))
                        type = "java.lang.Double";
                    else if (kvp.Value.GetType() == typeof(bool))
                        type = "java.lang.Boolean";
                    else if (kvp.Value.GetType() == typeof(long))
                        type = "java.lang.Long";
                    else if (kvp.Value.GetType() == typeof(float))
                        type = "java.lang.Float";


                    using (AndroidJavaObject v = new AndroidJavaObject(type, kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                                putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }
    }
}


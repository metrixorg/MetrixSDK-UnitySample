using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MetrixSDK {

	public class Metrix
    {

		#if UNITY_ANDROID && !UNITY_EDITOR
		private static AndroidJavaClass metrix;
		#endif
      
		public static void Initialize(string apiKey)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
			setJavaObject();
			metrix.CallStatic("initialize", apiKey);
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

        public static void DisableLocationListening()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("disableLocationListening");
            #endif
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

        public static void SetEventUploadPeriodMillis(int eventUploadPeriodMillis)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setEventUploadPeriodMillis",eventUploadPeriodMillis);
            #endif
        }

        public static void SetSessionTimeoutMillis(long sessionTimeoutMillis)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setSessionTimeoutMillis", sessionTimeoutMillis);
            #endif
        }

        public static void EnableLogging(bool enableLogging)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("enableLogging", enableLogging);
            #endif
        }

        public static void SetLogLevel(int logLevel)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setLogLevel", logLevel);
            #endif
        }


        public static void SetFlushEventsOnClose(bool flushEventsOnClose)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setFlushEventsOnClose", flushEventsOnClose);
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

        public static void NewBusinessEvent(string itemType,
                                            string itemId,
                                            string cartType,
                                            string transactionNum,
                                            int amount)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("newBusinessEvent", itemType, itemId, cartType, transactionNum, amount);
            #endif
        }

        public static void AddUserAttributes(Dictionary<string, string> userAttrs)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("addUserAttributes", ConvertDictionaryToMap(userAttrs));
            #endif
        }

        public static void SetUserMetrics(Dictionary<string, object> userMets)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("setUserMetrics", ConvertDictionaryToMap(userMets));
            #endif
        }

        public static void ScreenDisplayed(string screenName)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            metrix.CallStatic("screenDisplayed", screenName);
            #endif
        }

        public static bool IsScreenFlowsAutoFill()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            return metrix.CallStatic<Boolean>("isScreenFlowsAutoFill");
            #else
            return false;
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
            AndroidJavaClass stringClazz = new AndroidJavaClass("java.lang.String");
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


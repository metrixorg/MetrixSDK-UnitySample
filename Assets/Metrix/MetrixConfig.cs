using System;
using System.Collections;
using System.Collections.Generic;

namespace MetrixSDK
{
    public class MetrixConfig
    {
        internal Dictionary<string, object> settings = new Dictionary<string, object>();
        internal Action<string> deferredDeeplinkDelegate = null;
        internal Action<string> sessionIdDelegate = null;
        internal Action<string> userIdDelegate = null;
        public MetrixConfig(string appId)
        {
            settings.Add("shouldLaunchDeeplink", false);
            settings.Add("appId", appId);
        }

        public void SetFirebaseAppId(string firebaseAppId)
        {
            settings.Add("firebaseAppId", firebaseAppId);
        }

        /**
         * set location listening
         *
         * @param locationListening location listening
         */
        public void SetLocationListening(bool locationListening)
        {
            settings.Add("locationListening", locationListening);

        }


        /**
         * set sdk signature
         *
         * @param secretId secret id
         * @param info1    info 1
         * @param info2    info 2
         * @param info3    info 3
         * @param info4    info 4
         */
        public void SetAppSecret(long secretId, long info1, long info2, long info3, long info4)
        {

            settings.Add("secretId", secretId.ToString());
            settings.Add("info1", info1.ToString());
            settings.Add("info2", info2.ToString());
            settings.Add("info3", info3.ToString());
            settings.Add("info4", info4.ToString());

        }

        /**
         * set store
         *
         * @param store store
         */

        public void SetStore(String store)
        {
            settings.Add("store", store);

        }

        public void SetFlushEventsOnClose(bool flushEventsOnClose)
        {
            settings.Add("flushEventsOnClose", flushEventsOnClose);
        }


        /**
         * Sets event upload threshold. The SDK will attempt to batch upload unsent events
         * every eventUploadPeriodMillis milliseconds, or if the unsent event count exceeds the
         * event upload threshold.
         *
         * @param eventUploadThreshold the event upload threshold
         */
        public void SetEventUploadThreshold(int eventUploadThreshold)
        {
            settings.Add("eventUploadThreshold", eventUploadThreshold);

        }

        /**
         * Sets event upload max batch size. This controls the maximum number of events sent with
         * each upload request.
         *
         * @param eventUploadMaxBatchSize the event upload max batch size
         */
        public void SetEventUploadMaxBatchSize(int eventUploadMaxBatchSize)
        {
            settings.Add("eventUploadMaxBatchSize", eventUploadMaxBatchSize);

        }

        /**
         * Sets event max count. This is the maximum number of unsent events to keep on the device
         * (for example if the device does not have internet connectivity and cannot upload events).
         * If the number of unsent events exceeds the max count, then the SDK begins dropping events,
         * starting from the earliest logged.
         *
         * @param eventMaxCount the event max count
         */
        public void SetEventMaxCount(int eventMaxCount)
        {
            settings.Add("eventMaxCount", eventMaxCount);

        }

        /**
         * Sets event upload period millis. The SDK will attempt to batch upload unsent events
         * every eventUploadPeriodMillis milliseconds, or if the unsent event count exceeds the
         * event upload threshold.
         *
         * @param eventUploadPeriodMillis the event upload period millis
         */
        public void SetEventUploadPeriodMillis(long eventUploadPeriodMillis)
        {
            settings.Add("eventUploadPeriodMillis", eventUploadPeriodMillis);

        }

        /**
         * Sets session timeout millis. If foreground tracking has not been enabled with
         * {@code enableForegroundTracking()}, then new sessions will be started after
         * sessionTimeoutMillis milliseconds have passed since the last event logged.
         *
         * @param sessionTimeoutMillis the session timeout millis
         */
        public void SetSessionTimeoutMillis(long sessionTimeoutMillis)
        {
            settings.Add("sessionTimeoutMillis", sessionTimeoutMillis);

        }

        /**
         * Sets the logging level. Logging messages will only appear if they are the same severity
         * level or higher than the set log level.
         *
         * @param logLevel the log level
         * @return the MetrixClient
         */
        public void SetLogLevel(int logLevel)
        {
            settings.Add("logLevel", logLevel);

        }

        /**
         * Sets default tracker. The tracker added to all subsequent events.
         *
         * @param trackerToken pass tracker token
         */
        public void SetDefaultTrackerToken(string defaultTrackerToken)
        {
            settings.Add("defaultTrackerToken", defaultTrackerToken);

        }


        /**
         * Enable/disable message logging by the SDK.
         *
         * @param loggingEnabled whether to enable message logging by the SDK.
         */
        public void EnableLogging(bool loggingEnabled)
        {
            settings.Add("loggingEnabled", loggingEnabled);

        }

        public void SetShouldLaunchDeeplink(bool shouldLaunchDeeplink)
        {
            settings.Add("shouldLaunchDeeplink", shouldLaunchDeeplink);
        }

        public void SetDeferredDeeplinkDelegate(Action<string> deferredDeeplinkDelegate)
        {
            this.deferredDeeplinkDelegate = deferredDeeplinkDelegate;
        }
       public void SetSessionIdDelegate(Action<string> sessionIdDelegate)
        {
            this.sessionIdDelegate = sessionIdDelegate;
        }
       public void SetUserIdDelegate(Action<string> userIdDelegate)
        {
            this.userIdDelegate = userIdDelegate;
        }

        
    }
}
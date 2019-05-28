## MetrixSDK Unity Doc 

      
*Read this in other languages: [فارسی](README.md) , [English](README.en.md).*  
  
<h2>Table of contents</h2>  
<a href=#project_setup>1. Basic integration</a><br>  
<a href=#install_referrer>2. install referrer</a><br>
<a href=#google_play_store_intent>2.1. google play store intent</a><br>
<a href=#integration>3. Add the SDK to your project</a><br>
<a style="padding-left:2em" href=#application_setup>3.1. Initial configuration in the app</a><br>
<a href=#methods>4. Additional features</a><br>
<a style="padding-left:2em" href=#session_event_description>4.1. Explain the concepts of event and session</a><br>
<a style="padding-left:2em" href=#enableLocationListening>4.2. Enable location listening</a><br>
<a style="padding-left:2em" href=#setEventUploadThreshold>4.3. Limitation in number of events to upload</a><br>
<a style="padding-left:2em" href=#setEventUploadMaxBatchSize>4.4. Limitation in number of events to send per request</a><br>
<a style="padding-left:2em" href=#setEventMaxCount>4.5. Limitation in number of events to buffer on the device</a><br>
<a style="padding-left:2em" href=#setEventUploadPeriodMillis>4.6. The time interval for sending events</a><br>
<a style="padding-left:2em" href=#setSessionTimeoutMillis>4.7. The session timeout</a><br>
<a style="padding-left:2em" href=#enableLogging>4.8. Log management</a><br>
<a style="padding-left:2em" href=#setLogLevel>4.9. Set LogLevel</a><br>
<a style="padding-left:2em" href=#setFlushEventsOnClose>4.10. Flush all events</a><br>
<a style="padding-left:2em" href=#getSessionNum>4.11. Current session number</a><br>
<a style="padding-left:2em" href=#newEvent>4.12. Custom event</a><br>
<a style="padding-right:2em" href=#newRevenue>4.13. Track Revenue</a><br>
<a style="padding-left:2em" href=#setScreenFlowsAutoFill>4.14. Enable the process of storing the user flow</a><br>
<a style="padding-left:2em" href=#setDefaultTracker>4.15. Pre-installed trackers</a><br>
  
  
  
<h2 id=project_setup>Basic integration</h2>  
  
1. Download the latest version from [our releases page](https://storage.backtory.com/metricx/sdk-unity/MetrixSDK-v0.9.1.unitypackage).
Open your project in the Unity Editor and navigate to Assets → Import Package → Custom Package and select the downloaded Unity package file.
  
2. Add the following libraries to the `dependencies` section of your `Asset/Plugins/Android/mainTemplate.gradle` file:  
<div dir="ltr">  
  
    implementation fileTree(dir: 'libs', include: [‘*.jar'])

    implementation 'com.android.installreferrer:installreferrer:1.0'

    implementation 'com.google.code.gson:gson:2.8.5'

    implementation 'com.squareup.retrofit2:retrofit:2.5.0'

    implementation 'com.squareup.retrofit2:converter-gson:2.5.0'

    implementation 'com.squareup.okhttp3:logging-interceptor:3.12.1'

    implementation 'com.squareup.retrofit2:converter-scalars:2.5.0'

    implementation 'com.google.android.gms:play-services-analytics:16.0.7'

</div>  
  
3. Add the following options to the `android` block of your application's `Asset/Plugins/Android/mainTemplate.gradle` file:  
  
<div dir="ltr">  
  
    compileOptions {  
        targetCompatibility = "8"  
        sourceCompatibility = "8"  
    }  
</div>  
  
  
4. If you are using Proguard, add these lines to your `Proguard` file: 
  
<div dir=ltr>  
      
    #Unity Player
    -keep class com.unity3d.player.** { *; }

    -keepattributes Signature
    -keepattributes *Annotation*
    -keepattributes EnclosingMethod
    -keepattributes InnerClasses

    -keepclassmembers enum * { *; }
    -keep class **.R$* { *; }
    -keep interface ir.metrix.sdk.NoProguard
    -keep class * implements ir.metrix.sdk.NoProguard { *; }
    -keep interface * extends ir.metrix.sdk.NoProguard { *; }
    -keep class ir.metrix.sdk.network.model.** { *; }

    # retrofit
    # Retain service method parameters when optimizing.
    -keepclassmembers,allowshrinking,allowobfuscation interface * {
    @retrofit2.http.* <methods>;
    }

    # Ignore JSR 305 annotations for embedding nullability information.
    -dontwarn javax.annotation.**

    # Guarded by a NoClassDefFoundError try/catch and only used when on the classpath.
    -dontwarn kotlin.Unit

    # Top-level functions that can only be used by Kotlin.
    -dontwarn retrofit2.-KotlinExtensions
    
    # With R8 full mode, it sees no subtypes of Retrofit interfaces since they are created with a Proxy
    # and replaces all potential values with null. Explicitly keeping the interfaces prevents this.
    -if interface * { @retrofit2.http.* <methods>; }
    -keep,allowobfuscation interface <1>
    
    #OkHttp
    # A resource is loaded with a relative path so the package of this class must be preserved.
    -keepnames class okhttp3.internal.publicsuffix.PublicSuffixDatabase
    
    # Animal Sniffer compileOnly dependency to ensure APIs are compatible with older versions of Java.
    -dontwarn org.codehaus.mojo.animal_sniffer.*
    
    # OkHttp platform used only on JVM and when Conscrypt dependency is available.
    -dontwarn okhttp3.internal.platform.ConscryptPlatform
    
    
    
    #Gson
    # Gson specific classes
    -dontwarn sun.misc.**
    #-keep class com.google.gson.stream.** { *; }
    
    # Prevent proguard from stripping interface information from TypeAdapterFactory,
    # JsonSerializer, JsonDeserializer instances (so they can be used in @JsonAdapter)
    -keep class * implements com.google.gson.TypeAdapterFactory
    -keep class * implements com.google.gson.JsonSerializer
    -keep class * implements com.google.gson.JsonDeserializer
    
    #referral
    -keep public class com.android.installreferrer.** { *; }
    
    #gms
    -keep class com.google.android.gms.** { *; }
    -dontwarn android.content.pm.PackageInfo

</div>  

5. Please add the following permissions, which the Metrix SDK needs, if they are not already present in your `AndroidManifest.xml` file in `Plugins/Android` folder:
  
<div dir=ltr>  
  
    <uses-permission android:name="android.permission.INTERNET" />  
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />  
    <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" /> <!--optional-->  
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" /> <!--optional-->  
</div>  
  
(Two last permissions are optional)  

<h2 id=install_referrer>Install Referrer</h2>

In order to correctly attribute an install of your app to its source, Metrix needs information about the **install referrer**. This can be obtained by using the **Google Play Referrer API** or by catching the **Google Play Store intent** with a broadcast receiver.

**Important**: The Google Play Referrer API is newly introduced by Google with the express purpose of providing a more reliable and secure way of obtaining install referrer information and to aid attribution providers in the fight against click injection. It is **strongly advised** that you support this in your application. The Google Play Store intent is a less secure way of obtaining install referrer information. It will continue to exist in parallel with the new Google Play Referrer API temporarily, but it is set to be deprecated in future.


<h3 id=google_play_store_intent>Google Play Store intent</h3>

The Google Play Store `INSTALL_REFERRER` intent should be captured with a broadcast receiver. If you are  **not using your own broadcast receiver**  to receive the  `INSTALL_REFERRER`  intent, add the following  `receiver`  tag inside the  `application` tag in your  `AndroidManifest.xml`.

```
<receiver
    android:name="ir.metrix.sdk.MetrixReferrerReceiver"
    android:permission="android.permission.INSTALL_PACKAGES"
    android:exported="true" >
    <intent-filter>
        <action android:name="com.android.vending.INSTALL_REFERRER" />
    </intent-filter>
</receiver>
```

<h2 id=integration>Implement the SDK in your project</h2>  
  
<h3 id=application_setup>Initial configuration in the app</h3>  
  
You need to initialize the Metrix SDK in `onCreate` method of your `Application`. If you do not already have a class `Application` in your project, create this class as below:<br>  
Initialize the Metrix according to the codes below: <br>  

<div dir=ltr>  

    Metrix.Initialize("APP_ID");
</div>  


Replace `APP_ID` with your application id. You can find that in your Metrix's dashboard.  
    
<h2 id=methods>Additional features</h2>  
  
<h3 id=session_event_description>Explain the concepts of event and session</h3>  
In each interaction that the user has with the app, the Metrix sends this interaction to the server as a <b>event</b>. In Metrix, <b>session</b> means that a specific timeframe that the user interacts with the app.<br>  
There are three types of events in the Metrix:<br>  
<b>1. Session Start:</b> The time of start a session.<br>  
<b>2. Session Stop:</b> The time of stop a session.<br>  
<b>3. Custom:</b> Depending on your application logic and the interactiion that the user has with your app, you can create and send custom events as below:<br>  

  
<h3 id=enableLocationListening>Enable location listening</h3>  
You can declare to Metrix to send information about the location of the user using the following functions. (In order to these method work properly, the optional permissions must be enabled) <br>  
<div dir=ltr>  
  
    Metrix.EnableLocationListening();  
  
    Metrix.DisableLocationListening();  
</div>  
  
<h3 id=setEventUploadThreshold>Limitation in number of events to upload</h3>  
Using the following function, you can specify that each time the number of your buffered events reaches the threshold, SDK should send them to the server:<br>  
<div dir=ltr>  
  
    Metrix.SetEventUploadThreshold(50);   
</div>  
(The default value is 30 events.)<br>  
  
<h3 id=setEventUploadMaxBatchSize>Limitation in number of events to send per request</h3>  
Using this function, you can specify the maximum number of outcoming events per request as shown below:<br>  
<div dir=ltr>  

      Metrix.SetEventUploadMaxBatchSize(100);  
</div>  
(The default value is 100 events.)<br>  
  
<h3 id=setEventMaxCount>Limitation in number of events to buffer on the device</h3>  
Using the following function, you can specify how much the maximum number of buffered events is in the SDK (for example, if the user device lost its internet connection, the events will be stored in the library as the amount of you specify) and if the number of buffered events in the library pass this amount. Old events are not kept and destroyed by SDK:<br>  
<div dir=ltr>  
  
    Metrix.SetEventMaxCount(1000);  
</div>  
(The default value is 100 events.)<br>  
  
<h3 id=setEventUploadPeriodMillis>The time interval for sending events</h3>  
By using this function, you can specify that how long the request for sending events should be sent: <br>  
<div dir=ltr>  
  
    Metrix.SetEventUploadPeriodMillis(30000);   
</div>  
(The default value is 30 seconds.)<br>  
  
<h3 id=setSessionTimeoutMillis>The session timeout</h3>  
Using this function, you can specify the limit of session lengthes in your application. For example, if the value of this value is 10,000, if the user interacts with the application in 70 seconds, the Metrix calculates this interaction in seven sessions.<br>  
<div dir=ltr>  
  
    Metrix.SetSessionTimeoutMillis(1800000);  
</div>  
(The default value is 30 minutes.)<br>  

<h3 id=enableLogging>Log management</h3>  
Note that setting the value of this value to `false` during the release of your application:<br>  
<div dir=ltr>  
  
    Metrix.EnableLogging(true);  
</div>  
(The default value is true.)<br>  
  
<h3 id=setLogLevel>Set LogLevel</h3>  
  
Using this function, you can specify what level of logs to be printed in `logcat`, for example, the following command will display all logs except `VERBOSE` in `logcat`:<br>  
<div dir=ltr>  
  
    Metrix.SetLogLevel(3);
</div>  
  
(The default value is `Log.INFO`.)<br>

The value of `Log Level` can be one of the following:

<div dir=ltr>

    VERBOSE = 2;
    DEBUG = 3;
    INFO = 4;
    WARN = 5;
    ERROR = 6;
    ASSERT = 7;

</div>

<h3 id=setFlushEventsOnClose>Flush all events</h3>  
Using this function, you can specify that when the application is closed, all events buffered in the device, should be sent or not:  
<br>  
<div dir=ltr>  
  
    Metrix.SetFlushEventsOnClose(false);   
</div>  
(The default value is true.)<br>  
  
<h3 id=getSessionNum>Current session number</h3>  
By this function, you can find the current session number:<br>  
<div dir=ltr>  
  
    Metrix.GetSessionNum(); 
</div>  
  
<h3 id=newEvent>Custom event</h3>  
  
You can use Metrix to track any event in your app. Suppose you want to track every tap on a button. You would have to create a new event slug in the Events Management section of your dashboard. Let's say that event slug is `abc123`. In your button's onClick method you could then add the following lines to track the click.  
<br>  
You can call this function in this way:<br>  
Make a custom event that has only one specified name:<br>  
  
<div dir=ltr>  
  
    Metrix.NewEvent(“my_event_slug");    
</div>  
  
The input of this function is String.<br>  
<br>  
  

<h3 id=newRevenue>Track Revenue</h3>  

If your users can generate revenue by tapping on advertisements or making in-app purchases, you can track those revenues too with events. Let's say a tap is worth 12000 Rial. You can also add an optional order ID to avoid tracking duplicate revenues. By doing so, the last ten order IDs will be remembered and revenue events with duplicate order IDs are skipped. This is especially useful for tracking in-app purchases. You can see an  example below.

<div dir=ltr>

    Metrix.NewRevenue("my_event_slug", 12000, 0, "2");
</div>
The first parameter is the slug you get from the dashboard. <br>
The second parameter is the amount of revenue. <br>
The third parameter is the currency of this event. If you do not set the value, Rial is be considered as default value. You can see below its values: <br>

1- `0` Rial
  
2- `1` Dollars
  
3- `2` Euro

The fourth parameter is your order number. <br>
<br>
  
  
<h3 id=setScreenFlowsAutoFill>Enable the process of storing the user flow</h3>  
  
Using this function, you can inform the Metrix to gather information about user's flow in each `Activity`/`Fragment` and these details should be stored automatically:<br>  
<div dir=ltr>  
  
    Metrix.ScreenDisplayed("First Screen"); 
</div>  
   
  
<h3 id=setDefaultTracker>Pre-installed trackers</h3>  
  
If you want to use the Metrix SDK to recognize users whose devices came with your app pre-installed, open your app delegate and set the default tracker of your config. Replace `trackerToken` with the tracker token you created in dashboard. Please note that the Dashboard displays a tracker URL (including http://tracker.metrix.ir/). In your source code, you should specify only the six-character token and not the entire URL. <br>  
<div dir=ltr>  
  
    Metrix.SetDefaultTracker("trackerToken"); 
</div>  
  
  
</div>


## MetrixSDK Unity Doc  

<div dir="rtl">  
  
<h2>فهرست</h2>  
<a href=#project_setup>۱. تنظیمات اولیه در پروژه</a><br>  
<a href=#integration>۲. راه‌اندازی و پیاده‌سازی</a><br>  
<a style="padding-right:2em" href=#application_setup>۲.۱. تنظیمات اولیه در اپلیکیشن</a><br>  
<a href=#methods>۳. امکانات کتابخانه متریکس</a><br>  
<a style="padding-right:2em" href=#session_event_description>۳.۱. توضیح مفاهیم رویداد (event) و نشست (session)</a><br>  
<a style="padding-right:2em" href=#enableLocationListening>۳.۲. ثبت اطلاعات مربوط به مکان</a><br>  
<a style="padding-right:2em" href=#setEventUploadThreshold>۳.۳. سقف تعداد رویدادهای ارسالی</a><br>  
<a style="padding-right:2em" href=#setEventUploadMaxBatchSize>۳.۴. حداکثر تعداد رویدادی ارسالی هر درخواست</a><br>  
<a style="padding-right:2em" href=#setEventMaxCount>۳.۵. تعداد حداکثر ذخیره رویداد در مخزن کتابخانه</a><br>  
<a style="padding-right:2em" href=#setEventUploadPeriodMillis>۳.۶. بازه زمانی ارسال رویدادها</a><br>  
<a style="padding-right:2em" href=#setSessionTimeoutMillis>۳.۷. بازه زمانی دلخواه برای نشست‌ها</a><br>  
<a style="padding-right:2em" href=#setOptOut>۳.۸. دستور عمل نکردن کل کتابخانه</a><br>  
<a style="padding-right:2em" href=#enableLogging>۳.۹. مدیریت لاگ‌ها</a><br>  
<a style="padding-right:2em" href=#setLogLevel>۳.۱۰. تعیین LogLevel</a><br>  
<a style="padding-right:2em" href=#setOffline>۳.۱۱. حالت آفلاین</a><br>  
<a style="padding-right:2em" href=#setFlushEventsOnClose>۳.۱۲. ارسال همه‌ی رویدادها</a><br>  
<a style="padding-right:2em" href=#getSessionNum>۳.۱۳. شماره نشست جاری</a><br>  
<a style="padding-right:2em" href=#newEvent>۳.۱۴. رویداد سفارشی</a><br>  
<a style="padding-right:2em" href=#setScreenFlowsAutoFill>۳.۱۵. فعال کردن فرآیند نگهداری حرکت کاربر بین صفحات مختلف در اپلیکیشن</a><br>  
<a style="padding-right:2em" href=#setDefaultTracker>۳.۱۶. مشخص کردن Pre-installed Tracker</a><br>  
  
  
  
<h2 id=project_setup> تنظیمات اولیه در پروژه</h2>  

۱. ابتدا  کتابخانه‌ متریکس  را  از [این  لینک](https://storage.backtory.com/metricx/sdk-unity/MetrixSDK-v0.3.0.unitypackage) دانلود  کنید  و  در  پروژه  خود import کنید.

۲. سپس  دیپندنسی  های  زیر  راه  به  بلاک dependencies فایل

`Asset/Plugins/Android/mainTemplate.gradle` اضافه  کنید.


<div dir=ltr>  
  
    implementation fileTree(dir: 'libs', include: [‘*.jar'])

    implementation 'com.android.installreferrer:installreferrer:1.0'

    implementation 'com.google.code.gson:gson:2.8.5'

    implementation 'com.squareup.retrofit2:retrofit:2.5.0'

    implementation 'com.squareup.retrofit2:converter-gson:2.5.0'

    implementation 'com.squareup.okhttp3:logging-interceptor:3.13.1'

    implementation 'com.squareup.retrofit2:converter-scalars:2.5.0'


</div>  

۳. آپشن زیر را به بلاک `android` فایل `Asset/Plugins/Android/mainTemplate.gradle` اپلیکیشن خود اضافه کنید:

<div dir="ltr">

    compileOptions {
        targetCompatibility = "8"
        sourceCompatibility = "8"
    }
</div>

۴.اگر از پروگارد برای ماینیفای کردن اپلیکیشن خود استفاده میکنید تنظیمات زیر را به `Asset/Plugins/Android/proguard-user.txt` پروژه خود اضافه کنید:  
  
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

</div>  
  
۵. دسترسی  های  زیر  را  به  فایل `AndroidManifest.xml` موجود  در  فولدر `Plugins/Android` پروژه  خود  اضافه  کنید: 
  
<div dir=ltr>  
  
    <uses-permission android:name="android.permission.INTERNET" />  
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />  
    <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" /> <!--optional-->  
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" /> <!--optional-->  
</div>  
  
(دو permission دوم اختیاری است)  
  
<h2 id=integration>راه‌اندازی و پیاده‌سازی sdk در اپلیکیشن اندروید:</h2>  
  
<h3 id=application_setup>تنظیمات اولیه در اپلیکیشن:</h3>  
  کتابخانه  متریکس  را  در  ابتدای  برنامه‌ی  خود  به  این  روش initialize کنید:

  

<div dir=ltr>  
  
    Metrix.Initialize("APP_ID");
</div>  

`APP_ID`: کلید اپلیکیشن شما که از پنل متریکس آن را دریافت می‌کنید.  
  
<h2 id=methods>امکانات کتابخانه متریکس</h2>  
  
<h3 id=session_event_description>۱. توضیح مفاهیم رویداد (event) و نشست (session)</h3>  
در هر تعاملی که کاربر با اپلیکیشن دارد، کتابخانه متریکس این تعامل را در قالب یک <b>رویداد</b> برای سرور ارسال می‌کند. تعریف کتابخانه متریکس از یک <b>نشست</b>، بازه زمانی مشخصی است که کاربر با اپلیکیشن در تعامل است.<br>  
<br>  
در کتابخانه متریکس سه نوع رویداد داریم:<br>  
<b>۱. شروع نشست (session_start):</b> زمان شروع یک نشست.<br>  
<b>۲. پایان نشست (session_stop):‌</b> زمان پایان یک نشست.<br>  
<b>۳. سفارشی (custom):</b> وابسته به منطق اپلیکیشن شما و تعاملی که کاربر با اپلیکیشن شما دارد می‌توانید رویدادهای سفارشی خود را در قالبی که در ادامه شرح داده خواهد شد بسازید و ارسال کنید.<br>  
  
<h3 id=enableLocationListening>۲. فعال یا غیرفعال کردن ثبت اطلاعات مکان کاربر در رویدادها</h3>  
می‌توانید با استفاده از دو تابع زیر به کتابخانه متریکس اعلام کنید که در رویدادها اطلاعات مربوط به مکان کاربر را به همراه دیگر اطلاعات ارسال کند یا نکند. (برای اینکه این متد به درستی عمل کند دسترسی‌های اختیاری که بالاتر ذکر شد باید فعال باشند)<br>  
<div dir=ltr>  
  
    Metrix.EnableLocationListening();  
  
    Metrix.DisableLocationListening();  
</div>  
  
<h3 id=setEventUploadThreshold>۳. تعیین سقف تعداد رویدادها برای ارسال به سمت سرور</h3>  
با استفاده از تابع زیر می‌توانید مشخص کنید که هر موقع تعداد رویدادهای ذخیره شده شما به تعداد مورد نظر شما رسید کتابخانه رویدادها را برای سرور ارسال کند:<br>  
<div dir=ltr>  
  
    Metrix.SetEventUploadThreshold(50);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه ۳۰ رویداد است.)<br>  
  
<h3 id=setEventUploadMaxBatchSize>۴. تعیین حداکثر تعداد رویداد ارسالی در هر درخواست</h3>  
با استفاده از این تابع می‌توانید حداکثر تعداد رویداد ارسالی در هر درخواست را به شکل زیر مشخص کنید:<br>  
<div dir=ltr>  
  
    Metrix.SetEventUploadMaxBatchSize(100);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه ۱۰۰ رویداد است.)<br>  
  
<h3 id=setEventMaxCount>۵. تعیین تعداد حداکثر ذخیره رویداد در مخزن کتابخانه</h3>  
با استفاده از تابع زیر می‌توانید مشخص کنید که حداکثر تعداد رویدادهای ذخیر شده در کتابخانه متریکس چقدر باشد (به عنوان مثال اگر دستگاه کاربر اتصال خود به اینترنت را از دست داد رویدادها تا مقداری که شما مشخص می‌کنید در کتابخانه ذخیره خواهند شد) و اگر تعداد رویدادهای ذخیره شده در کتابخانه از این مقدار بگذرد رویدادهای قدیمی توسط sdk نگهداری نشده و از بین می‌روند:<br>  
<div dir=ltr>  
  
    Metrix.SetEventMaxCount(1000);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه ۱۰۰۰ رویداد است.)<br>  
  
<h3 id=setEventUploadPeriodMillis>۶. تعیین بازه زمانی ارسال رویدادها به سمت سرور</h3>  
با استفاده از این تابع می‌توانید مشخص کنید که درخواست آپلود رویدادها بعد از گذشت چند میلی‌ثانیه فرستاده شود:<br>  
<div dir=ltr>  
  
    Metrix.SetEventUploadPeriodMillis(30000);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه ۳۰ ثانیه است.)<br>  
  
<h3 id=setSessionTimeoutMillis>۷. تعیین بازه زمانی دلخواه برای نشست‌ها</h3>  
با استفاده از این تابع می‌توانید حد نشست‌ها را در اپلیکیشن خود مشخص کنید که هر نشست حداکثر چند ثانیه محاسبه شود. به عنوان مثال اگر مقدار این تابع را ۱۰۰۰۰ وارد کنید اگر کاربر در اپلیکیشن ۷۰ ثانیه تعامل داشته باشد، کتابخانه متریکس این تعامل را ۷ نشست محاسبه می‌کند.<br>  
<div dir=ltr>  
  
    Metrix.SetSessionTimeoutMillis(1800000);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه ۳۰ دقیقه است.)<br>  
  
<h3 id=setOptOut>۸. دستور عمل نکردن کل کتابخانه</h3>  
با استفاده از این تابع می‌توانید به کتابخانه دستور بدهید که هیچ رویدادی را ثبت نکند:<br>  
<div dir=ltr>  
  
    Metrix.SetOptOut(true);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه false است.)<br>  
  
<h3 id=enableLogging>۹. فعال کردن مدیریت لاگ‌ها کتابخانه متریکس</h3>  
توجه داشته باشید که موقع release اپلیکیشن خود مقدار این تابع را false قرار دهید:<br>  
<div dir=ltr>  
  
    Metrix.EnableLogging(true);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه true است.)<br>  
  
  
<h3 id=setLogLevel>۱۰. تعیین LogLevel</h3>

با استفاده از این تابع می‌توانید مشخص کنید که چه سطحی از لاگ‌ها در `logcat` چاپ شود، به عنوان مثال دستور زیر همه‌ی سطوح لاگ‌ها به جز `VERBOSE` در `logcat` نمایش داده شود:<br>

<div dir=ltr>

    Metrix.SetLogLevel(3);
</div>

(مقدار پیش‌فرض این تابع در کتابخانه `INFO` است.)<br>
نکته : مقدار متناظر با `Log Level`

<div dir=ltr>

    VERBOSE = 2;
    DEBUG = 3;
    INFO = 4;
    WARN = 5;
    ERROR = 6;
    ASSERT = 7;

</div>
<h3 id=setOffline>۱۱. روشن کردن حالت آفلاین کتابخانه</h3>  
با استفاده از این تابع کتابخانه رویدادها را برای سرور ارسال نمی‌کند اما همچنان رویدادها را با تنظیمات دلخواه شما ثبت می‌کند:<br>  
<div dir=ltr>  
  
    Metrix.SetOffline(true);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه false است.)<br>  
  
<h3 id=setFlushEventsOnClose>۱۲. فعال یا غیرفعال کردن ارسال همه‌ی رویدادها</h3>  
با استفاده از این تابع می‌توانید مشخص کنید که زمانی که اپلیکیشن بسته می‌شود همه رویدادهای ذخیره شده در کتابخانه ارسال شود یا نشود:<br>  
<div dir=ltr>  
  
    Metrix.SetFlushEventsOnClose(false);  
</div>  
(مقدار پیش‌فرض این تابع در کتابخانه true است.)<br>  
  
<h3 id=getSessionNum>۱۳. اطلاع یافتن از شماره نشست جاری</h3>  
با استفاده از این تابع می‌توانید از شماره نشست (session)  جاری اطلاع پیدا کنید:<br>  
<div dir=ltr>  
  
    Metrix.GetSessionNum();  
</div>  
  
<h3 id=newEvent>۱۴. ساختن یک رویداد سفارشی</h3>  
با استفاده از این تابع می‌توانید یک رویداد سفارشی بسازید. برای این کار شما در ابتدا باید در داشبورد متریکس از قسمت مدیریت رخدادها، رخداد موردنظر خود را ثبت کنید و نامک (slug) آن را بعنوان نام رخداد در sdk استفاده کنید.<br>  
این تابع را به دو صورت می‌توانید صدا بزنید:<br>  
۱. یک رویداد سفارشی که فقط یک اسم مشخص دارد بسازید:<br>  
  
<div dir=ltr>  
  
    Metrix.NewEvent(“my_event_slug");  
</div>  
  
ورودی این تابع از جنس String است<br>  

<h3 id=setScreenFlowsAutoFill>۱۵. نگهداری حرکات کاربر در صفحات مختلف در اپلیکیشن</h3>

با اضافه کردن تابع زیر صفحات خود میتوانید از حرکت کاربر بین صفحات اطلاع پیدا کنید:<br>
<div dir=ltr>

    Metrix.ScreenDisplayed("First Screen");
</div>
  
<h3 id=setDefaultTracker>۱۶. مشخص کردن Pre-installed Tracker</h3>  
  
با استفاده از این تابع می‌توانید با استفاده از یک `trackerToken` که از پنل آن را دریافت می‌کنید، برای همه‌ی رویدادها یک `tracker` پیش‌فرض را قرار دهید:<br>  
<div dir=ltr>  
  
    Metrix.SetDefaultTracker("trackerToken");  
</div>  
  
  
</div>
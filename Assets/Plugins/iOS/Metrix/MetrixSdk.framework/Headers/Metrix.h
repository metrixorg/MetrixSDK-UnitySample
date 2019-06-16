//
//  Metrix.h
//  Metrix
//
//  V4.17.1
//  Created by Christian Wellenbrock (wellle) on 23rd July 2013.
//  Copyright © 2012-2017 Metrix GmbH. All rights reserved.
//

#import "MXConfig.h"

@class MXCustomEvent;

@interface MetrixTestOptions : NSObject

@property (nonatomic, copy, nullable) NSString *baseUrl;
@property (nonatomic, copy, nullable) NSString *basePath;
@property (nonatomic, copy, nullable) NSNumber *timerIntervalInMilliseconds;
@property (nonatomic, copy, nullable) NSNumber *timerStartInMilliseconds;
@property (nonatomic, copy, nullable) NSNumber *sessionIntervalInMilliseconds;
@property (nonatomic, copy, nullable) NSNumber *subsessionIntervalInMilliseconds;
@property (nonatomic, assign) BOOL teardown;
@property (nonatomic, assign) BOOL deleteState;
@property (nonatomic, assign) BOOL noBackoffWait;
@property (nonatomic, assign) BOOL iAdFrameworkEnabled;

@end

/**
 * Constants for our supported tracking environments
 */
extern NSString * __nonnull const MXEnvironmentSandbox;
extern NSString * __nonnull const MXEnvironmentProduction;

/**
 * @brief The main interface to Metrix.
 *
 * @note Use the methods of this class to tell Metrix about the usage of your app.
 *       See the README for details.
 */
@interface Metrix : NSObject

/**
 * @brief Tell Metrix that the application did launch.
 *        This is required to initialize Metrix. Call this in the didFinishLaunching
 *        method of your AppDelegate.
 *
 * @note See MXConfig.h for more configuration options
 *
 * @param metrixConfig The configuration object that includes the environment
 *                     and the App Token of your app. This unique identifier can
 *                     be found it in your dashboard at http://metrix.ir and should always
 *                     be 12 characters long.
 */
+ (void)appDidLaunch:(nullable MXConfig *)metrixConfig;

/**
 * @brief Tell Metrix that a particular event has happened.
 *
 */
+ (void)trackCustomEvent:(MXCustomEvent *)event;

/**
 * @brief Tell Metrix that a particular event has happened.
 *
 */
+ (void)trackScreen:(NSString *)screenName;

/**
 * @brief Enable or disable the metrix SDK. This setting is saved for future sessions.
 *
 * @param enabled The flag to enable or disable the metrix SDK.
 */
+ (void)setEnabled:(BOOL)enabled;

/**
 * @brief Check if the SDK is enabled or disabled.
 *
 * return Boolean indicating whether SDK is enabled or not.
 */
+ (BOOL)isEnabled;

/**
 * @brief Enable or disable offline mode. Activities won't be sent but they are saved when
 *        offline mode is disabled. This feature is not saved for future sessions.
 *
 * @param enabled The flag to enable or disable offline mode.
 */
+ (void)setOfflineMode:(BOOL)enabled;

/**
 * @brief Retrieve iOS device IDFA value.
 *
 * @return Device IDFA value.
 */
+ (nullable NSString *)idfa;

/**
 * @brief Get current metrix identifier for the user.
 *
 * @note Metrix identifier is available only after installation has been successfully tracked.
 *
 * @return Current metrix identifier value for the user.
 */
+ (nullable NSString *)mxid;

/**
 * @brief Get current Metrix SDK version string.
 *
 * @return Metrix SDK version string (iosX.Y.Z).
 */
+ (nullable NSString *)sdkVersion;

/**
 * @brief Tell the metrix SDK to stop waiting for delayed initialisation timer to complete but rather to start
 *        upon this call. This should be called if you have obtained needed callback/partner parameters which you
 *        wanted to put as default ones before the delayedStart value you have set on MXConfig has expired.
 */
+ (void)sendFirstPackages;

/**
 * Obtain singleton Metrix object.
 */
+ (nullable id)getInstance;

+ (void)setTestOptions:(nullable MetrixTestOptions *)testOptions;

- (void)appDidLaunch:(nullable MXConfig *)metrixConfig;

- (void)trackCustomEvent:(nullable MXCustomEvent *)event;

- (void)trackScreen:(NSString *)screenName;

- (void)setEnabled:(BOOL)enabled;

- (void)teardown;

- (void)setOfflineMode:(BOOL)enabled;

- (void)sendFirstPackages;

- (BOOL)isEnabled;

- (nullable NSString *)mxid;

- (nullable NSString *)idfa;

- (nullable NSString *)sdkVersion;

- (nullable NSURL *)convertUniversalLink:(nonnull NSURL *)url scheme:(nonnull NSString *)scheme;

@end

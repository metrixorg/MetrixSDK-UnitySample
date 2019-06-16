//
//  MXConfig.h
//  metrix
//

#import <Foundation/Foundation.h>

#import "MXLogger.h"
#import "MXEventSuccess.h"
#import "MXEventFailure.h"
#import "MXSessionSuccess.h"
#import "MXSessionFailure.h"

/**
 * @brief Optional delegate that will get informed about tracking results.
 */
@protocol MetrixDelegate

@optional

/**
 * @brief Optional delegate method that gets called when an event is tracked with success.
 *
 * @param eventSuccessResponseData The response information from tracking with success
 *
 * @note See MXEventSuccess for details.
 */
- (void)metrixEventTrackingSucceeded:(nullable MXEventSuccess *)eventSuccessResponseData;

/**
 * @brief Optional delegate method that gets called when an event is tracked with failure.
 *
 * @param eventFailureResponseData The response information from tracking with failure
 *
 * @note See MXEventFailure for details.
 */
- (void)metrixEventTrackingFailed:(nullable MXEventFailure *)eventFailureResponseData;

/**
 * @brief Optional delegate method that gets called when an session is tracked with success.
 *
 * @param sessionSuccessResponseData The response information from tracking with success
 *
 * @note See MXSessionSuccess for details.
 */
- (void)metrixSessionTrackingSucceeded:(nullable MXSessionSuccess *)sessionSuccessResponseData;

/**
 * @brief Optional delegate method that gets called when an session is tracked with failure.
 *
 * @param sessionFailureResponseData The response information from tracking with failure
 *
 * @note See MXSessionFailure for details.
 */
- (void)metrixSessionTrackingFailed:(nullable MXSessionFailure *)sessionFailureResponseData;

/**
 * @brief Optional delegate method that gets called when a deferred deep link is about to be opened by the metrix SDK.
 *
 * @param deeplink The deep link url that was received by the metrix SDK to be opened.
 *
 * @return Boolean that indicates whether the deep link should be opened by the metrix SDK or not.
 */
- (BOOL)metrixDeeplinkResponse:(nullable NSURL *)deeplink;

@end

/**
 * @brief Metrix configuration object class.
 */
@interface MXConfig : NSObject<NSCopying>

/**
 * @brief SDK prefix.
 *
 * @note Not to be used by users, intended for non-native metrix SDKs only.
 */
@property (nonatomic, copy, nullable) NSString *sdkPrefix;

/**
 * @brief Tracker token to attribute organic installs to (optional).
 */
@property (nonatomic, copy, nullable) NSString *trackerToken;

/**
 * @brief Metrix app id
 */
@property (nonatomic, copy, readonly, nonnull) NSString *appId;

/**
 * @brief Metrix environment variable.
 */
@property (nonatomic, copy, readonly, nonnull) NSString *environment;

/**
 * @brief Change the verbosity of Metrix's logs.
 *
 * @note You can increase or reduce the amount of logs from Metrix by passing
 *       one of the following parameters. Use MXLogLevelSuppress to disable all logging.
 *       The desired minimum log level (default: info)
 *       Must be one of the following:
 *         - MXLogLevelVerbose    (enable all logging)
 *         - MXLogLevelDebug      (enable more logging)
 *         - MXLogLevelInfo       (the default)
 *         - MXLogLevelWarn       (disable info logging)
 *         - MXLogLevelError      (disable warnings as well)
 *         - MXLogLevelAssert     (disable errors as well)
 *         - MXLogLevelSuppress   (suppress all logging)
 */
@property (nonatomic, assign) MXLogLevel logLevel;

/**
 * @brief Enable event buffering if your app triggers a lot of events.
 *        When enabled, events get buffered and only get tracked each
 *        minute. Buffered events are still persisted, of course.
 */
@property (nonatomic, assign) BOOL eventBufferingEnabled;

/**
 * @brief Set the optional delegate that will inform you about attribution or events.
 *
 * @note See the MetrixDelegate declaration above for details.
 */
@property (nonatomic, weak, nullable) NSObject<MetrixDelegate> *delegate;

/**
 * @brief Enables sending in the background.
 */
@property (nonatomic, assign) BOOL sendInBackground;

/**
 * @brief Enables delayed start of the SDK.
 */
@property (nonatomic, assign) double delayStart;

/**
 * @brief User agent for the requests.
 */
@property (nonatomic, copy, nullable) NSString *userAgent;

/**
 * @brief Set if the device is known.
 */
@property (nonatomic, assign) BOOL isDeviceKnown;

/**
 * @brief Metrix app secret id.
 */
@property (nonatomic, copy, readonly, nullable) NSString *secretId;

/**
 * @brief Metrix app secret.
 */
@property (nonatomic, copy, readonly, nullable) NSString *appSecret;

/**
 * @brief Metrix screen auto fill.
 */
@property (nonatomic, assign) BOOL isScreenFlowAutoFill;

/**
 * @brief Metrix set app secret.
 */
- (void)setAppSecret:(NSUInteger)secretId
               info1:(NSUInteger)info1
               info2:(NSUInteger)info2
               info3:(NSUInteger)info3
               info4:(NSUInteger)info4;

/**
 * @brief Get configuration object for the initialization of the Metrix SDK.
 *
 * @param appId The App Id of your app. This unique identifier can
 *                 be found it in your dashboard at https://metrix.ir.
 * @param environment The current environment your app. We use this environment to
 *                    distinguish between real traffic and artificial traffic from test devices.
 *                    It is very important that you keep this value meaningful at all times!
 *                    Especially if you are tracking revenue.
 *
 * @returns Metrix configuration object.
 */
+ (nullable MXConfig *)configWithAppId:(nonnull NSString *)appId
                            environment:(nonnull NSString *)environment;

- (nullable id)initWithAppId:(nonnull NSString *)appId
                 environment:(nonnull NSString *)environment;

/**
 * @brief Configuration object for the initialization of the Metrix SDK.
 *
 * @param appId The App Id of your app. This unique identifier can
 *                 be found it in your dashboard at http://metrix.ir and should always
 *                 be 12 characters long.
 * @param environment The current environment your app. We use this environment to
 *                    distinguish between real traffic and artificial traffic from test devices.
 *                    It is very important that you keep this value meaningful at all times!
 *                    Especially if you are tracking revenue.
 * @param allowSuppressLogLevel If set to true, it allows usage of MXLogLevelSuppress
 *                              and replaces the default value for production environment.
 *
 * @returns Metrix configuration object.
 */
+ (nullable MXConfig *)configWithAppId:(NSString *)appId
                           environment:(NSString *)environment
                 allowSuppressLogLevel:(BOOL)allowSuppressLogLevel;

- (nullable id)initWithAppId:(nonnull NSString *)appId
                 environment:(nonnull NSString *)environment
       allowSuppressLogLevel:(BOOL)allowSuppressLogLevel;

/**
 * @brief Check if metrix configuration object is valid.
 * 
 * @return Boolean indicating whether metrix config object is valid or not.
 */
- (BOOL)isValid;

@end

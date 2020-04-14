//
//  MXLogger.h
//  Metrix
//
#import <Foundation/Foundation.h>

typedef enum {
    MXLogLevelVerbose  = 1,
    MXLogLevelDebug    = 2,
    MXLogLevelInfo     = 3,
    MXLogLevelWarn     = 4,
    MXLogLevelError    = 5,
    MXLogLevelAssert   = 6,
    MXLogLevelSuppress = 7
} MXLogLevel;

/**
 * @brief Metrix logger protocol.
 */
@protocol MXLogger

/**
 * @brief Set the log level of the SDK.
 *
 * @param logLevel Level of the logs to be displayed.
 */
- (void)setLogLevel:(MXLogLevel)logLevel isProductionEnvironment:(BOOL)isProductionEnvironment;

/**
 * @brief Prevent log level changes.
 */
- (void)lockLogLevel;

/**
 * @brief Print verbose logs.
 */
- (void)verbose:(nonnull NSString *)message, ...;

/**
 * @brief Print debug logs.
 */
- (void)debug:(nonnull NSString *)message, ...;

/**
 * @brief Print info logs.
 */
- (void)info:(nonnull NSString *)message, ...;

/**
 * @brief Print warn logs.
 */
- (void)warn:(nonnull NSString *)message, ...;
- (void)warnInProduction:(nonnull NSString *)message, ...;

/**
 * @brief Print error logs.
 */
- (void)error:(nonnull NSString *)message, ...;

/**
 * @brief Print assert logs.
 */
- (void)assert:(nonnull NSString *)message, ...;

@end

/**
 * @brief Metrix logger class.
 */
@interface MXLogger : NSObject<MXLogger>

/**
 * @brief Convert log level string to MXLogLevel enumeration.
 *
 * @param logLevelString Log level as string.
 *
 * @return Log level as MXLogLevel enumeration.
 */
+ (MXLogLevel)logLevelFromString:(nonnull NSString *)logLevelString;

@end

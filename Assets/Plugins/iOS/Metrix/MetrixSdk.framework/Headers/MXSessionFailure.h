//
//  MXFailureResponseData.h
//  metrix
//

#import <Foundation/Foundation.h>

@interface MXSessionFailure : NSObject <NSCopying>

/**
 * @brief Message from the metrix backend.
 */
@property (nonatomic, copy, nullable) NSString *message;

/**
 * @brief Timestamp from the metrix backend.
 */
@property (nonatomic, copy, nullable) NSString *timeStamp;

/**
 * @brief Metrix identifier of the device.
 */
@property (nonatomic, copy, nullable) NSString *adid;

/**
 * @brief Information whether sending of the package will be retried or not.
 */
@property (nonatomic, assign) BOOL willRetry;

/**
 * @brief Backend response in JSON format.
 */
@property (nonatomic, strong, nullable) NSDictionary *jsonResponse;

/**
 * @brief Initialisation method.
 *
 * @return MXSessionFailure instance.
 */
+ (nullable MXSessionFailure *)sessionFailureResponseData;

@end

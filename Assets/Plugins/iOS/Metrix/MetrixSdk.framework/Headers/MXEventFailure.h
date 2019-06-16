//
//  MXEventFailure.h
//  metrix
//

#import <Foundation/Foundation.h>

@interface MXEventFailure : NSObject

/**
 * @brief Message from the metrix backend.
 */
@property (nonatomic, copy) NSString * message;

/**
 * @brief Timestamp from the metrix backend.
 */
@property (nonatomic, copy) NSString * timeStamp;

/**
 * @brief Metrix identifier of the device.
 */
@property (nonatomic, copy) NSString * adid;

/**
 * @brief Event token value.
 */
@property (nonatomic, copy) NSString * eventToken;

/**
 * @brief Event callback ID.
 */
@property (nonatomic, copy) NSString *callbackId;

/**
 * @brief Information whether sending of the package will be retried or not.
 */
@property (nonatomic, assign) BOOL willRetry;

/**
 * @brief Backend response in JSON format.
 */
@property (nonatomic, strong) NSDictionary *jsonResponse;

/**
 * @brief Initialisation method.
 *
 * @return MXEventFailure instance.
 */
+ (MXEventFailure *)eventFailureResponseData;

@end

//
//  MXSuccessResponseData.h
//  metrix
//

#import <Foundation/Foundation.h>

@interface MXSessionSuccess : NSObject <NSCopying>

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
 * @brief Backend response in JSON format.
 */
@property (nonatomic, strong, nullable) NSDictionary *jsonResponse;

/**
 * @brief Initialisation method.
 *
 * @return MXSessionSuccess instance.
 */
+ (nullable MXSessionSuccess *)sessionSuccessResponseData;

@end

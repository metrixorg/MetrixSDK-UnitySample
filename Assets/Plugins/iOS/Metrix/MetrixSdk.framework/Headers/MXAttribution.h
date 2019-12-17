//
//  MXAttribution.h
//  Metrix
//

#import <Foundation/Foundation.h>

/**
 * @brief Metrix attribution object.
 */
@interface MXAttribution : NSObject <NSCoding, NSCopying>

/**
 * @brief Tracker token.
 */
@property (nonatomic, copy, nullable) NSString *trackerToken;
@property (nonatomic, copy, nullable) NSString *acquisitionAd;
@property (nonatomic, copy, nullable) NSString *acquisitionAdSet;
@property (nonatomic, copy, nullable) NSString *acquisitionCampaign;
@property (nonatomic, copy, nullable) NSString *acquisitionSource;
@property (nonatomic, copy, nullable) NSString *attributionStatus;

///**
// * @brief Tracker name.
// */
//@property (nonatomic, copy, nullable) NSString *trackerName;
//
///**
// * @brief Network name.
// */
//@property (nonatomic, copy, nullable) NSString *network;
//
///**
// * @brief Campaign name.
// */
//@property (nonatomic, copy, nullable) NSString *campaign;
//
///**
// * @brief Adgroup name.
// */
//@property (nonatomic, copy, nullable) NSString *adgroup;
//
///**
// * @brief Creative name.
// */
//@property (nonatomic, copy, nullable) NSString *creative;
//
///**
// * @brief Click label content.
// */
//@property (nonatomic, copy, nullable) NSString *clickLabel;
//
///**
// * @brief Metrix identifier value.
// */
//@property (nonatomic, copy, nullable) NSString *mxid;

/**
 * @brief Make attribution object.
 * 
 * @param jsonDict Dictionary holding attribution key value pairs.
// * @param mxid metrix identifier value.
 * 
 * @return metrix attribution object.
 */
+ (nullable MXAttribution *)dataWithJsonDict:(nonnull NSDictionary *)jsonDict;
//+ (nullable MXAttribution *)dataWithJsonDict:(nonnull NSDictionary *)jsonDict mxid:(nonnull NSString *)mxid;

- (nullable id)initWithJsonDict:(nonnull NSDictionary *)jsonDict;
//- (nullable id)initWithJsonDict:(nonnull NSDictionary *)jsonDict mxid:(nonnull NSString *)mxid;

/**
 * @brief Check if given attribution equals current one.
 * 
 * @param attribution Attribution object to be compared with current one.
 * 
 * @return Boolean indicating whether two attribution objects are the equal.
 */
- (BOOL)isEqualToAttribution:(nonnull MXAttribution *)attribution;

/**
 * @brief Get attribution value as dictionary.
 * 
 * @return Dictionary containing attribution as key-value pairs.
 */
- (nullable NSDictionary *)dictionary;

@end

//

#import <Foundation/Foundation.h>


@interface MXCustomEvent : NSObject<NSCopying>
@property (nonatomic, copy, nonnull, readonly) NSString *slug;
@property (nonatomic, copy, nonnull, readonly) NSDictionary *attributes;
@property (nonatomic, copy, nonnull, readonly) NSDictionary *metrics;

+(MXCustomEvent *)newEvent:(NSString *)slug attributes:(NSDictionary *)attributes metrics:(NSDictionary *)metrics;
-(id)initWithSlug:(NSString *)slug attributes:(NSDictionary *)attributes metrics:(NSDictionary *)metrics;
/**
 * @brief Check if created metrix event object is valid.
 *
 * @return Boolean indicating whether the metrix event object is valid or not.
 */
- (BOOL)isValid;

@end
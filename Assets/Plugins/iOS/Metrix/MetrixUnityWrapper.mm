#import <MetrixSdk/Metrix.h>
#import <MetrixSdk/MXCustomEvent.h>
#import "UnityAppController.h"
#import <Foundation/Foundation.h>


// Converts NSString to C style string by way of copy (Mono will free it)
#define MX_MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define MX_GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isnt empty
#define MX_GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

#define MX_NSSTRING_OR_EMPTY(x)                        (x ? x : @"")
#define MX_NSDICTIONARY_OR_EMPTY(x)                    (x ? x : @{})
#define MX_IS_STRING_SET(x)                            (x && ![x isEqualToString:@""])

void UnitySendMessage(const char *className, const char *methodName, const char *param);

void MXSafeUnitySendMessage(const char *methodName, const char *param) {
    if (methodName == NULL) {
        methodName = "";
    }
    if (param == NULL) {
        param = "";
    }
    UnitySendMessage("MetrixManager", methodName, param);
}

@interface MetrixUnityController : NSObject

- (void) initializeWithAppKey:(NSString* )appkey;
- (void) newEvent:(NSString* )slug;

@end

@implementation MetrixUnityController

+ (MetrixUnityController*) sharedInstance {
    static MetrixUnityController * instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[MetrixUnityController alloc] init];
       
    });
    return instance;
}

- (void) initializeWithAppKey:(NSString* )appkey{

        MXConfig *metrixConfig = [MXConfig configWithAppId:appkey
                                           environment:MXEnvironmentProduction];
    
    [Metrix appDidLaunch:metrixConfig];

}

- (void) newEvent:(NSString* )slug{
    
    MXCustomEvent *event = [MXCustomEvent newEvent:slug attributes:nil metrics:nil];
    [Metrix trackCustomEvent:event];
}



@end

extern "C" {
    void _MXInitialize(const char *appkey)
    {
        [[MetrixUnityController sharedInstance] initializeWithAppKey:MX_GetStringParam(appkey)];
    }

    void _MXEvent(const char *slug)
    {
        [[MetrixUnityController sharedInstance] newEvent:MX_GetStringParam(slug)];
    }
    
}

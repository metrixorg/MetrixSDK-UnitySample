//
// Created by Abr Studio on 2019-06-25.
// Copyright (c) 2019 metrix. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MXCurrency){
    IRR, USD, EUR
};

@interface MXCurrencyHelper: NSObject
+(NSString *)toString:(MXCurrency)currency ;
@end
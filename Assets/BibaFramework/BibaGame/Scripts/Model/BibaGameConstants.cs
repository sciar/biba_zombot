using System;

namespace BibaFramework.BibaGame
{
    public class BibaGameConstants
    {
        public const string BIBA_URL = "http://www.playbiba.com";
        public const string BIBA_PRIVACY_URL = "http://www.playbiba.com/privacy-policy";

        private const int ONE_DAY_IN_SECONDS = 86400; //1 day
        private const int ONE_HOUR_IN_SECONDS = 3600; //1 hr

        public static readonly TimeSpan AR_REMINDER_DURATION = TimeSpan.FromSeconds(ONE_DAY_IN_SECONDS);
        public static readonly TimeSpan CHARTBOOST_CHECK_DURATION = TimeSpan.FromSeconds(ONE_DAY_IN_SECONDS);
        public static readonly TimeSpan INACTIVE_DURATION = TimeSpan.FromSeconds(ONE_HOUR_IN_SECONDS);

        //Achievement
        public const string ACHIEVEMENT_PREFIX_BRIDGE = "You’ve crossed the equivalent of";
        public const string ACHIEVEMENT_PREFIX_CLIMBER = "You have climbed the height of";
        public const string ACHIEVEMENT_PREFIX_TUBE = "You have crawled the length of";
        public const string ACHIEVEMENT_PREFIX_SLIDE = "You have slid the height of";
        public const string ACHIEVEMENT_PREFIX_SWING = "You’ve swung as many times as";
        public const string ACHIEVEMENT_PREFIX_OVERHANG = "You've climbed the equivalent of";
    }
}
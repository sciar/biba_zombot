namespace BibaFramework.BibaGame
{
    public class BibaEditorConstants
    {
        //Google Doc Settings
        public const string REGEX_STARTDATE_ENDDATE = "(?<startDate>(0[1-9]|1[0-2])[/](0[1-9]|[1-2][0-9]|3[0-1]))[ ]*-[ ]*(?<endDate>(0[1-9]|1[0-2])[/](0[1-9]|[1-2][0-9]|3[0-1]))";
        public const string REGEX_GROUP_STARTDATE = "startDate";
        public const string REGEX_GROUP_ENDDATE = "endDate";
        public const string REGEX_TIME_PLAYED = "played([1-9][0-9]*)";
        public const string DATETIME_PARSE_EXACT_FORMAT = "MM/dd";
    }
}
﻿namespace WebCore.Utils.Config
{
    public static class ConstantConfig
    {
        public static class MemoryCacheConfig
        {
            public const string LanguageCache = "LanguageCache";
            public const string LanguageSelectCache = "LanguageSelectCache";
            public const string SystemConfigCache = "SystemConfigCache";
        }
        public static class WebApiStatusCode
        {
            public const long Success = 0;
            public const long Error = 1;
            public const long Warning = 2;
            public const long ModelInValid = 3;
        }
        public static class WebApiResultMessage
        {
            public const string UpdateSuccess = "WEB_API_RESULT_UPDATE_SUCCESS";
            public const string DeleteSuccess = "WEB_API_RESULT_DELETE_SUCCESS";
            public const string RestoreSuccess = "WEB_API_RESULT_RESTORE_SUCCESS";
            public const string InsertSuccess = "WEB_API_RESULT_INSERT_SUCCESS";
            public const string Success = "WEB_API_RESULT_SUCCESS";
            public const string Error = "WEB_API_RESULT_FAIL";
            public const string UpdateTokenNotMatch = "WEB_API_RESULT_UPDATETOKEN_NOT_MATCH";
        }
        public static class RecordStatusConfig
        {
            public const long Active = 0;
            public const long Deleted = 1;
        }
        public static class UserRecordStatus
        {
            public const long Active = 0;
            public const long Deleted = 1;
            public const long InActive = 2;
        }
        public static class AdminMenuRecordStatus
        {
            public const long Active = 0;
            public const long Deleted = 1;
            public const long Maintenance = 2;
        }
        public static class ClaimType
        {
            public const string Permission = "Permission";
        }
        public static class SystemConfigName
        {
            public const string PageDefaultNumber = "PageDefaultNumber";
        }
        public static class SessionName
        {
            public const string UserSession = "UserSession";
            public const string RoleSession = "RoleSession";
            public const string LanguageSession = "LanguageSession";
            public const string LanguageDetailSession = "LanguageDetailSession";
            public const string AdminMenuSession = "AdminMenuSession";
            public const string MasterListSession = "MasterListSession";
            public const string MasterListGroupSession = "MasterListGroupSession";
            public const string MetaDescriptionSession = "MetaDescriptionSession";
            public const string FeaturedSession = "FeaturedSession";
        }

        public static class MasterListGroup
        {
            public const string FeaturedCombobox = "FeaturedCombobox";
        }

        public const string MasterListMasterGroup = "Master";
    }
}

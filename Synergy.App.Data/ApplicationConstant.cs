using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.App.Common
{

    public static class ApplicationConstant
    {

        public const string AppName = "SYNERGY CMS";
        public const string WindowsServiceUserId = "45bba746-3309-49b7-9c03-b5793369d73c";
        public static class Messages
        {
            public const string DeleteConfirmation = "Are you sure you want to delete the selected item? You cannot undo this action if you proceed. Please confirm";
        }
        public static class DateAndTime
        {
            public static DateTime MinDate = new DateTime(1900, 1, 1);
            public static DateTime MaxDate = new DateTime(9999, 12, 31);
            public const string DefaultJqueryDateFormat = "dd-MM-yyyy";
            public const string DefaultJqueryDateFormatOnly = "yyyy-MM-dd";
            public const string DefaultJqueryDateFormatForMoment = "DD-MM-YYYY";
            public const string DefaultJqueryDateTimeFormatForMoment = "DD-MM-YYYY HH:mm";
            public const string DefaultJqueryDateTimeSecondFormatForMoment = "DD-MM-YYYY HH:mm:ss";
            public const string DefaultJqueryDateTimeFormat = "dd-MM-yyyy HH:mm";
            public const string DefaultDateFormat = "{0:dd.MM.yyyy}";
            public const string DefaultDateTimeFormatOnly = "dd MMM yyyy HH:mm";
            public const string DefaultDateFormatOnly = "MMM dd, yyyy";
            public const string SequenceNumberFormat = "{0:dd-MM-yyyy}";
            public const string DefaultDateTimeFormat = "{0:dd-MM-yyyy HH:mm}";
            public const string YYYY_MM_DD = "{0:yyyy/MM/dd}";
            public const string YYYYMMDD = "{0:yyyyMMdd}";
            public const string YYYY_MM_DD_HH_MM = "{0:yyyy/MM/dd HH:mm}";
            public const string LongDateTimeFormat = "{0:dd MMM yyyy HH:mm:ss}";
            public const string DefaultYY_MM_DD = "{0:yyyy-MM-dd}";
            public const string DefaultYY_MM_DD_HH_MM_SS = "{0:yyyy-MM-dd HH:mm:ss}";
            public const string DefaultDD_MM_YYYY = "{0:dd-MM-yyyy}";
            public const string DefaultDD_MM_YYYY_HH_MM_SS = "{0:dd-MM-yyyy HH:mm:ss}";
            public const string DefaultMM_DD_YYYY_HH_MM_SS = "{0:M/dd/yyyy hh:mm:ss tt}";

            public const string DefaultJqueryTimeFormatForMoment = "HH:mm";
        }
        public static class Database
        {
            public const string TableSpace = "pg_default";
            public const string DefaultCollation = "cms_collation_ci";
            public const string DateFormat = "{0:yyyy-MM-dd HH:mm:ss.ff}";
            public const string JQueryDateFormat = "yyyy-MM-dd HH:mm:ss";
            public static class Schema
            {
                public const string Dbo = "dbo";
                public const string Form = "form";
                public const string _Public = "public";
                public const string Log = "log";

            }
            public static class Owner
            {
                public const string Postgres = "postgres";
            }
        }


        public static class Roles
        {
            public const string User = "User";
            public const string Admin = "Admin";
            public const string SystemAdmin = "SystemAdmin";
            public const string All = "All";
        }
        public static class Session
        {
            public const string UserId = "UserId";
            public const string UserName = "UserName";
            public const string JobTitle = "JobTitle";
            public const string UserEmail = "UserEmail";
            public const string CompanyId = "CompanyId";
        }
        public class AppSettings
        {
            public static string CubeJsUrlDsi(IConfiguration config)
            {
                return $"{Convert.ToString(config.GetSection("CubeJSSettings").GetSection("CubeJSBaseUrlDsi").Value).TrimEnd('/')}/";

            }
            public static string GoogleAPIKey(IConfiguration config)
            {
                return $"{Convert.ToString(config["GoogleAPIKey"]).TrimEnd('/')}/";

            }
            public static string UploadPath(IConfiguration config)
            {
                return $"{Convert.ToString(config["UploadPath"]).TrimEnd('/')}/";

            }
            public static bool SkipHangfireEnqueue(IConfiguration config)
            {
                return Convert.ToBoolean(config["SkipHangfireEnqueue"]);

            }
            public static string ApplicationBaseUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["ApplicationBaseUrl"]).TrimEnd('/')}/";

            }

            public static string CoreApplicationBaseUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["CoreApplicationBaseUrl"]).TrimEnd('/')}/";

            }

            public static string LicensePrivateKey(IConfiguration config)
            {
                return $"{Convert.ToString(config["LPK"])}";
            }
            public static string LicenseApiBaseUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["LSU"])}";
            }
            public static string EsdbConnectionUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["EsdbConnectionUrl"]).TrimEnd('/')}/";

            }
            public static string OCRConnectionUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["OCRConnectionUrl"]).TrimEnd('/')}";
            }
            public static string LogstashConfigPath(IConfiguration config)
            {
                return $"{Convert.ToString(config["LogstashConfigPath"])}";

            }
            public static string WebApiUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["WebApiUrl"]).TrimEnd('/')}/";

            }
            public static string CubeJsUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config.GetSection("CubeJSSettings").GetSection("CubeJSBaseUrl").Value).TrimEnd('/')}/";

            }
            public static string SocialApiUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["SocialApiUrl"]).TrimEnd('/')}/";

            }
            public static string WebApiDevUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["WebApiDevUrl"]).TrimEnd('/')}/";

            }
            public static string WordCloudApiUrl(IConfiguration config)
            {
                try
                {
                    return $"{Convert.ToString(config["WordCloudApiUrl"]).TrimEnd('/')}";
                }
                catch (Exception)
                {

                    return null;
                }


            }
            public static string CctvStreamingUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["CctvStreamingUrl"]).TrimEnd('/')}/";

            }
            public static string ClientName(IConfiguration config)
            {
                return $"{Convert.ToString(config["ClientName"])}";

            }
            public static string YoutubeTrendKey1()
            {
                return "AIzaSyAf8dbbCovdlB7uJKiQDSPL3PzrgPMa0T4";

            }
            public static string YoutubeTrendKey2()
            {
                return "AIzaSyC6NnNso-rRq2eNuykBPFKd6aatSmCkM98";

            }

            public static string PyhtonApiUrl(IConfiguration config)
            {
                return $"{Convert.ToString(config["PyhtonApiUrl"]).TrimEnd('/')}/";

            }
        }
        public class Language
        {
            public const string English = "English";
            public const string Arabic = "Arabic";
            public const string Hindi = "Hindi";
            public const string French = "French";
            public const string Spanish = "Spanish";
        }
        public class LanguageCode
        {
            public const string English = "en-US";
            public const string Arabic = "ar-SA";
            public const string Hindi = "hi-IN";
            public const string French = "fr-FR";
            public const string Spanish = "es-ES";
        }

        public static string PlaceHolder_SelectOption = "--Select--";
        public static string PlaceHolder_AllOption = "--All--";
        public static string EncryptionKey = "d5Pc2gsc91sZ5AJG3mSHqFWi";

        public const int ApplicationStartYear = 2019;
        public const double MaxFileSizeAllowed = 2147483648;
        public class BusinessAnalytics
        {
            public const string MaxDateDial100PastData = @"{""size"": 0,""aggs"": {""max_date"":{""max"":{""field"": ""event_time""}}},""query"": {""simple_query_string"" : {""query"": true,""fields"": [""archiveData""],""default_operator"": ""and""}}}";
            public const string CctnsMaxDateQueryByDistrict = @"{""size"": 0,""aggs"": {""max_date"":{""max"":{""field"": ""#FILTERCOLUMN#""}}},""query"": {""simple_query_string"" : {""query"": ""#DISTRICT#"",""fields"": [""#DISTRICTCOL#""],""default_operator"": ""and""}}}";
            public const string CctnsMaxDateQueryForBackdata = @"{
                                                                ""size"":0,
                                                                  ""query"": {
                                                                    ""bool"": {
                                                                      ""filter"": [
                                                                        {
                                                                          ""term"": {
                                                                            ""#DISTRICTCOL#.keyword"": ""#DISTRICT#""
                                                                          }
                                                                        },        
                                                                        {
                                                                          ""term"": {
                                                                            ""IsPastData"": true
                                                                          }
                                                                        }
                                                                      ]
                                                                    }
                                                                  },
                                                                  ""aggs"": {
                                                                    ""max_date"": {
                                                                      ""max"": {
                                                                        ""field"": ""#FILTERCOLUMN#""
                                                                      }
                                                                    }
                                                                  }
                                                                }
                                                                ";
            public const string WhatsAppQRCode = @"{""phone_no"": #MOBILENO#,""name"":""#NAME#""}";
            public const string DsiDataByFilters = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_DISTRICT#
                                #QUERY_INCIDENT#
                                #QUERY_INSTITUTE_TYPE#
                                #QUERY_INSTITUTE#                               
                                #QUERY_DEMAND#                               
                                #QUERY_LEADER#
                                #QUERY_INCIDENT_DES#
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""leadername"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}			                   
                                }}}";
            public const string DsiDataMaxAttendeeByFilters = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_INCIDENTNAME#
                                #QUERY_DISTRICT#
                                #QUERY_INCIDENT#
                                #QUERY_INSTITUTE_TYPE#
                                #QUERY_INSTITUTE#            
                                #QUERY_LEADER#
                                #QUERY_INCIDENT_DES#
                                #QUERY_DEMAND#
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""leadername"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}			                   
                                }}}";
            public const string DsiLeaderDataByFilters = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_DISTRICT#                                
                                #QUERY_INSTITUTE_TYPE#
                                #QUERY_INSTITUTE#         
                                #QUERY_LEADER#                               
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""leadername"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}			                   
                                }}}";
            public const string DsiDemandDataByFilters = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_DISTRICT#                                
                                #QUERY_DEMAND#                                                           
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}
                                }";
            public const string DsiSeriousEventsDataByFilters = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                {""multi_match"":{""fields"":[ ""incident""],""query"":""मृत्यु मौत मृतक""}},                                
                                #QUERY_DISTRICT#
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}		                   
                                }}}";
            public const string DsiDataById = @"{""size"": 100000,""query"":{""bool"":{""must"":[                               
                                {""multi_match"":{""fields"":[ ""id""],""query"":""#ID#""}}
                                ]}}}";
            public const string DsiLeaderData = @"{
                                                      ""size"": 0,
                                                      ""aggs"": {
                                                        ""distinct_leadernames"": {
                                                          ""terms"": {
                                                            ""field"": ""leadername.keyword"",
                                                            ""size"": 10000
                                                          }
                                                        }
                                                      },
                                                      ""query"":{
                                                          ""bool"":{
                                                              ""must"":[
                                                                        #QUERY_DISTRICT#
                                                                        #QUERY_INSTITUTE_TYPE#
                                                                        #QUERY_INSTITUTE#
                                                                    ]
                                                                }
                                                            }
                                                    }";


            public const string DsiDataByDeath = @"{""size"": 100000, ""query"": { ""bool"": 
                                { ""must"": [
                                #QUERY_DISTRICT#                                                          
                                { ""range"": { ""time"": { ""gte"": ""#STARTDATE#"", ""lte"": ""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}},
                                { ""bool"": { ""should"": [ { ""range"": { ""gov_emp_die"": { ""gt"": 0 }}}, { ""range"": { ""police_Total_die"": { ""gt"": 0 }}},
                                        { ""range"": { ""media_dead"": { ""gt"": 0 }}}, { ""range"": { ""people_Total_die"": { ""gt"": 0 }}} ]}}
                                ],
                                ""must_not"": [
                                { ""terms"": { ""incident_type_id"": [
                                        ""10"", ""11"", ""8"", ""17"", ""18"", ""16"", ""2"", ""57"", ""5"", ""3"",
                                        ""7"", ""6"", ""4"", ""14"", ""12"", ""19"", ""9"", ""20"", ""1"", ""13"",
                                        ""169"", ""15"" ]}}
                                ]}}}";
            public const string ReadWhatsappDataQueryWithFilters = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                #QUERY_SEARCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""chat_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""owner_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""msgText"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""profile_username"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""phone_no"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                
                ""quoted_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},               
                ""quoted_mention_text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string WhatsappCutoffDateQuery = @"{""size"": 0,""aggs"": {""max_date"":{""max"":{""field"": ""scrape_datetime""}}}}";
            public const string ReadTwitterDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadTwitterDataQuery2 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""hashtags"", ""text"", ""location"" ],""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadTwitterDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""hashtags"", ""text"", ""location"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadTwitterDataQuery4 = @"
                        {""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""hashtags"", ""text"", ""location""],
                                ""default_operator"": ""and""
                            }
                          },
                    ""highlight"":{""fields"":{""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";


            public const string ReadFacebookDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadFacebookDataQuery2 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""pagename"", ""post_message"" ],""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{""pagename"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""post_message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadFacebookDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""pagename"", ""post_message"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{""pagename"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""post_message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadFacebookDataQuery4 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""post_message"", ""pagename""],
                                ""default_operator"": ""and""
                            }
                          },
                        ""highlight"":{""fields"":{""pagename"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""post_message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";


            public const string ReadInstagramDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadInstagramDataQuery2 = @"{
                    ""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""hashtags"", ""caption"" ],""query"":""#SEARCHWHERE#""}}}},
                    ""highlight"":{""fields"":{""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""caption"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";

            public const string ReadInstagramDataQuery3 = @"{
                    ""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""hashtags"", ""caption"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},
                    ""highlight"":{""fields"":{""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""caption"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadInstagramDataQuery4 = @"{
                    ""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""hashtags"", ""caption""],
                                ""default_operator"": ""and""
                            }
                          },
                    ""highlight"":{""fields"":{""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""caption"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";

            public const string ReadYoutubeDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadYoutubeDataQuery2 = @"{
""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""title"", ""description"" ],""query"":""#SEARCHWHERE#""}}}},
""highlight"":{""fields"":{""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadYoutubeDataQuery3 = @"{
""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""title"", ""description"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},
""highlight"":{""fields"":{""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadYoutubeDataQuery4 = @"{
                    ""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""title"", ""description""],
                                ""default_operator"": ""and""
                            }
                          },
                    ""highlight"":{""fields"":{""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";

            public const string ReadWhatsappDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadWhatsappDataQuery2 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""user"", ""messages"" ],""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{""user"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""messages"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadWhatsappDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""user"", ""messages"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{""user"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""messages"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string ReadWhatsappDataQuery4 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""user"", ""messages""],
                                ""default_operator"": ""and""
                            }
                          },
						  ""highlight"":{""fields"":{""user"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""messages"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";


            public const string ReadTwitterAdvanceDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadTwitterAdvanceDataQuery2 = @"{""match"":{""text"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadTwitterAdvanceDataQuery3 = @"{""match"":{""text"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadTwitterAdvanceDataQuery4 = @"{""query"":{""bool"":{""should"":[#SEARCHWHERE#]}},""highlight"":{""fields"":{""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";



            public const string ReadFacebookAdvanceDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadFacebookAdvanceDataQuery2 = @"{""match"":{""post_message"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadFacebookAdvanceDataQuery3 = @"{""match"":{""post_message"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadFacebookAdvanceDataQuery4 = @"{""query"":{""bool"":{""should"":[#SEARCHWHERE#]}},""highlight"":{""fields"":{""post_message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";



            public const string ReadInstagramAdvanceDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadInstagramAdvanceDataQuery2 = @"{""match"":{""image_links"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadInstagramAdvanceDataQuery3 = @"{""match"":{""image_links"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadInstagramAdvanceDataQuery4 = @"{""query"":{""bool"":{""should"":[#SEARCHWHERE#]}},""highlight"":{""fields"":{""image_links"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";


            public const string ReadYoutubeAdvanceDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadYoutubeAdvanceDataQuery2 = @"{""match"":{""description"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadYoutubeAdvanceDataQuery3 = @"{""match"":{""description"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}},";
            public const string ReadYoutubeAdvanceDataQuery4 = @"{""query"":{""bool"":{""should"":[#SEARCHWHERE#]}},""highlight"":{""fields"":{""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";

            public const string ReadWhatsappAdvanceDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadWhatsappAdvanceDataQuery2 = @"{""match"":{""messages"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadWhatsappAdvanceDataQuery3 = @"{""match"":{""messages"": {""query"": ""#SEARCHWHERE#"",""operator"": ""#OPERATORWHERE#""}}}";
            public const string ReadWhatsappAdvanceDataQuery4 = @"{""query"":{""bool"":{""should"":[#SEARCHWHERE#]}},""highlight"":{""fields"":{""messages"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";

            public const string ReadDial100DataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadDial100DataQuery2 = @"{""size"":1000 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{
                                ""caller_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""caller_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""event_remark"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_type"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_subType"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""district_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""police_Station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""Frv_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""disposition_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                                }}}";

            public const string ReadDial100DataQuery3 = @"{""size"":1000 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{
                                ""caller_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""caller_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""event_remark"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_type"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_subType"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""district_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""police_Station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""Frv_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""disposition_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                                }}}";
            public const string ReadDial100DataQuery4 = @"{""size"":1000 ,""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
                                ""default_operator"": ""and""
                            }
                          },
                                ""highlight"":{""fields"":{
                                ""caller_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""caller_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""event_remark"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_type"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_subType"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""district_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""police_Station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""Frv_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}, 
                                ""disposition_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                                }}}";
            public const string ReadDial100DataQuery5 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""range"": {""event_time"": {""gte"":""#STARTDATE#"",
                        ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";
            public const string ReadDial100DataQuery6 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""event_time"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}},""highlight"":{""fields"":{
                                ""caller_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""caller_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""event_remark"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_type"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_subType"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""district_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""police_Station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""Frv_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""disposition_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}

                            }}}";
            public const string ReadDial100DataQuery7 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""event_time"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }
                                          }}]}},""highlight"":{""fields"":{
                                ""caller_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""caller_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""event_remark"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_type"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_subType"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""district_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""police_Station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""Frv_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""disposition_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}

                                }}}";
            public const string ReadDial100DataQuery8 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""simple_query_string"":{""query"":""#SEARCHWHERE#"",""default_operator"": ""and""}},{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"",    ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                                ""caller_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""caller_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""event_remark"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_type"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""event_subType"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""district_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""police_Station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""Frv_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                                
                                ""disposition_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}

                                }}}";
            public const string ReadCCTVCamera1DataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadCCTVCamera1DataQuery2 = @"{""size"":100 ,""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
, ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"" ],""query"":""#SEARCHWHERE#""}}],{""range"": {""@timestamp"": {
                            ""gte"":""07/07/2021"",   
                            ""lte"":""08/07/2021"",
                            ""format"": ""yyyy-MM-dd HH:mm:ss.fffffffK""
            }
                    }
                }]}
        },""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make "":{ ""pre_tags"" : ["" < em > ""], ""post_tags"" : ["" </ em > ""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCCTVCamera1DataQuery3 = @"{""size"":100 ,""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
, ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"" ],""query"":""#SEARCHWHERE#""}}]}},""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCCTVCamera1DataQuery4 = @"{""size"":100 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
, ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCCTVCamera1DataQuery5 = @"{""size"":100 ,""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
, ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"" ],
                                ""default_operator"": ""and""
                            }
                          },
						  
""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";


            public const string ReadCCTVCamera2DataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadCCTVCamera2DataQuery2 = @"{""size"":100 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
, ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"" ],""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCCTVCamera2DataQuery3 = @"{""size"":100 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
, ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCCTVCamera2DataQuery4 = @"{""size"":100 ,""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
, ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"" ],
                                ""default_operator"": ""and""
                            }
                          },
						  
""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";

            public const string readdial100byeventtype = @"{""size"":1000 ,""query"":{ ""bool"":{ ""must"":{ ""multi_match"":{ ""fields"":[ ""event_type"",""district_Code"",""event_subType"" ],""query"":""#SEARCHWHERE#""}}}}}";
            public const string GetFilteredDial100Data = @"{""size"":1000 ,""query"":{ ""simple_query_string"":{""fields"":[ ""event_type"",""district_Code"",""event_subType"",""police_Station"" ],""query"":""#SEARCHWHERE#"",""default_operator"": ""and""}}}";
            public const string GetTwitterTrendingResultsByLocation = @"{ ""size"": 10000, ""query"":{ ""bool"":{ ""must"":{ ""multi_match"":{ ""fields"":[ ""location"" ],""query"":""#SEARCHWHERE#""}}}},""sort"": [{""created_date"": {""order"": ""desc""}}]}";
            public const string GetYoutubeTrendingResultsByLocation = @"{ ""size"": 10000, ""query"":{ ""bool"":{ ""must"":{ ""multi_match"":{ ""fields"":[ ""location"" ],""query"":""#SEARCHWHERE#""}}}},""sort"": [{""publishedAt"": {""order"": ""desc""}}]}";
            public const string GetFriendsListByParentId = @"{""query"": {""simple_query_string"" : {""query"": ""#SEARCHWHERE#"",""fields"": [""parent_id""],""default_operator"": ""and""}}}";
            public const string GetFBUserById = @"{ ""size"": 1, ""query"":{ ""bool"":{ ""must"":{ ""multi_match"":{ ""fields"":[ ""profile_url"" ],""query"":""#SEARCHWHERE#""}}}}}";
            public const string ReadCamera1DataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadIipAlertList = @"{""size"":10000 ,""query"": {""match_all"": { } },""sort"": [{""alert_date"": {""order"": ""desc""}}] }";
            public const string ReadIipAlertActionList = @"{""query"": {""simple_query_string"" : {""query"": ""#SEARCHWHERE#"",""fields"": [""parentId""],""default_operator"": ""and""}}}";
            public const string ReadTwitterTrendingDataQuery = @"{""size"":10000 ,""query"": {""match_all"": { } },""sort"": [{""created_date"": {""order"": ""desc""}}] }";
            public const string ReadYoutubeTrendingDataQuery = @"{""size"":10000 ,""query"": {""match_all"": { } },""sort"": [{""publishedAt"": {""order"": ""desc""}}] }";
            public const string ReadCctvCameraDataQuery2 = @"{""size"":1000 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""cameraName"", ""city"", ""location"", ""policeStation"", ""longitude"", ""latitude""
                                , ""ipAddress"", ""switchHostName"", ""rtspLink"", ""typeOfCamera"", ""make"" ],""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{
                                ""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""city"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""switchHostName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                               
                                }}}";
            public const string ReadCctvCameraDataQuery3 = @"{""size"":1000 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""cameraName"", ""city"", ""location"", ""policeStation"", ""longitude"", ""latitude""
                                , ""ipAddress"", ""switchHostName"", ""rtspLink"", ""typeOfCamera"", ""make"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{
                                ""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""city"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""switchHostName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                                }}}";
            public const string ReadCctvCameraDataQuery4 = @"{""size"":1000 ,""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [ ""cameraName"", ""city"", ""location"", ""policeStation"", ""longitude"", ""latitude""
                                , ""ipAddress"", ""switchHostName"", ""rtspLink"", ""typeOfCamera"", ""make"" ],
                                ""default_operator"": ""and""
                            }
                          },
                                ""highlight"":{""fields"":{
                                ""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""city"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""switchHostName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                                }}}";

            public const string ReadCctnsDataQuery1 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""range"": {""#FILTERCOLUMN#"": {""gte"":""#STARTDATE#"",
                        ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";
            public const string ReadCctnsDataQuery2 = @"{""size"":1000 ,""query"":{""bool"":{""must"":{""multi_match"":{""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{
""jsonString"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCctnsDataQuery3 = @"{""size"":1000 ,""query"":{""bool"":{""must"":{""multi_match"":{""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{
""jsonString"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCctnsDataQuery4 = @"{""size"":1000 ,""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",                                
                                ""default_operator"": ""and""
                            }
                          },""highlight"":{""fields"":{
""jsonString"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCctnsDataQuery5 = @"{""size"":1000 ,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""jsonString"" ],""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{
""jsonString"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCctnsDataQuery6 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""multi_match"":{""query"":""#SEARCHWHERE#""}},{""range"": { ""#FILTERCOLUMN#"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
""jsonString"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCctnsDataQuery7 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""multi_match"":{""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}},{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"",    ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
""jsonString"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string ReadCctnsDataQuery8 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""simple_query_string"":{""query"":""#SEARCHWHERE#"",""default_operator"": ""and""}},{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"",    ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
""jsonString"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";




            public const string GetSocialMediaChartDataQuery1 = @"{""query"": {
                        ""simple_query_string"" : {
                            ""query"": ""#SEARCHWHERE#"",
                            ""fields"": [""post_message"", ""pagename""],
                            ""default_operator"": ""and""
                        }
                        },
                    ""highlight"":{""fields"":{""pagename"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""post_message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string GetSocialMediaChartDataQuery2 = @"
                        {""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""hashtags"", ""text"", ""location""],
                                ""default_operator"": ""and""
                            }
                          },
                    ""highlight"":{""fields"":{""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string GetSocialMediaChartDataQuery3 = @"{
                        ""query"": {
                                ""simple_query_string"" : {
                                    ""query"": ""#SEARCHWHERE#"",
                                    ""fields"": [""title"", ""description""],
                                    ""default_operator"": ""and""
                                }
                                },
                        ""highlight"":{""fields"":{""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";
            public const string GetSocialMediaChartDataQuery4 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""user"", ""messages""],
                                ""default_operator"": ""and""
                            }
                          },
						  ""highlight"":{""fields"":{""user"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},""messages"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}}}}";


            //public const string ReadTimesOfIndiaNewsFeedDataQuery1 = @"{""size"": 100,""query"": {""match_all"": { } },""sort"": [{""published"": {""order"": ""desc""}}] }";
            public const string ReadTimesOfIndiaNewsFeedDataQuery1 = @"{""size"": 1000,""query"": {""match_all"": { } } }";
            public const string ReadTimesOfIndiaNewsFeedDataQuery2 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""range"": {""published"": {""gte"":""#STARTDATE#"",
                        ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";
            public const string ReadTimesOfIndiaNewsFeedDataQuery3 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""link"" ],
""query"":""#SEARCHWHERE#""}}}},""highlight"":{""fields"":{
""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}

}}}";
            public const string ReadTimesOfIndiaNewsFeedDataQuery4 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""message"", ""title"",""link"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""published"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}},""highlight"":{""fields"":{
                            ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                            ""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                            ""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}

                            }}}";
            public const string ReadTimesOfIndiaNewsFeedDataQuery5 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""link"" ],
""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}},""highlight"":{""fields"":{
""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}

}}}";

            public const string ReadTimesOfIndiaNewsFeedDataQuery6 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""message"", ""title"",""link"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""published"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }
                                          }}]}},""highlight"":{""fields"":{
                                ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                                ""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}

                                }}}";
            public const string ReadTimesOfIndiaNewsFeedDataQuery7 = @"{""size"": 1000,""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""message"", ""title"",""link"" ],
                                ""default_operator"": ""and""
                            }
                          },
						  
""highlight"":{""fields"":{
""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";

            public const string LocationDashboardDataQuery1 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""pagename"", ""post_message"",
                        ""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",
                        ""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",
                        ""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
                        , ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",
                        ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""  ],
                        ""query"":""#SEARCHWHERE#""}}}}}";
            public const string LocationDashboardDataQuery2 = @"{}
{""query"":{""multi_match"":{""fields"":[""message"", ""title"",""pagename"", ""post_message"",""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""],""query"":""#SEARCHWHERE#""}}}
";


            public const string PoliceStationMapDataQuery1 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""pagename"", ""post_message"",
                        ""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",
                        ""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",
                        ""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
                        , ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",
                        ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""  ],
                        ""query"":""#SEARCHWHERE#""}}}}}";


            public const string UpdateLocationCountUsingHangfireDataQuery1 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""pagename"", ""post_message"",
                        ""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",
                        ""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",
                        ""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
                        , ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",
                        ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""  ],
                        ""query"":""#SEARCHWHERE#""}}}}}";




            public const string HomeDataQuery1 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""pagename"", ""post_message"",
                        ""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",
                        ""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",
                        ""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
                        , ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",
                        ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""  ],
                        ""query"":""#SEARCHWHERE#""}}}}}";




            public const string CreateNotificationForSocialMediaUsingHangfireQuery1 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""pagename"", ""post_message"",
                        ""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",
                        ""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",
                        ""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
                        , ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",
                        ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""  ],
                        ""query"":""#SEARCHWHERE#""}},""filter"":{""terms"":{""is_notified"":[false]}}}},
""highlight"":{""fields"":{
""srNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""cameraName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""locationName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""policeStation"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""longitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""latitude"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ipAddress"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""rtspLink"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""typeOfCamera"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""make"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""unityp"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""unit_status"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""unique_id"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""track_personnel"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""tycod"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""sub_tycod"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""eid"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""dgroup"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""ag_id"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""user"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""messages"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""caption"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""pagename"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""post_message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
""location"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
}}}";
            public const string CreateNotificationForSocialMediaUsingHangfireQuery2 = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""pagename"", ""post_message"",
                                ""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",
                                ""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",
                                ""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude""
                                , ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",
                                ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""  ],
                                ""query"":""#SEARCHWHERE#""}},""filter"":{""terms"":{""is_notified"":[false]}}}},""script"": { ""source"": ""ctx._source['is_notified'] =true""}}";



            public const string ShowAlertQuery1 = @"{}
{""query"":{""multi_match"":{""fields"":[""message"", ""title"",""pagename"", ""post_message"",""title"", ""description"",""user"", ""messages"",""hashtags"", ""text"", ""location"",""unityp"", ""unit_status"", ""unique_id"",""track_personnel"", ""tycod"", ""sub_tycod"", ""station"", ""latitude"", ""location"", ""longitude"", ""eid"", ""dgroup"",""ag_id"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"", ""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""srNo"", ""cameraName"", ""locationName"", ""policeStation"",""longitude"", ""latitude"", ""ipAddress"", ""rtspLink"", ""typeOfCamera"", ""make"",""hashtags"", ""caption""],""query"":""#SEARCHWHERE#""}}}
";

            public const string ManageElasticDbQuery1 = @"{""hashtags"" : ""#HASHTAGS#"" ,""created_at"" :""#DATE#"" ,""text"" : ""#TEXT#"",""location"" : ""#LOCATION#"",""is_notified"":false,""is_alerted"":false}";

            public const string MaxDateQuery = @"{""size"": 0,""aggs"": {""max_date"":{""max"":{""field"": ""#FILTERCOLUMN#""}}}}";
            public const string ReadDail100QueryWithSearch = @"{""query"": {""simple_query_string"" : {""query"": ""#SEARCHWHERE#"",""fields"": [""event_number""],""default_operator"": ""and""}}}";
            public const string ReadIIPAlertDataQueryWithSearch = @"{""query"": {""simple_query_string"" : {""query"": true,""fields"": [""isVisible""],""default_operator"": ""and""}}}";
            public const string ReadRoipDataQuery2 = @"{""size"": 1,""query"":{""bool"":{""must"":[{""range"": {""date_value"": {""gte"":""#STARTDATE#"",
                        ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd HH:mm:ss""}}}]}}}";
            public const string ReadRoipDataQuery1 = @"{""size"": 10000,""query"":{""bool"":{""must"":[{""range"": {""date_value"": {""gte"":""#STARTDATE#"",
                        ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd HH:mm:ss""}}},{""range"": {""start_datetime"": {""gte"":""#STARTTIME#"",
                        ""lte"":""#ENDTIME#"",""format"": ""yyyy-MM-dd HH:mm:ss""}}}]}}}";
            public const string ReadRoipDataQuery3 = @"{""size"": 10000,""query"":{""bool"":{""must"":[{""match"":{""channel_no"": {""query"": ""#CHANELNO#""}}},{""range"": {""date_value"": {""gte"":""#STARTDATE#"",
                        ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd HH:mm:ss""}}},{""range"": {""start_datetime"": {""gte"":""#STARTTIME#"",
                        ""lte"":""#ENDTIME#"",""format"": ""yyyy-MM-dd HH:mm:ss""}}}]}}}";

            public const string ReadDial100EventQuery = @"{""size"": 10000,""query"":{""bool"":{""must"":[{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"",    ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd HH:mm:ss""}}}]}}}";

            public const string ReadYoutubeCountDataQuery1 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""description"", ""title"",""channelTitle"" ],
""query"":""#SEARCHWHERE#""}}}}}";

            public const string ReadYoutubeCountDataQuery2 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""description"", ""title"",""channelTitle"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""publishTime"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}}}";

            public const string ReadYoutubeCountDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[""description"", ""title"",""channelTitle"" ],
""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}}}";

            public const string ReadYoutubeCountDataQuery4 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""description"", ""title"",""channelTitle"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""publishTime"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }}}]}}}";

            public const string ReadYoutubeCountDataQuery5 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""description"", ""title"",""channelTitle"" ],
                                ""default_operator"": ""and""
                            }}}";

            public const string ReadTwitterCountDataQuery1 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""text"", ""source"",""keyword"",""type"" ],
""query"":""#SEARCHWHERE#""}}}}}";

            public const string ReadTwitterCountDataQuery2 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""text"", ""source"",""keyword"",""type"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""created_at"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}}}";

            public const string ReadTwitterCountDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[""text"", ""source"",""keyword"",""type"" ],
""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}}}";

            public const string ReadTwitterCountDataQuery4 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""text"", ""source"",""keyword"",""type"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""created_at"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }}}]}}}";

            public const string ReadTwitterCountDataQuery5 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""text"", ""source"",""keyword"",""type"" ],
                                ""default_operator"": ""and""
                            }}}";

            public const string ReadRssFeedCountDataQuery1 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""message"", ""title"",""link"" ],
""query"":""#SEARCHWHERE#""}}}}}";

            public const string ReadRssFeedCountDataQuery2 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""message"", ""title"",""link"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""published"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}}}";

            public const string ReadRssFeedCountDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[""message"", ""title"",""link"" ],
""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}}}";

            public const string ReadRssFeedCountDataQuery4 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[""message"", ""title"",""link"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""published"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }}}]}}}";

            public const string ReadRssFeedCountDataQuery5 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""message"", ""title"",""link"" ],
                                ""default_operator"": ""and""
                            }}}";

            public const string ReadDial100CountDataQuery1 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
""query"":""#SEARCHWHERE#""}}}}}";

            public const string ReadDial100CountDataQuery2 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""event_time"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}}}";

            public const string ReadDial100CountDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}}}";

            public const string ReadDial100CountDataQuery4 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""event_time"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }}}]}}}";

            public const string ReadDial100CountDataQuery5 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type""
                                , ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],
                                ""default_operator"": ""and""
                            }}}";

            public const string ReadDial100CountDataQuery6 = @"{""query"":{
                                ""bool"":{""must"":[{""simple_query_string"":{""query"":""#SEARCHWHERE#"",""default_operator"": ""and""}},
                                {""range"": { ""event_time"": { ""gte"":""#STARTDATE#"",    ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";

            public const string ReadFacebookCountDataQuery1 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""post_text"", ""username"",""post_link"" ],
""query"":""#SEARCHWHERE#""}}}}}";

            public const string ReadFacebookCountDataQuery2 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""post_text"", ""username"",""post_link"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""publishTime"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}}}";

            public const string ReadFacebookCountDataQuery3 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[""post_text"", ""username"",""post_link"" ],
""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}}}";

            public const string ReadFacebookCountDataQuery4 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""post_text"", ""username"",""post_link"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""publishTime"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }}}]}}}";

            public const string ReadFacebookCountDataQuery5 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",
                                ""fields"": [""post_text"", ""username"",""post_link"" ],
                                ""default_operator"": ""and""
                            }}}";

            public const string ReadCctnsCountDataQuery1 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""query"":""#SEARCHWHERE#""}}}}}";
            public const string ReadCctnsCountDataQuery2 = @"{""query"":{""bool"":{""must"":{""multi_match"":{""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}}}}";
            public const string ReadCctnsCountDataQuery3 = @"{""query"": {
                            ""simple_query_string"" : {
                                ""query"": ""#SEARCHWHERE#"",                                
                                ""default_operator"": ""and""
                            }
                          }}";
            public const string ReadCctnsCountDataQuery4 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""jsonString"" ],
                            ""query"":""#SEARCHWHERE#""}}, {""range"": { ""reportDate"": {""gte"":""#STARTDATE#"",   ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}
                                      }}}]}}}";
            public const string ReadCctnsCountDataQuery5 = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[""jsonString"" ],
                                ""query"":""#SEARCHWHERE#""}},{""range"": {
                                            ""reportDate"": {
                                            ""gte"":""#STARTDATE#"",   
                                            ""lte"":""#ENDDATE#"",
                                            ""format"": ""yyyy-MM-dd'T'HH:mm:ss""
                                            }}}]}}}";
            public const string ReadCctnsCountDataQuery6 = @"{""query"":{
                                ""bool"":{""must"":[{""simple_query_string"":{""query"":""#SEARCHWHERE#"",""default_operator"": ""and""}},
                                {""range"": { ""reportDate"": { ""gte"":""#STARTDATE#"",    ""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";

            public const string ReadDial100DataByFilters = @" {""size"": 1000,""query"":{""bool"":{""must"":[{""multi_match"":{""fields"":[ ""caller_name"", ""caller_number"", ""event_number"", ""event_remark"", ""event_type"", ""event_subType"", ""latitude"", ""longitude"", ""district_Code"", ""police_Station"", ""Frv_Code"",""disposition_Code"" ],							
                                                                ""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}},{""range"": { ""event_time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}
                                                            ";
            public const string ReadCCTNSDataByFilters = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}},{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";
            public const string CctnsDateRangeQuery = @"{""size"": 10000,""query"":{""bool"":{""must"":[{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"",""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""sort"": [{""#FILTERCOLUMN#"": {""order"": ""asc""}}]}";
            public const string ReadCctnsReportQuery = @"{""size"": 10000,""query"":{""bool"":{""must"":[{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"",""lte"":""#ENDDATE#"",""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";
            public const string CctnsBulkQuery = @"{""size"": 10000,""query"": {""match_all"": { } },""sort"": [{""#FILTERCOLUMN#"": {""order"": ""asc""}}] }";
            public const string CctnsMaxDateQuery = @"{""size"": 0,""aggs"": {""max_date"":{""max"":{""field"": ""reportDate""}}},""query"": {""simple_query_string"" : {""query"": ""#INDEXNAME#"",""fields"": [""indexName""],""default_operator"": ""and""}}}";
            public const string RoipDistinctTables = @"{""size"":0,""aggs"": {""distinct_date_value"": {""terms"": {""field"": ""date_value"",""size"": 100000}}}}";
            public const string ReadVdpDataQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""simple_query_string"" : {""query"": ""#DISTRICTNAME#"",""fields"": [""districtName""],""default_operator"": ""and""}}]}}}";
            public const string ReadVdpDataQuery1 = @"{""size"":1000 ,""query"": {""match_all"": { } } }";
            public const string ReadDial100ForecastDataQuery = @"{""size"":1 ,""query"": {""match_all"": { } } ,""sort"": [{""created_at"": {""order"": ""desc""}}]}";
            public const string ReadDial100DataByIdsQuery = @"{""size"":10000, ""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""terms"" :{""_id"":#IDS#}}]}}}";
            public const string ReadCctnsCommonQuerywithDsitrictSearch = @"{""query"": { ""bool"": {""must"": [{ ""match"": { ""jsonString"":{""query"":""#POLICESTATION#"",""fuzziness"":""AUTO""}}},{ ""match"": { ""district"":""#DISTRICT#""}}]}}}";
            public const string ReadNewsFeedDataQueryWithFilters = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadNewsFeedDataQueryWithFilters2 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}
                ,""highlight"":{""fields"":{
                ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadNewsFeedDataQueryWithFilters3 = @"{""size"": 1000,""query"":{""bool"":{""should"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH# ],""must"":[
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}],""minimum_should_match"" : 1}},""highlight"":{""fields"":{
                ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadNewsFeedDataQueryWithFilters4 = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                #QUERY_SEARCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""message"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadYoutubeDataQueryWithFilters = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                ]}}
                ,""highlight"":{""fields"":{
			    ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""channelTitle"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadYoutubeDataQueryWithFilters2 = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                ]}}
                ,""highlight"":{""fields"":{
			    ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""channelTitle"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadYoutubeDataQueryWithFilters3 = @"{""size"": 1000,""query"":{""bool"":{""should"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH# ],""must"":[
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                ],""minimum_should_match"" : 1}}
                ,""highlight"":{""fields"":{
			    ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""channelTitle"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadYoutubeDataQueryWithFilters4 = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                #QUERY_SEARCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                ]}}
                ,""highlight"":{""fields"":{
			    ""title"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""description"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			    ""channelTitle"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                }}}";
            public const string ReadTwitterDataQueryWithFilters = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadTwitterDataQueryWithFilters2 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadTwitterDataQueryWithFilters3 = @"{""size"": 1000,""query"":{""bool"":{""should"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH# ],""must"":[
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                ],""minimum_should_match"" : 1}}
                ,""highlight"":{""fields"":{
                ""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadTwitterDataQueryWithFilters4 = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                #QUERY_SEARCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""text"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadTwitterDataQueryWithFilters5 = @"{
	                                                                    ""size"": 10000,
	                                                                    ""query"":{
		                                                                    ""bool"":{
			                                                                    ""must"":[
                                                                                    {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}                
			                                                                    ],
			                                                                    ""filter"":[
				                                                                    {
					                                                                    ""bool"":{
						                                                                    ""must"":[
							                                                                    #QUERY_SEARCH#
						                                                                    ]
					                                                                    }
				                                                                    }
			                                                                    ]
		                                                                    }
	                                                                    }
	                                                                    ,""highlight"":{
		                                                                    ""fields"":{
			                                                                    ""text"":{
				                                                                    ""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2
			                                                                    }               
		                                                                    }
	                                                                    }
                                                                    }";
            public const string ReadFacebookDataQueryWithFilters = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""post_msg"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""page_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadFacebookDataQueryWithFilters2 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""post_msg"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""page_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadFacebookDataQueryWithFilters3 = @"{""size"": 1000,""query"":{""bool"":{""should"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH# ],""must"":[
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                ],""minimum_should_match"" : 1}}
                ,""highlight"":{""fields"":{
                ""post_msg"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""page_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadFacebookDataQueryWithFilters4 = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                #QUERY_SEARCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""post_msg"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
                ""page_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}               
                }}}";
            public const string ReadInstagramDataQueryWithFilters = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""url"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}                             
                }}}";
            public const string ReadInstagramDataQueryWithFilters2 = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""url"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}                               
                }}}";
            public const string ReadInstagramDataQueryWithFilters3 = @"{""size"": 1000,""query"":{""bool"":{""should"":[#QUERY_MATCH#
                #QUERY_MULTIMATCH# ],""must"":[
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                ],""minimum_should_match"" : 1}}
                ,""highlight"":{""fields"":{
                ""url"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}                             
                }}}";
            public const string ReadInstagramDataQueryWithFilters4 = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                #QUERY_SEARCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}},""highlight"":{""fields"":{
                ""name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                             
                ""loc_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                             
                ""loc_link"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                             
                ""hashtags"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},                             
                ""caption"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}                             
                }}}";
            public const string RtspStreamQuery = @"{
                                                      ""name"": ""#CAMERANAME#"",
                                                      ""channels"": {
	                                                      ""0"": {
		                                                      ""name"": ""#CAMERANAME#"",
		                                                      ""url"": ""#RTSP_URL#"",
		                                                      ""on_demand"": true,
		                                                      ""debug"": false,
		                                                      ""status"": 0
	                                                      }
                                                      }
                                                    }";
            public const string CutoffDateQuery = @"{""size"": 0,""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""term"" :{""_id"":""#ID#""}}]}},""aggs"": {""max_date"":{""max"":{""field"": ""#FILTERCOLUMN#""}}}}";
            public const string FacebookAndInstaCutoffDateQuery = @"{""size"": 0,""query"": {""bool"" : {""must"": {""match"":{""server_name"": {""query"": ""#SERVERNAME#"",""operator"": ""AND""}}}}},""aggs"": {""max_date"":{""max"":{""field"": ""scrape_datetime""}}}}";
            public const string CctnsCommonQuerywithFilters = @"{""size"": 1,""query"": { ""bool"": {""must"": [{""range"": { ""REG_DT"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}} #JSONSTR# #POLICESTATION#]}}}";
            public const string ReadAlertListByEventSubType = @"{""size"":10000 ,""query"": {""bool"": {""must"": {""match"":{""event_sub_types"": {""query"": ""RAPE"",""operator"": ""AND""}}}}},""sort"": [{""alert_date"": {""order"": ""desc""}}] }";
            public const string Dial100DataByFilters = @"{""size"": 10000,""query"":{""bool"":{""must"":[
                                #QUERY_DISTRICT#
                                #QUERY_POLICESTATION#
                                #QUERY_CALLERNAME#
                                #QUERY_MOBILENO#
                                #QUERY_EVENTNO#
                                #QUERY_EVENTTYPE#
                                #QUERY_EVENTSUBTYPE#
                                {""range"": { ""event_time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""caller_name"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""caller_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""event_number"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""event_type"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""event_subType"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""district_Code"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""police_Station"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
                                }}}";

            public const string ReadCRPFCCTNSDataQueryWithFilters = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                #QUERY_DISTRICT#
                #QUERY_SEARCH#
                {""range"": { ""#FILTERCOLUMN#"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}]}}}";
        }
        public class Document
        {
            //public const string ReadDocumentQuery = @"{""size"": 100,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""noteSubject"", ""noteDescription"",""noteNo"",""fileName"",""fileExtension"",""fileExtractedText"" ],
            //""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}},""filter"":{""terms"":{""_id"":[""#IDS#""]}}}},""highlight"":{""fields"":{
            //""noteSubject"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
            //""noteDescription"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},            
            //""noteNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
            //""fileName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
            //""fileExtension"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
            //""fileExtractedText"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}
            //}}}";
            public const string ReadDocumentQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""noteSubject"",""fileExtractedText"",""udfJson"" ],
            ""query"":""#SEARCHWHERE#"",""operator"": ""AND"",""type"":""cross_fields""}},""filter"":{""terms"":{""_id"":[""#IDS#""]}}}},""highlight"":{""fields"":{
            ""noteSubject"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""fileExtractedText"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2},
            ""udfJson"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}
            }}}";
            public const string ReadGlobalSearchQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":{""multi_match"":{""fields"":[ ""noteSubject"",""fileExtractedText"",""udfJson"" ],
            ""query"":""#SEARCHWHERE#"",""operator"": ""AND"",""type"":""cross_fields""}},""filter"":{""terms"":{""_id"":[""#IDS#""]}}}},""highlight"":{""fields"":{
            ""noteSubject"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""fileExtractedText"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 120, ""number_of_fragments"" : 2},
            ""udfJson"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 120, ""number_of_fragments"" : 2}
            }}}";
            public const string GlobalSearchPhraseQuery = @"{""multi_match"":{""fields"":[ ""noteSubject"",""fileExtractedText"",""udfJson"",""fileName"",""noteNo"" ],
            ""query"":""#SEARCHWHERE#"",""operator"": ""AND"",""type"":""phrase""}}";
            public const string GlobalSearchFuzzyQuery = @"{""multi_match"":{""fields"":[ ""noteSubject"",""fileExtractedText"",""udfJson"",""fileName"",""noteNo"" ],
            ""query"":""#SEARCHWHERE#"",""operator"": ""AND"",""fuzziness"": ""AUTO""}}";
            public const string GlobalSearchCrossFieldQuery = @"{""multi_match"":{""fields"":[ ""noteSubject"",""fileExtractedText"",""udfJson"",""fileName"",""noteNo"" ],
            ""query"":""#SEARCHWHERE#"",""operator"": ""AND"",""type"":""cross_fields""}}";
            public const string ReadGlobalSearchQueryAND = @"{""size"": 1000,""query"":{""bool"":{""#MUST_SHOULD#"":[#SEARCH_QUERIES#
                                       ,#DATE_RANGE#
                                      ]}},""highlight"":{""fields"":{
            ""noteSubject"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""noteNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""fileName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""fileExtractedText"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 500, ""number_of_fragments"" : 2},
            ""udfJson"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 120, ""number_of_fragments"" : 2}
            }}}";
            public const string ReadGlobalSearchQueryOR = @"{""size"": 1000,""query"":{""bool"":{
                                        #DATE_RANGE#
                                        ""#MUST_SHOULD#"":[#SEARCH_QUERIES#],
            ""minimum_should_match"": 1}},""highlight"":{""fields"":{
            ""noteSubject"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""noteNo"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""fileName"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 100, ""number_of_fragments"" : 2}, 
            ""fileExtractedText"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 500, ""number_of_fragments"" : 2},
            ""udfJson"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 120, ""number_of_fragments"" : 2}
            }}}";
            public const string ReadGlobalSearchQueryORHEG = @"{
  ""size"": 1000,
  ""query"": {
    ""bool"": {
      ""must"": [
        {
          ""bool"": {
            #DATE_RANGE#
          }
        },
        {
          ""bool"": {
            ""should"":
              [#SEARCH_QUERIES#],
            ""minimum_should_match"": 1
          }
        }
      ]
    }
  },
  ""highlight"": {
    ""fields"": {
      ""noteSubject"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 100,
        ""number_of_fragments"": 2
      },
      ""noteNo"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 100,
        ""number_of_fragments"": 2
      },
      ""fileName"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 100,
        ""number_of_fragments"": 2
      },
      ""fileExtractedText"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 500,
        ""number_of_fragments"": 2
      },
      ""udfJson"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 120,
        ""number_of_fragments"": 2
      }
    }
  }
}
";

            public const string ReadGlobalSearchQueryByDateHEG = @"{
                                          ""size"": 10000,
                                          ""query"": {
                                            ""bool"": {
                                              ""must"": [
                                                {
                                                  ""bool"": {
                                                    #DATE_RANGE#
                                                  }
                                                }
                                              ]
                                            }
                                          }
                                        }
                                        ";

            public const string ReadGlobalSearchQueryANDHEG = @"{
  ""size"": 1000,
  ""query"": {
    ""bool"": {
      ""must"": [
        {
          ""bool"": {
            #DATE_RANGE#
          }
        },
        {
          ""bool"": {
            ""must"":
              [#SEARCH_QUERIES#]            
          }
        }
      ]
    }
  },
  ""highlight"": {
    ""fields"": {
      ""noteSubject"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 100,
        ""number_of_fragments"": 2
      },
      ""noteNo"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 100,
        ""number_of_fragments"": 2
      },
      ""fileName"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 100,
        ""number_of_fragments"": 2
      },
      ""fileExtractedText"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 500,
        ""number_of_fragments"": 2
      },
      ""udfJson"": {
        ""pre_tags"": [""<em>""],
        ""post_tags"": [""</em>""],
        ""fragment_size"": 120,
        ""number_of_fragments"": 2
      }
    }
  }
}
";
            public const string ReadMetadataTableDMS = @"{""query"":{""bool"":{""must"":[{""multi_match"":{""query"":""#SEARCHWHERE#"",""fuzziness"":""AUTO""}}],""filter"":{""terms"":{""_id"":[""#IDS#""]}}}}}";
            public const string GetFilesWitoutExtractedText = @"{""size"": 20,
	                                                                ""query"": {
		                                                                ""bool"": {
			                                                                ""must"": [
				                                                                {""match"": {""fileExtension"": {""query"": "".pdf""}}}
			                                                                ],
			                                                                ""must_not"" : [                     
				                                                                {""exists"": {""field"": ""fileExtractedText""}}        
			                                                                ]    
		                                                                }
	                                                                }
                                                                }";
            public const string GetDataWitoutTemplateCode = @"{""size"": 20,
	                                                                ""query"": {
		                                                                ""bool"": {
			                                                                
			                                                                ""must_not"" : [                     
				                                                                {""exists"": {""field"": ""templateCode""}}        
			                                                                ]    
		                                                                }
	                                                                }
                                                                }";
            public const string GetDataWitoutDocumentDate = @"{
                                                                  ""size"": 100,
                                                                  ""query"": {
                                                                    ""bool"": {
                                                                      ""must"": [
                                                                        {
                                                                          ""bool"": {
                                                                            ""should"": [
                                                                              {
                                                                                ""term"": {
                                                                                  ""isDocumentDateUpdated"": false
                                                                                }
                                                                              },
                                                                              {
                                                                                ""bool"": {
                                                                                  ""must_not"": {
                                                                                    ""exists"": {
                                                                                      ""field"": ""isDocumentDateUpdated""
                                                                                    }
                                                                                  }
                                                                                }
                                                                              }
                                                                            ],
                                                                            ""minimum_should_match"": 1
                                                                          }
                                                                        },
                                                                        {
                                                                          ""bool"": {
                                                                            ""must_not"": {
                                                                              ""exists"": {
                                                                                ""field"": ""documentDate""
                                                                              }
                                                                            }
                                                                          }
                                                                        }
                                                                      ]
                                                                    }
                                                                  },
                                                                  ""sort"": [
                                                                    {
                                                                      ""lastUpdatedDate"": {
                                                                        ""order"": ""desc""
                                                                      }
                                                                    }
                                                                  ]
                                                                }
                                                                ";
            public const string GetDocSummaryById = @"{
                                                      ""_source"": [""summary""],
                                                      ""query"": {
                                                        ""term"": {
                                                          ""_id"": ""#ID#""
                                                        }
                                                      }
                                                    }
                                                    ";

            public const string ReadGlobalSearchSummaryQueryOR = @"{
                                                                  ""size"": 10,
                                                                  ""query"": {
                                                                    ""bool"": {
                                                                      ""must"": [
                                                                        {
                                                                          ""exists"": {
                                                                            ""field"": ""summary""
                                                                          }
                                                                        }
                                                                      ],
                                                                      ""#MUST_SHOULD#"": [#SEARCH_QUERIES#],
                                                                      ""minimum_should_match"": 1
                                                                    }
                                                                  },
                                                                  ""highlight"": {
                                                                    ""fields"": {
                                                                      ""noteSubject"": {
                                                                        ""pre_tags"": [""<em>""],
                                                                        ""post_tags"": [""</em>""],
                                                                        ""fragment_size"": 100,
                                                                        ""number_of_fragments"": 2
                                                                      },
                                                                      ""noteNo"": {
                                                                        ""pre_tags"": [""<em>""],
                                                                        ""post_tags"": [""</em>""],
                                                                        ""fragment_size"": 100,
                                                                        ""number_of_fragments"": 2
                                                                      },
                                                                      ""fileName"": {
                                                                        ""pre_tags"": [""<em>""],
                                                                        ""post_tags"": [""</em>""],
                                                                        ""fragment_size"": 100,
                                                                        ""number_of_fragments"": 2
                                                                      },
                                                                      ""fileExtractedText"": {
                                                                        ""pre_tags"": [""<em>""],
                                                                        ""post_tags"": [""</em>""],
                                                                        ""fragment_size"": 500,
                                                                        ""number_of_fragments"": 2
                                                                      },
                                                                      ""udfJson"": {
                                                                        ""pre_tags"": [""<em>""],
                                                                        ""post_tags"": [""</em>""],
                                                                        ""fragment_size"": 120,
                                                                        ""number_of_fragments"": 2
                                                                      }
                                                                    }
                                                                  }
                                                                }";

            public const string ReadDocWithSummary = @"{
                                                          ""size"": 10,
                                                          ""query"": {
                                                            ""bool"": {
                                                              ""must"": [
                                                                {
                                                                  ""exists"": {
                                                                    ""field"": ""summary""
                                                                  }
                                                                }
                                                              ]
                                                            }
                                                          },
                                                          ""sort"": [
                                                            {
                                                              ""createdDate"": {
                                                                ""order"": ""desc""
                                                              }
                                                            }
                                                          ]
                                                        }";
        }

        public class IIP
        {
            public const string ReadCctnsFirListContent = @"{""District"" : #DISTRICT# ,""PS"" :  ""#PS#"",""FIRNo"" :""#FIRNO#"",""Accused_MobileNo"" : #ACCUSED_MOBILENO#,""Accused_Name"" : ""#ACCUSED_NAME#"", ""Accused_FatherName"" :""#ACCUSED_FATHERNAME#"",""Accused_Gender"" :#ACCUSED_GENDER#,""Accused_Age"" :#ACCUSED_AGE#,""IPC_Section"" :""#IPC_SECTION#"",""Todate"" :""#TODATE#"",""Fromdate"" :""#FROMDATE#""}";

            public const string GetCctnsTokenContent = @"{""UserID"" : ""#CCTNSAPIUSER#"" ,""Password"" :""#Testing@123#""}";


            public const string CCTVContent = @"{""size"":10000, ""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""terms"" :{""_id"":[""#IDS#""]}}]}},""script"": { ""source"": ""ctx._source['isRead'] =true;DateFormat df = new SimpleDateFormat(\""yyyy-MM-dd'T'HH:mm:ss.SSS\"");\ndf.setTimeZone(TimeZone.getTimeZone(\""IST\""));\nDate date = new Date();ctx._source['open_datetime'] =df.format(date);""} }";

            public const string UpdateIsAlertedInElasticDbContent = @"{""query"": {""match_all"": { } },""script"": { ""source"": ""ctx._source['is_alerted'] =true""} }";

            public const string CheckAlertNoExistContent = @"{""size"": 1000,""query"":{""bool"":{""must"":[{""simple_query_string"" : {""query"": ""#ALERTNO#"",""fields"": [""alert_number""],""default_operator"": ""and""}}]}}}";

            public const string UpdateAlertVisibilityContent = @"{""size"":10000, ""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""terms"" :{""_id"":[""#IDS#""]}}]}},""script"": { ""source"": ""ctx._source['isVisible'] =false""} }";

            public const string GetCctnsChargesheetDetailsContent = @"{""FirNo"" :""#FIRNO#"",""REG_YEAR"": ""#YEAR#""}";

            public const string GetCctnsArresstDetailsContent = @"{""FirNo"" :""#FIRNO#"",""REG_YEAR"": ""#YEAR#""}";

            public const string GetCctnsCrimeDetailsContent = @"{""FirNo"" :""#FIRNO#"",""REG_YEAR"": ""#YEAR#""}";

            public const string Dial100PastDataMigrationToElasticDBContent = @"{""fromDate"" : ""#FROM_DATE#"" ,""toDate"" :""#To_DATE#"",""noOfRecords"":50000,""archiveDatabase"":true }";

            public const string GetFIRPDFContent = @"{""FirNo"" :""#FIRNO#""}";

            public const string GetCctnsAccusedDetailsContent = @"{""FirNo"" :""#FIRNO#"",""REG_YEAR"": ""#YEAR#""}";


            public const string ExecuteCubeJsContent2 = @"{""query"": {""range"": {""alert_date"": { ""gte"": ""#YEAR#"",""lte"": ""now"",""format"":""yyyy""}}}}";

            public const string ExecuteCubeJsContent3 = @"{""size"":10000, ""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""terms"" :{""_id"":#IDS#}}]}},""script"": { ""source"": ""ctx._source['isAlerted'] =true""} }";

            public const string ExecuteCubeJsContent4 = @"{""query"": {""range"": {""alert_date"": { ""gte"": ""#YEAR#"",""lte"": ""now"",""format"":""yyyy""}}}}";

            public const string ExecuteCubeJsContent5 = @"{""size"":10000, ""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""terms"" :{""_id"":#IDS#}}]}},""script"": { ""source"": ""ctx._source['isAlerted'] =true""} }";


            public const string GetCommonFBFriendsListContent1 = @"{""filters"": [],""measures"": [""FacebookFriend.count""],""timeDimensions"": [],""order"": {""FacebookFriend.count"": ""desc""},""dimensions"": [""FacebookFriend.friend_profile_url"",""FacebookFriend.friend_name""],""limit"": 10000}";

            public const string GetCommonFBFriendsListContent2 = @"{""filters"": [{""member"": ""FacebookFriend.parent_id"",""operator"": ""equals"",""values"": [""#PARENTID#""]}],""measures"": [""FacebookFriend.count""],""timeDimensions"": [],""order"": {""FacebookFriend.count"": ""desc"" },""dimensions"": [ ""FacebookFriend.friend_profile_url""],""limit"": 10000}";




            public const string ManageAlertRemarksContent1 = @"{""size"":10000, ""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""terms"" :{""_id"":[""#IDS#""]}}]}},""script"": { ""source"": ""ctx._source['alert_status'] ='#ACTION_TAKEN#';ctx._source['isVisible'] =false;ctx._source['isFalseEvent'] =#ISFALSEEVENT#;DateFormat df = new SimpleDateFormat(\""yyyy-MM-dd'T'HH:mm:ss.SSS\"");\ndf.setTimeZone(TimeZone.getTimeZone(\""IST\""));\nDate date = new Date();ctx._source['close_datetime'] =df.format(date);""} }";

            public const string ManageAlertRemarksContent2 = @"{""size"":10000, ""query"": {""bool"" : {""must"": { ""match_all"" : {}},""filter"" : [{ ""terms"" :{""_id"":[""#IDS#""]}}]}},""script"": { ""source"": ""ctx._source['alert_status'] ='#ACTION_TAKEN#'""} }";

            public const string DsiDistrictBarChartByFilters = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_DISTRICTNAME#
                                #QUERY_DISTRICT#
                                #QUERY_INCIDENT#
                                #QUERY_INSTITUTE#
                                #QUERY_INSTITUTE_TYPE#                                            
                                #QUERY_DEMAND#
                                #QUERY_LEADER#
                                #QUERY_DEMANDNAME#
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}}";
            public const string DsiIncidentBarChartByFilters = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_INCIDENTNAME#
                                #QUERY_DISTRICT#
                                #QUERY_INCIDENT#
                                #QUERY_INSTITUTE_TYPE#
                                #QUERY_INSTITUTE#            
                                #QUERY_LEADER#
                                #QUERY_INCIDENT_DES#
                                #QUERY_DEMAND#
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""leadername"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}			                   
                                }}}";
            public const string PredictionDetailsById = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_INCIDENT#],
                                ""should"":[#QUERY_WORDS#]}},
                                ""highlight"": {
                                ""fields"": {
                                  ""incident"": {
                                    ""pre_tags"": [""<em>""],
                                    ""post_tags"": [""</em>""],
                                    ""fragment_size"": 1000,
                                    ""number_of_fragments"": 2
                                  }
                                }
                              }}";
            public const string DsiSeriousEventsDataByClick = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                {""multi_match"":{""fields"":[ ""incident""],""query"":""मृत्यु मौत मृतक""}},  
                                #QUERY_DISTRICTNAME#
                                #QUERY_DISTRICT#
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}		                   
                                }}}";
            public const string DsiDataByDeathClick = @"{""size"": 100000, ""query"": { ""bool"": 
                                { ""must"": [
                               #QUERY_DISTRICTNAME#
                                #QUERY_DISTRICT#                                                         
                                { ""range"": { ""time"": { ""gte"": ""#STARTDATE#"", ""lte"": ""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}},
                                { ""bool"": { ""should"": [ { ""range"": { ""gov_emp_die"": { ""gt"": 0 }}}, { ""range"": { ""police_Total_die"": { ""gt"": 0 }}},
                                        { ""range"": { ""media_dead"": { ""gt"": 0 }}}, { ""range"": { ""people_Total_die"": { ""gt"": 0 }}} ]}}
                                ],
                                ""must_not"": [
                                { ""terms"": { ""incident_type_id"": [
                                        ""10"", ""11"", ""8"", ""17"", ""18"", ""16"", ""2"", ""57"", ""5"", ""3"",
                                        ""7"", ""6"", ""4"", ""14"", ""12"", ""19"", ""9"", ""20"", ""1"", ""13"",
                                        ""169"", ""15"" ]}}
                                ]}}}";
            public const string DsiLeaderDataByClick = @"{""size"": 100000,""query"":{""bool"":{""must"":[
                                #QUERY_DISTRICTNAME#
                                #QUERY_INCIDENTNAME#
                                #QUERY_DISTRICT#
                                #QUERY_INCIDENT#
                                #QUERY_DEMAND#
                                #QUERY_INSTITUTE_TYPE#
                                #QUERY_INSTITUTE#         
                                #QUERY_LEADER#                               
                                {""range"": { ""time"": { ""gte"":""#STARTDATE#"", ""lte"":""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ],""must_not"": [
                                {""terms"": {""incident_type_id"": [""10"",
                                            ""11"",
                                            ""8"",
                                            ""17"",
                                            ""18"",
                                            ""16"",
                                            ""2"",
                                            ""57"",
                                            ""5"",
                                            ""3"",
                                            ""7"",
                                            ""6"",
                                            ""4"",
                                            ""14"",
                                            ""12"",
                                            ""19"",
                                            ""9"",
                                            ""20"",
                                            ""1"",
                                            ""13"",
                                            ""169"",
                                            ""15""]}}
                                ]}}
                                ,""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2},
			                    ""leadername"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}			                   
                                }}}";

            public const string DsiWordCloudDetails = @"{""size"": 100000, ""query"": { ""bool"": 
                                { ""must"": [
                                #QUERY_DISTRICT#
                                #QUERY_INCIDENT#                                                         
                                { ""range"": { ""time"": { ""gte"": ""#STARTDATE#"", ""lte"": ""#ENDDATE#"", ""format"": ""yyyy-MM-dd'T'HH:mm:ss""}}}
                                ]}},""highlight"":{""fields"":{
			                    ""incident"":{""pre_tags"" : [""<em>""], ""post_tags"" : [""</em>""] ,""fragment_size"" : 1000, ""number_of_fragments"" : 2}		                   
                                }}}";
        }

        public class CRPF
        {
            public const string GlobalSearchPhraseQuery = @"{""query_string"":{""fields"":[ ""noteSubject"",""fileExtractedText"",""udfJson"",""fileName"",""noteNo"" ],
            ""query"":""#SEARCHWHERE#""}}";


            public const string CdrDataFilterQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                
                                #QUERY_STRING#  
                   
                        ]}} }  ";

            public const string CdrIntelligenceFilterQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":[
                
                               #QUERY_STRING#
       
                        ]}} }  ";

            public const string LicenceHolderDataFilterQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_STRING#]}}}";


            public const string PassportDataFilterQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_CLAUSES#]}}}";

            public const string VehicleRegistrationDataFilterQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_CLAUSES#]}}}";
            public const string InterrogationDataFilterQuery = @"{""size"": 1000,""query"":{""bool"":{""must"":[#QUERY_CLAUSES#]}}}";

        }
    }
}

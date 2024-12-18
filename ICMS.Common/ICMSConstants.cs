using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Common
{
    public static class ICMSConstants
    {
        public class CareerCodes
        {
            public const string UnderGraduate = "UGRD";
        }

        public const int AdmitTerm = 1910;

        public const int eLearnAdmitTerm = 2100;

        public const int LearningObjectiveToSelect = 3;

        public static readonly Dictionary<string, string> Programs = new Dictionary<string, string>()
        {
            { "AC", "Active" },
            { "SP", "Suspended" }
        };

        public class WordCountValidation
        {
            public const int TA_MIN_WC = 1;
            public const int TA_MAX_WC = 150;
            public const int TA_MIN_WC_POSTA = 100;
            public const int TA_MAX_WC_POSTA = 350;
        }

        public class ProgramCodes
        {
            public const string ProbonoProgramCode = "106";
        }

        public class IsActiveValues
        {
            public const string Active = "A";
            public const string Inactive = "I";
        }

        public class YesNoValues
        {
            public const string Yes = "Y";
            public const string No = "N";
        }

        public class ActivityStageCode
        {
            public const string PRE = "PR";
            public const string MID = "MI";
            public const string POST = "PO";
        }

        public class ActivityReflectionCodes
        {
            public const string PreActivityNotStarted = "PX";
            public const string PreActivityDraft = "PD";
            public const string PreActivitySubmitted = "PS";
            public const string MidActivityNotStarted = "MX";
            public const string MidActivityDraft = "MD";
            public const string MidActivitySubmitted = "MS";
            public const string PostActivityNotStarted = "AX";
            public const string PostActivityDraft = "AD";
            public const string Completed = "AS";
            public const string Optional = "OP";
            public const string NotApplicable = "NA";
        };

        public class XLATKeys
        {
            public const string ActivityStatus = "SIS_R_STATUS";
            public const string OverallGrade = "SIS_EVAL_ASSM";
            public const string OverallStatus = "SIS_GRADE_STATUS";
            public const string ActivityType = "SIS_INTERN_TYPE";
        }

        public static readonly Dictionary<string, string> UnitTypes = new Dictionary<string, string>()
        {
            { "WK", "Weeks"},
            { "HR", "Hours"}
        };

        public class ActivityTypeCodes
        {
            public const string CommunityService = "CS";
            public const string Internship = "BA";
        };

        public class OverallStatusCodes
        {
            public const string ASM = "ASM";
            public const string COM = "COM";
            public const string EPG = "EPG";
            public const string EPR = "EPR";
            public const string GRD = "GRD";
            public const string NCM = "NCM";
            public const string PGF = "PGF";
            public const string SAS = "SAS";
            public const string SLF = "SLF";
        };

        public static readonly Dictionary<string, Dictionary<string, string>> OverallStatusForStudents = new Dictionary<string, Dictionary<string, string>>()
        {
            {
                ActivityTypeCodes.CommunityService, new Dictionary<string, string>()
                {
                    { OverallStatusCodes.ASM, "In Progress"},
                    { OverallStatusCodes.COM, "Completed"},
                    { OverallStatusCodes.EPG, ""},
                    { OverallStatusCodes.EPR, ""},
                    { OverallStatusCodes.GRD, "Grading in progress"},
                    { OverallStatusCodes.NCM, "Not Completed"},
                    { OverallStatusCodes.PGF, "In Progress"},
                    { OverallStatusCodes.SAS, "In Progress"},
                    { OverallStatusCodes.SLF, "In Progress"}
                }
            },
            {
                ActivityTypeCodes.Internship, new Dictionary<string, string>()
                {
                    { OverallStatusCodes.ASM, "In Progress"},
                    { OverallStatusCodes.COM, "Completed"},
                    { OverallStatusCodes.EPG, ""},
                    { OverallStatusCodes.EPR, ""},
                    { OverallStatusCodes.GRD, "Grading in progress"},
                    { OverallStatusCodes.NCM, "Not Completed"},
                    { OverallStatusCodes.PGF, "In Progress"},
                    { OverallStatusCodes.SAS, "In Progress"},
                    { OverallStatusCodes.SLF, "In Progress"}
                }
            }
        };

        public static readonly List<string> AdminActions = new List<string>()
        {
            "getActivityDetails",
            "getActivityQuestions",
            "getPostReflectionHeader",
            "getLearningObjectives",
            "UpdateActivityStatus"
        };
    }
}

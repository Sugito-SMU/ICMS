export const VALIDATION_PARAMS = {
    TA_MIN_WC: 1,
    TA_MAX_WC: 150,
    TA_MIN_WC_POSTA: 100,
    TA_MAX_WC_POSTA: 350
}

export const ActivityTypes = {
    Internship: "BA",
    CommunityService: "CS"
}

export const LocalStorageKeys = {
    EnablePreActivity: "enable_pre_activity",
    EnableMidActivity: "enable_mid_activity",
    EnablePostActivity: "enable_post_activity",
    RefreshToastMessage: "refresh_toast_message",
    RefreshDisplayTab: "refresh_display_tab",
    StudentId: "student_id",
    StudentName: "student_name",
    APIEndPoint: "API_BASE_URL",
    WebsiteEndPoint: "WEBSITE_BASE_URL",
    IsAdmin: "is_admin"
}

export const ActivityReflectionCodes = {
    PreActivityNotStarted: "PX",
    PreActivityDraft: "PD",
    PreActivitySubmitted: "PS",
    MidActivityNotStarted: "MX",
    MidActivityDraft: "MD",
    MidActivitySubmitted: "MS",
    PostActivityNotStarted: "AX",
    PostActivityDraft: "AD",
    Optional: "OP",
    NotApplicable: "NA"
};

export const ActivityStatusTypes = {
    PRE: { Code: "PR", Description: "Pre Activity" },
    MID: { Code: "MI", Description: "Mid Activity" },
    POST: { Code: "PO", Description: "Post Activity" },

    getStatusCodeByDescription(description: string) {
        switch (description) {
            case ActivityStatusTypes.PRE.Description:
                return ActivityStatusTypes.PRE.Code;
            case ActivityStatusTypes.MID.Description:
                return ActivityStatusTypes.MID.Code;
            case ActivityStatusTypes.POST.Description:
                return ActivityStatusTypes.POST.Code;
        }
    },

    getStatusDescriptionByCode(code: string) {
        switch (code) {
            case ActivityStatusTypes.PRE.Code:
                return ActivityStatusTypes.PRE.Description;
            case ActivityStatusTypes.MID.Code:
                return ActivityStatusTypes.MID.Description;
            case ActivityStatusTypes.POST.Code:
                return ActivityStatusTypes.POST.Description;
        }
    }
}

export const SYSTEM_CONSTANTS = {
    API_BASE_URL: localStorage.getItem(LocalStorageKeys.APIEndPoint),
    WEBSITE_BASE_URL: localStorage.getItem(LocalStorageKeys.WebsiteEndPoint),
    WC_REGEX: /[\w\d\’\'-]+/gi,
    AUTOSAVE_TIMER: 30000,
    SHORT_DESC_LENGTH: 1000,
    PRO_BONO_PROG_CODE: '106'
}
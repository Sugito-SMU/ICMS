
export class Activity {
    ActivityId: string;
    Description: string;
    ActivityTypeCode: string;
    ActivityTypeDescription: string;
    ProgrammeDescription: string;
    PositionTitle: string;
    StartDate: string;
    EndDate: string;
    OrganisationDescription: string;
    CountryDescription: string;
    Duration: number;
    UnitRecognised: number;
    OverallGradeDescription: string;
    ReflectionStatusCode: string;
    ReflectionStatusDescription: string;
    Unit: string;
    ShowPreActivity: boolean;
    ShowMidActivity: boolean;
    ShowPostActivity: boolean;
    EnablePreActivity: boolean;
    EnableMidActivity: boolean;
    EnablePostActivity: boolean;
    UnitsRecognised: any[];
    Url: string;
}

export class OverallSummary {
    Description: string;
    Status: string;
    UnitsRequired: number;
    UnitsRecognized: number;
    ProBono: number;
    UnitsRemaining: number;
    Exempted: number;
    ProgrammeCode: string;
}

export class UpdateActivity {
    ActivityId: string;
    LearningObjectives?: PostLearningObjective[];
    ActivityQuestionAnswers?: PostActivityQuestion[];
    Stage: string;
}

export class PostLearningObjective {
    IsSelected?: boolean;
    IsMidActivityOnTarget?: boolean;
    IsObjectiveAchieved?: boolean;
    LearningObjectiveId: string;
    LearningObjectiveQuestions: PostLearningObjectiveQuestion[];
}

export class PostLearningObjectiveQuestion {
    LearningObjectiveQuestionId: string;
    Answer: string;
}

export class PostActivityQuestion {
    ActivityQuestionId: string;
    Answer: string;
}

export class UpdateActivityStatus {
    StudentId: string;
    ActivityId: string;
    Status: string;
}
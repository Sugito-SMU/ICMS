export class LearningObjective {
    LearningObjectiveId: string;
    Description: string;
    ActivityId: string;
    MidActivityNotOnTargetInstruction: string;
    IsSelected: boolean;
    LearningObjectiveQuestions: QuestionAnswer[];
    LearningObjectiveStageQuestionAnswers: QuestionAnswer[];
    disabled: boolean;
    completed: boolean;
    prerefstatus: boolean;
    midrefstatus: boolean;
    postrefstatus: boolean;
    IsMidActivityOnTarget: boolean;
    IsObjectiveAchieved: boolean;
}

export class QuestionAnswer {
    ActivityId: string;
    LearningObjectiveId: string;
    LearningObjectiveQuestionId: string;
    Question: string;
    Answer: string;
}

export class ActivityQuestionAnswer {
    ActivityQuestionId: string;
    ActivityId: string;
    Question: string;
    Answer: string;
    QuestionGuide: string;
    Show: boolean = false;
}

export class SequenceContent {
    Content: string;
    SequenceNo: number;
}
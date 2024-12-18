using ICMS.Common;
using ICMS.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ICMS.Common.ICMSConstants;

namespace ICMS.API.Validation
{
    public class SaveValidation
    {
        public static bool IsValid(ActivityUpdateViewModel model)
        {
            switch (model.Stage)
            {
                case ActivityStageCode.PRE:
                    Func<LearningObjectiveViewModel, bool> isSelected = delegate (LearningObjectiveViewModel lo) { return lo.IsSelected != null && (bool)lo.IsSelected; };
                    return model.LearningObjectives.Count(isSelected) <= LearningObjectiveToSelect
                        && IsValid(model.LearningObjectives.Where(isSelected).ToList(), model.Stage);
                case ActivityStageCode.MID:
                    return IsValid(model.LearningObjectives, model.Stage);
                case ActivityStageCode.POST:
                    return IsValid(model.LearningObjectives, model.Stage) && IsValid(model.ActivityQuestionAnswers);
            }

            return false;
        }

        private static bool IsValid(List<LearningObjectiveViewModel> models, string stage)
        {
            return models.All(lo => (stage == ActivityStageCode.PRE && IsValid(lo.LearningObjectiveQuestions)) || (stage == ActivityStageCode.MID && (IsValid(lo.LearningObjectiveStageQuestionAnswers))) || (stage == ActivityStageCode.POST && (IsValid(lo.LearningObjectiveStageQuestionAnswers))));
        }

        private static bool IsValid(List<LearningObjectiveQuestionAnswerViewModel> models)
        {
            return models.All(qa => IsValid(qa));
        }

        private static bool IsValid(LearningObjectiveQuestionAnswerViewModel model)
        {
            int count = ICMSMethods.GetWordCount(model.Answer);
            return count <= WordCountValidation.TA_MAX_WC;
        }

        private static bool IsValid(List<ActivityQuestionAnswerViewModel> models)
        {
            return models.All(qa => IsValid(qa));
        }

        private static bool IsValid(ActivityQuestionAnswerViewModel model)
        {
            int count = ICMSMethods.GetWordCount(model.Answer);
            return count <= WordCountValidation.TA_MAX_WC_POSTA;
        }
    }
}
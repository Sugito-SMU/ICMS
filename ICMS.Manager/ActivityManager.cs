using ICMS.Data.Infrastructure;
using ICMS.Data.Repositories;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Manager
{
    // operations you want to expose
    public interface IActivityManager
    {
        IEnumerable<Activity> GetActivities(string studentId, string typeCode);
        string GetPostReflectionHeader(string studentId, string activityId);
        Activity GetActivity(string id, string studentId);
        IEnumerable<KeyValueDescription> GetActivityStatusForChangeByAdmin(string currentStatus);
        void SaveActivity();
    }

    public class ActivityManager : IActivityManager
    {
        private readonly IActivityRepository activityRepository;
        private readonly IUnitOfWork unitOfWork;

        public ActivityManager(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
        {
            this.activityRepository = activityRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Activity> GetActivities(string studentId, string typeCode)
        {
            if (string.IsNullOrEmpty(typeCode))
                return null;
            else
                return activityRepository.GetActivities(studentId, typeCode.ToUpper());
        }

        public Activity GetActivity(string id, string studentId)
        {
            return activityRepository.GetActivity(id, studentId);
        }

        public string GetPostReflectionHeader(string studentId, string activityId)
        {
            return activityRepository.GetPostReflectionHeader(studentId, activityId);
        }

        public IEnumerable<KeyValueDescription> GetActivityStatusForChangeByAdmin(string currentStatus)
        {
            List<string> statuses = new List<string>();

            switch (currentStatus)
            {
                case ActivityReflectionCodes.Completed:
                    statuses.Add(ActivityReflectionCodes.PreActivityDraft);
                    statuses.Add(ActivityReflectionCodes.MidActivityDraft);
                    statuses.Add(ActivityReflectionCodes.PostActivityDraft);
                    break;
                case ActivityReflectionCodes.MidActivitySubmitted:
                case ActivityReflectionCodes.PostActivityNotStarted:
                case ActivityReflectionCodes.PostActivityDraft:
                    statuses.Add(ActivityReflectionCodes.PreActivityDraft);
                    statuses.Add(ActivityReflectionCodes.MidActivityDraft);
                    break;
                case ActivityReflectionCodes.PreActivitySubmitted:
                case ActivityReflectionCodes.MidActivityNotStarted:
                case ActivityReflectionCodes.MidActivityDraft:
                    statuses.Add(ActivityReflectionCodes.PreActivityDraft);
                    break;
                case ActivityReflectionCodes.PreActivityNotStarted:
                case ActivityReflectionCodes.PreActivityDraft:
                    break;
            }

            return activityRepository.GetActivityStatusForChangeByAdmin(statuses);
        }

        public void SaveActivity()
        {
            unitOfWork.Commit();
        }
    }
}

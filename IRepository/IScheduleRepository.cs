namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IScheduleRepository
    {
        List<Schedule> GetScheduleList(ref List<string> errors);

        List<Schedule> GetScheduleByCourseId(int course_id, ref List<string> errors);

        List<Schedule> GetScheduleByScheduleId(int schedule_id, ref List<string> errors);

        List<Schedule> GetScheduleByYear(string year, ref List<string> errors);

        List<Schedule> GetScheduleBySession(string sessionId, ref List<string> errors);

        List<Schedule> GetScheduleByScheduleDay(int schedule_day_id, ref List<string> errors);

        List<Schedule> GetScheduleByScheduleTime(int schedule_time_id, ref List<string> errors);

        List<Schedule> GetScheduleByInstructor(int instructor_id, ref List<string> errors);

        List<Schedule> GetScheduleByQuarter(string quarter, ref List<string> errors);

        List<Schedule> GetScheduleDay(ref List<string> errors);

        List<Schedule> GetScheduleTime(ref List<string> errors);

        void InsertSchedule(Schedule schedule, ref List<string> errors);

        void UpdateSchedule(Schedule schedule, ref List<string> errors);

        void DeleteSchedule(int schedule_id, ref List<string> errors);
    }
}

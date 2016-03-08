namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class ScheduleService
    {
        private readonly IScheduleRepository repository;

        public ScheduleService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public List<Schedule> GetScheduleList(ref List<string> errors)
        {         
                return this.repository.GetScheduleList(ref errors);       
        }

        public List<Schedule> GetScheduleDay(ref List<string> errors)
        {           
                return this.repository.GetScheduleDay(ref errors);           
        }

        public List<Schedule> GetScheduleTime(ref List<string> errors)
        {          
                return this.repository.GetScheduleTime(ref errors);           
        }

        public List<Schedule> GetScheduleByCourseId(int course_id, ref List<string> errors)
        {
            if (course_id <= 0)
            {
                errors.Add("course_id cannot be Zero or negative");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleByCourseId(course_id, ref errors);
        }

        public List<Schedule> GetScheduleByScheduleId(int schedule_id, ref List<string> errors)
        {
            if (schedule_id < 0)
            {
                errors.Add("course_id cannot be Zero or negative");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleByScheduleId(schedule_id, ref errors);
        }

        public List<Schedule> GetScheduleByYear(string year, ref List<string> errors)
        {
            if (year.Length < 0)
            {
                errors.Add("year cannot be negative");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleByYear(year, ref errors);
        }

        public List<Schedule> GetScheduleBySessionId(string session_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(session_id))
            {
                errors.Add("session id cannot be empty");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleBySession(session_id, ref errors);
        }

        public List<Schedule> GetScheduleByScheduleDayId(int day_id, ref List<string> errors)
        {
            if (day_id < 0)
            {
                errors.Add("day id cannot be zero or negative");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleByScheduleDay(day_id, ref errors);
        }

        public List<Schedule> GetScheduleByScheduleTimeId(int time_id, ref List<string> errors)
        {
            if (time_id < 0)
            {
                errors.Add("time id cannot be zero or negative");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleByScheduleTime(time_id, ref errors);
        }

        public List<Schedule> GetScheduleByInstructorId(int inst_id, ref List<string> errors)
        {
            if (inst_id < 0)
            {
                errors.Add("instructor id cannot be zero or negative");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleByInstructor(inst_id, ref errors);
        }

        public List<Schedule> GetScheduleByQuarter(string quarter, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(quarter))
            {
                errors.Add("instructor id cannot be zero or negative");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleByQuarter(quarter, ref errors);
        }

        public void InsertSchedule(Schedule schedule, ref List<string> errors)
        {
            if (schedule == null)
            {
                errors.Add("Schedule cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertSchedule(schedule, ref errors);
        }

        public void UpdateSchedule(Schedule schedule, ref List<string> errors)
        {
            if (schedule == null)
            {
                errors.Add("Schedule cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateSchedule(schedule, ref errors);
        }

        public void DeleteSchedule(int schedule_id, ref List<string> errors)
        {
            if (schedule_id < 0)
            {
                errors.Add("schedule id cannot be empty or negative");
                throw new ArgumentException();
            }

            this.repository.DeleteSchedule(schedule_id, ref errors);
        }
    }
}

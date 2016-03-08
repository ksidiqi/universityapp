namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        private const string GetScheduleListProcedure = "spGetScheduleList";
        private const string GetScheduleListByCourseIdProcedure = "spGetScheduleByCourseId";
        private const string GetScheduleListByScheduleIdProcedure = "spGetScheduleByScheduleId";

        private const string GetScheduleListByYearProcedure = "spGetScheduleByYear";
        private const string GetScheduleListBySessionProcedure = "spGetScheduleBySession";
        private const string GetScheduleListByScheduleDayProcedure = "spGetScheduleByScheduleDate";
        private const string GetScheduleListByScheduleTimeProcedure = "spGetScheduleByScheduleTime";
        private const string GetScheduleListByInstructorProcedure = "spGetScheduleByInstructor";
        private const string GetScheduleListByQuarterProcedure = "spGetScheduleByQuarter";
        private const string InsertScheduleProcedure = "spInsertCourseSchedule";
        private const string UpdateScheduleProcedure = "spUpdateCourseSchedule";
        private const string DeleteScheduleProcedure = "spDeleteCourseSchedule";

        private const string GetScheduleDayProcedure = "spGetScheduleDay";

        private const string GetScheduleTimeProcedure = "spGetScheduleTime";

        public List<Schedule> GetScheduleDay(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleDayProcedure, conn);
                
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedule = new Schedule
                    {
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_day = dataSet.Tables[0].Rows[i]["schedule_day"].ToString()                  
                    };
                    scheduleList.Add(schedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleTime(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleTimeProcedure, conn);

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedule = new Schedule
                    {
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],
                        Schedule_time = dataSet.Tables[0].Rows[i]["schedule_time"].ToString()
                    };
                    scheduleList.Add(schedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleList(ref List<string> errors)       
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListProcedure, conn);

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedule = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(), 
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(), 
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(), 
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(), 
                            Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(), 
                            Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(), 
                        },
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                            Fullname = dataSet.Tables[0].Rows[i]["Instructorname"].ToString()
                        },
                        Schedule_day_id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_day_id"].ToString()),
                        Schedule_time_id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_time_id"].ToString()),
                        Schedule_day = dataSet.Tables[0].Rows[i]["schedule_day"].ToString(),
                        
                        Schedule_time = dataSet.Tables[0].Rows[i]["schedule_time"].ToString()
                    };
                    scheduleList.Add(schedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleByCourseId(int course_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByCourseIdProcedure, conn);

                if (course_id > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@course_id"].Value = course_id;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[1].Rows[0]["course_id"].ToString(),
                            Title = dataSet.Tables[1].Rows[0]["course_title"].ToString(),
                            Description = dataSet.Tables[1].Rows[0]["course_description"].ToString(),
                        },
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],

                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),                         
                        },
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleByScheduleId(int schedule_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByScheduleIdProcedure, conn);

                if (schedule_id > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@schedule_id"].Value = schedule_id;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[0]["course_id"].ToString(),     
                        },
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),                           
                        },
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleByYear(string year, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByYearProcedure, conn);

                if (year.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@year"].Value = year;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[0]["course_id"].ToString(),
                            Title = dataSet.Tables[0].Rows[0]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[0]["course_description"].ToString(),
                        },
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        }
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleBySession(string sessionId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByScheduleDayProcedure, conn);

                if (sessionId.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 50));
                    adapter.SelectCommand.Parameters["@session"].Value = sessionId;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[0]["course_id"].ToString(),
                            Title = dataSet.Tables[0].Rows[0]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[0]["course_description"].ToString(),
                        },
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        }
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleByScheduleDay(int schedule_day_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByScheduleDayProcedure, conn);

                if (schedule_day_id > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_date_id", SqlDbType.VarChar, 50));
                    adapter.SelectCommand.Parameters["@schedule_date_id"].Value = schedule_day_id;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[0]["course_id"].ToString(),
                            Title = dataSet.Tables[0].Rows[0]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[0]["course_description"].ToString(),
                        },
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        }
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleByScheduleTime(int schedule_time_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByScheduleTimeProcedure, conn);

                if (schedule_time_id > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.VarChar, 50));
                    adapter.SelectCommand.Parameters["@schedule_time_id"].Value = schedule_time_id;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[0]["course_id"].ToString(),
                            Title = dataSet.Tables[0].Rows[0]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[0]["course_description"].ToString(),
                        },
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        }
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleByInstructor(int instructor_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByInstructorProcedure, conn);

                if (instructor_id > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.VarChar, 50));
                    adapter.SelectCommand.Parameters["@instructor_id"].Value = instructor_id;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[0]["course_id"].ToString(),
                            Title = dataSet.Tables[0].Rows[0]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[0]["course_description"].ToString(),
                        },
                        Schedule_day_id = (int)dataSet.Tables[0].Rows[i]["schedule_day_id"],
                        Schedule_time_id = (int)dataSet.Tables[0].Rows[i]["schedule_time_id"],
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        }
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public List<Schedule> GetScheduleByQuarter(string quarter, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListByQuarterProcedure, conn);

                if (!string.IsNullOrEmpty(quarter))
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));
                    adapter.SelectCommand.Parameters["@quarter"].Value = quarter;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedules = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[0]["course_id"].ToString(),
                            Title = dataSet.Tables[0].Rows[0]["course_title"].ToString(),
                            Description = dataSet.Tables[0].Rows[0]["course_description"].ToString(),
                        },
                        Schedule_day = dataSet.Tables[0].Rows[i]["schedule_day"].ToString(),
                        Schedule_time = dataSet.Tables[0].Rows[i]["schedule_time"].ToString(),
                        Instructor = new Instructor
                        {
                            Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        }
                    };
                    scheduleList.Add(schedules);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }
        
        public void InsertSchedule(Schedule schedule, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertScheduleProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = schedule.Course.CourseId;
                adapter.SelectCommand.Parameters["@year"].Value = schedule.Year;
                adapter.SelectCommand.Parameters["@quarter"].Value = schedule.Quarter;
                adapter.SelectCommand.Parameters["@session"].Value = schedule.Session;
                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = schedule.Schedule_day_id;
                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = schedule.Schedule_time_id;
                adapter.SelectCommand.Parameters["@instructor_id"].Value = schedule.Instructor.Id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void UpdateSchedule(Schedule schedule, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(UpdateScheduleProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = schedule.ScheduleId;
                adapter.SelectCommand.Parameters["@course_id"].Value = schedule.Course.CourseId;
                adapter.SelectCommand.Parameters["@year"].Value = schedule.Year;
                adapter.SelectCommand.Parameters["@quarter"].Value = schedule.Quarter;
                adapter.SelectCommand.Parameters["@session"].Value = schedule.Session;
                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = schedule.Schedule_day_id;
                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = schedule.Schedule_time_id;
                adapter.SelectCommand.Parameters["@instructor_id"].Value = schedule.Instructor.Id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void DeleteSchedule(int schedule_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteScheduleProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
             
                adapter.SelectCommand.Parameters["@schedule_id"].Value = schedule_id;
               
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}

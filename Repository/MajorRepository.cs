namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class MajorRepository : BaseRepository, IMajorRepository
    {
        // const string
        private const string InsertMajorProcedure = "spInsertMajor";
        private const string DeleteMajorProcedure = "spDeleteMajor";
        private const string UpdateMajorProcedure = "spUpdateMajorDescription";
        private const string GetMajorListProcedure = "spGetMajorList";
    
        // new method start from here (assignment #2)
        public void InsertMajor(Major major, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertMajorProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@major_code", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@major_description", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@major_code"].Value = major.MajorCode;
                adapter.SelectCommand.Parameters["@major_description"].Value = major.MajorDescription;

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

        public void DeleteMajor(string major_code, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(DeleteMajorProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@major_code", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@major_code"].Value = major_code;

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

        public void UpdateMajor(Major major, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateMajorProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@major_code", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@major_description", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@major_code"].Value = major.MajorCode;
                adapter.SelectCommand.Parameters["@major_description"].Value = major.MajorDescription;

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

        public List<Major> GetMajorList(ref List<string> errors)
        {
            var majorList = new List<Major>();
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(GetMajorListProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var major = new Major
                                     {
                                         MajorCode = dataSet.Tables[0].Rows[i]["major_code"].ToString(),
                                         MajorDescription = dataSet.Tables[0].Rows[i]["major_description"].ToString(),
                                     };
                    majorList.Add(major);
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

            return majorList;
        }
    }
}

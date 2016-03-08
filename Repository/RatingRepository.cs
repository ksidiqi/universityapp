namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using IRepository;

    using POCO;

    public class RatingRepository : BaseRepository, IRatingRepository
    {
        private const string InsertRatingProcedure = "spInsertRating";
        private const string UpdateRatingProcedure = "spUpdateRating";
        private const string DeleteRatingProcedure = "spDeleteRating";

        public void InsertRating(string ratingCode, string ratingDescription, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertRatingProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rating_rate", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rating_description", SqlDbType.VarChar, 40));

                adapter.SelectCommand.Parameters["@rating_rate"].Value = ratingCode;
                adapter.SelectCommand.Parameters["@rating_description"].Value = ratingDescription;

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

        public void UpdateRating(string ratingCode, string ratingDescription, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(UpdateRatingProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rating_rate", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rating_description", SqlDbType.VarChar, 40));

                adapter.SelectCommand.Parameters["@rating_rate"].Value = ratingCode;
                adapter.SelectCommand.Parameters["@rating_description"].Value = ratingDescription;

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

        public void DeleteRating(string ratingCode, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteRatingProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType
                            =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@rating_rate", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@rating_rate"].Value = ratingCode;

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

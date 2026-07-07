using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GymManagement.Classes;

namespace GymManagement.Database
{
    public class DatabaseHelper
    {
        // change this connection string to match your SQL Server instance
        private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=GymManagementDB;Integrated Security=True";

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // test connection
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // ===================== MEMBER METHODS =====================

        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetAllMembers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Member m = new Member();
                        m.MemberID = Convert.ToInt32(reader["MemberID"]);
                        m.FirstName = reader["FirstName"].ToString();
                        m.LastName = reader["LastName"].ToString();
                        m.DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["DateOfBirth"]);
                        m.Gender = reader["Gender"].ToString();
                        m.PhoneNumber = reader["PhoneNumber"].ToString();
                        m.Address = reader["Address"].ToString();
                        m.TrainingProgram = reader["TrainingProgram"].ToString();
                        m.MembershipStartDate = reader["MembershipStartDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["MembershipStartDate"]);
                        m.MembershipEndDate = reader["MembershipEndDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["MembershipEndDate"]);
                        members.Add(m);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error loading members: " + ex.Message, ex);
            }

            return members;
        }

        public void AddMember(Member m)
        {
            try
            {
                if (!m.IsValid())
                    throw new MemberValidationException("Please fill in all required member fields.");

                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_AddMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName", m.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", m.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", m.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", m.Gender ?? "");
                    cmd.Parameters.AddWithValue("@PhoneNumber", m.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", m.Address ?? "");
                    cmd.Parameters.AddWithValue("@TrainingProgram", m.TrainingProgram ?? "");
                    cmd.Parameters.AddWithValue("@MembershipStartDate", m.MembershipStartDate);
                    cmd.Parameters.AddWithValue("@MembershipEndDate", m.MembershipEndDate);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (MemberValidationException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error adding member: " + ex.Message, ex);
            }
        }

        public void UpdateMember(Member m)
        {
            try
            {
                if (!m.IsValid())
                    throw new MemberValidationException("Member data is not valid.");

                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MemberID", m.MemberID);
                    cmd.Parameters.AddWithValue("@FirstName", m.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", m.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", m.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", m.Gender ?? "");
                    cmd.Parameters.AddWithValue("@PhoneNumber", m.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", m.Address ?? "");
                    cmd.Parameters.AddWithValue("@TrainingProgram", m.TrainingProgram ?? "");
                    cmd.Parameters.AddWithValue("@MembershipStartDate", m.MembershipStartDate);
                    cmd.Parameters.AddWithValue("@MembershipEndDate", m.MembershipEndDate);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (MemberValidationException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error updating member: " + ex.Message, ex);
            }
        }

        public void DeleteMember(int memberID)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_DeleteMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error deleting member: " + ex.Message, ex);
            }
        }

        public List<Member> SearchMembers(string searchTerm)
        {
            List<Member> members = new List<Member>();

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SearchMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Member m = new Member();
                        m.MemberID = Convert.ToInt32(reader["MemberID"]);
                        m.FirstName = reader["FirstName"].ToString();
                        m.LastName = reader["LastName"].ToString();
                        m.DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["DateOfBirth"]);
                        m.Gender = reader["Gender"].ToString();
                        m.PhoneNumber = reader["PhoneNumber"].ToString();
                        m.Address = reader["Address"].ToString();
                        m.TrainingProgram = reader["TrainingProgram"].ToString();
                        m.MembershipStartDate = reader["MembershipStartDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["MembershipStartDate"]);
                        m.MembershipEndDate = reader["MembershipEndDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["MembershipEndDate"]);
                        members.Add(m);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Search failed: " + ex.Message, ex);
            }

            return members;
        }

        public List<Member> GetMembersByInstructor(string instructor)
        {
            List<Member> members = new List<Member>();

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetMembersByInstructor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Instructor", instructor);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Member m = new Member();
                        m.MemberID = Convert.ToInt32(reader["MemberID"]);
                        m.FirstName = reader["FirstName"].ToString();
                        m.LastName = reader["LastName"].ToString();
                        m.Gender = reader["Gender"].ToString();
                        m.PhoneNumber = reader["PhoneNumber"].ToString();
                        m.TrainingProgram = reader["TrainingProgram"].ToString();
                        members.Add(m);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error getting members by instructor: " + ex.Message, ex);
            }

            return members;
        }

        // ===================== CLASS METHODS =====================

        public List<GymClass> GetAllClasses()
        {
            List<GymClass> classes = new List<GymClass>();

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetAllClasses", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GymClass gc = new GymClass();
                        gc.ClassID = Convert.ToInt32(reader["ClassID"]);
                        gc.ClassName = reader["ClassName"].ToString();
                        gc.Description = reader["Description"].ToString();
                        gc.Instructor = reader["Instructor"].ToString();
                        gc.Schedule = reader["Schedule"].ToString();
                        gc.Capacity = reader["Capacity"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Capacity"]);
                        gc.Duration = reader["Duration"].ToString();
                        classes.Add(gc);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error loading classes: " + ex.Message, ex);
            }

            return classes;
        }

        public void AddClass(GymClass gc)
        {
            try
            {
                if (!gc.IsValid())
                    throw new MemberValidationException("Class name and capacity are required.");

                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_AddClass", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClassName", gc.ClassName);
                    cmd.Parameters.AddWithValue("@Description", gc.Description ?? "");
                    cmd.Parameters.AddWithValue("@Instructor", gc.Instructor ?? "");
                    cmd.Parameters.AddWithValue("@Schedule", gc.Schedule ?? "");
                    cmd.Parameters.AddWithValue("@Capacity", gc.Capacity);
                    cmd.Parameters.AddWithValue("@Duration", gc.Duration ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
            catch (MemberValidationException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error adding class: " + ex.Message, ex);
            }
        }

        public void UpdateClass(GymClass gc)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateClass", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClassID", gc.ClassID);
                    cmd.Parameters.AddWithValue("@ClassName", gc.ClassName);
                    cmd.Parameters.AddWithValue("@Description", gc.Description ?? "");
                    cmd.Parameters.AddWithValue("@Instructor", gc.Instructor ?? "");
                    cmd.Parameters.AddWithValue("@Schedule", gc.Schedule ?? "");
                    cmd.Parameters.AddWithValue("@Capacity", gc.Capacity);
                    cmd.Parameters.AddWithValue("@Duration", gc.Duration ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error updating class: " + ex.Message, ex);
            }
        }

        public void DeleteClass(int classID)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_DeleteClass", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassID", classID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error deleting class: " + ex.Message, ex);
            }
        }

        public List<GymClass> SearchClasses(string searchTerm)
        {
            List<GymClass> classes = new List<GymClass>();

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SearchClass", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GymClass gc = new GymClass();
                        gc.ClassID = Convert.ToInt32(reader["ClassID"]);
                        gc.ClassName = reader["ClassName"].ToString();
                        gc.Description = reader["Description"].ToString();
                        gc.Instructor = reader["Instructor"].ToString();
                        gc.Schedule = reader["Schedule"].ToString();
                        gc.Capacity = Convert.ToInt32(reader["Capacity"]);
                        gc.Duration = reader["Duration"].ToString();
                        classes.Add(gc);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Search error: " + ex.Message, ex);
            }

            return classes;
        }
    }
}

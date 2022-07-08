using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using System.Data;
using MySql.Data.MySqlClient;
//IM DOING SOME CHANGES
namespace DAL
{
    public class EmployeDal
    {
        public static string connection = @"server=localhost;database=knowitdb;user=root;password='Maharaja@789'";
        public static List<Employe> GetAll()
        {
            List<Employe> emp = new List<Employe>();

            IDbConnection conn = new MySqlConnection(connection);
            try
            {
                conn.Open();

                string query = "select * from employe";
                IDbCommand cmd = new MySqlCommand(query, conn as MySqlConnection);
                cmd.CommandType = CommandType.Text;

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employe e = new Employe();
                    e.Id = int.Parse(reader["id"].ToString());
                    e.Name = reader["name"].ToString();
                    e.Designation = reader["designation"].ToString();
                    e.salary = double.Parse(reader["salary"].ToString());
                    emp.Add(e);
                }
                reader.Close();

            }
            catch(MySqlException ex)
            {
                string error = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return emp;
        }

        public static Employe GetEleById(int id)
        {
            

            IDbConnection conn = new MySqlConnection(connection);
            Employe emp = new Employe();
            
                try
            {
                conn.Open();
                string query = "select * from employe where id=" + id;
                IDbCommand cmd = new MySqlCommand(query,conn as MySqlConnection);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    emp.Id = int.Parse(reader["id"].ToString());
                    emp.Name = reader["name"].ToString();
                    emp.Designation = reader["designation"].ToString();
                    emp.salary = double.Parse(reader["salary"].ToString());
                }
                reader.Close();
                return emp;

            }
            catch(MySqlException ex)
            {
                string err = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return emp;
        }

        public static bool delete(int id)
        {
            IDbConnection conn = new MySqlConnection(connection);
            bool status = false;
            try
            {
                conn.Open();
                string query = "delete from employe where id =" + id;

                IDbCommand cmd = new MySqlCommand(query,conn as MySqlConnection);
                cmd.CommandType = CommandType.Text;
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    status = true;
                }

            }
            catch (MySqlException ex)
            {
                string err = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }

        public static bool Update(Employe emp)
        {
            IDbConnection conn = new MySqlConnection(connection);
            bool status = false;
            try
            {
                conn.Open();

           
                string query = "update employe SET name=@empname,designation=@empdes,salary=@empsal where id=@empid";
                MySqlCommand cmd = new MySqlCommand(query, conn as MySqlConnection);

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@empname",emp.Name);
                cmd.Parameters.AddWithValue("@empdes", emp.Designation);
                cmd.Parameters.AddWithValue("@empsal", emp.salary);
                cmd.Parameters.AddWithValue("@empid", emp.Id);

                cmd.CommandType = CommandType.Text;
                int no=cmd.ExecuteNonQuery();
                if (no > 0)
                {
                    status = true;
                }
            }
            catch(MySqlException ex)
            {
                string err = ex.Message;
            }
            finally{
                conn.Close();
            }
            return status;
        }

        public static bool Insert(Employe emp)
        {
            bool status = false;
            IDbConnection conn = new MySqlConnection(connection);

            try
            {
                conn.Open();
                string query = "INSERT INTO employe (id,name, designation, salary) values(@id, @empName, @empDesignation,@empSalary)";
                MySqlCommand cmd = new MySqlCommand(query,conn as MySqlConnection);
            

                cmd.CommandType = CommandType.Text;
                //To avoid SQL Injection , we use parameterized Query using Parameters collection with command object
               
         

               
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", emp.Id);
                cmd.Parameters.AddWithValue("@empName", emp.Name);
                cmd.Parameters.AddWithValue("@empDesignation", emp.Designation);
                cmd.Parameters.AddWithValue("@empSalary", emp.salary);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    status = true;
                }
            }
            catch(MySqlException ex)
            {
                string err = ex.Message;
            }
            finally{
                conn.Close();
            }


            return status;

        }
    }
}

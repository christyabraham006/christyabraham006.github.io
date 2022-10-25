using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientLibrary
{
    public class PatientDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            con = new SqlConnection("Data Source=SQL5108.site4now.net;Initial Catalog=db_a7dcab_hospitaldb;User Id=db_a7dcab_hospitaldb_admin;Password=Maneesh*66");
        }
        private void connectionManage()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            else
                con.Open();
        }


        // **************** ADD NEW PATIENT *********************
        public bool AddPatient(Patient pmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewPatient", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", pmodel.Name);
            cmd.Parameters.AddWithValue("@Age", pmodel.Age);
            cmd.Parameters.AddWithValue("@Address", pmodel.Address);
            cmd.Parameters.AddWithValue("@Doctor", pmodel.Doctor);
            cmd.Parameters.AddWithValue("@In_or_Out", 1);
            cmd.Parameters.AddWithValue("@Deleted", 0);
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW PATIENT DETAILS ********************
        public List<Patient> GetPatients(int pageStart, int rowsOfPage,string search)
        {
            connection();
            List<Patient> Patientlist = new List<Patient>();

            SqlCommand cmd = new SqlCommand("GetPatients", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@PageNumber", pageStart);
            cmd.Parameters.AddWithValue("@RowsOfPage", rowsOfPage);
            cmd.Parameters.AddWithValue("@search", search);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                Patientlist.Add(
                    new Patient
                    {
                        PatientId = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Age = Convert.ToInt32(dr["Age"]),
                        Address = Convert.ToString(dr["Address"]),
                        Doctor = Convert.ToString(dr["Doctor"]),
                        In_or_Out = Convert.ToBoolean(dr["In_or_Out"])
                    });
            }

            dt.Dispose();
            return Patientlist;
        }

        // ***************** UPDATE PATIENT DETAILS *********************
        public bool UpdateDetails(Patient pmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdatePatient", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", pmodel.PatientId);
            cmd.Parameters.AddWithValue("@name", pmodel.Name);
            cmd.Parameters.AddWithValue("@Age", pmodel.Age);
            cmd.Parameters.AddWithValue("@Address", pmodel.Address);
            cmd.Parameters.AddWithValue("@In_or_Out", pmodel.In_or_Out);
            cmd.Parameters.AddWithValue("@Deleted", 0);
            cmd.Parameters.AddWithValue("@Doctor", pmodel.Doctor);

            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE PATIENT DETAILS *******************
        public bool DeletePatient(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeletePatient", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public Patient GetPatient(int patientId)
        {
            connection();
            Patient patient = new Patient();

            SqlCommand cmd = new SqlCommand("GetSingleRecord", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Id", patientId);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();
            if (dt.Rows.Count != 0)
            {
                patient.PatientId = Convert.ToInt32(dt.Rows[0]["Id"]);
                patient.Name = Convert.ToString(dt.Rows[0]["Name"]);
                patient.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                patient.Address = Convert.ToString(dt.Rows[0]["Address"]);
                patient.Doctor = Convert.ToString(dt.Rows[0]["Doctor"]);
                patient.In_or_Out = Convert.ToBoolean(dt.Rows[0]["In_or_Out"]);
                dt.Dispose();
                return patient;
            }
            dt.Dispose();
            return null;

        }
    }
}

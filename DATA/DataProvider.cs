﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Phan_mem_quan_ly_bien_ban.DTO;

namespace Phan_mem_quan_ly_bien_ban.DATA
{
    internal class DataProvider
    {
        private static SqlDataAdapter myAdapter;
        private static SqlConnection conn;
        public DataProvider()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["do_an"].ConnectionString);
        }
        private static SqlConnection OpenConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
        public DataTable ExecuteSelectAllQuery(String _query)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();

            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = OpenConnection();
                myCommand.CommandText = _query;

                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {

            }
            return dataTable;
        }
        public EmployeeDTO ExecuteDangNhapQuery(String _query, SqlParameter[] sqlParameter)
        {
            EmployeeDTO employee = null;
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            //dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = OpenConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
                if (dataTable.Rows.Count > 0)
                {
                    employee = new EmployeeDTO();
                    employee.maNhanVien = int.Parse(dataTable.Rows[0][0].ToString()); // Access the value in the first column of the first row
                    employee.tenNhanVien = dataTable.Rows[0][1].ToString();
                    employee.taiKhoan = dataTable.Rows[0][2].ToString();
                    employee.matKhau = dataTable.Rows[0][3].ToString();
                    employee.isAdmin = bool.Parse (dataTable.Rows[0][4].ToString());
                    employee.maChiNhanh = int.Parse(dataTable.Rows[0][5].ToString());
                }
            }

            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {

            }
           
            return employee;
        }


        public DataTable ExecuteSelectQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            //dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = OpenConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {

            }
            return dataTable;
        }
        public void ExecuteQuery(String _query, SqlParameter[] sqlParameter)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = OpenConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());

            }

        }
    }
}
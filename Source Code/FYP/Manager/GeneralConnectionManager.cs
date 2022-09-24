using FYP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP.Manager
{
    public class GeneralConnectionManager
    {
        protected static MoreToIt_DatabaseEntities DB = new MoreToIt_DatabaseEntities();
        //public static string conString = @"Data Source=DESKTOP-V5U01KT\MSSQLSERVER01;Initial Catalog=MoreToIt_Database;Integrated Security=True";
        //public static SqlConnection con = new SqlConnection(conString);
    }
}
using AffichageDynamique.Helpers;
using AffichageDynamique.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AffichageDynamique.Models
{
    public class UtilController
    {
        private static IConfigurationRoot configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
        string connectionString = configuration.GetSection("ConnectionStrings")["Connection"];

        public static readonly object UsersKey = new Object();

        public string GetHostName()
        {
            String strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            return strHostName;
        }
        public UsersModel GetUsersByLdapLogin(string name, HttpContext httpContext, out List<SQLErrorModel> lstErrorMessages)
        {
            lstErrorMessages = null;

            string domaine = configuration.GetSection("Users")["Domaine"];
            string ldap_login = name.Replace(domaine, "");

            UsersModel ObjMaster = new UsersModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                ldap_login = "%" + ldap_login + "%";
                string sql = "SELECT * FROM dbo.users where ldap_login like @ldap_login";
                string parameters = "@ldap_login NVARCHAR(200)";
                SqlCommand cmd = new SqlCommand("sp_executesql", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stmt", sql);
                cmd.Parameters.AddWithValue("@params", parameters);
                cmd.Parameters.AddWithValue("@ldap_login", ldap_login);
                try
                {
                    con.Open();
                    SqlDataReader rsVS = cmd.ExecuteReader();
                    while (rsVS.Read())
                    {
                        ObjMaster.id = Convert.ToInt32(rsVS["id"]);
                        ObjMaster.tri = rsVS["tri"].ToString();
                        ObjMaster.name = rsVS["name"].ToString();
                        ObjMaster.firstName = rsVS["firstName"].ToString();
                        ObjMaster.email = rsVS["email"].ToString();
                        ObjMaster.username = rsVS["username"].ToString();
                        ObjMaster.user_service = Convert.ToInt32(rsVS["user_service"]);
                        ObjMaster.ldap_login = rsVS["ldap_login"].ToString();
                    }
                    httpContext.Items[UsersKey] = ObjMaster;
                }
                catch (SqlException ex) //when (ex.Number == 50000)
                {
                    lstErrorMessages = ErrorUtility.GetListError(ex, "UsersDataAccessLayer.GetUsersByLdapLogin().Sql");
                }
                catch (Exception ex)
                {
                    //ExceptionUtility.SendErrorToMail(ex, Global.ExceptionCaughtIn + "UsersDataAccessLayer.GetUsersByLdapLogin()");
                }
                finally
                {
                    con.Close();
                }
            }

            return ObjMaster;
        }
    }
}


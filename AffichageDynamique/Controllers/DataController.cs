using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using AffichageDynamique.Models;
using Microsoft.Extensions.Configuration;

namespace AffichageDynamique.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        IConfiguration _config;
        public DataController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("[action]")]
        public IEnumerable<Organisation> CreateUpdateOrganisation([FromBody] Organisation o)
        {
            if (o != null)
            {
                string connStr = _config.GetSection("ConnectionStrings").GetSection("Connection").Value;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[upsertOrganisation]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = o.id;
                    cmd.Parameters.Add("@nomOrganisation", SqlDbType.NVarChar).Value = o.nomOrganisation.ToString();
                    cmd.Parameters.Add("@domaine", SqlDbType.NVarChar).Value = o.domaine.ToString();
                    cmd.Parameters.Add("@slug", SqlDbType.NVarChar).Value = o.slug.ToString();
                    cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = o.description.ToString();
                    cmd.Parameters.Add("@logo", SqlDbType.NVarChar).Value = o.logo.ToString();
                    cmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = o.image.ToString();
                    cmd.Parameters.Add("@imageContact", SqlDbType.NVarChar).Value = o.imageContact.ToString();
                    cmd.Parameters.Add("@nomContact", SqlDbType.NVarChar).Value = o.nomContact.ToString();
                    cmd.Parameters.Add("@telContact", SqlDbType.NVarChar).Value = o.telContact.ToString();
                    cmd.Parameters.Add("@emailContact", SqlDbType.NVarChar).Value = o.emailContact.ToString();
                    cmd.Parameters.Add("@webContact", SqlDbType.NVarChar).Value = o.webContact.ToString();
                    cmd.Parameters.Add("@effectif", SqlDbType.NVarChar).Value = o.effectif.ToString();
                    cmd.Parameters.Add("@couleur", SqlDbType.NVarChar).Value = o.couleur.ToString();
                    cmd.Parameters.Add("@etat", SqlDbType.SmallInt).Value = o.etat;

                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    o.id = Convert.ToInt32(reader["current_id"].ToString());
                }

            }
            List<Organisation> listOrganisation = new List<Organisation>
            {
                o
            };
            return listOrganisation;
        }
        [HttpGet("[action]")]
        public OrganisationsPaginated RechercheOrganisation(string terme, int etat)
        {
            

            List<Organisation> orga = new List<Organisation>();
            int totalPages = 1;

            string connStr = _config.GetSection("ConnectionStrings").GetSection("Connection").Value;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[rechercheOrganisations]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@terme", SqlDbType.NVarChar).Value = terme.ToString();
                cmd.Parameters.Add("@etat", SqlDbType.Int).Value = etat;

                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                reader.Read();
                totalPages = Convert.ToInt32(reader["totalPages"].ToString());
                reader.NextResult();
                while (reader.Read())
                {
                    Organisation o = new Organisation
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        nomOrganisation = reader["nomOrganisation"].ToString(),
                        slug = reader["slug"].ToString(),
                        /*domaine = reader["domaine"].ToString(),
                        description = reader["description"].ToString(),
                        logo = reader["logo"].ToString(),
                        image = reader["image"].ToString(),
                        imageContact = reader["imageContact"].ToString(),
                        nomContact = reader["nomContact"].ToString(),
                        telContact = reader["telContact"].ToString(),
                        emailContact = reader["emailContact"].ToString(),
                        webContact = reader["webContact"].ToString(),
                        effectif = reader["effectif"].ToString(),
                        couleur = reader["couleur"].ToString(),
                        etat = Convert.ToInt32(reader["etat"].ToString())*/
                    };
                    orga.Add(o);

                }

                return new OrganisationsPaginated { organisations = orga, totalPages = totalPages };
            }
        }
        [HttpGet("[action]")]
        public OrganisationsPaginated GetOrganisations(int etat)
        {
            string connStr = _config.GetSection("ConnectionStrings").GetSection("Connection").Value;

            List<Organisation> orga = new List<Organisation>();
            int totalPages = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("getOrganisations", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@etat", SqlDbType.Int).Value = etat;

                SqlDataReader reader = null;

                reader = cmd.ExecuteReader();

                reader.Read();

                totalPages = Convert.ToInt32(reader["totalPages"].ToString());

                reader.NextResult();
                while (reader.Read())
                {
                    Organisation o = new Organisation
                    {
                        id = Convert.ToInt32(reader["id"].ToString()),
                        nomOrganisation = reader["nomOrganisation"].ToString(),
                        slug = reader["slug"].ToString(),
                        /*domaine = reader["domaine"].ToString(),
                        description = reader["description"].ToString(),
                        logo = reader["logo"].ToString(),
                        image = reader["image"].ToString(),
                        imageContact = reader["imageContact"].ToString(),
                        nomContact = reader["nomContact"].ToString(),
                        telContact = reader["telContact"].ToString(),
                        emailContact = reader["emailContact"].ToString(),
                        webContact = reader["webContact"].ToString(),
                        effectif = reader["effectif"].ToString(),
                        couleur = reader["couleur"].ToString(),
                        etat = Convert.ToInt32(reader["etat"].ToString())*/
                    };
                    orga.Add(o);

                }

                return new OrganisationsPaginated { organisations = orga, totalPages = totalPages };
            }
        }
    }
}

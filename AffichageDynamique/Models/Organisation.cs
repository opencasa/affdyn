using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AffichageDynamique.Models
{
    public class Organisation
    {
        public int id { get; set; }
        public string nomOrganisation { get; set; }
        public string domaine { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string logo { get; set; }
        public string image { get; set; }
        public string imageContact { get; set; }
        public string nomContact { get; set; }
        public string telContact { get; set; }
        public string emailContact { get; set; }
        public string webContact { get; set; }
        public string effectif { get; set; }
        public string couleur { get; set; }
        public int etat { get; set; }
    }
    public class OrganisationsPaginated
    {
        public List<Organisation> organisations;
        public int totalPages;
    }
}

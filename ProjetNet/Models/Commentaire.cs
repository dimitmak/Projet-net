using System;
using System.Data.Entity;

namespace ProjetNet.Models
{
    public class Commentaire
    {
        public int ID { get; set; }
        public string Texte { get; set; }
        public DateTime Date { get; set; }
        public string Posteur { get; set; }
        public string id_message { get; set; }
        public Commentaire() { }

        public virtual Message Message { get; set; }

    }

    public class CommentaireDBContext : DbContext
    {
        public DbSet<Commentaire> Commentaires { get; set; }
    }
}
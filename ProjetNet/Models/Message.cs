using System;
using System.Collections.Generic;
using System.Data.Entity;


namespace ProjetNet.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Texte { get; set; }
        public DateTime Date { get; set; }
        public string Posteur { get; set; }

        public Message()
        {
            Commentaires = new List<Commentaire>();
        }
        public virtual ICollection<Commentaire> Commentaires { get; set; }

    }

    public class MessageDBContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}
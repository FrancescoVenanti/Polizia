using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Polizia.Models
{
    //modello per la tabella Violazione
    public class Violazione
    {
        public int Id { get; set; }

        [DisplayName("Descrizione")]
        [Required(ErrorMessage = "La descrizione e' obbligatoria")]
        [StringLength(200, MinimumLength = 5,
        ErrorMessage = "La descrizione deve essere compresa tra 5 e 200 caratteri")]
        public string Descrizione { get; set; }

        [DisplayName("E' Contestabile?")]
        public bool isContestabile { get; set; }
    }
}
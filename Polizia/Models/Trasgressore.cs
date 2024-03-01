using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Polizia.Models
{
    public class Trasgressore
    {
        public int IDAnagrafica { get; set; }
        [DisplayName("Cognome")]
        [Required(ErrorMessage = "Il Cognome è obbligatorio")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Il Cognome deve essere compreso tra 3 e 50 caratteri")]
        public string Cognome { get; set; }
        [DisplayName("Nome")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Il Nome deve essere compreso tra 3 e 50 caratteri")]
        [Required(ErrorMessage = "Il Nome è obbligatorio")]
        public string Nome { get; set; }
        [DisplayName("Indirizzo")]
        [StringLength(250, MinimumLength = 10,
        ErrorMessage = "L'indirizzo deve essere compreso tra 10 e 250 caratteri")]
        [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio")]
        public string Indirizzo { get; set; }
        [DisplayName("Città")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Citta' deve essere essere compreso tra 3 e 50 caratteri")]
        [Required(ErrorMessage = "Il campo Città è obbligatorio")]

        public string Citta { get; set; }
        [DisplayName("CAP")]
        [StringLength(5, MinimumLength = 5,
        ErrorMessage = "Cap non valido")]
        [Required(ErrorMessage = "Il campo CAP è obbligatorio")]
        public string CAP { get; set; }
        [DisplayName("Cod. Fisc.")]
        [StringLength(16, MinimumLength = 16,
        ErrorMessage = "Codice fiscale non valido")]
        [Required(ErrorMessage = "Il campo Cod. Fisc. è obbligatorio")]
        public string Cod_Fisc { get; set; }
    }
}
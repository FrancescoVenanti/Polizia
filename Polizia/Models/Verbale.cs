using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Polizia.Models
{
    public class Verbale
    {

        public int idVerbale { get; set; }
        [DisplayName("Data violazione")]
        [Required(ErrorMessage = "La data e' obbligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataViolazione { get; set; }
        [DisplayName("Indirizzo")]
        [Required(ErrorMessage = "l'indirizzo e' obbligatorio")]
        [StringLength(200, MinimumLength = 5,
        ErrorMessage = "l'indirizzo deve essere compreso tra 5 e 200 caratteri")]
        public string IndirizzoViolazione { get; set; }
        [DisplayName("Nominativo agente")]
        [Required(ErrorMessage = "il nominativo agrente e' obbligatorio")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "il nominativo deve essere compreso tra 3 e 50 caratteri")]
        public string NominativoAgente { get; set; }
        [DisplayName("Data verbale")]
        [Required(ErrorMessage = "La data e' obbligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataVerbale { get; set; }
        [DisplayName("Importo verbale")]
        [Required(ErrorMessage = "L'importo e' obbligatorio")]
        public double Importo { get; set; }
        [DisplayName("Punti decurtati")]
        [Required(ErrorMessage = "i punti sono obbligatori")]
        public int DecurtamentoPunti { get; set; }
        public int idAnagrafica { get; set; }
        public int idViolazione { get; set; }
        public string NomeTrasgressore { get; set; }
        public string CognomeTrasgressore { get; set; }
        public string Violazione { get; set; }
    }
}
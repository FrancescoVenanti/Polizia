using Polizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polizia.Controllers
{
    public class OperazioniController : Controller
    {
        // GET: Operazioni
        public ActionResult Index()
        {
            return View();
        }

        //metodo per visualizzare i verbali con importo superiore a 400
        public ActionResult PiuDi400()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Verbale> verbali = new List<Verbale>();
            try
            {
                conn.Open();
                string query = "SELECT v.*, tv.Descrizione, a.Nome, a.Cognome " +
                               "FROM Verbale v " +
                               "INNER JOIN tipo_violazione tv ON v.idViolazione = tv.idViolazione " +
                               "INNER JOIN Anagrafica a ON v.idAnagrafica = a.idAnagrafica " +
                               "WHERE v.Importo > 400";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Verbale v = new Verbale();
                    v.idVerbale = Convert.ToInt32(reader["idVerbale"]);
                    v.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    v.IndirizzoViolazione = reader["IndirizzoViolazione"].ToString();
                    v.NominativoAgente = reader["NominativoAgente"].ToString();
                    v.DataVerbale = Convert.ToDateTime(reader["DataVerbale"]);
                    v.Importo = Convert.ToDouble(reader["Importo"]);
                    v.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                    v.idAnagrafica = Convert.ToInt32(reader["idAnagrafica"]);
                    v.idViolazione = Convert.ToInt32(reader["idViolazione"]);
                    v.Violazione = reader["descrizione"].ToString();
                    v.NomeTrasgressore = reader["Nome"].ToString();
                    v.CognomeTrasgressore = reader["Cognome"].ToString();
                    verbali.Add(v);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return View(verbali);
        }

        //metodo per visualizzare i verbali con decurtamento punti superiore a 10
        public ActionResult PiuDi10()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Verbale> verbali = new List<Verbale>();
            try
            {
                conn.Open();
                string query = "SELECT v.*, tv.Descrizione, a.Nome, a.Cognome " +
                               "FROM Verbale v " +
                               "INNER JOIN tipo_violazione tv ON v.idViolazione = tv.idViolazione " +
                               "INNER JOIN Anagrafica a ON v.idAnagrafica = a.idAnagrafica " +
                               "WHERE v.DecurtamentoPunti > 10";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Verbale v = new Verbale();
                    v.idVerbale = Convert.ToInt32(reader["idVerbale"]);
                    v.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    v.IndirizzoViolazione = reader["IndirizzoViolazione"].ToString();
                    v.NominativoAgente = reader["NominativoAgente"].ToString();
                    v.DataVerbale = Convert.ToDateTime(reader["DataVerbale"]);
                    v.Importo = Convert.ToDouble(reader["Importo"]);
                    v.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                    v.idAnagrafica = Convert.ToInt32(reader["idAnagrafica"]);
                    v.idViolazione = Convert.ToInt32(reader["idViolazione"]);
                    v.Violazione = reader["descrizione"].ToString();
                    v.NomeTrasgressore = reader["Nome"].ToString();
                    v.CognomeTrasgressore = reader["Cognome"].ToString();
                    verbali.Add(v);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return View(verbali);
        }

        //metodo per visualizzare la somma dei punti decurtati per ogni trasgressore
        public ActionResult Punti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<PuntiTrasgressore> puntiTrasgressori = new List<PuntiTrasgressore>();
            try
            {
                conn.Open();
                string query = "SELECT a.Nome, a.Cognome, SUM(v.DecurtamentoPunti) AS PuntiDecurtati " +
                               "FROM Verbale v " +
                               "INNER JOIN Anagrafica a ON v.idAnagrafica = a.idanagrafica " +
                               "GROUP BY a.Nome, a.Cognome";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PuntiTrasgressore pt = new PuntiTrasgressore();
                    pt.NomeTrasgressore = reader["Nome"].ToString();
                    pt.CognomeTrasgressore = reader["Cognome"].ToString();
                    pt.PuntiDecurtati = Convert.ToInt32(reader["PuntiDecurtati"]);
                    puntiTrasgressori.Add(pt);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return View(puntiTrasgressori);
        }

        //metodo per visualizzare il numero dei verbali per ogni trasgressore
        public ActionResult verbali()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            //creo una lista di verbali uso il modello puntitransgressore perche ha un cambo int che posso utilizzare per memorizzare il totale dei verbali
            //in questo modo posso riutilizzare la vista punti per visualizzare il totale dei verbali
            //senza dover creare un nuovo modello
            List<PuntiTrasgressore> puntiTrasgressori = new List<PuntiTrasgressore>();
            try
            {
                conn.Open();
                string query = "SELECT a.Nome, a.Cognome, COUNT(*) AS TotaleVerbali " +
                               "FROM Verbale v " +
                               "INNER JOIN Anagrafica a ON v.idAnagrafica = a.idAnagrafica " +
                               "GROUP BY a.Nome, a.Cognome";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PuntiTrasgressore pt = new PuntiTrasgressore();
                    pt.NomeTrasgressore = reader["Nome"].ToString();
                    pt.CognomeTrasgressore = reader["Cognome"].ToString();
                    //uso il campo punti decurtati per memorizzare il totale dei verbali
                    pt.PuntiDecurtati = Convert.ToInt32(reader["TotaleVerbali"]);
                    puntiTrasgressori.Add(pt);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return View(puntiTrasgressori);
        }
    }
   
}
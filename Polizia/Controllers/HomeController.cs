using System;
using Polizia.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polizia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Trasgressori()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            List<Trasgressore> trasgressori = new List<Trasgressore>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM Anagrafica";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Trasgressore t = new Trasgressore();
                    t.IDAnagrafica = Convert.ToInt32(reader["idanagrafica"]);
                    t.Cognome = reader["Cognome"].ToString();
                    t.Nome = reader["Nome"].ToString();
                    t.Indirizzo = reader["Indirizzo"].ToString();
                    t.Citta = reader["Città"].ToString();
                    t.CAP = reader["CAP"].ToString();
                    t.Cod_Fisc = reader["Cod_Fisc"].ToString();
                    trasgressori.Add(t);
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
            return View(trasgressori);
        }
        public ActionResult CreateTrasgressore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrasgressore(Trasgressore t)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) VALUES (@Cognome, @Nome, @Indirizzo, @Città, @CAP, @Cod_Fisc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Cognome", t.Cognome);
                cmd.Parameters.AddWithValue("@Nome", t.Nome);
                cmd.Parameters.AddWithValue("@Indirizzo", t.Indirizzo);
                cmd.Parameters.AddWithValue("@Città", t.Citta);
                cmd.Parameters.AddWithValue("@CAP", t.CAP);
                cmd.Parameters.AddWithValue("@Cod_Fisc", t.Cod_Fisc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante l'inserimento dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Trasgressori");
        }

        public ActionResult EditTrasgressore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditTrasgressore(Trasgressore t)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "UPDATE Anagrafica SET Cognome = @Cognome, Nome = @Nome, Indirizzo = @Indirizzo, Città = @Città, CAP = @CAP, Cod_Fisc = @Cod_Fisc WHERE IDAnagrafica = @IDAnagrafica";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDAnagrafica", t.IDAnagrafica);
                cmd.Parameters.AddWithValue("@Cognome", t.Cognome);
                cmd.Parameters.AddWithValue("@Nome", t.Nome);
                cmd.Parameters.AddWithValue("@Indirizzo", t.Indirizzo);
                cmd.Parameters.AddWithValue("@Città", t.Citta);
                cmd.Parameters.AddWithValue("@CAP", t.CAP);
                cmd.Parameters.AddWithValue("@Cod_Fisc", t.Cod_Fisc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante l'aggiornamento dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Trasgressori");
        }


        public ActionResult Violazioni()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Violazione> violazioni = new List<Violazione>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Tipo_Violazione WHERE IsContestabile = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Violazione v = new Violazione();
                    v.Id = Convert.ToInt32(reader["idViolazione"]);
                    v.Descrizione = reader["descrizione"].ToString();
                    v.isContestabile = Convert.ToBoolean(reader["IsContestabile"]);
                    violazioni.Add(v);
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
            return View(violazioni);
        }

        public ActionResult Verbali()
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
                               "INNER JOIN Anagrafica a ON v.idAnagrafica = a.idAnagrafica ";
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
        public ActionResult CreateVerbale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVerbale(Verbale v)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Verbale (DataViolazione, IndirizzoViolazione, NominativoAgente, DataVerbale, Importo, DecurtamentoPunti, idAnagrafica, idViolazione) VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataVerbale, @Importo, @DecurtamentoPunti, @idAnagrafica, @idViolazione)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DataViolazione", v.DataViolazione);
                cmd.Parameters.AddWithValue("@IndirizzoViolazione", v.IndirizzoViolazione);
                cmd.Parameters.AddWithValue("@NominativoAgente", v.NominativoAgente);
                cmd.Parameters.AddWithValue("@DataVerbale", v.DataVerbale);
                cmd.Parameters.AddWithValue("@Importo", v.Importo);
                cmd.Parameters.AddWithValue("@DecurtamentoPunti", v.DecurtamentoPunti);
                cmd.Parameters.AddWithValue("@idAnagrafica", v.idAnagrafica);
                cmd.Parameters.AddWithValue("@idViolazione", v.idViolazione);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante l'inserimento dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Verbali");
        }
    }
}
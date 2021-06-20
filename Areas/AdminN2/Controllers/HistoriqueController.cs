using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GestionAbscences.Services;
using GestionAbscences.Areas.Admin.Models;
using System.Net;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace GestionAbscences.Areas.AdminN2.Controllers
{
    public class HistoriqueController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        private readonly DemandeService demandeService;

        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult choixVal()
        {
            string d1 = Request["debut"].ToString();
            string f1 = Request["fin"].ToString();
            ViewBag.d2 = Request["debut"].ToString();
            ViewBag.f2 = Request["fin"].ToString();


            string val = Request["validation"].ToString();



            if (d1.Equals("") || f1.Equals("") || val.Equals(""))
            {
                ViewBag.message = "Selectionner les dates et la catégorie svp !";
                return RedirectToAction("historique");
            }
            else
            {
                DateTime debut = Convert.ToDateTime(Request["debut"]);

                DateTime fin = Convert.ToDateTime(Request["fin"]);

                if (val.Equals("1"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("2"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("3"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("4"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("5"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("6"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("7"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("8"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("9"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("10"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }


            }


            return RedirectToAction("historique");

        }

       
        public ActionResult historique()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours").OrderByDescending(news => news.DateDC).Take(10).ToList();

            return View(demandeConge);
        }
        public ActionResult validation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentDemande = demandeService.ReadById(id.Value);
            if (currentDemande == null)
            {
                return HttpNotFound($"this demande ({id}) is not found");
            }


            demandeconge demandeconge = db.demandeconge.Find(id);
            Session["uid"] = currentDemande.idDemandeConge;

            if (demandeconge == null)
            {
                return HttpNotFound();
            }

            return View(demandeconge);
        }

        [HttpPost]
        public ActionResult Validation()
        {
            int uid = int.Parse(Session["uid"].ToString());
            demandeconge e = db.demandeconge.Find(uid);
            string button = Request["button"];

         
                
            switch (button)
            {
                case "Accepté":
                    e.ValidationN2 = "accepte";
                    e.DateValidationN2= DateTime.Now;
                   
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Refusé":
                    e.ValidationN2 = "refuse";
                    e.ValdationRH = "*******";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Annulé":

                    return RedirectToAction("historique");
                default:
                    return View();

            }



        }

        /*  [HttpPost]
         * 
        public ActionResult Validation()
        {
             string validation1 = Request.Form["validation1"];
               string validation2 = Request.Form["validation2"];
               int uid = int.Parse(Session["uid"].ToString());
               demandeconge e = db.demandeconge.Find(uid);
               Session["index"] = uid;
               if (validation2.Equals("Accepte"))
               {
                        e.ValidationN2 = "Accepté";
               }
                else if (validation2.Equals("Refuse"))
               {
                        e.ValidationN2 = "refusé";
               }
           else 
           {
               e.ValidationN2 = "En cours";
           }
           if (validation1.Equals("Accepte"))
               {
               e.ValidationN1 = "Accepté";
               }
           else if (validation1.Equals("Refuse"))
           {
               e.ValidationN1 = "refusé";
           }
           else
           {
               e.ValidationN1 = "En cours";
           }
           db.Entry(e).State = EntityState.Modified;
               db.SaveChanges();
           return RedirectToAction("Index" ,"Default");
       }*/




    }
}











using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAbscences.Data;
using GestionAbscences.Models;
using System.Data.Entity;

using GestionAbscences.Areas.Admin.Models;
using System.Web.Helpers;
using System.Web.UI;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestionAbscences.Areas.Admin.Controllers
{
    public class Dashb1Controller : Controller
    {
        // GET: Admin/Dashb1
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        // GET: DashB
        public ActionResult Index()
        {

            int[] tab = new int[] { };

            int i = 0;
            var entites = db.entite.ToList();
            foreach (var item in entites)
            {
                int somme = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.typeconge.designation == "2eme tranche" && p.employe.affectation == item.Designation).Count();

                tab[i] = 1;

                i = i + 1;
               

            }

            Session["test"] = tab.Length.ToString();

            string x = Session["matricule"].ToString();
            //global
            var count1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Count();
            Session["global"] = count1.ToString();

            var test = count1.ToString();
            if(test.Equals("0"))
            {
                Session["accept1"] = 0;
                Session["refuse1"] = 0;
                Session["enCours1"] = 0;

            }
            else
            {
                //accepte
                var count2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.ValdationRH == "accepte").Count();
                Session["accepte"] = count2.ToString();
                var num1 = count2 * 100;
                Session["accept1"] = num1 / count1;
                //refuse
                var count3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  (p.ValdationRH == "refuse" || p.ValidationN1 == "refuse" || p.ValidationN2 == "refuse")).Count();
                Session["refuse"] = count3.ToString();
                var num2 = count3 * 100;
                Session["refuse1"] = num2 / count1;
                //encours
                var count4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => (p.ValdationRH == "En cours" || p.ValidationN1 == "En cours" || p.ValidationN2 == "En cours")).Count();
                Session["enCours"] = count4.ToString();
                var num3 = count4 * 100;
                Session["enCours1"] = num3 / count1;
           
            }

            return View(entites);
        }
        public ActionResult Global()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).OrderByDescending(news => news.DateDC).ToList();



            //  var t = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x).Count();
            //ViewBag.count = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x).Count().ToString();
            return View(demandeConge);

        }
        public ActionResult Accepte()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.ValdationRH == "accepte").OrderByDescending(news => news.DateDC).ToList();


            return View(demandeConge);

        }
        public ActionResult Refuse()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  (p.ValdationRH == "refuse" || p.ValidationN1 == "refuse" || p.ValidationN2 == "refuse")).OrderByDescending(news => news.DateDC).ToList();


            return View(demandeConge);

        }
        public ActionResult Encours()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  (p.ValdationRH == "En cours" || p.ValidationN1 == "En cours" || p.ValidationN2 == "En cours")).OrderByDescending(news => news.DateDC).ToList();


            return View(demandeConge);

        }

        public ActionResult MyChart()
        {  
            DateTime jD = Convert.ToDateTime(DateTime.Now.ToString("01/01/yyyy"));
            DateTime jF = Convert.ToDateTime(DateTime.Now.ToString("31/01/yyyy"));

            DateTime FD = Convert.ToDateTime(DateTime.Now.ToString("01/02/yyyy"));
            DateTime FF = Convert.ToDateTime(DateTime.Now.ToString("28/02/yyyy"));

            DateTime MarsD = Convert.ToDateTime(DateTime.Now.ToString("01/03/yyyy"));
            DateTime MarsF = Convert.ToDateTime(DateTime.Now.ToString("31/03/yyyy"));

            DateTime AD = Convert.ToDateTime(DateTime.Now.ToString("01/04/yyyy"));
            DateTime AF = Convert.ToDateTime(DateTime.Now.ToString("30/04/yyyy"));

            DateTime MD = Convert.ToDateTime(DateTime.Now.ToString("01/05/yyyy"));
            DateTime MF = Convert.ToDateTime(DateTime.Now.ToString("31/05/yyyy"));

            DateTime juD = Convert.ToDateTime(DateTime.Now.ToString("01/06/yyyy"));
            DateTime juF = Convert.ToDateTime(DateTime.Now.ToString("30/06/yyyy"));

            DateTime julD = Convert.ToDateTime(DateTime.Now.ToString("01/07/yyyy"));
            DateTime julF = Convert.ToDateTime(DateTime.Now.ToString("31/07/yyyy"));


            DateTime AoutD = Convert.ToDateTime(DateTime.Now.ToString("01/08/yyyy"));
            DateTime AoutF = Convert.ToDateTime(DateTime.Now.ToString("31/08/yyyy"));


            DateTime sepD = Convert.ToDateTime(DateTime.Now.ToString("01/09/yyyy"));
            DateTime sepF = Convert.ToDateTime(DateTime.Now.ToString("30/09/yyyy"));

            DateTime OcD = Convert.ToDateTime(DateTime.Now.ToString("01/10/yyyy"));
            DateTime OcF = Convert.ToDateTime(DateTime.Now.ToString("31/10/yyyy"));

            DateTime novD = Convert.ToDateTime(DateTime.Now.ToString("01/11/yyyy"));
            DateTime novF = Convert.ToDateTime(DateTime.Now.ToString("30/11/yyyy"));

            DateTime DEcD = Convert.ToDateTime(DateTime.Now.ToString("01/12/yyyy"));
            DateTime  DecF = Convert.ToDateTime(DateTime.Now.ToString("31/12/yyyy"));


            string[] xv = { "janvier", "Février", "Mars", "Avril", "May", "juin" , "juillet" , "Aout" , "September" , "October" , "November","December"};
           


            //Les demandes non traiter
         
          var NT1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            var NT2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            var NT3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var NT4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            var NT5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            var NT6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            var NT7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            var NT8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var NT9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var NT10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >=OcD && p.DateFin <= OcF).Count();
            var NT11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            var NT12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            //Accepte

            var A1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            var A2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            var A3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var A4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            var A5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            var A6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            var A7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            var A8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var A9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var A10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var A11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            var A12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            //Refuse

            var R1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            var RT2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            var RT3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var RT4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            var RT5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            var RT6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            var RT7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            var RT8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var RT9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var RT10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var RT11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 =="refuse" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            var RT12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();

            //tous les demandes

            var T1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= jD && p.DateFin <= jF).Count();
            var T2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= FD && p.DateFin <= FF).Count();
            var T3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var T4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= AD && p.DateFin <= AF).Count();
            var T5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= MD && p.DateFin <= MF).Count();
            var T6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= juD && p.DateFin <= juF).Count();
            var T7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= julD && p.DateFin <= julF).Count();

            var T8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var T9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var T10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var T11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= novD && p.DateFin <= novF).Count();
            var T12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p =>  p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            int[] yv = { NT1 , NT2 , NT3 , NT4, NT5 , NT6 , NT7 , NT8 , NT9 , NT10 , NT11 , NT12};
            int[] yv1 = { T1 , T2 , T3 , T4, T5 , T6 , T7 , T8 , T9 , T10 , T11 , T12};
            int[] yv2 = { A1 , A2 , A3 , A4, A5 , A6 , A7 , A8 , A9 , A10 , A11 , A12};
            int[] yv3 = { R1 , RT2 , RT3 ,RT4, RT5 , RT6 , RT7 , RT8 , RT9 , RT10 , RT11 , RT12};

            // Chart 

            var chart = new System.Web.Helpers.Chart(1500, 400 , ChartTheme.Blue);

            ChartArea chartA = new ChartArea();
          //chartA.AxisX.Interval

            Series s1 = new Series();
            s1.IsValueShownAsLabel = true;
            

            chart.SetXAxis("Mois", 0, 12);

            chart.AddLegend("Legend", "yyyyy");

           
            chart.AddSeries
            (chartType: "column",
           

           

        name: "DC",
            xValue: xv, xField: "Mois",

            markerStep: 1,

            yValues: yv1)
             .AddSeries

            (chartType: "Column",

            name: "Dc Non-traiter",

            xValue: xv, xField: "Mois",

            markerStep: 1,

            yValues: yv



            )
                 .AddSeries
                (chartType: "column",
                
                


                name: "DC Refusé",


             xValue: xv, xField: "Mois",

            markerStep: 1,

                yValues: yv3)

                 .AddSeries
                (chartType: "column",
                chartArea: default,


                name: "DC Accepté",
             xValue: xv, xField: "Mois",

            markerStep: 1,

                yValues: yv2)

                .Write();



            return null;
        }

        public ActionResult MyLine()
        {
            DateTime jD = Convert.ToDateTime(DateTime.Now.ToString("01/01/yyyy"));
            DateTime jF = Convert.ToDateTime(DateTime.Now.ToString("31/01/yyyy"));

            DateTime FD = Convert.ToDateTime(DateTime.Now.ToString("01/02/yyyy"));
            DateTime FF = Convert.ToDateTime(DateTime.Now.ToString("28/02/yyyy"));

            DateTime MarsD = Convert.ToDateTime(DateTime.Now.ToString("01/03/yyyy"));
            DateTime MarsF = Convert.ToDateTime(DateTime.Now.ToString("31/03/yyyy"));

            DateTime AD = Convert.ToDateTime(DateTime.Now.ToString("01/04/yyyy"));
            DateTime AF = Convert.ToDateTime(DateTime.Now.ToString("30/04/yyyy"));

            DateTime MD = Convert.ToDateTime(DateTime.Now.ToString("01/05/yyyy"));
            DateTime MF = Convert.ToDateTime(DateTime.Now.ToString("31/05/yyyy"));

            DateTime juD = Convert.ToDateTime(DateTime.Now.ToString("01/06/yyyy"));
            DateTime juF = Convert.ToDateTime(DateTime.Now.ToString("30/06/yyyy"));

            DateTime julD = Convert.ToDateTime(DateTime.Now.ToString("01/07/yyyy"));
            DateTime julF = Convert.ToDateTime(DateTime.Now.ToString("31/07/yyyy"));


            DateTime AoutD = Convert.ToDateTime(DateTime.Now.ToString("01/08/yyyy"));
            DateTime AoutF = Convert.ToDateTime(DateTime.Now.ToString("31/08/yyyy"));


            DateTime sepD = Convert.ToDateTime(DateTime.Now.ToString("01/09/yyyy"));
            DateTime sepF = Convert.ToDateTime(DateTime.Now.ToString("30/09/yyyy"));

            DateTime OcD = Convert.ToDateTime(DateTime.Now.ToString("01/10/yyyy"));
            DateTime OcF = Convert.ToDateTime(DateTime.Now.ToString("31/10/yyyy"));

            DateTime novD = Convert.ToDateTime(DateTime.Now.ToString("01/11/yyyy"));
            DateTime novF = Convert.ToDateTime(DateTime.Now.ToString("30/11/yyyy"));

            DateTime DEcD = Convert.ToDateTime(DateTime.Now.ToString("01/12/yyyy"));
            DateTime DecF = Convert.ToDateTime(DateTime.Now.ToString("31/12/yyyy"));


            string[] xv = { "janvier", "Février", "Mars", "Avril", "May", "juin", "juillet", "Aout", "September", "October", "November", "December" };



            //Les demandes non traiter

            var NT1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            var NT2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            var NT3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var NT4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            var NT5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            var NT6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            var NT7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            var NT8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var NT9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var NT10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var NT11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            var NT12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            //Accepte

            var A1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            var A2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            var A3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var A4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            var A5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            var A6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            var A7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            var A8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var A9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var A10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var A11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            var A12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            //Refuse

            var R1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            var RT2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            var RT3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var RT4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            var RT5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            var RT6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            var RT7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            var RT8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var RT9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var RT10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var RT11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            var RT12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();

            //tous les demandes

            var T1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= jD && p.DateFin <= jF).Count();
            var T2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= FD && p.DateFin <= FF).Count();
            var T3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var T4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= AD && p.DateFin <= AF).Count();
            var T5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= MD && p.DateFin <= MF).Count();
            var T6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= juD && p.DateFin <= juF).Count();
            var T7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= julD && p.DateFin <= julF).Count();

            var T8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var T9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var T10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var T11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= novD && p.DateFin <= novF).Count();
            var T12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            int[] yv = { NT1, NT2, NT3, NT4, NT5, NT6, NT7, NT8, NT9, NT10, NT11, NT12 };
            int[] yv1 = { T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12 };
            int[] yv2 = { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12 };
            int[] yv3 = { R1, RT2, RT3, RT4, RT5, RT6, RT7, RT8, RT9, RT10, RT11, RT12 };



            // Chart 

            var chart = new System.Web.Helpers.Chart(1500, 400, ChartTheme.Vanilla);

          
            chart.SetXAxis("Mois", 0, 12);
            
            chart.AddLegend("Legend", "yyyyy");

            chart.AddSeries

                 (
        chartType: "line",

       
 
                name: "DC Refusé",


             xValue: xv, xField: "Mois",

            markerStep: 1,

                yValues: yv3)

                 .AddSeries

                   (chartType: "line",
                chartArea: default,


                name: "DC Accepté",
             xValue: xv, xField: "Mois",

            markerStep: 1,

                yValues: yv2)
                    .AddSeries


            (chartType: "line",

            name: "DC",
            xValue: xv, xField: "Mois",

            markerStep: 1,

            yValues: yv1)
             .AddSeries

            (chartType: "line",

            name: "Dc Non-traiter",

            xValue: xv, xField: "Mois",

            markerStep: 1,

            yValues: yv



            )
                
               
              

                .Write();



            return null;
        }

        public ActionResult Pie1()
        {
            /*var entites = db.entite.Select(KK => new SelectListItem { 
                Text = KK.Designation,
                Value = KK.idEntite.ToString()
            }).ToList();*/
            var entites = db.entite.ToList();
            Session["name"] = entites.ToString();
          
           
                var x = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.typeconge.designation == "1ere tranche" && p.employe.affectation == "RHN").Count();
                var x1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.typeconge.designation == "1ere tranche" && p.employe.affectation == "AC").Count();
                var x2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.typeconge.designation == "1ere tranche" && p.employe.affectation == "LK").Count();
                var x3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.typeconge.designation == "1ere tranche" && p.employe.affectation == "L2").Count();
                var x4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.typeconge.designation == "1ere tranche" && p.employe.affectation == "2M").Count();

            int[] tt = {x , x1 , x2 , x3 , x4 };
            string[] names = {"RHN" , "AC" , "L2" , "LK" , "2M" };
            var chart = new System.Web.Helpers.Chart(500, 500, ChartTheme.Vanilla);

            chart.AddLegend("Legend", "yyyyy");
            chart.AddTitle("Tranche1");
            chart.AddLegend("Services");
            chart.AddSeries

                 (
        chartType: "Pie",
        yValues: tt
     



             ).Write();

            return null;
        }
        public ActionResult Pie2()
        {
            int[] tab = new int[] { };

            int i = 0;
            var entites = db.entite.ToList();
            foreach (var item in entites)
            {
                int somme = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.typeconge.designation == "2eme tranche" && p.employe.affectation == item.Designation).Count();

                // tab[i] = somme;
                i++;
            }

          
            
            var chart = new System.Web.Helpers.Chart(500, 500, ChartTheme.Vanilla);
            ChartArea area = new ChartArea();
            Series ser = new Series();
           
            chart.AddLegend("Services", "yyyyy");
            chart.AddTitle("Tranche 2");

            chart.AddSeries

                 (
        chartType: "Pie",
      
        yValues: tab

        

             ).Write();

            return null;
        }
        

    }
}

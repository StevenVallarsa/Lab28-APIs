using Lab28_APIs.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab28_APIs.Controllers
{
    public class HomeController : Controller
    {

        //public ActionResult Search(string title)
        //{
        //    string APIText = GetAPIText(title);

        //    Movie mov = ConvertToMovie(APIText);

        //    return View(mov);

        //}


        //public Movie ConvertToMovie(string APIText)
        //{
        //    JToken jsonData = JToken.Parse(APIText);
        //    Movie m = new Movie();
        //    m.Title = jsonData["Title"].ToString();
        //    m.Genre = jsonData["Genre"].ToString();
        //    m.Plot = jsonData["Plot"].ToString();
        //    m.PosterURL = jsonData["Poster"].ToString();

        //    return m;

        //}

        //public string GetAPIText(string title)
        //{
        //    string APIkey = "5af6084";
        //    string URL = $"http://www.omdbapi.com/?apikey={APIkey}&t={title}";

        //    HttpWebRequest request = WebRequest.CreateHttp(URL);

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    StreamReader rd = new StreamReader(response.GetResponseStream());

        //    string APIText = rd.ReadToEnd();

        //    return APIText;
        //}

        //public string GetListMovies(string title)
        //{
        //    string APIkey = "5af6084";
        //    string URL = $"http://www.omdbapi.com/?apikey={APIkey}&s={title}";

        //    HttpWebRequest request = WebRequest.CreateHttp(URL);

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    StreamReader rd = new StreamReader(response.GetResponseStream());

        //    string APIText = rd.ReadToEnd();

        //    return APIText;
        //}

        //public List<Movie> ConvertListOfMovies(string APIText)
        //{
        //    JToken json = JToken.Parse(APIText);

        //    List<JToken> filmTokens = json["Search"].ToList();

        //    List<Movie> MovieResults = new List<Movie>();

        //    foreach(JToken j in filmTokens)
        //    {
        //        Movie m = new Movie();

        //        m.Title = j["Title"].ToString();
        //        MovieResults.Add(m);
        //    }

        //    return MovieResults;
        //}

        //public ActionResult MovieList(string title)
        //{
        //    string APIText = GetListMovies(title);
        //    List<Movie> movies = ConvertListOfMovies(APIText);

        //    return View(movies);
        //}

        public ActionResult Draw()
        {

        }


        public ActionResult Index()
        {
            string shuffleDeck = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

            HttpWebRequest request = WebRequest.CreateHttp(shuffleDeck);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string APIText = rd.ReadToEnd();

            JToken jsonDeckID = JToken.Parse(APIText);

            string deckID = jsonDeckID["deck_id"].ToString();

            string initialDraw = $"https://deckofcardsapi.com/api/deck/{deckID}/draw/?count=5";

            request = WebRequest.CreateHttp(initialDraw);
            response = (HttpWebResponse)request.GetResponse();
            rd = new StreamReader(response.GetResponseStream());
            APIText = rd.ReadToEnd();

            JToken jsonCards = JToken.Parse(APIText);

            List<JToken> cardTokens = jsonCards["cards"].ToList();
            List<string> cardList = new List<string>();

            foreach(JToken j in cardTokens)
            {
                string card = j["image"].ToString();
                cardList.Add(card);
            }

            ViewBag.CardList = cardList;

            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
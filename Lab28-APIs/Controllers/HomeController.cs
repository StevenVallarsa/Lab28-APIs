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


        public ActionResult GetDeck()
        {
            string shuffleDeck = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

            HttpWebRequest request = WebRequest.CreateHttp(shuffleDeck);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string APIText = rd.ReadToEnd();

            JToken jsonDeckID = JToken.Parse(APIText);

            string deckID = jsonDeckID["deck_id"].ToString();
            Session["DeckID"] = deckID;
            int numberOfCards = 5;

            List<string> listOfCards = Draw(deckID, numberOfCards);
            Session["Hand"] = listOfCards;
        

            return RedirectToAction("GamePlay");

        }

        public ActionResult GamePlay()
        {
            return View();
        }

        public List<string> Draw(string deckID, int numberOfCards)
        {
            string initialDraw = $"https://deckofcardsapi.com/api/deck/{deckID}/draw/?count={numberOfCards}";

            HttpWebRequest request = WebRequest.CreateHttp(initialDraw);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string APIText = rd.ReadToEnd();

            JToken jsonCards = JToken.Parse(APIText);

            List<JToken> cardTokens = jsonCards["cards"].ToList();
            List<string> cardList = new List<string>();

            foreach (JToken j in cardTokens)
            {
                string card = j["image"].ToString();
                cardList.Add(card);
            }

            return cardList;
        }

        public ActionResult NewCards(List<bool> cards)
        {
            int count = 0;
            List<string> cardsList = (List<string>)Session["Hand"];
            for(int i = 4; i > -1; i--)
            {
                if (cards[i] == true)
                {
                    cardsList.RemoveAt(i);
                    count++;
                }
            }

            string deckID = (string)Session["DeckID"];
            List<string> addedCards = Draw(deckID, count);

            cardsList.AddRange(addedCards);
            Session["Hand"] = cardsList;

            return RedirectToAction("GamePlay");
        }


        public ActionResult Index()
        {
            //string shuffleDeck = "https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1";

            //HttpWebRequest request = WebRequest.CreateHttp(shuffleDeck);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //StreamReader rd = new StreamReader(response.GetResponseStream());
            //string APIText = rd.ReadToEnd();

            //JToken jsonDeckID = JToken.Parse(APIText);

            //string deckID = jsonDeckID["deck_id"].ToString();

            //string initialDraw = $"https://deckofcardsapi.com/api/deck/{deckID}/draw/?count=5";

            //request = WebRequest.CreateHttp(initialDraw);
            //response = (HttpWebResponse)request.GetResponse();
            //rd = new StreamReader(response.GetResponseStream());
            //APIText = rd.ReadToEnd();

            //JToken jsonCards = JToken.Parse(APIText);

            //List<JToken> cardTokens = jsonCards["cards"].ToList();
            //List<string> cardList = new List<string>();

            //foreach(JToken j in cardTokens)
            //{
            //    string card = j["image"].ToString();
            //    cardList.Add(card);
            //}

            //ViewBag.CardList = cardList;

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
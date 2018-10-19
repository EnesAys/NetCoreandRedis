﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisExample.Models;

namespace RedisExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache cache;
        List<Player> playerlist = new List<Player>();
        public HomeController(IDistributedCache _cache)
        {
            cache = _cache;

            Player player1 = new Player {
                ID=1,
                Name = "Enes",
                Surname = "Aysan"
            };
            Player player2 = new Player
            {
                ID=2,
                Name = "Enes",
                Surname = "Enes"
            };
            Player player3 = new Player
            {
                ID=3,
                Name = "Aysan",
                Surname = "Aysan"
            };

            playerlist.Add(player1);
            playerlist.Add(player2);
            playerlist.Add(player3);

        }

        public List<Player> GetNames()
        {      
            return playerlist;
        }

        [ResponseCache(Duration = 60)]
        public Player GetNameByID(int id)
        {
            var player = playerlist.FirstOrDefault(x=>x.ID==id);
            return player;
        }

        public IActionResult Index()
        {
            var items = GetNames();
            return View(items);
        }

        public IActionResult About(int id)
        {
            var value = cache.GetStringAsync(Convert.ToString(id));
            if (value.Result == null)
            {
                var player = GetNameByID(id);
                cache.SetStringAsync(id.ToString(), "Cache eklendi " + player.Name +" "+  player.Surname);
                //if you want sent object to redis you can use Newtonsoft.json libtary
                //JsonConvert.SerializeObject(object)
                return View(player);
            }
            else
            {
                //var newcache = cache.GetStringAsync(Convert.ToString(id));
                return Content(value.Result.ToString());
            }
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

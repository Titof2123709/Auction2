﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace WEB.Attribute
{
    public class PreventSpamAttribute : ActionFilterAttribute
    {
        //This stores the time between Requests (in seconds)
        public int DelayRequest = 10;
        //The Error Message that will be displayed in case of excessive Requests
        public string ErrorMessage = "Excessive Request Attempts Detected.";
        //This will store the URL to Redirect errors to
        public string RedirectURL;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Store our HttpContext (for easier reference and code brevity)
            var request = filterContext.HttpContext.Request;
            //Store our HttpContext.Cache (for easier reference and code brevity)
            var cache = filterContext.HttpContext.Cache;

            //Grab the IP Address from the originating Request (very simple implementation for example purposes)
            var originationInfo = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;

            //Append the User Agent
            originationInfo += request.UserAgent;

            //Now we just need the target URL Information
            var targetInfo = request.RawUrl + request.QueryString;

            //Generate a hash for your strings (this appends each of the bytes of the value into a single hashed string
            var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(originationInfo + targetInfo)).Select(s => s.ToString("x2")));

            //Checks if the hashed value is contained in the Cache (indicating a repeat request)
            if (cache[hashValue] != null)
            {
                //Adds the Error Message to the Model and Redirect
                filterContext.Controller.ViewData.ModelState.AddModelError("ExcessiveRequests", ErrorMessage);
            }
            else
            {
                //Adds an empty object to the cache using the hashValue to a key (This sets the expiration that will determine
                //if the Request is valid or not
                cache.Add(hashValue, null, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
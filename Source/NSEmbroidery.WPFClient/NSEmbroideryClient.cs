﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using NSEmbroidery.Data.Models;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Configuration;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace NSEmbroidery.WPFClient
{
    class NSEmbroideryClient : IDisposable
    {
        private HttpClient _client;
        private static NSEmbroideryClient _singleton;

        public HttpClient HttpClient {
            get
            {
                return _client;
            }
        }

        private NSEmbroideryClient()
        {
            _client = new HttpClient();
            string address = "http://172.24.218.21:8009";
            _client.BaseAddress = new Uri(address);
        }

        public static NSEmbroideryClient GetNSEmbroideryClient()
        {
            if (_singleton != null)
                return _singleton;
            else
            {
                _singleton = new NSEmbroideryClient();
                return _singleton;
            }

        }


        public bool Login(LoginModel model)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Email", model.Email),
                new KeyValuePair<string, string>("Password", model.Password)
            });

            HttpResponseMessage responce = _client.PostAsync("api/login", content).Result;

            if (responce.IsSuccessStatusCode)
                return true;

            throw new NotImplementedException();
        }

        public bool Logoff()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage responce = _client.PostAsync("api/logoff", null).Result;

            if (responce.IsSuccessStatusCode)
            {
                bool result = (bool)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return result;
            }

            throw new NotImplementedException();
        }

        public bool Register(RegisterModel model)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Email", model.Email),
                new KeyValuePair<string, string>("Password", model.Password),
                new KeyValuePair<string, string>("ConfirmPassword", model.ConfirmPassword),
                new KeyValuePair<string, string>("FirstName", model.FirstName),
                new KeyValuePair<string, string>("LastName", model.LastName)
            });

            HttpResponseMessage responce = _client.PostAsync("api/register", content).Result;

            if (responce.IsSuccessStatusCode)
            {
                bool result = (bool)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return result;
            }

            throw new NotImplementedException();
        }

        public List<Like> GetLikes()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/likes").Result;

            if (responce.IsSuccessStatusCode)
            {
                List<Like> likes = (List<Like>)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return likes;
            }

            throw new NotImplementedException();
        }

        public List<Comment> GetComments()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/comments").Result;

            if (responce.IsSuccessStatusCode)
            {
                List<Comment> comments = (List<Comment>)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return comments;
            }

            throw new NotImplementedException();
        }

        public Comment GetComment(int id)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/comments/" + id.ToString()).Result;

            if (responce.IsSuccessStatusCode)
            {
                Comment comment = (Comment)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return comment;
            }

            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/users").Result;

            if (responce.IsSuccessStatusCode)
            {
                string str = responce.Content.ReadAsStringAsync().Result;
                List<User> users = (List<User>)JsonConvert.DeserializeObject(str);
                return users;
            }

            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/users/" + id.ToString()).Result;

            if (responce.IsSuccessStatusCode)
            {
                User user = (User)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return user;
            }

            throw new NotImplementedException();
        }

        public List<Embroidery> GetEmbroideries()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/embroideries").Result;

            if (responce.IsSuccessStatusCode)
            {
                List<Embroidery> embroideries = (List<Embroidery>)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return embroideries;
            }

            throw new NotImplementedException();
        }

        public Embroidery GetEmbroiderie(int id)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/embroideries/" + id.ToString()).Result;

            if (responce.IsSuccessStatusCode)
            {
                Embroidery embroiderie = (Embroidery)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);
                return embroiderie;
            }

            throw new NotImplementedException();
        }

        public List<BitmapSource> GetSmallEmbroideries()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responce = _client.GetAsync("api/embroideries/true").Result;

            if (responce.IsSuccessStatusCode)
            {
                List<Embroidery> embroideries = (List<Embroidery>)JsonConvert.DeserializeObject(responce.Content.ReadAsStringAsync().Result);

                List<BitmapSource> bitmaps = new List<BitmapSource>();

                foreach (var item in embroideries)
                    bitmaps.Add(GetBitmapSource(item.SmallImage));

                return bitmaps;
            }

            throw new NotImplementedException();
        }

        private BitmapSource GetBitmapSource(Bitmap image)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(),
                                                      IntPtr.Zero,
                                                      System.Windows.Int32Rect.Empty,
                                                      BitmapSizeOptions.FromWidthAndHeight(image.Width, image.Height));


            return bitmapSource;
        }


        public void Dispose()
        {
            _client.Dispose();
            _singleton = null;
        }

    }
}
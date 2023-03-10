using DataBookView.Models;
using DataBookView.Interfaces;
using Newtonsoft.Json;
using System.Text;
using System;
using static System.Net.WebRequestMethods;
using DataBookView.Authentification;
using Microsoft.AspNetCore.Identity;
using DataBookView.Roles;
using System.Xml.Linq;

namespace DataBookView.Controllers
{
    public class DataBookDataApi : IDataBookData
    {
        string baseUrl = @"https://localhost:7177/api/DataBookApi/";
        private HttpClient httpClient { get; set; }

        public DataBookDataApi()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Добавить токен в заголовок.
        /// </summary>
        /// <param name="accessToken">Токен</param>
        public void AddTokenToClient(string accessToken = "")
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        public string DeleteUser(string name)
        {
            string url = "https://localhost:7177/api/AccountApi/" + name;

            var request = httpClient.DeleteAsync(requestUri: url).Result;

            if (request.IsSuccessStatusCode)
                return request.Content.ReadAsStringAsync().Result;

            return String.Empty;
        }

        /// <summary>
        /// Список пользователей.
        /// </summary>
        public List<User> UserList()
        {
            string url = "https://localhost:7177/api/AccountApi/";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        /// <summary>
        /// Список ролей пользователя.
        /// </summary>
        /// <param name="name">Имя</param>
        public ChangeRole GetUserAndRoles(string name)
        {
            string url = "https://localhost:7177/api/RolesApi/GetUserAndRoles/" + name;

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<ChangeRole>(json);
        }

        /// <summary>
        /// Получить текущего пользователя.
        /// </summary>
        public ChangeRole GetCurrentUser()
        {
            string url = "https://localhost:7177/api/RolesApi/GetCurrentUser/";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<ChangeRole>(json);
        }

        public List<string> EditUserAndRoles(string userName, List<string> roles)
        {
            string url = "https://localhost:7177/api/RolesApi/EditUserAndRoles/" + userName;

            var request = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(roles), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;

            return request.Content.ReadFromJsonAsync<List<string>>().Result;
            
        }

        /// <summary>
        /// Получить роли.
        /// </summary>
        public List<IdentityRole> GetRoles()
        {
            string url = "https://localhost:7177/api/RolesApi/";

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<List<IdentityRole>>(json);
        }

        /// <summary>
        /// Удалить роль.
        /// </summary>
        public string DeleteRole(string roleName)
        {
            string url = "https://localhost:7177/api/RolesApi/" + roleName;

            var request = httpClient.DeleteAsync(requestUri: url).Result;

            if (request.IsSuccessStatusCode)
                return request.Content.ReadAsStringAsync().Result;

            return String.Empty;
        }

        /// <summary>
        /// Создать роль.
        /// </summary>
        public string CreateRole(string roleName)
        {
            string url = "https://localhost:7177/api/RolesApi/" + roleName;

            var request = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent("")).Result;

            if (request.IsSuccessStatusCode)
                return request.Content.ReadAsStringAsync().Result;

            return String.Empty;
        }

        /// <summary>
        /// Логин (получить токен).
        /// </summary>
        /// <param name="UserLogin">Модель логина</param>
        public string Login(UserLogin userlogin)
        {
            string url = "https://localhost:7177/api/AccountApi/Login";

            var request = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(userlogin), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;


            if (request.IsSuccessStatusCode)
                return request.Content.ReadAsStringAsync().Result;

            return String.Empty;
        }

        /// <summary>
        /// Регистрация (получить токен).
        /// </summary>
        /// <param name="UserLogin">Модель логина</param>
        public string Register(UserRegistration userRegister)
        {
            string url = "https://localhost:7177/api/AccountApi/Register";

            var request = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(userRegister), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;


            if (request.IsSuccessStatusCode)
                return request.Content.ReadAsStringAsync().Result;

            return String.Empty;
        }

        /// <summary>
        /// Получить все записи из БД.
        /// </summary>
        /// <returns></returns>
        public List<DataBook> GetAllDatabooks()
        {
            string url = baseUrl;

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<List<DataBook>>(json);
        }

        /// <summary>
        /// Добавить запись в БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        public void CreateDataBook(DataBook dataBook)
        {
            string url = baseUrl;

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(dataBook), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }

        /// <summary>
        /// Получить запись из БД.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        /// <returns>Запись</returns>
        public DataBook ReadDataBook(int dataBookId)
        {
            string url = baseUrl + dataBookId;

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<DataBook>(json);
        }

        /// <summary>
        /// Изменить запсиь в БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        public void UpdateDataBook(DataBook dataBook)
        {
            UpdateDataBookById(dataBook.Id, dataBook);
        }

        /// <summary>
        /// Изменить запсиь в БД по Id.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        /// <param name="dataBook">Запись</param>
        public void UpdateDataBookById(int dataBookId, DataBook dataBook)
        {
            string url = baseUrl + dataBookId;

            var dataBookToModify = ReadDataBook(dataBookId);

            if (dataBookToModify != null)
            {
                var r = httpClient.PutAsync(
                    requestUri: url,
                    content: new StringContent(JsonConvert.SerializeObject(dataBook), Encoding.UTF8,
                    mediaType: "application/json")
                    ).Result;
            }
        }

        /// <summary>
        /// Удалить запись из БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        public void DeleteDataBook(DataBook dataBook)
        {
            DeleteDataBookById(dataBook.Id);
        }

        /// <summary>
        /// Удалить запись из БД по Id.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        public void DeleteDataBookById(int dataBookId)
        {
            string url = baseUrl + dataBookId;

            var dataBookToDelete = ReadDataBook(dataBookId);

            if (dataBookToDelete != null)
            {
                var r = httpClient.DeleteAsync(requestUri: url).Result;
            }
        }
    }
}
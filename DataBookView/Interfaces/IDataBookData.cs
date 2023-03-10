using DataBookView.Authentification;
using DataBookView.Models;
using DataBookView.Roles;
using Microsoft.AspNetCore.Identity;

namespace DataBookView.Interfaces
{
    public interface IDataBookData
    {
        /// <summary>
        /// Получить все записи из БД.
        /// </summary>
        /// <returns></returns>
        List<DataBook> GetAllDatabooks();

        /// <summary>
        /// Добавить запись в БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        void CreateDataBook(DataBook dataBook);

        /// <summary>
        /// Получить запись из БД.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        /// <returns>Запись</returns>
        DataBook ReadDataBook(int dataBookId);

        /// <summary>
        /// Изменить запсиь в БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        void UpdateDataBook(DataBook dataBook);

        /// <summary>
        /// Изменить запсиь в БД по Id.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        /// <param name="dataBook">Запись</param>
        void UpdateDataBookById(int dataBookId, DataBook dataBook);

        /// <summary>
        /// Удалить запись из БД.
        /// </summary>
        /// <param name="dataBook">Запись</param>
        void DeleteDataBook(DataBook dataBook);

        /// <summary>
        /// Удалить запись из БД по Id.
        /// </summary>
        /// <param name="dataBookId">Id записи</param>
        void DeleteDataBookById(int dataBookId);

        /// <summary>
        /// Логин (получить токен).
        /// </summary>
        /// <param name="UserLogin">Модель логина</param>
        string Login(UserLogin userlogin);

        /// <summary>
        /// Регистрация (получить токен).
        /// </summary>
        /// <param name="UserRegistration">Модель регистрации</param>
        string Register(UserRegistration userRegister);

        /// <summary>
        /// Добавить токен в заголовок.
        /// </summary>
        /// <param name="accessToken">Токен</param>
        void AddTokenToClient(string accessToken);

        /// <summary>
        /// Получить роли.
        /// </summary>
        /// <returns></returns>
        List<IdentityRole> GetRoles();

        /// <summary>
        /// Создать роль.
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <returns></returns>
        string CreateRole(string roleName);

        /// <summary>
        /// Удалить роль.
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <returns></returns>
        string DeleteRole(string roleName);

        /// <summary>
        /// Список пользователей.
        /// </summary>
        List<User> UserList();

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="name">Имя</param>
        string DeleteUser(string name);

        /// <summary>
        /// Список ролей пользователя.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ChangeRole GetUserAndRoles(string name);

        /// <summary>
        /// Изменить роли.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        List<string> EditUserAndRoles(string userName, List<string> roles);

        /// <summary>
        /// Получить текущего пользователя.
        /// </summary>
        /// <returns></returns>
        ChangeRole GetCurrentUser();
    }
}
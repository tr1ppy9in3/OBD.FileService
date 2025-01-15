using OBD.FileService.Users.Core;
using OBD.FileService.Users.Core.Auth.Options;

namespace OBD.FileService.Users.UseCases;

/// <summary>
/// Репозиторий для доступа к пользователям.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получить всех пользователей.
    /// </summary>
    /// <returns> Список пользвателей </returns>
    IAsyncEnumerable<BaseUser> GetAll();

    /// <summary>
    /// Получить пользователя.
    /// </summary>
    /// <param name="login"> Логин пользователя. </param>
    /// <param name="password"> Пароль пользователя. </param>
    /// <returns> Пользователь. </returns>
    Task<BaseUser?> Resolve(string login, string password);

    /// <summary>
    /// Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор пользователя. </param>
    /// <returns> Пользователь. </returns>
    Task<BaseUser?> GetById(long id);

    /// <summary>
    /// Получить пользователя по логину.
    /// </summary>
    /// <param name="login"> Логин пользователя. </param>
    /// <returns> Пользователь. </returns>
    Task<BaseUser?> GetByLogin(string login);

    /// <summary>
    /// Получить пользователя по почте.
    /// </summary>
    /// <param name="email"> Почта пользователя. </param>
    /// <returns> Пользователь. </returns>
    Task<BaseUser?> GetByEmail(string email);

    /// <summary>
    /// Изменить почту пользователя.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    /// <param name="email"> Новая почта.</param>
    Task ChangeEmail(BaseUser user, string email);

    /// <summary>
    /// Изменить пароль пользователя.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    /// <param name="password"> Новый пароль. </param>
    /// <param name="passwordOptions"> Параметры пароля. </param>
    Task ChangePassword(BaseUser user, string password, PasswordOptions passwordOptions);

    /// <summary>
    /// Добавить пользователя.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    Task Add(BaseUser user);

    /// <summary>
    /// Отредактировать пользователя.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    Task Update(BaseUser user);

    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="user"> Пользователь. </param>
    Task Delete(BaseUser user);

    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="id"> Идентификатор пользователя.</param>
    Task Delete(long id);
}

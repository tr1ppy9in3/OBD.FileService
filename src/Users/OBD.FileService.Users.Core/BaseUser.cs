﻿using OBD.FileService.Users.Core.Audit;
using OBD.FileService.Users.Core.Auth;
using OBD.FileService.Users.Core.Auth.Options;
using OBD.FileService.Users.Core.Auth.Services;

namespace OBD.FileService.Users.Core;

/// <summary>
/// Базовый класс пользователя.
/// </summary>
public abstract class BaseUser
{
    /// <summary>
    /// Индетификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Тип пользователя.
    /// </summary>
    public abstract UserType Type { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>
    /// Роли
    /// </summary>
    public List<Role> Roles { get; } = [];

    /// <summary>
    /// Все действия, совершенные пользователем.
    /// </summary>
    public List<UserAction> Actions { get; set; } = new(0);

    /// <summary>
    /// Заблокирован (да, нет)
    /// </summary>
    public bool IsBlocked { get; set; } = false;

    /// <summary>
    /// Сеттер пароля
    /// </summary>
    /// <param name="password"> Строка пароля</param>
    /// <param name="passwordOptions"> Параметры пароля</param>
    public void SetPassword(string password, PasswordOptions passwordOptions)
    {
        PasswordHash = CryptographyService.HashPassword(password, passwordOptions.Salt);
    }
}

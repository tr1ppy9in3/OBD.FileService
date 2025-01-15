﻿namespace OBD.FileService.Users.UseCases.Commands.UpdateInitialsCommand;

public class UserInitialsModel
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; } = string.Empty;
}

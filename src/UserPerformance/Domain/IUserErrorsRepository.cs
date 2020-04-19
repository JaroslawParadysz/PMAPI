﻿using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUserErrorsRepository
    {
        Task SaveUserErrors(UserError userError);
        UserError GetUserError(Guid userErrorId);
        Task UpdateUserError(UserError userError);
        Task DeleteUserError(Guid userErrorId);
    }
}

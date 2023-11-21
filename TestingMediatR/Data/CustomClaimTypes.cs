﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingMediatR.Models;

namespace TestingMediatR.Data
{
    public class CustomClaimTypes
    {
        public const string Permission = "Application.Permission";
    }

    public static class UserPermissions
    {
        public const string Add = "users.add";
        public const string Edit = "users.edit";
        public const string EditRole = "users.edit.role";

    }

}

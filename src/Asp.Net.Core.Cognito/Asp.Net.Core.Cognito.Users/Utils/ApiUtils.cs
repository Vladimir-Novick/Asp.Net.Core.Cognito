﻿using System;

namespace Asp.Net.Core.Cognito.Users.Utils
{
    public class ApiUtils
    {
        public static bool isEmpty(string value)
        {
            if (String.IsNullOrEmpty(value)) return true;
            if (String.IsNullOrWhiteSpace(value)) return true;
            return false;
        }
    }
}

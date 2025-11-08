using System;

namespace KMCI_System.Login
{
    public static class Session
    {
        // Holds the currently-logged-in user's email
        public static string CurrentUserEmail { get; set; }
        public static string CurrentUserDepartment { get; internal set; }
    }
}